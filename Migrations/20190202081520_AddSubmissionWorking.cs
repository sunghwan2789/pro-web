using Microsoft.EntityFrameworkCore.Migrations;

namespace pro_web.Migrations
{
    public partial class AddSubmissionWorking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Working",
                table: "pro_submit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Working",
                table: "pro_submit");
        }
    }
}
