using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseWork.Migrations.Application
{
    public partial class UpdateUserAnswer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnswerScore",
                table: "UserAnswers");

            migrationBuilder.DropColumn(
                name: "QuestionText",
                table: "UserAnswers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AnswerScore",
                table: "UserAnswers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "QuestionText",
                table: "UserAnswers",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
