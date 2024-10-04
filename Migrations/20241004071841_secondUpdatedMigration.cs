using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cabinet_Prototype.Migrations
{
    /// <inheritdoc />
    public partial class secondUpdatedMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "StudentGroupIdId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_StudentGroupIdId",
                table: "AspNetUsers",
                column: "StudentGroupIdId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Groups_StudentGroupIdId",
                table: "AspNetUsers",
                column: "StudentGroupIdId",
                principalTable: "Groups",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Groups_StudentGroupIdId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_StudentGroupIdId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "StudentGroupIdId",
                table: "AspNetUsers");
        }
    }
}
