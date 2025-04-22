using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CookingCourseAPI.Migrations
{
    /// <inheritdoc />
    public partial class updatedata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Blogs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Likes",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Blogs",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReportedAt",
                table: "BlogReports",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "BlogReports",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_BlogReports_UserId",
                table: "BlogReports",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogReports_Users_UserId",
                table: "BlogReports",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogReports_Users_UserId",
                table: "BlogReports");

            migrationBuilder.DropIndex(
                name: "IX_BlogReports_UserId",
                table: "BlogReports");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "Likes",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "ReportedAt",
                table: "BlogReports");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "BlogReports");
        }
    }
}
