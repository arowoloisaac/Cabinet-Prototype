using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cabinet_Prototype.Migrations
{
    /// <inheritdoc />
    public partial class AddCourseSemester3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_CourseTeachers_CourseTeacherId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CourseTeacherId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CourseTeacherId",
                table: "AspNetUsers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CourseTeacherId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CourseTeacherId",
                table: "AspNetUsers",
                column: "CourseTeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_CourseTeachers_CourseTeacherId",
                table: "AspNetUsers",
                column: "CourseTeacherId",
                principalTable: "CourseTeachers",
                principalColumn: "Id");
        }
    }
}
