using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CookingCourseAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedatabaseRepices24 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_LearningPaths_LearningPathId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_LearningPathId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "LearningPathId",
                table: "Courses");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "LearningPaths",
                newName: "Title");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "LearningPaths",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "learningPathCourses",
                columns: table => new
                {
                    LearningPathId = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_learningPathCourses", x => new { x.LearningPathId, x.CourseId });
                    table.ForeignKey(
                        name: "FK_learningPathCourses_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_learningPathCourses_LearningPaths_LearningPathId",
                        column: x => x.LearningPathId,
                        principalTable: "LearningPaths",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_learningPathCourses_CourseId",
                table: "learningPathCourses",
                column: "CourseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "learningPathCourses");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "LearningPaths");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "LearningPaths",
                newName: "Name");

            migrationBuilder.AddColumn<int>(
                name: "LearningPathId",
                table: "Courses",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Courses_LearningPathId",
                table: "Courses",
                column: "LearningPathId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_LearningPaths_LearningPathId",
                table: "Courses",
                column: "LearningPathId",
                principalTable: "LearningPaths",
                principalColumn: "Id");
        }
    }
}
