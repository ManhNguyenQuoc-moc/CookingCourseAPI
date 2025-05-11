using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CookingCourseAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedatabaseRepices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CookingTips",
                table: "Recipes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Ingredients",
                table: "Recipes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CookingTips",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "Ingredients",
                table: "Recipes");
        }
    }
}
