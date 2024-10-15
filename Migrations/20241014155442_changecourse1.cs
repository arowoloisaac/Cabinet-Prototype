using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cabinet_Prototype.Migrations
{
    /// <inheritdoc />
    public partial class changecourse1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Directions_DirectionId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_DirectionId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "DirectionId",
                table: "Courses");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DirectionId",
                table: "Courses",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Courses_DirectionId",
                table: "Courses",
                column: "DirectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Directions_DirectionId",
                table: "Courses",
                column: "DirectionId",
                principalTable: "Directions",
                principalColumn: "Id");
        }
    }
}
