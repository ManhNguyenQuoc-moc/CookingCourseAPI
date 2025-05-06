using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CookingCourseAPI.Migrations
{
    /// <inheritdoc />
    public partial class fixdata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Recipes_CourseVideoId1",
                table: "Recipes");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_CourseVideoId1",
                table: "Recipes",
                column: "CourseVideoId1",
                unique: true,
                filter: "[CourseVideoId1] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Recipes_CourseVideoId1",
                table: "Recipes");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_CourseVideoId1",
                table: "Recipes",
                column: "CourseVideoId1");
        }
    }
}
