using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cabinet_Prototype.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedReesultToTheDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Result_AspNetUsers_UserId",
                table: "Result");

            migrationBuilder.DropForeignKey(
                name: "FK_Result_Courses_CourseId",
                table: "Result");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Result",
                table: "Result");

            migrationBuilder.RenameTable(
                name: "Result",
                newName: "Results");

            migrationBuilder.RenameIndex(
                name: "IX_Result_UserId",
                table: "Results",
                newName: "IX_Results_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Result_CourseId",
                table: "Results",
                newName: "IX_Results_CourseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Results",
                table: "Results",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Results_AspNetUsers_UserId",
                table: "Results",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Results_Courses_CourseId",
                table: "Results",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Results_AspNetUsers_UserId",
                table: "Results");

            migrationBuilder.DropForeignKey(
                name: "FK_Results_Courses_CourseId",
                table: "Results");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Results",
                table: "Results");

            migrationBuilder.RenameTable(
                name: "Results",
                newName: "Result");

            migrationBuilder.RenameIndex(
                name: "IX_Results_UserId",
                table: "Result",
                newName: "IX_Result_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Results_CourseId",
                table: "Result",
                newName: "IX_Result_CourseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Result",
                table: "Result",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Result_AspNetUsers_UserId",
                table: "Result",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Result_Courses_CourseId",
                table: "Result",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
