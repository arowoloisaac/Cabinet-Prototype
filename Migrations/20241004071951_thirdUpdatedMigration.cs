using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cabinet_Prototype.Migrations
{
    /// <inheritdoc />
    public partial class thirdUpdatedMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "StudentFacultyIdId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_StudentFacultyIdId",
                table: "AspNetUsers",
                column: "StudentFacultyIdId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Faculties_StudentFacultyIdId",
                table: "AspNetUsers",
                column: "StudentFacultyIdId",
                principalTable: "Faculties",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Faculties_StudentFacultyIdId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_StudentFacultyIdId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "StudentFacultyIdId",
                table: "AspNetUsers");
        }
    }
}
