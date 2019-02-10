using Microsoft.EntityFrameworkCore.Migrations;

namespace pro_web.Migrations
{
    public partial class SubmissionStatusEnum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "status",
                table: "pro_submit",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int(11)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "status",
                table: "pro_submit",
                type: "int(11)",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
