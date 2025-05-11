using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CookingCourseAPI.Migrations
{
    /// <inheritdoc />
    public partial class Updatedatabase29 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompletedLessonIds",
                table: "CourseProgresses");

            migrationBuilder.DropColumn(
                name: "CurrentLessonId",
                table: "CourseProgresses");

            migrationBuilder.DropColumn(
                name: "CurrentLessonTitle",
                table: "CourseProgresses");

            migrationBuilder.CreateTable(
                name: "CompletedCourseVideos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseVideoId = table.Column<int>(type: "int", nullable: false),
                    CourseProgressId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompletedCourseVideos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompletedCourseVideos_CourseProgresses_CourseProgressId",
                        column: x => x.CourseProgressId,
                        principalTable: "CourseProgresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompletedCourseVideos_CourseProgressId",
                table: "CompletedCourseVideos",
                column: "CourseProgressId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompletedCourseVideos");

            migrationBuilder.AddColumn<string>(
                name: "CompletedLessonIds",
                table: "CourseProgresses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CurrentLessonId",
                table: "CourseProgresses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CurrentLessonTitle",
                table: "CourseProgresses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
