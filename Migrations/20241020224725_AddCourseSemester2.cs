using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cabinet_Prototype.Migrations
{
    /// <inheritdoc />
    public partial class AddCourseSemester2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseTeachers_AspNetUsers_TeacherId",
                table: "CourseTeachers");

            migrationBuilder.AlterColumn<int>(
                name: "Semester",
                table: "Courses",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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

            migrationBuilder.AddForeignKey(
                name: "FK_CourseTeachers_AspNetUsers_TeacherId",
                table: "CourseTeachers",
                column: "TeacherId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_CourseTeachers_CourseTeacherId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseTeachers_AspNetUsers_TeacherId",
                table: "CourseTeachers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CourseTeacherId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CourseTeacherId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "Semester",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseTeachers_AspNetUsers_TeacherId",
                table: "CourseTeachers",
                column: "TeacherId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
