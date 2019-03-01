using Microsoft.EntityFrameworkCore.Migrations;

namespace pro_web.Migrations
{
    public partial class FixPhoneNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE Members SET PhoneNumber=NULL WHERE PhoneNumber='0'");
            migrationBuilder.Sql("UPDATE Members SET PhoneNumber=CONCAT('0', PhoneNumber) WHERE PhoneNumber IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE Members SET PhoneNumber='0' WHERE PhoneNumber IS NULL");
        }
    }
}
