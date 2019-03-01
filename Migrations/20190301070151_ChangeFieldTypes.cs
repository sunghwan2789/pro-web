using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace pro_web.Migrations
{
    public partial class ChangeFieldTypes : Migration
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

            migrationBuilder.AlterColumn<int>(
                name: "TaskId",
                table: "TaskTests",
                nullable: false,
                oldClrType: typeof(uint));

            migrationBuilder.AlterColumn<int>(
                name: "Score",
                table: "TaskTests",
                nullable: false,
                oldClrType: typeof(uint));

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "TaskTests",
                nullable: false,
                oldClrType: typeof(uint))
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<bool>(
                name: "Hidden",
                table: "Tasks",
                nullable: false,
                oldClrType: typeof(sbyte));

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Tasks",
                nullable: false,
                oldClrType: typeof(uint))
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<int>(
                name: "TaskId",
                table: "Submissions",
                nullable: false,
                oldClrType: typeof(uint));

            migrationBuilder.AlterColumn<long>(
                name: "Size",
                table: "Submissions",
                nullable: false,
                oldClrType: typeof(uint));

            migrationBuilder.AlterColumn<int>(
                name: "Sequence",
                table: "Submissions",
                nullable: false,
                oldClrType: typeof(uint));

            migrationBuilder.AlterColumn<int>(
                name: "AuthorId",
                table: "Submissions",
                nullable: false,
                oldClrType: typeof(uint));

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Submissions",
                nullable: false,
                oldClrType: typeof(uint))
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<int>(
                name: "PhoneNumber",
                table: "Members",
                nullable: false,
                oldClrType: typeof(uint));

            migrationBuilder.AlterColumn<int>(
                name: "Gen",
                table: "Members",
                nullable: false,
                oldClrType: typeof(byte));

            migrationBuilder.AlterColumn<int>(
                name: "Authority",
                table: "Members",
                nullable: false,
                oldClrType: typeof(byte));

            migrationBuilder.AlterColumn<int>(
                name: "StudentNumber",
                table: "Members",
                nullable: false,
                oldClrType: typeof(uint))
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<int>(
                name: "MemberId",
                table: "MemberLogs",
                nullable: false,
                oldClrType: typeof(uint));

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "MemberLogs",
                nullable: false,
                oldClrType: typeof(uint))
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<int>(
                name: "MemberId",
                table: "MemberHistory",
                nullable: false,
                oldClrType: typeof(uint));

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "MemberHistory",
                nullable: false,
                oldClrType: typeof(uint))
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<int>(
                name: "AttandeeId",
                table: "ActivityAttendees",
                nullable: false,
                oldClrType: typeof(uint));

            migrationBuilder.AlterColumn<int>(
                name: "ActivityId",
                table: "ActivityAttendees",
                nullable: false,
                oldClrType: typeof(uint));

            migrationBuilder.AlterColumn<int>(
                name: "ActivityId",
                table: "ActivityAttachments",
                nullable: false,
                oldClrType: typeof(uint));

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "ActivityAttachments",
                nullable: false,
                oldClrType: typeof(uint))
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<int>(
                name: "AuthorId",
                table: "Activities",
                nullable: false,
                oldClrType: typeof(uint));

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Activities",
                nullable: false,
                oldClrType: typeof(uint))
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

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
            migrationBuilder.DropForeignKey("FK_Activities_Members_AuthorId", "Activities");
            migrationBuilder.DropForeignKey("FK_ActivityAttachments_Activities_ActivityId", "ActivityAttachments");
            migrationBuilder.DropForeignKey("FK_ActivityAttendees_Activities_ActivityId", "ActivityAttendees");
            migrationBuilder.DropForeignKey("FK_ActivityAttendees_Members_AttandeeId", "ActivityAttendees");
            migrationBuilder.DropForeignKey("FK_MemberHistory_Members_MemberId", "MemberHistory");
            migrationBuilder.DropForeignKey("FK_MemberLogs_Members_MemberId", "MemberLogs");
            migrationBuilder.DropForeignKey("FK_Submissions_Members_AuthorId", "Submissions");
            migrationBuilder.DropForeignKey("FK_Submissions_Tasks_TaskId", "Submissions");
            migrationBuilder.DropForeignKey("FK_TaskTests_Tasks_TaskId", "TaskTests");

            migrationBuilder.AlterColumn<uint>(
                name: "TaskId",
                table: "TaskTests",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<uint>(
                name: "Score",
                table: "TaskTests",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<uint>(
                name: "Id",
                table: "TaskTests",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<sbyte>(
                name: "Hidden",
                table: "Tasks",
                nullable: false,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<uint>(
                name: "Id",
                table: "Tasks",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<uint>(
                name: "TaskId",
                table: "Submissions",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<uint>(
                name: "Size",
                table: "Submissions",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<uint>(
                name: "Sequence",
                table: "Submissions",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<uint>(
                name: "AuthorId",
                table: "Submissions",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<uint>(
                name: "Id",
                table: "Submissions",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<uint>(
                name: "PhoneNumber",
                table: "Members",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<byte>(
                name: "Gen",
                table: "Members",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<byte>(
                name: "Authority",
                table: "Members",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<uint>(
                name: "StudentNumber",
                table: "Members",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<uint>(
                name: "MemberId",
                table: "MemberLogs",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<uint>(
                name: "Id",
                table: "MemberLogs",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<uint>(
                name: "MemberId",
                table: "MemberHistory",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<uint>(
                name: "Id",
                table: "MemberHistory",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<uint>(
                name: "AttandeeId",
                table: "ActivityAttendees",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<uint>(
                name: "ActivityId",
                table: "ActivityAttendees",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<uint>(
                name: "ActivityId",
                table: "ActivityAttachments",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<uint>(
                name: "Id",
                table: "ActivityAttachments",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<uint>(
                name: "AuthorId",
                table: "Activities",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<uint>(
                name: "Id",
                table: "Activities",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

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
