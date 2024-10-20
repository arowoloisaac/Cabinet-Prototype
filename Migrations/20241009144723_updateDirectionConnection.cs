using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cabinet_Prototype.Migrations
{
    /// <inheritdoc />
    public partial class updateDirectionConnection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "StudentDirectionId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_StudentDirectionId",
                table: "AspNetUsers",
                column: "StudentDirectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Directions_StudentDirectionId",
                table: "AspNetUsers",
                column: "StudentDirectionId",
                principalTable: "Directions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Directions_StudentDirectionId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_StudentDirectionId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "StudentDirectionId",
                table: "AspNetUsers");
        }
    }
}
