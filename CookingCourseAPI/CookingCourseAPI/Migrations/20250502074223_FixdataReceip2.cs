using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CookingCourseAPI.Migrations
{
    /// <inheritdoc />
    public partial class FixdataReceip2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CourseVideoId1",
                table: "Recipes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_CourseVideoId1",
                table: "Recipes",
                column: "CourseVideoId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_CourseVideos_CourseVideoId1",
                table: "Recipes",
                column: "CourseVideoId1",
                principalTable: "CourseVideos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_CourseVideos_CourseVideoId1",
                table: "Recipes");

            migrationBuilder.DropIndex(
                name: "IX_Recipes_CourseVideoId1",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "CourseVideoId1",
                table: "Recipes");
        }
    }
}
