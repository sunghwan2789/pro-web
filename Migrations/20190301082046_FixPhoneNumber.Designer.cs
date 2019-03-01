﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using pro_web;

namespace pro_web.Migrations
{
    [DbContext(typeof(ProContext))]
    [Migration("20190301082046_FixPhoneNumber")]
    partial class FixPhoneNumber
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.2-servicing-10034")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("pro_web.Models.Activity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AuthorId");

                    b.Property<string>("Content");

                    b.Property<DateTime>("EndAt");

                    b.Property<string>("Place");

                    b.Property<DateTime>("StartAt");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("StartAt", "EndAt");

                    b.ToTable("Activities");
                });

            modelBuilder.Entity("pro_web.Models.ActivityAttachment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ActivityId");

                    b.Property<string>("Filename")
                        .IsRequired();

                    b.Property<string>("MediaType");

                    b.Property<string>("OriginalFilename");

                    b.Property<long>("Size");

                    b.HasKey("Id");

                    b.HasIndex("ActivityId");

                    b.ToTable("ActivityAttachments");
                });

            modelBuilder.Entity("pro_web.Models.ActivityAttendee", b =>
                {
                    b.Property<int>("ActivityId");

                    b.Property<int>("AttandeeId");

                    b.HasKey("ActivityId", "AttandeeId");

                    b.HasIndex("ActivityId");

                    b.HasIndex("AttandeeId");

                    b.ToTable("ActivityAttendees");
                });

            modelBuilder.Entity("pro_web.Models.Member", b =>
                {
                    b.Property<int>("Id");

                    b.Property<int>("Authority");

                    b.Property<int>("Gen");

                    b.Property<string>("Name");

                    b.Property<string>("Password");

                    b.Property<string>("PhoneNumber");

                    b.HasKey("Id");

                    b.ToTable("Members");
                });

            modelBuilder.Entity("pro_web.Models.MemberHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content");

                    b.Property<int>("MemberId");

                    b.Property<DateTime>("OccurredAt");

                    b.HasKey("Id");

                    b.HasIndex("MemberId");

                    b.ToTable("MemberHistory");
                });

            modelBuilder.Entity("pro_web.Models.MemberLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content");

                    b.Property<int>("MemberId");

                    b.Property<DateTime>("OccurredAt")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.HasKey("Id");

                    b.HasIndex("MemberId");

                    b.ToTable("MemberLogs");
                });

            modelBuilder.Entity("pro_web.Models.Submission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AuthorId");

                    b.Property<string>("Error");

                    b.Property<string>("Filename")
                        .IsRequired();

                    b.Property<int>("Language");

                    b.Property<int>("Score");

                    b.Property<int>("Sequence");

                    b.Property<long>("Size");

                    b.Property<int>("Status");

                    b.Property<DateTime>("SubmitAt")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<int>("TaskId");

                    b.Property<bool>("Working");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("TaskId");

                    b.ToTable("Submissions");
                });

            modelBuilder.Entity("pro_web.Models.Task", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content");

                    b.Property<DateTime>("EndAt");

                    b.Property<string>("ExampleInput");

                    b.Property<string>("ExampleOutput");

                    b.Property<bool>("Hidden");

                    b.Property<DateTime>("StartAt");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("EndAt");

                    b.HasIndex("Hidden");

                    b.HasIndex("StartAt", "EndAt");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("pro_web.Models.TaskTest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Input")
                        .IsRequired();

                    b.Property<string>("Output")
                        .IsRequired();

                    b.Property<int>("Score");

                    b.Property<int>("TaskId");

                    b.HasKey("Id");

                    b.HasIndex("TaskId");

                    b.HasIndex("TaskId", "Score");

                    b.ToTable("TaskTests");
                });

            modelBuilder.Entity("pro_web.Models.Activity", b =>
                {
                    b.HasOne("pro_web.Models.Member", "Author")
                        .WithMany("Activities")
                        .HasForeignKey("AuthorId");
                });

            modelBuilder.Entity("pro_web.Models.ActivityAttachment", b =>
                {
                    b.HasOne("pro_web.Models.Activity", "Activity")
                        .WithMany("Attachments")
                        .HasForeignKey("ActivityId");
                });

            modelBuilder.Entity("pro_web.Models.ActivityAttendee", b =>
                {
                    b.HasOne("pro_web.Models.Activity", "Activity")
                        .WithMany("ActivityAttendees")
                        .HasForeignKey("ActivityId");

                    b.HasOne("pro_web.Models.Member", "Attendee")
                        .WithMany("ActivityAttendees")
                        .HasForeignKey("AttandeeId");
                });

            modelBuilder.Entity("pro_web.Models.MemberHistory", b =>
                {
                    b.HasOne("pro_web.Models.Member", "Member")
                        .WithMany("MemberHistory")
                        .HasForeignKey("MemberId");
                });

            modelBuilder.Entity("pro_web.Models.MemberLog", b =>
                {
                    b.HasOne("pro_web.Models.Member", "Member")
                        .WithMany("MemberLogs")
                        .HasForeignKey("MemberId");
                });

            modelBuilder.Entity("pro_web.Models.Submission", b =>
                {
                    b.HasOne("pro_web.Models.Member", "Author")
                        .WithMany("Submissions")
                        .HasForeignKey("AuthorId");

                    b.HasOne("pro_web.Models.Task", "Task")
                        .WithMany("Sources")
                        .HasForeignKey("TaskId");
                });

            modelBuilder.Entity("pro_web.Models.TaskTest", b =>
                {
                    b.HasOne("pro_web.Models.Task", "Task")
                        .WithMany("Tests")
                        .HasForeignKey("TaskId");
                });
#pragma warning restore 612, 618
        }
    }
}