using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace pro_web.Migrations
{
    public partial class AlterModelNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "pro_activities_ibfk_1",
                table: "pro_activities");

            migrationBuilder.DropForeignKey(
                name: "pro_activity_attach_ibfk_1",
                table: "pro_activity_attach");

            migrationBuilder.DropForeignKey(
                name: "pro_activity_attend_ibfk_2",
                table: "pro_activity_attend");

            migrationBuilder.DropForeignKey(
                name: "pro_activity_attend_ibfk_1",
                table: "pro_activity_attend");

            migrationBuilder.DropForeignKey(
                name: "pro_member_history_ibfk_1",
                table: "pro_member_history");

            migrationBuilder.DropForeignKey(
                name: "pro_member_log_ibfk_1",
                table: "pro_member_log");

            migrationBuilder.DropForeignKey(
                name: "pro_submit_ibfk_1",
                table: "pro_submit");

            migrationBuilder.DropForeignKey(
                name: "pro_submit_ibfk_2",
                table: "pro_submit");

            migrationBuilder.DropForeignKey(
                name: "pro_task_tests_ibfk_1",
                table: "pro_task_tests");

            migrationBuilder.DropPrimaryKey(
                name: "PRIMARY",
                table: "pro_tasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_pro_task_tests",
                table: "pro_task_tests");

            migrationBuilder.DropPrimaryKey(
                name: "PRIMARY",
                table: "pro_submit");

            migrationBuilder.DropPrimaryKey(
                name: "PRIMARY",
                table: "pro_members");

            migrationBuilder.DropPrimaryKey(
                name: "PRIMARY",
                table: "pro_member_log");

            migrationBuilder.DropPrimaryKey(
                name: "PRIMARY",
                table: "pro_member_history");

            migrationBuilder.DropPrimaryKey(
                name: "PK_pro_activity_attend",
                table: "pro_activity_attend");

            migrationBuilder.DropPrimaryKey(
                name: "PRIMARY",
                table: "pro_activity_attach");

            migrationBuilder.DropPrimaryKey(
                name: "PRIMARY",
                table: "pro_activities");

            migrationBuilder.RenameTable(
                name: "pro_tasks",
                newName: "Tasks");

            migrationBuilder.RenameTable(
                name: "pro_task_tests",
                newName: "TaskTests");

            migrationBuilder.RenameTable(
                name: "pro_submit",
                newName: "Submissions");

            migrationBuilder.RenameTable(
                name: "pro_members",
                newName: "Members");

            migrationBuilder.RenameTable(
                name: "pro_member_log",
                newName: "MemberLogs");

            migrationBuilder.RenameTable(
                name: "pro_member_history",
                newName: "MemberHistory");

            migrationBuilder.RenameTable(
                name: "pro_activity_attend",
                newName: "ActivityAttendees");

            migrationBuilder.RenameTable(
                name: "pro_activity_attach",
                newName: "ActivityAttachments");

            migrationBuilder.RenameTable(
                name: "pro_activities",
                newName: "Activities");

            migrationBuilder.RenameColumn(
                name: "title",
                table: "Tasks",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "content",
                table: "Tasks",
                newName: "Content");

            migrationBuilder.RenameColumn(
                name: "start",
                table: "Tasks",
                newName: "StartAt");

            migrationBuilder.RenameColumn(
                name: "block",
                table: "Tasks",
                newName: "Hidden");

            migrationBuilder.RenameColumn(
                name: "out",
                table: "Tasks",
                newName: "ExampleOutput");

            migrationBuilder.RenameColumn(
                name: "in",
                table: "Tasks",
                newName: "ExampleInput");

            migrationBuilder.RenameColumn(
                name: "end",
                table: "Tasks",
                newName: "EndAt");

            migrationBuilder.RenameColumn(
                name: "deleted_at",
                table: "Tasks",
                newName: "DeletedAt");

            migrationBuilder.RenameColumn(
                name: "idx",
                table: "Tasks",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "score",
                table: "TaskTests",
                newName: "Score");

            migrationBuilder.RenameColumn(
                name: "output",
                table: "TaskTests",
                newName: "Output");

            migrationBuilder.RenameColumn(
                name: "input",
                table: "TaskTests",
                newName: "Input");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "TaskTests",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "task_id",
                table: "TaskTests",
                newName: "TaskId");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "Submissions",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "size",
                table: "Submissions",
                newName: "Size");

            migrationBuilder.RenameColumn(
                name: "score",
                table: "Submissions",
                newName: "Score");

            migrationBuilder.RenameColumn(
                name: "error",
                table: "Submissions",
                newName: "Error");

            migrationBuilder.RenameColumn(
                name: "tid",
                table: "Submissions",
                newName: "TaskId");

            migrationBuilder.RenameColumn(
                name: "date",
                table: "Submissions",
                newName: "SubmitAt");

            migrationBuilder.RenameColumn(
                name: "fid",
                table: "Submissions",
                newName: "Sequence");

            migrationBuilder.RenameColumn(
                name: "uid",
                table: "Submissions",
                newName: "AuthorId");

            migrationBuilder.RenameColumn(
                name: "source_id",
                table: "Submissions",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Members",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "gen",
                table: "Members",
                newName: "Gen");

            migrationBuilder.RenameColumn(
                name: "authority",
                table: "Members",
                newName: "Authority");

            migrationBuilder.RenameColumn(
                name: "contact",
                table: "Members",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "pw",
                table: "Members",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Members",
                newName: "StudentNumber");

            migrationBuilder.RenameColumn(
                name: "text",
                table: "MemberLogs",
                newName: "Text");

            migrationBuilder.RenameColumn(
                name: "date",
                table: "MemberLogs",
                newName: "OccurredAt");

            migrationBuilder.RenameColumn(
                name: "uid",
                table: "MemberLogs",
                newName: "MemberId");

            migrationBuilder.RenameColumn(
                name: "idx",
                table: "MemberLogs",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "content",
                table: "MemberHistory",
                newName: "Content");

            migrationBuilder.RenameColumn(
                name: "date",
                table: "MemberHistory",
                newName: "OccurredAt");

            migrationBuilder.RenameColumn(
                name: "uid",
                table: "MemberHistory",
                newName: "MemberId");

            migrationBuilder.RenameColumn(
                name: "idx",
                table: "MemberHistory",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "uid",
                table: "ActivityAttendees",
                newName: "AttandeeId");

            migrationBuilder.RenameColumn(
                name: "aid",
                table: "ActivityAttendees",
                newName: "ActivityId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "ActivityAttachments",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "ActivityAttachments",
                newName: "OriginalFilename");

            migrationBuilder.RenameColumn(
                name: "md5",
                table: "ActivityAttachments",
                newName: "Filename");

            migrationBuilder.RenameColumn(
                name: "aid",
                table: "ActivityAttachments",
                newName: "ActivityId");

            migrationBuilder.RenameColumn(
                name: "place",
                table: "Activities",
                newName: "Place");

            migrationBuilder.RenameColumn(
                name: "content",
                table: "Activities",
                newName: "Content");

            migrationBuilder.RenameColumn(
                name: "purpose",
                table: "Activities",
                newName: "Summary");

            migrationBuilder.RenameColumn(
                name: "start",
                table: "Activities",
                newName: "StartAt");

            migrationBuilder.RenameColumn(
                name: "end",
                table: "Activities",
                newName: "EndAt");

            migrationBuilder.RenameColumn(
                name: "uid",
                table: "Activities",
                newName: "AuthorId");

            migrationBuilder.RenameColumn(
                name: "idx",
                table: "Activities",
                newName: "Id");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Tasks",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "tinytext");

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Tasks",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartAt",
                table: "Tasks",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AlterColumn<sbyte>(
                name: "Hidden",
                table: "Tasks",
                nullable: false,
                oldClrType: typeof(sbyte),
                oldType: "tinyint(4)",
                oldDefaultValueSql: "'0'");

            migrationBuilder.AlterColumn<string>(
                name: "ExampleOutput",
                table: "Tasks",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "ExampleInput",
                table: "Tasks",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndAt",
                table: "Tasks",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletedAt",
                table: "Tasks",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Score",
                table: "Submissions",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int(11)");

            migrationBuilder.AlterColumn<string>(
                name: "Filename",
                table: "Submissions",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Error",
                table: "Submissions",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<DateTime>(
                name: "SubmitAt",
                table: "Submissions",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Members",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(4)");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Members",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "char(60)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "MemberLogs",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<DateTime>(
                name: "OccurredAt",
                table: "MemberLogs",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "MemberHistory",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<DateTime>(
                name: "OccurredAt",
                table: "MemberHistory",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AlterColumn<string>(
                name: "OriginalFilename",
                table: "ActivityAttachments",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "tinytext");

            migrationBuilder.AlterColumn<string>(
                name: "Filename",
                table: "ActivityAttachments",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "tinytext");

            migrationBuilder.AlterColumn<string>(
                name: "Place",
                table: "Activities",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "tinytext");

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Activities",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Summary",
                table: "Activities",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "tinytext");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartAt",
                table: "Activities",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndAt",
                table: "Activities",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tasks",
                table: "Tasks",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TaskTests",
                table: "TaskTests",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Submissions",
                table: "Submissions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Members",
                table: "Members",
                column: "StudentNumber");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MemberLogs",
                table: "MemberLogs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MemberHistory",
                table: "MemberHistory",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ActivityAttendees",
                table: "ActivityAttendees",
                columns: new[] { "ActivityId", "AttandeeId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ActivityAttachments",
                table: "ActivityAttachments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Activities",
                table: "Activities",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Members_AuthorId",
                table: "Activities",
                column: "AuthorId",
                principalTable: "Members",
                principalColumn: "StudentNumber",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityAttachments_Activities_ActivityId",
                table: "ActivityAttachments",
                column: "ActivityId",
                principalTable: "Activities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityAttendees_Activities_ActivityId",
                table: "ActivityAttendees",
                column: "ActivityId",
                principalTable: "Activities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityAttendees_Members_AttandeeId",
                table: "ActivityAttendees",
                column: "AttandeeId",
                principalTable: "Members",
                principalColumn: "StudentNumber",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MemberHistory_Members_MemberId",
                table: "MemberHistory",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "StudentNumber",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MemberLogs_Members_MemberId",
                table: "MemberLogs",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "StudentNumber",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Submissions_Members_AuthorId",
                table: "Submissions",
                column: "AuthorId",
                principalTable: "Members",
                principalColumn: "StudentNumber",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Submissions_Tasks_TaskId",
                table: "Submissions",
                column: "TaskId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskTests_Tasks_TaskId",
                table: "TaskTests",
                column: "TaskId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Members_AuthorId",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_ActivityAttachments_Activities_ActivityId",
                table: "ActivityAttachments");

            migrationBuilder.DropForeignKey(
                name: "FK_ActivityAttendees_Activities_ActivityId",
                table: "ActivityAttendees");

            migrationBuilder.DropForeignKey(
                name: "FK_ActivityAttendees_Members_AttandeeId",
                table: "ActivityAttendees");

            migrationBuilder.DropForeignKey(
                name: "FK_MemberHistory_Members_MemberId",
                table: "MemberHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_MemberLogs_Members_MemberId",
                table: "MemberLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_Submissions_Members_AuthorId",
                table: "Submissions");

            migrationBuilder.DropForeignKey(
                name: "FK_Submissions_Tasks_TaskId",
                table: "Submissions");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskTests_Tasks_TaskId",
                table: "TaskTests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TaskTests",
                table: "TaskTests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tasks",
                table: "Tasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Submissions",
                table: "Submissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Members",
                table: "Members");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MemberLogs",
                table: "MemberLogs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MemberHistory",
                table: "MemberHistory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ActivityAttendees",
                table: "ActivityAttendees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ActivityAttachments",
                table: "ActivityAttachments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Activities",
                table: "Activities");

            migrationBuilder.RenameTable(
                name: "TaskTests",
                newName: "pro_task_tests");

            migrationBuilder.RenameTable(
                name: "Tasks",
                newName: "pro_tasks");

            migrationBuilder.RenameTable(
                name: "Submissions",
                newName: "pro_submit");

            migrationBuilder.RenameTable(
                name: "Members",
                newName: "pro_members");

            migrationBuilder.RenameTable(
                name: "MemberLogs",
                newName: "pro_member_log");

            migrationBuilder.RenameTable(
                name: "MemberHistory",
                newName: "pro_member_history");

            migrationBuilder.RenameTable(
                name: "ActivityAttendees",
                newName: "pro_activity_attend");

            migrationBuilder.RenameTable(
                name: "ActivityAttachments",
                newName: "pro_activity_attach");

            migrationBuilder.RenameTable(
                name: "Activities",
                newName: "pro_activities");

            migrationBuilder.RenameColumn(
                name: "Score",
                table: "pro_task_tests",
                newName: "score");

            migrationBuilder.RenameColumn(
                name: "Output",
                table: "pro_task_tests",
                newName: "output");

            migrationBuilder.RenameColumn(
                name: "Input",
                table: "pro_task_tests",
                newName: "input");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "pro_task_tests",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "TaskId",
                table: "pro_task_tests",
                newName: "task_id");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "pro_tasks",
                newName: "title");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "pro_tasks",
                newName: "content");

            migrationBuilder.RenameColumn(
                name: "StartAt",
                table: "pro_tasks",
                newName: "start");

            migrationBuilder.RenameColumn(
                name: "Hidden",
                table: "pro_tasks",
                newName: "block");

            migrationBuilder.RenameColumn(
                name: "ExampleOutput",
                table: "pro_tasks",
                newName: "out");

            migrationBuilder.RenameColumn(
                name: "ExampleInput",
                table: "pro_tasks",
                newName: "in");

            migrationBuilder.RenameColumn(
                name: "EndAt",
                table: "pro_tasks",
                newName: "end");

            migrationBuilder.RenameColumn(
                name: "DeletedAt",
                table: "pro_tasks",
                newName: "deleted_at");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "pro_tasks",
                newName: "idx");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "pro_submit",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "Size",
                table: "pro_submit",
                newName: "size");

            migrationBuilder.RenameColumn(
                name: "Score",
                table: "pro_submit",
                newName: "score");

            migrationBuilder.RenameColumn(
                name: "Error",
                table: "pro_submit",
                newName: "error");

            migrationBuilder.RenameColumn(
                name: "TaskId",
                table: "pro_submit",
                newName: "tid");

            migrationBuilder.RenameColumn(
                name: "SubmitAt",
                table: "pro_submit",
                newName: "date");

            migrationBuilder.RenameColumn(
                name: "Sequence",
                table: "pro_submit",
                newName: "fid");

            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "pro_submit",
                newName: "uid");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "pro_submit",
                newName: "source_id");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "pro_members",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Gen",
                table: "pro_members",
                newName: "gen");

            migrationBuilder.RenameColumn(
                name: "Authority",
                table: "pro_members",
                newName: "authority");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "pro_members",
                newName: "contact");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "pro_members",
                newName: "pw");

            migrationBuilder.RenameColumn(
                name: "StudentNumber",
                table: "pro_members",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Text",
                table: "pro_member_log",
                newName: "text");

            migrationBuilder.RenameColumn(
                name: "OccurredAt",
                table: "pro_member_log",
                newName: "date");

            migrationBuilder.RenameColumn(
                name: "MemberId",
                table: "pro_member_log",
                newName: "uid");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "pro_member_log",
                newName: "idx");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "pro_member_history",
                newName: "content");

            migrationBuilder.RenameColumn(
                name: "OccurredAt",
                table: "pro_member_history",
                newName: "date");

            migrationBuilder.RenameColumn(
                name: "MemberId",
                table: "pro_member_history",
                newName: "uid");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "pro_member_history",
                newName: "idx");

            migrationBuilder.RenameColumn(
                name: "AttandeeId",
                table: "pro_activity_attend",
                newName: "uid");

            migrationBuilder.RenameColumn(
                name: "ActivityId",
                table: "pro_activity_attend",
                newName: "aid");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "pro_activity_attach",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "OriginalFilename",
                table: "pro_activity_attach",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Filename",
                table: "pro_activity_attach",
                newName: "md5");

            migrationBuilder.RenameColumn(
                name: "ActivityId",
                table: "pro_activity_attach",
                newName: "aid");

            migrationBuilder.RenameColumn(
                name: "Place",
                table: "pro_activities",
                newName: "place");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "pro_activities",
                newName: "content");

            migrationBuilder.RenameColumn(
                name: "Summary",
                table: "pro_activities",
                newName: "purpose");

            migrationBuilder.RenameColumn(
                name: "StartAt",
                table: "pro_activities",
                newName: "start");

            migrationBuilder.RenameColumn(
                name: "EndAt",
                table: "pro_activities",
                newName: "end");

            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "pro_activities",
                newName: "uid");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "pro_activities",
                newName: "idx");

            migrationBuilder.AlterColumn<string>(
                name: "title",
                table: "pro_tasks",
                type: "tinytext",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "content",
                table: "pro_tasks",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "start",
                table: "pro_tasks",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<sbyte>(
                name: "block",
                table: "pro_tasks",
                type: "tinyint(4)",
                nullable: false,
                defaultValueSql: "'0'",
                oldClrType: typeof(sbyte));

            migrationBuilder.AlterColumn<string>(
                name: "out",
                table: "pro_tasks",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "in",
                table: "pro_tasks",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "end",
                table: "pro_tasks",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "deleted_at",
                table: "pro_tasks",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "score",
                table: "pro_submit",
                type: "int(11)",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "Filename",
                table: "pro_submit",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "error",
                table: "pro_submit",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "date",
                table: "pro_submit",
                type: "datetime",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "pro_members",
                type: "varchar(4)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "pw",
                table: "pro_members",
                type: "char(60)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "text",
                table: "pro_member_log",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "date",
                table: "pro_member_log",
                type: "datetime",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AlterColumn<string>(
                name: "content",
                table: "pro_member_history",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "date",
                table: "pro_member_history",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "pro_activity_attach",
                type: "tinytext",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "md5",
                table: "pro_activity_attach",
                type: "tinytext",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "place",
                table: "pro_activities",
                type: "tinytext",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "content",
                table: "pro_activities",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "purpose",
                table: "pro_activities",
                type: "tinytext",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "start",
                table: "pro_activities",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "end",
                table: "pro_activities",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime));

            migrationBuilder.AddPrimaryKey(
                name: "PK_pro_task_tests",
                table: "pro_task_tests",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PRIMARY",
                table: "pro_tasks",
                column: "idx");

            migrationBuilder.AddPrimaryKey(
                name: "PRIMARY",
                table: "pro_submit",
                column: "source_id");

            migrationBuilder.AddPrimaryKey(
                name: "PRIMARY",
                table: "pro_members",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PRIMARY",
                table: "pro_member_log",
                column: "idx");

            migrationBuilder.AddPrimaryKey(
                name: "PRIMARY",
                table: "pro_member_history",
                column: "idx");

            migrationBuilder.AddPrimaryKey(
                name: "PK_pro_activity_attend",
                table: "pro_activity_attend",
                columns: new[] { "aid", "uid" });

            migrationBuilder.AddPrimaryKey(
                name: "PRIMARY",
                table: "pro_activity_attach",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PRIMARY",
                table: "pro_activities",
                column: "idx");

            migrationBuilder.AddForeignKey(
                name: "pro_activities_ibfk_1",
                table: "pro_activities",
                column: "uid",
                principalTable: "pro_members",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "pro_activity_attach_ibfk_1",
                table: "pro_activity_attach",
                column: "aid",
                principalTable: "pro_activities",
                principalColumn: "idx",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "pro_activity_attend_ibfk_2",
                table: "pro_activity_attend",
                column: "aid",
                principalTable: "pro_activities",
                principalColumn: "idx",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "pro_activity_attend_ibfk_1",
                table: "pro_activity_attend",
                column: "uid",
                principalTable: "pro_members",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "pro_member_history_ibfk_1",
                table: "pro_member_history",
                column: "uid",
                principalTable: "pro_members",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "pro_member_log_ibfk_1",
                table: "pro_member_log",
                column: "uid",
                principalTable: "pro_members",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "pro_submit_ibfk_1",
                table: "pro_submit",
                column: "uid",
                principalTable: "pro_members",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "pro_submit_ibfk_2",
                table: "pro_submit",
                column: "tid",
                principalTable: "pro_tasks",
                principalColumn: "idx",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "pro_task_tests_ibfk_1",
                table: "pro_task_tests",
                column: "task_id",
                principalTable: "pro_tasks",
                principalColumn: "idx",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
