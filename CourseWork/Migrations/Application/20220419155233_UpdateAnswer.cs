using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseWork.Migrations.Application
{
    public partial class UpdateAnswer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RightNumber",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "RightNumbers",
                table: "Answers");

            migrationBuilder.RenameColumn(
                name: "RightTextAnswer",
                table: "Answers",
                newName: "Response");

            migrationBuilder.AddColumn<string>(
                name: "CorrectResponse",
                table: "Answers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsCorrect",
                table: "Answers",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CorrectResponse",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "IsCorrect",
                table: "Answers");

            migrationBuilder.RenameColumn(
                name: "Response",
                table: "Answers",
                newName: "RightTextAnswer");

            migrationBuilder.AddColumn<int>(
                name: "RightNumber",
                table: "Answers",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int[]>(
                name: "RightNumbers",
                table: "Answers",
                type: "integer[]",
                nullable: true);
        }
    }
}
