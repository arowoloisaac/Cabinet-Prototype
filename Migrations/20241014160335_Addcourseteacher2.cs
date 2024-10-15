using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cabinet_Prototype.Migrations
{
    /// <inheritdoc />
    public partial class Addcourseteacher2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseTeachers_AspNetUsers_UserId",
                table: "CourseTeachers");

            migrationBuilder.DropIndex(
                name: "IX_CourseTeachers_UserId",
                table: "CourseTeachers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "CourseTeachers");

            migrationBuilder.CreateIndex(
                name: "IX_CourseTeachers_CourseId",
                table: "CourseTeachers",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseTeachers_TeacherId",
                table: "CourseTeachers",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseTeachers_AspNetUsers_TeacherId",
                table: "CourseTeachers",
                column: "TeacherId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseTeachers_Courses_CourseId",
                table: "CourseTeachers",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseTeachers_AspNetUsers_TeacherId",
                table: "CourseTeachers");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseTeachers_Courses_CourseId",
                table: "CourseTeachers");

            migrationBuilder.DropIndex(
                name: "IX_CourseTeachers_CourseId",
                table: "CourseTeachers");

            migrationBuilder.DropIndex(
                name: "IX_CourseTeachers_TeacherId",
                table: "CourseTeachers");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "CourseTeachers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CourseTeachers_UserId",
                table: "CourseTeachers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseTeachers_AspNetUsers_UserId",
                table: "CourseTeachers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
