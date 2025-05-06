using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CookingCourseAPI.Migrations
{
    /// <inheritdoc />
    public partial class FixdataReceip : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_Courses_CourseId",
                table: "Recipes");

            migrationBuilder.AlterColumn<int>(
                name: "CourseId",
                table: "Recipes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CourseVideoId",
                table: "Recipes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_CourseVideoId",
                table: "Recipes",
                column: "CourseVideoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_CourseVideos_CourseVideoId",
                table: "Recipes",
                column: "CourseVideoId",
                principalTable: "CourseVideos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_Courses_CourseId",
                table: "Recipes",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_CourseVideos_CourseVideoId",
                table: "Recipes");

            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_Courses_CourseId",
                table: "Recipes");

            migrationBuilder.DropIndex(
                name: "IX_Recipes_CourseVideoId",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "CourseVideoId",
                table: "Recipes");

            migrationBuilder.AlterColumn<int>(
                name: "CourseId",
                table: "Recipes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_Courses_CourseId",
                table: "Recipes",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
