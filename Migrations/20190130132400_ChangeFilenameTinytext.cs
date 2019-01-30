using Microsoft.EntityFrameworkCore.Migrations;

namespace pro_web.Migrations
{
    public partial class ChangeFilenameTinytext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "md5",
                table: "pro_activity_attach",
                type: "tinytext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char(32)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "md5",
                table: "pro_activity_attach",
                type: "char(32)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "tinytext");
        }
    }
}
