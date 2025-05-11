using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CookingCourseAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedatabaseRepices2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Courses_CourseId",
                table: "Enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_Courses_CourseId",
                table: "Recipes");

            migrationBuilder.DropIndex(
                name: "IX_Recipes_CourseId",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "Recipes");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Courses_CourseId",
                table: "Enrollments",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Courses_CourseId",
                table: "Enrollments");

            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "Recipes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_CourseId",
                table: "Recipes",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Courses_CourseId",
                table: "Enrollments",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_Courses_CourseId",
                table: "Recipes",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id");
        }
    }
}
