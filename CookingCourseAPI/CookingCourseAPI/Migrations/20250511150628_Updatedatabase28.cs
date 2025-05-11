using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CookingCourseAPI.Migrations
{
    /// <inheritdoc />
    public partial class Updatedatabase28 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CompletedLessonIds",
                table: "CourseProgresses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");

            migrationBuilder.AddColumn<string>(
                name: "CurrentLessonTitle",
                table: "CourseProgresses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompletedLessonIds",
                table: "CourseProgresses");

            migrationBuilder.DropColumn(
                name: "CurrentLessonTitle",
                table: "CourseProgresses");
        }
    }
}
