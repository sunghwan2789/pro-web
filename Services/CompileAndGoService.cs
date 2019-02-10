﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using pro_web.Extensions;
using pro_web.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace pro_web.Services
{
    public class CompileAndGoService : BackgroundService
    {
        public CompileAndGoService(
            ICompileAndGoQueue queue,
            IHostingEnvironment env,
            IServiceScopeFactory scopeFactory)
        {
            Queue = queue;
            this.env = env;
            this.scopeFactory = scopeFactory;
        }

        private readonly IHostingEnvironment env;
        private readonly IServiceScopeFactory scopeFactory;

        public ICompileAndGoQueue Queue { get; }

        public string GetTemporaryDirectory()
        {
            string tempFolder = Path.GetTempFileName();
            File.Delete(tempFolder);
            Directory.CreateDirectory(tempFolder);
            return tempFolder;
        }

        public string MountDrive => "S:";

        public string GetSourcePath(Submission submission)
        {
            return Path.Combine(env.ContentRootPath, "storage", "sources", submission.Filename);
        }

        private CompileAndGo.ILanguageSdk GetLanguageSdkSpec(Submission submission)
            => (CompileAndGo.ILanguageSdk)Activator.CreateInstance(Type.GetType(
                $"pro_web.CompileAndGo.{submission.Language.ToString()}"));

        protected override async System.Threading.Tasks.Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using (var scope = scopeFactory.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<ProContext>();
                db.Submissions.Where(i => i.Working).ToList().ForEach(Queue.QueueBackgroundWorkItem);
            }

            while (!stoppingToken.IsCancellationRequested)
            {
                var workItem = await Queue.DequeueAsync(stoppingToken);
                var volume = GetTemporaryDirectory();

                using (var scope = scopeFactory.CreateScope())
                {
                    var db = scope.ServiceProvider.GetRequiredService<ProContext>();
                    workItem = await db.Submissions.FindAsync(workItem.Id);

                    var sdk = GetLanguageSdkSpec(workItem);

                    var path = GetSourcePath(workItem);
                    File.Copy(path, Path.Combine(volume, sdk.SourceFilename));

                    if (sdk is CompileAndGo.ICompiledLanguageSdk csdk)
                    {
                        using (var p = Process.Start(new ProcessStartInfo
                        {
                            FileName = "docker",
                            Arguments = string.Join(' ', new[] {
                            "run",
                            "-i",
                            "-a stdout",
                            "--rm",
                            "-v",
                            $"{volume}:{MountDrive}",
                            sdk.ImageName,
                            $"cd /d {MountDrive} && {csdk.CompileCommand} 2>&1"
                        }),
                            UseShellExecute = false,
                            CreateNoWindow = true,
                            RedirectStandardOutput = true,
                            StandardOutputEncoding = Encoding.UTF8,
                        }))
                        {
                            var compileErrorTask = csdk.ProcessCompileErrorAsync(p.StandardOutput);
                            await p.WaitForExitAsync();
                            workItem.Error = await compileErrorTask;
                            if (p.ExitCode != 0)
                            {
                                workItem.Status = Submission.StatusCode.CompilationError;
                                goto SUBMIT;
                            }
                        }
                    }

                    workItem.Status = Submission.StatusCode.PartialSuccess;
                    await db.SaveChangesAsync();

                    // 프로그램 채점
                    foreach (var test in workItem.Task.Tests.OrderBy(i => i.Score))
                    {
                        await File.WriteAllTextAsync(Path.Combine(volume, "in"), test.Input);
                        using (var p = Process.Start(new ProcessStartInfo
                        {
                            FileName = "docker",
                            Arguments = string.Join(' ', new[] {
                                "run",
                                "-i",
                                "-a stdout",
                                "--rm",
                                "-v",
                                $"{volume}:{MountDrive}",
                                sdk.ImageName,
                                $"cd /d {MountDrive} && {sdk.ExecuteCommand} <in"
                            }),
                            UseShellExecute = false,
                            CreateNoWindow = true,
                            RedirectStandardOutput = true,
                            StandardOutputEncoding = Encoding.UTF8,
                        }))
                        {
                            var timeLimitTask = System.Threading.Tasks.Task.Delay(30_000);
                            var outputTask = p.StandardOutput.ReadToEndAsync();
                            await System.Threading.Tasks.Task.WhenAny(timeLimitTask, p.WaitForExitAsync());

                            // 시간 초과
                            if (timeLimitTask.Status == TaskStatus.RanToCompletion)
                            {
                                goto SUBMIT;
                            }

                            // 런타임 에러
                            if (p.ExitCode != 0)
                            {
                                workItem.Status = Submission.StatusCode.RuntimeError;
                                goto SUBMIT;
                            }

                            // 출력의 우측 공백 제거
                            var expectedOutput = test.Output.Split('\n').Select(i => i.TrimEnd()).ToList();
                            var output = (await outputTask ?? "").Split('\n').Select(i => i.TrimEnd()).ToList();

                            // 출력이 부족하면 오답
                            if (output.Count < expectedOutput.Count)
                            {
                                goto SUBMIT;
                            }

                            // 각 줄마다 비교하며 오답 검출
                            for (var i = 0; i < expectedOutput.Count; i++)
                            {
                                if (output[i] != expectedOutput[i])
                                {
                                    goto SUBMIT;
                                }
                            }

                            // 모두 비교하고 남은 출력이 있으면 오답
                            if (output.Skip(expectedOutput.Count).Any(i => !string.IsNullOrEmpty(i)))
                            {
                                goto SUBMIT;
                            }

                            // 정답 처리
                            workItem.Score = (int)test.Score;
                            await db.SaveChangesAsync();
                        }
                    }

                    // 모든 테스트를 통과한 정답
                    workItem.Status = Submission.StatusCode.SuccessOrInitialization;

                SUBMIT:
                    // 채점 완료
                    workItem.Working = false;
                    await db.SaveChangesAsync();
                }

                Directory.Delete(volume, true);
            }
        }
    }

    public interface ICompileAndGoQueue
    {
        void QueueBackgroundWorkItem(Submission workItem);

        Task<Submission> DequeueAsync(CancellationToken cancellationToken);
    }

    public class CompileAndGoQueue : ICompileAndGoQueue
    {
        private ConcurrentQueue<Submission> _workItems = new ConcurrentQueue<Submission>();
        private SemaphoreSlim _signal = new SemaphoreSlim(0);

        public void QueueBackgroundWorkItem(Submission workItem)
        {
            //if (workItem == null)
            //{
            //    throw new ArgumentNullException(nameof(workItem));
            //}

            _workItems.Enqueue(workItem);
            _signal.Release();
        }

        public async Task<Submission> DequeueAsync(CancellationToken cancellationToken)
        {
            await _signal.WaitAsync(cancellationToken);
            _workItems.TryDequeue(out var workItem);

            return workItem;
        }
    }
}
