using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SurveySystem.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixQuestions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Surveys_CreatedBy",
                table: "Surveys",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Surveys_LastModifiedBy",
                table: "Surveys",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyQuestions_QuestionId",
                table: "SurveyQuestions",
                column: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_SurveyQuestions_Questions_QuestionId",
                table: "SurveyQuestions",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Surveys_Users_CreatedBy",
                table: "Surveys",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Surveys_Users_LastModifiedBy",
                table: "Surveys",
                column: "LastModifiedBy",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SurveyQuestions_Questions_QuestionId",
                table: "SurveyQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_Surveys_Users_CreatedBy",
                table: "Surveys");

            migrationBuilder.DropForeignKey(
                name: "FK_Surveys_Users_LastModifiedBy",
                table: "Surveys");

            migrationBuilder.DropIndex(
                name: "IX_Surveys_CreatedBy",
                table: "Surveys");

            migrationBuilder.DropIndex(
                name: "IX_Surveys_LastModifiedBy",
                table: "Surveys");

            migrationBuilder.DropIndex(
                name: "IX_SurveyQuestions_QuestionId",
                table: "SurveyQuestions");
        }
    }
}
