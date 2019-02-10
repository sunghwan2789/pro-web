using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace pro_web.Migrations
{
    public partial class FixActivityAttendeeRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PRIMARY",
                table: "pro_activity_attend");

            migrationBuilder.DropIndex(
                name: "aid_2",
                table: "pro_activity_attend");

            migrationBuilder.DropColumn(
                name: "id",
                table: "pro_activity_attend");

            migrationBuilder.AddPrimaryKey(
                name: "PK_pro_activity_attend",
                table: "pro_activity_attend",
                columns: new[] { "aid", "uid" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_pro_activity_attend",
                table: "pro_activity_attend");

            migrationBuilder.AddColumn<uint>(
                name: "id",
                table: "pro_activity_attend",
                nullable: false,
                defaultValue: 0u)
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PRIMARY",
                table: "pro_activity_attend",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "aid_2",
                table: "pro_activity_attend",
                columns: new[] { "aid", "uid" },
                unique: true);
        }
    }
}
