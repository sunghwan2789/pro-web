using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace pro_web.Migrations
{
    public partial class AlterUnusedMisusedFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey("FK_Activities_Members_AuthorId", "Activities");
            migrationBuilder.DropForeignKey("FK_ActivityAttachments_Activities_ActivityId", "ActivityAttachments");
            migrationBuilder.DropForeignKey("FK_ActivityAttendees_Activities_ActivityId", "ActivityAttendees");
            migrationBuilder.DropForeignKey("FK_ActivityAttendees_Members_AttandeeId", "ActivityAttendees");
            migrationBuilder.DropForeignKey("FK_MemberHistory_Members_MemberId", "MemberHistory");
            migrationBuilder.DropForeignKey("FK_MemberLogs_Members_MemberId", "MemberLogs");
            migrationBuilder.DropForeignKey("FK_Submissions_Members_AuthorId", "Submissions");
            migrationBuilder.DropForeignKey("FK_Submissions_Tasks_TaskId", "Submissions");
            migrationBuilder.DropForeignKey("FK_TaskTests_Tasks_TaskId", "TaskTests");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Tasks");

            migrationBuilder.RenameColumn(
                name: "StudentNumber",
                table: "Members",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "Text",
                table: "MemberLogs",
                newName: "Content");

            migrationBuilder.RenameColumn(
                name: "Summary",
                table: "Activities",
                newName: "Title");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Members",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Members_AuthorId",
                table: "Activities",
                column: "AuthorId",
                principalTable: "Members",
                principalColumn: "Id",
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
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MemberHistory_Members_MemberId",
                table: "MemberHistory",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MemberLogs_Members_MemberId",
                table: "MemberLogs",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Submissions_Members_AuthorId",
                table: "Submissions",
                column: "AuthorId",
                principalTable: "Members",
                principalColumn: "Id",
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
            migrationBuilder.DropForeignKey("FK_Activities_Members_AuthorId", "Activities");
            migrationBuilder.DropForeignKey("FK_ActivityAttachments_Activities_ActivityId", "ActivityAttachments");
            migrationBuilder.DropForeignKey("FK_ActivityAttendees_Activities_ActivityId", "ActivityAttendees");
            migrationBuilder.DropForeignKey("FK_ActivityAttendees_Members_AttandeeId", "ActivityAttendees");
            migrationBuilder.DropForeignKey("FK_MemberHistory_Members_MemberId", "MemberHistory");
            migrationBuilder.DropForeignKey("FK_MemberLogs_Members_MemberId", "MemberLogs");
            migrationBuilder.DropForeignKey("FK_Submissions_Members_AuthorId", "Submissions");
            migrationBuilder.DropForeignKey("FK_Submissions_Tasks_TaskId", "Submissions");
            migrationBuilder.DropForeignKey("FK_TaskTests_Tasks_TaskId", "TaskTests");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Members",
                newName: "StudentNumber");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "MemberLogs",
                newName: "Text");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Activities",
                newName: "Summary");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Tasks",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PhoneNumber",
                table: "Members",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

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
    }
}
