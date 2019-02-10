using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using pro_web.Models;
using System;

namespace pro_web
{
    public partial class ProContext : DbContext
    {
        public ProContext()
        {
        }

        public ProContext(DbContextOptions<ProContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ActivityAttachment> ActivityAttachments { get; set; }
        public virtual DbSet<ActivityAttendee> ActivityAttendees { get; set; }
        public virtual DbSet<Activity> Activities { get; set; }
        public virtual DbSet<MemberHistory> MemberHistory { get; set; }
        public virtual DbSet<MemberLog> MemberLogs { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<Submission> Submissions { get; set; }
        public virtual DbSet<TaskTest> TaskTests { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }

        // Unable to generate entity type for table 'pro_activity_attach'. Please see the warning messages.
        // Unable to generate entity type for table 'pro_activity_attend'. Please see the warning messages.

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ActivityAttachment>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PRIMARY");

                entity.ToTable("pro_activity_attach");

                entity.HasIndex(e => e.ActivityId)
                    .HasName("aid");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ActivityId).HasColumnName("aid");

                entity.Property(e => e.Filename)
                    .IsRequired()
                    .HasColumnName("md5")
                    .HasColumnType("tinytext");

                entity.Property(e => e.OriginalFilename)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("tinytext");

                entity.HasOne(d => d.Activity)
                    .WithMany(p => p.Attachments)
                    .HasForeignKey(d => d.ActivityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("pro_activity_attach_ibfk_1");
            });

            modelBuilder.Entity<ActivityAttendee>(entity =>
            {
                entity.HasKey(e => new { e.ActivityId, e.AttandeeId });

                entity.ToTable("pro_activity_attend");

                entity.HasIndex(e => e.ActivityId)
                    .HasName("aid");

                entity.HasIndex(e => e.AttandeeId)
                    .HasName("uid");

                entity.Property(e => e.ActivityId).HasColumnName("aid");

                entity.Property(e => e.AttandeeId).HasColumnName("uid");

                entity.HasOne(d => d.Activity)
                    .WithMany(p => p.ActivityAttendees)
                    .HasForeignKey(d => d.ActivityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("pro_activity_attend_ibfk_2");

                entity.HasOne(d => d.Attendee)
                    .WithMany(p => p.ActivityAttendees)
                    .HasForeignKey(d => d.AttandeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("pro_activity_attend_ibfk_1");
            });

            modelBuilder.Entity<Activity>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PRIMARY");

                entity.ToTable("pro_activities");

                entity.HasIndex(e => e.AuthorId)
                    .HasName("uid");

                entity.HasIndex(e => new { e.StartAt, e.EndAt })
                    .HasName("start");

                entity.Property(e => e.Id).HasColumnName("idx");

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasColumnName("content")
                    .HasColumnType("text");

                entity.Property(e => e.EndAt)
                    .HasColumnName("end")
                    .HasColumnType("datetime");

                entity.Property(e => e.Place)
                    .IsRequired()
                    .HasColumnName("place")
                    .HasColumnType("tinytext");

                entity.Property(e => e.Summary)
                    .IsRequired()
                    .HasColumnName("purpose")
                    .HasColumnType("tinytext");

                entity.Property(e => e.StartAt)
                    .HasColumnName("start")
                    .HasColumnType("datetime");

                entity.Property(e => e.AuthorId).HasColumnName("uid");

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.Activities)
                    .HasForeignKey(d => d.AuthorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("pro_activities_ibfk_1");
            });

            modelBuilder.Entity<MemberHistory>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PRIMARY");

                entity.ToTable("pro_member_history");

                entity.HasIndex(e => e.MemberId)
                    .HasName("uid");

                entity.Property(e => e.Id).HasColumnName("idx");

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasColumnName("content")
                    .HasColumnType("text");

                entity.Property(e => e.OccurredAt)
                    .HasColumnName("date")
                    .HasColumnType("date");

                entity.Property(e => e.MemberId).HasColumnName("uid");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.MemberHistory)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("pro_member_history_ibfk_1");
            });

            modelBuilder.Entity<MemberLog>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PRIMARY");

                entity.ToTable("pro_member_log");

                entity.HasIndex(e => e.MemberId)
                    .HasName("uid");

                entity.Property(e => e.Id).HasColumnName("idx");

                entity.Property(e => e.OccurredAt)
                    .HasColumnName("date")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasColumnName("text")
                    .HasColumnType("text");

                entity.Property(e => e.MemberId).HasColumnName("uid");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.MemberLogs)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("pro_member_log_ibfk_1");
            });

            modelBuilder.Entity<Member>(entity =>
            {
                entity.HasKey(e => e.StudentNumber)
                    .HasName("PRIMARY");

                entity.ToTable("pro_members");

                entity.Property(e => e.StudentNumber).HasColumnName("id");

                entity.Property(e => e.Authority).HasColumnName("authority");

                entity.Property(e => e.PhoneNumber).HasColumnName("contact");

                entity.Property(e => e.Gen).HasColumnName("gen");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(4)");

                entity.Property(e => e.Password)
                    .HasColumnName("pw")
                    .HasColumnType("char(60)");
            });

            modelBuilder.Entity<Submission>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PRIMARY");

                entity.ToTable("pro_submit");

                entity.HasIndex(e => e.TaskId)
                    .HasName("tid");

                entity.HasIndex(e => e.AuthorId)
                    .HasName("uid");

                entity.HasIndex(e => new { e.TaskId, e.AuthorId, e.Sequence })
                    .HasName("tid_2")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("source_id");

                entity.Property(e => e.SubmitAt)
                    .HasColumnName("date")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Error)
                    .IsRequired()
                    .HasColumnName("error")
                    .HasColumnType("text");

                entity.Property(e => e.Sequence).HasColumnName("fid");

                entity.Property(e => e.Score)
                    .HasColumnName("score")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Size).HasColumnName("size");

                entity.Property(e => e.Status)
                    .HasColumnName("status");

                entity.Property(e => e.TaskId).HasColumnName("tid");

                entity.Property(e => e.AuthorId).HasColumnName("uid");

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.Sources)
                    .HasForeignKey(d => d.TaskId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("pro_submit_ibfk_2");

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.Submissions)
                    .HasForeignKey(d => d.AuthorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("pro_submit_ibfk_1");
            });

            modelBuilder.Entity<TaskTest>(entity =>
            {
                entity.ToTable("pro_task_tests");

                entity.HasIndex(e => e.TaskId)
                    .HasName("task_id");

                entity.HasIndex(e => new { e.TaskId, e.Score })
                    .HasName("task_id_2");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Input)
                    .IsRequired()
                    .HasColumnName("input");

                entity.Property(e => e.Output)
                    .IsRequired()
                    .HasColumnName("output");

                entity.Property(e => e.Score).HasColumnName("score");

                entity.Property(e => e.TaskId).HasColumnName("task_id");

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.Tests)
                    .HasForeignKey(d => d.TaskId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("pro_task_tests_ibfk_1");
            });

            modelBuilder.Entity<Task>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PRIMARY");

                entity.ToTable("pro_tasks");

                entity.HasIndex(e => e.Hidden)
                    .HasName("block");

                entity.HasIndex(e => e.EndAt)
                    .HasName("end");

                entity.HasIndex(e => new { e.StartAt, e.EndAt })
                    .HasName("start");

                entity.Property(e => e.Id).HasColumnName("idx");

                entity.Property(e => e.Hidden)
                    .HasColumnName("block")
                    .HasColumnType("tinyint(4)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasColumnName("content")
                    .HasColumnType("text");

                entity.Property(e => e.DeletedAt)
                    .HasColumnName("deleted_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.EndAt)
                    .HasColumnName("end")
                    .HasColumnType("date");

                entity.Property(e => e.ExampleInput)
                    .IsRequired()
                    .HasColumnName("in")
                    .HasColumnType("text");

                entity.Property(e => e.ExampleOutput)
                    .IsRequired()
                    .HasColumnName("out")
                    .HasColumnType("text");

                entity.Property(e => e.StartAt)
                    .HasColumnName("start")
                    .HasColumnType("date");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasColumnType("tinytext");
            });
        }
    }
}
