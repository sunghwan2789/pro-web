using Microsoft.EntityFrameworkCore.Migrations;

namespace pro_web.Migrations
{
    public partial class DropSubmissionUniqueIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "tid_2",
                table: "pro_submit");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "tid_2",
                table: "pro_submit",
                columns: new[] { "tid", "uid", "fid" },
                unique: true);
        }
    }
}
