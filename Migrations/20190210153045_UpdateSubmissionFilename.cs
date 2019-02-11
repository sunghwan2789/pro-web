using Microsoft.EntityFrameworkCore.Migrations;

namespace pro_web.Migrations
{
    public partial class UpdateSubmissionFilename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"UPDATE pro_submit AS a, pro_submit AS b
                SET a.Filename = CONCAT(b.tid,'_',b.uid,'_',b.fid,'.c')
                WHERE a.source_id = b.source_id", true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE pro_submit SET Filename = NULL", true);
        }
    }
}
