using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cabinet_Prototype.Migrations
{
    /// <inheritdoc />
    public partial class updateTheUserRequestTableByRemovingGroupId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRequests_Groups_GroupId",
                table: "UserRequests");

            migrationBuilder.DropIndex(
                name: "IX_UserRequests_GroupId",
                table: "UserRequests");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "UserRequests");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "GroupId",
                table: "UserRequests",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_UserRequests_GroupId",
                table: "UserRequests",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRequests_Groups_GroupId",
                table: "UserRequests",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
