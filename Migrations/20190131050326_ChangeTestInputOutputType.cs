using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace pro_web.Migrations
{
    public partial class ChangeTestInputOutputType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "output",
                table: "pro_task_tests",
                nullable: false,
                oldClrType: typeof(byte[]));

            migrationBuilder.AlterColumn<string>(
                name: "input",
                table: "pro_task_tests",
                nullable: false,
                oldClrType: typeof(byte[]));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "output",
                table: "pro_task_tests",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<byte[]>(
                name: "input",
                table: "pro_task_tests",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
