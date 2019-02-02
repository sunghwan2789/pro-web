﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using pro_web;

namespace pro_web.Migrations
{
    [DbContext(typeof(ProContext))]
    partial class ProContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.1-servicing-10028")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("pro_web.Models.Activity", b =>
                {
                    b.Property<uint>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("idx");

                    b.Property<uint>("AuthorId")
                        .HasColumnName("uid");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnName("content")
                        .HasColumnType("text");

                    b.Property<DateTime>("EndAt")
                        .HasColumnName("end")
                        .HasColumnType("datetime");

                    b.Property<string>("Place")
                        .IsRequired()
                        .HasColumnName("place")
                        .HasColumnType("tinytext");

                    b.Property<DateTime>("StartAt")
                        .HasColumnName("start")
                        .HasColumnType("datetime");

                    b.Property<string>("Summary")
                        .IsRequired()
                        .HasColumnName("purpose")
                        .HasColumnType("tinytext");

                    b.HasKey("Id")
                        .HasName("PRIMARY");

                    b.HasIndex("AuthorId")
                        .HasName("uid");

                    b.HasIndex("StartAt", "EndAt")
                        .HasName("start");

                    b.ToTable("pro_activities");
                });

            modelBuilder.Entity("pro_web.Models.ActivityAttachment", b =>
                {
                    b.Property<uint>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<uint>("ActivityId")
                        .HasColumnName("aid");

                    b.Property<string>("Filename")
                        .IsRequired()
                        .HasColumnName("md5")
                        .HasColumnType("tinytext");

                    b.Property<string>("MediaType");

                    b.Property<string>("OriginalFilename")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("tinytext");

                    b.Property<long>("Size");

                    b.HasKey("Id")
                        .HasName("PRIMARY");

                    b.HasIndex("ActivityId")
                        .HasName("aid");

                    b.ToTable("pro_activity_attach");
                });

            modelBuilder.Entity("pro_web.Models.ActivityAttendee", b =>
                {
                    b.Property<uint>("ActivityId")
                        .HasColumnName("aid");

                    b.Property<uint>("AttandeeId")
                        .HasColumnName("uid");

                    b.HasKey("ActivityId", "AttandeeId");

                    b.HasIndex("ActivityId")
                        .HasName("aid");

                    b.HasIndex("AttandeeId")
                        .HasName("uid");

                    b.ToTable("pro_activity_attend");
                });

            modelBuilder.Entity("pro_web.Models.Member", b =>
                {
                    b.Property<uint>("StudentNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<byte>("Authority")
                        .HasColumnName("authority");

                    b.Property<byte>("Gen")
                        .HasColumnName("gen");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("varchar(4)");

                    b.Property<string>("Password")
                        .HasColumnName("pw")
                        .HasColumnType("char(60)");

                    b.Property<uint>("PhoneNumber")
                        .HasColumnName("contact");

                    b.HasKey("StudentNumber")
                        .HasName("PRIMARY");

                    b.ToTable("pro_members");
                });

            modelBuilder.Entity("pro_web.Models.MemberHistory", b =>
                {
                    b.Property<uint>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("idx");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnName("content")
                        .HasColumnType("text");

                    b.Property<uint>("MemberId")
                        .HasColumnName("uid");

                    b.Property<DateTime>("OccurredAt")
                        .HasColumnName("date")
                        .HasColumnType("date");

                    b.HasKey("Id")
                        .HasName("PRIMARY");

                    b.HasIndex("MemberId")
                        .HasName("uid");

                    b.ToTable("pro_member_history");
                });

            modelBuilder.Entity("pro_web.Models.MemberLog", b =>
                {
                    b.Property<uint>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("idx");

                    b.Property<uint>("MemberId")
                        .HasColumnName("uid");

                    b.Property<DateTime>("OccurredAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("date")
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnName("text")
                        .HasColumnType("text");

                    b.HasKey("Id")
                        .HasName("PRIMARY");

                    b.HasIndex("MemberId")
                        .HasName("uid");

                    b.ToTable("pro_member_log");
                });

            modelBuilder.Entity("pro_web.Models.Task", b =>
                {
                    b.Property<uint>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("idx");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnName("content")
                        .HasColumnType("text");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnName("deleted_at")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("EndAt")
                        .HasColumnName("end")
                        .HasColumnType("date");

                    b.Property<string>("ExampleInput")
                        .IsRequired()
                        .HasColumnName("in")
                        .HasColumnType("text");

                    b.Property<string>("ExampleOutput")
                        .IsRequired()
                        .HasColumnName("out")
                        .HasColumnType("text");

                    b.Property<sbyte>("Hidden")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("block")
                        .HasColumnType("tinyint(4)")
                        .HasDefaultValueSql("'0'");

                    b.Property<DateTime>("StartAt")
                        .HasColumnName("start")
                        .HasColumnType("date");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnName("title")
                        .HasColumnType("tinytext");

                    b.HasKey("Id")
                        .HasName("PRIMARY");

                    b.HasIndex("EndAt")
                        .HasName("end");

                    b.HasIndex("Hidden")
                        .HasName("block");

                    b.HasIndex("StartAt", "EndAt")
                        .HasName("start");

                    b.ToTable("pro_tasks");
                });

            modelBuilder.Entity("pro_web.Models.TaskSource", b =>
                {
                    b.Property<uint>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("source_id");

                    b.Property<uint>("AuthorId")
                        .HasColumnName("uid");

                    b.Property<string>("Error")
                        .IsRequired()
                        .HasColumnName("error")
                        .HasColumnType("text");

                    b.Property<int>("Score")
                        .HasColumnName("score")
                        .HasColumnType("int(11)");

                    b.Property<uint>("Sequence")
                        .HasColumnName("fid");

                    b.Property<uint>("Size")
                        .HasColumnName("size");

                    b.Property<int>("Status")
                        .HasColumnName("status");

                    b.Property<DateTime>("SubmitAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("date")
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<uint>("TaskId")
                        .HasColumnName("tid");

                    b.Property<bool>("Working");

                    b.HasKey("Id")
                        .HasName("PRIMARY");

                    b.HasIndex("AuthorId")
                        .HasName("uid");

                    b.HasIndex("TaskId")
                        .HasName("tid");

                    b.HasIndex("TaskId", "AuthorId", "Sequence")
                        .IsUnique()
                        .HasName("tid_2");

                    b.ToTable("pro_submit");
                });

            modelBuilder.Entity("pro_web.Models.TaskTest", b =>
                {
                    b.Property<uint>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<string>("Input")
                        .IsRequired()
                        .HasColumnName("input");

                    b.Property<string>("Output")
                        .IsRequired()
                        .HasColumnName("output");

                    b.Property<uint>("Score")
                        .HasColumnName("score");

                    b.Property<uint>("TaskId")
                        .HasColumnName("task_id");

                    b.HasKey("Id");

                    b.HasIndex("TaskId")
                        .HasName("task_id");

                    b.HasIndex("TaskId", "Score")
                        .HasName("task_id_2");

                    b.ToTable("pro_task_tests");
                });

            modelBuilder.Entity("pro_web.Models.Activity", b =>
                {
                    b.HasOne("pro_web.Models.Member", "Author")
                        .WithMany("Activities")
                        .HasForeignKey("AuthorId")
                        .HasConstraintName("pro_activities_ibfk_1");
                });

            modelBuilder.Entity("pro_web.Models.ActivityAttachment", b =>
                {
                    b.HasOne("pro_web.Models.Activity", "Activity")
                        .WithMany("Attachments")
                        .HasForeignKey("ActivityId")
                        .HasConstraintName("pro_activity_attach_ibfk_1");
                });

            modelBuilder.Entity("pro_web.Models.ActivityAttendee", b =>
                {
                    b.HasOne("pro_web.Models.Activity", "Activity")
                        .WithMany("ActivityAttendees")
                        .HasForeignKey("ActivityId")
                        .HasConstraintName("pro_activity_attend_ibfk_2");

                    b.HasOne("pro_web.Models.Member", "Attendee")
                        .WithMany("ActivityAttendees")
                        .HasForeignKey("AttandeeId")
                        .HasConstraintName("pro_activity_attend_ibfk_1");
                });

            modelBuilder.Entity("pro_web.Models.MemberHistory", b =>
                {
                    b.HasOne("pro_web.Models.Member", "Member")
                        .WithMany("MemberHistory")
                        .HasForeignKey("MemberId")
                        .HasConstraintName("pro_member_history_ibfk_1");
                });

            modelBuilder.Entity("pro_web.Models.MemberLog", b =>
                {
                    b.HasOne("pro_web.Models.Member", "Member")
                        .WithMany("MemberLogs")
                        .HasForeignKey("MemberId")
                        .HasConstraintName("pro_member_log_ibfk_1");
                });

            modelBuilder.Entity("pro_web.Models.TaskSource", b =>
                {
                    b.HasOne("pro_web.Models.Member", "Author")
                        .WithMany("TaskSources")
                        .HasForeignKey("AuthorId")
                        .HasConstraintName("pro_submit_ibfk_1");

                    b.HasOne("pro_web.Models.Task", "Task")
                        .WithMany("Sources")
                        .HasForeignKey("TaskId")
                        .HasConstraintName("pro_submit_ibfk_2");
                });

            modelBuilder.Entity("pro_web.Models.TaskTest", b =>
                {
                    b.HasOne("pro_web.Models.Task", "Task")
                        .WithMany("Tests")
                        .HasForeignKey("TaskId")
                        .HasConstraintName("pro_task_tests_ibfk_1");
                });
#pragma warning restore 612, 618
        }
    }
}
