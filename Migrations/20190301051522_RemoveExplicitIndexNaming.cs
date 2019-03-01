using Microsoft.EntityFrameworkCore.Migrations;

namespace pro_web.Migrations
{
    public partial class RemoveExplicitIndexNaming : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "task_id_2",
                table: "TaskTests",
                newName: "IX_TaskTests_TaskId_Score");

            migrationBuilder.RenameIndex(
                name: "task_id",
                table: "TaskTests",
                newName: "IX_TaskTests_TaskId");

            migrationBuilder.RenameIndex(
                name: "start",
                table: "Tasks",
                newName: "IX_Tasks_StartAt_EndAt");

            migrationBuilder.RenameIndex(
                name: "block",
                table: "Tasks",
                newName: "IX_Tasks_Hidden");

            migrationBuilder.RenameIndex(
                name: "end",
                table: "Tasks",
                newName: "IX_Tasks_EndAt");

            migrationBuilder.RenameIndex(
                name: "tid",
                table: "Submissions",
                newName: "IX_Submissions_TaskId");

            migrationBuilder.RenameIndex(
                name: "uid",
                table: "Submissions",
                newName: "IX_Submissions_AuthorId");

            migrationBuilder.RenameIndex(
                name: "uid",
                table: "MemberLogs",
                newName: "IX_MemberLogs_MemberId");

            migrationBuilder.RenameIndex(
                name: "uid",
                table: "MemberHistory",
                newName: "IX_MemberHistory_MemberId");

            migrationBuilder.RenameIndex(
                name: "uid",
                table: "ActivityAttendees",
                newName: "IX_ActivityAttendees_AttandeeId");

            migrationBuilder.RenameIndex(
                name: "aid",
                table: "ActivityAttendees",
                newName: "IX_ActivityAttendees_ActivityId");

            migrationBuilder.RenameIndex(
                name: "aid",
                table: "ActivityAttachments",
                newName: "IX_ActivityAttachments_ActivityId");

            migrationBuilder.RenameIndex(
                name: "start",
                table: "Activities",
                newName: "IX_Activities_StartAt_EndAt");

            migrationBuilder.RenameIndex(
                name: "uid",
                table: "Activities",
                newName: "IX_Activities_AuthorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "IX_TaskTests_TaskId_Score",
                table: "TaskTests",
                newName: "task_id_2");

            migrationBuilder.RenameIndex(
                name: "IX_TaskTests_TaskId",
                table: "TaskTests",
                newName: "task_id");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_StartAt_EndAt",
                table: "Tasks",
                newName: "start");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_Hidden",
                table: "Tasks",
                newName: "block");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_EndAt",
                table: "Tasks",
                newName: "end");

            migrationBuilder.RenameIndex(
                name: "IX_Submissions_TaskId",
                table: "Submissions",
                newName: "tid");

            migrationBuilder.RenameIndex(
                name: "IX_Submissions_AuthorId",
                table: "Submissions",
                newName: "uid");

            migrationBuilder.RenameIndex(
                name: "IX_MemberLogs_MemberId",
                table: "MemberLogs",
                newName: "uid");

            migrationBuilder.RenameIndex(
                name: "IX_MemberHistory_MemberId",
                table: "MemberHistory",
                newName: "uid");

            migrationBuilder.RenameIndex(
                name: "IX_ActivityAttendees_AttandeeId",
                table: "ActivityAttendees",
                newName: "uid");

            migrationBuilder.RenameIndex(
                name: "IX_ActivityAttendees_ActivityId",
                table: "ActivityAttendees",
                newName: "aid");

            migrationBuilder.RenameIndex(
                name: "IX_ActivityAttachments_ActivityId",
                table: "ActivityAttachments",
                newName: "aid");

            migrationBuilder.RenameIndex(
                name: "IX_Activities_StartAt_EndAt",
                table: "Activities",
                newName: "start");

            migrationBuilder.RenameIndex(
                name: "IX_Activities_AuthorId",
                table: "Activities",
                newName: "uid");
        }
    }
}
