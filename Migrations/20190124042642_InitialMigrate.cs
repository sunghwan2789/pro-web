using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace pro_web.Migrations
{
    public partial class InitialMigrate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "pro_members",
                columns: table => new
                {
                    id = table.Column<uint>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    gen = table.Column<byte>(nullable: false),
                    name = table.Column<string>(type: "varchar(4)", nullable: false),
                    contact = table.Column<uint>(nullable: false),
                    authority = table.Column<byte>(nullable: false),
                    pw = table.Column<string>(type: "char(60)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "pro_tasks",
                columns: table => new
                {
                    idx = table.Column<uint>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    start = table.Column<DateTime>(type: "date", nullable: false),
                    end = table.Column<DateTime>(type: "date", nullable: false),
                    title = table.Column<string>(type: "tinytext", nullable: false),
                    content = table.Column<string>(type: "text", nullable: false),
                    @in = table.Column<string>(name: "in", type: "text", nullable: false),
                    @out = table.Column<string>(name: "out", type: "text", nullable: false),
                    block = table.Column<sbyte>(type: "tinyint(4)", nullable: false, defaultValueSql: "'0'"),
                    deleted_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.idx);
                });

            migrationBuilder.CreateTable(
                name: "pro_activities",
                columns: table => new
                {
                    idx = table.Column<uint>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    start = table.Column<DateTime>(type: "datetime", nullable: false),
                    end = table.Column<DateTime>(type: "datetime", nullable: false),
                    purpose = table.Column<string>(type: "tinytext", nullable: false),
                    content = table.Column<string>(type: "text", nullable: false),
                    place = table.Column<string>(type: "tinytext", nullable: false),
                    uid = table.Column<uint>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.idx);
                    table.ForeignKey(
                        name: "pro_activities_ibfk_1",
                        column: x => x.uid,
                        principalTable: "pro_members",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "pro_member_history",
                columns: table => new
                {
                    idx = table.Column<uint>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    uid = table.Column<uint>(nullable: false),
                    content = table.Column<string>(type: "text", nullable: false),
                    date = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.idx);
                    table.ForeignKey(
                        name: "pro_member_history_ibfk_1",
                        column: x => x.uid,
                        principalTable: "pro_members",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "pro_member_log",
                columns: table => new
                {
                    idx = table.Column<uint>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    date = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    uid = table.Column<uint>(nullable: false),
                    text = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.idx);
                    table.ForeignKey(
                        name: "pro_member_log_ibfk_1",
                        column: x => x.uid,
                        principalTable: "pro_members",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "pro_submit",
                columns: table => new
                {
                    source_id = table.Column<uint>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    tid = table.Column<uint>(nullable: false),
                    uid = table.Column<uint>(nullable: false),
                    fid = table.Column<uint>(nullable: false),
                    date = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    size = table.Column<uint>(nullable: false),
                    status = table.Column<int>(type: "int(11)", nullable: false),
                    score = table.Column<int>(type: "int(11)", nullable: false),
                    error = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.source_id);
                    table.ForeignKey(
                        name: "pro_submit_ibfk_1",
                        column: x => x.uid,
                        principalTable: "pro_members",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "pro_submit_ibfk_2",
                        column: x => x.tid,
                        principalTable: "pro_tasks",
                        principalColumn: "idx",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "pro_task_tests",
                columns: table => new
                {
                    id = table.Column<uint>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    task_id = table.Column<uint>(nullable: false),
                    score = table.Column<uint>(nullable: false),
                    input = table.Column<byte[]>(nullable: false),
                    output = table.Column<byte[]>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pro_task_tests", x => x.id);
                    table.ForeignKey(
                        name: "pro_task_tests_ibfk_1",
                        column: x => x.task_id,
                        principalTable: "pro_tasks",
                        principalColumn: "idx",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "pro_activity_attach",
                columns: table => new
                {
                    id = table.Column<uint>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    aid = table.Column<uint>(nullable: false),
                    md5 = table.Column<string>(type: "char(32)", nullable: false),
                    name = table.Column<string>(type: "tinytext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "pro_activity_attach_ibfk_1",
                        column: x => x.aid,
                        principalTable: "pro_activities",
                        principalColumn: "idx",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "pro_activity_attend",
                columns: table => new
                {
                    id = table.Column<uint>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    aid = table.Column<uint>(nullable: false),
                    uid = table.Column<uint>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "pro_activity_attend_ibfk_2",
                        column: x => x.aid,
                        principalTable: "pro_activities",
                        principalColumn: "idx",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "pro_activity_attend_ibfk_1",
                        column: x => x.uid,
                        principalTable: "pro_members",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "uid",
                table: "pro_activities",
                column: "uid");

            migrationBuilder.CreateIndex(
                name: "start",
                table: "pro_activities",
                columns: new[] { "start", "end" });

            migrationBuilder.CreateIndex(
                name: "aid",
                table: "pro_activity_attach",
                column: "aid");

            migrationBuilder.CreateIndex(
                name: "aid",
                table: "pro_activity_attend",
                column: "aid");

            migrationBuilder.CreateIndex(
                name: "uid",
                table: "pro_activity_attend",
                column: "uid");

            migrationBuilder.CreateIndex(
                name: "aid_2",
                table: "pro_activity_attend",
                columns: new[] { "aid", "uid" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "uid",
                table: "pro_member_history",
                column: "uid");

            migrationBuilder.CreateIndex(
                name: "uid",
                table: "pro_member_log",
                column: "uid");

            migrationBuilder.CreateIndex(
                name: "uid",
                table: "pro_submit",
                column: "uid");

            migrationBuilder.CreateIndex(
                name: "tid",
                table: "pro_submit",
                column: "tid");

            migrationBuilder.CreateIndex(
                name: "tid_2",
                table: "pro_submit",
                columns: new[] { "tid", "uid", "fid" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "task_id",
                table: "pro_task_tests",
                column: "task_id");

            migrationBuilder.CreateIndex(
                name: "task_id_2",
                table: "pro_task_tests",
                columns: new[] { "task_id", "score" });

            migrationBuilder.CreateIndex(
                name: "end",
                table: "pro_tasks",
                column: "end");

            migrationBuilder.CreateIndex(
                name: "block",
                table: "pro_tasks",
                column: "block");

            migrationBuilder.CreateIndex(
                name: "start",
                table: "pro_tasks",
                columns: new[] { "start", "end" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "pro_activity_attach");

            migrationBuilder.DropTable(
                name: "pro_activity_attend");

            migrationBuilder.DropTable(
                name: "pro_member_history");

            migrationBuilder.DropTable(
                name: "pro_member_log");

            migrationBuilder.DropTable(
                name: "pro_submit");

            migrationBuilder.DropTable(
                name: "pro_task_tests");

            migrationBuilder.DropTable(
                name: "pro_activities");

            migrationBuilder.DropTable(
                name: "pro_tasks");

            migrationBuilder.DropTable(
                name: "pro_members");
        }
    }
}
