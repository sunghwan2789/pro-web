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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ActivityAttachment>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.ActivityId);

                entity.HasOne(d => d.Activity)
                    .WithMany(p => p.Attachments)
                    .HasForeignKey(d => d.ActivityId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<ActivityAttendee>(entity =>
            {
                entity.HasKey(e => new { e.ActivityId, e.AttandeeId });
                entity.HasIndex(e => e.ActivityId);
                entity.HasIndex(e => e.AttandeeId);

                entity.HasOne(d => d.Activity)
                    .WithMany(p => p.ActivityAttendees)
                    .HasForeignKey(d => d.ActivityId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Attendee)
                    .WithMany(p => p.ActivityAttendees)
                    .HasForeignKey(d => d.AttandeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Activity>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.AuthorId);
                entity.HasIndex(e => new { e.StartAt, e.EndAt });

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.Activities)
                    .HasForeignKey(d => d.AuthorId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<MemberHistory>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.MemberId);

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.MemberHistory)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<MemberLog>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.MemberId);

                entity.Property(e => e.OccurredAt)
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.MemberLogs)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Member>(entity =>
            {
                entity.HasKey(e => e.StudentNumber);
            });

            modelBuilder.Entity<Submission>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.TaskId);
                entity.HasIndex(e => e.AuthorId);

                entity.Property(e => e.SubmitAt)
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.Sources)
                    .HasForeignKey(d => d.TaskId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.Submissions)
                    .HasForeignKey(d => d.AuthorId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<TaskTest>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.TaskId);
                entity.HasIndex(e => new { e.TaskId, e.Score });

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.Tests)
                    .HasForeignKey(d => d.TaskId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Task>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.Hidden);
                entity.HasIndex(e => e.EndAt);
                entity.HasIndex(e => new { e.StartAt, e.EndAt });
            });
        }
    }
}
