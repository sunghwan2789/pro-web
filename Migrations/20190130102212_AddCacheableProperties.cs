using Microsoft.EntityFrameworkCore.Migrations;

namespace pro_web.Migrations
{
    public partial class AddCacheableProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MediaType",
                table: "pro_activity_attach",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Size",
                table: "pro_activity_attach",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MediaType",
                table: "pro_activity_attach");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "pro_activity_attach");
        }
    }
}
