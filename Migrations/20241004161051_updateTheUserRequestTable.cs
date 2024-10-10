using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cabinet_Prototype.Migrations
{
    /// <inheritdoc />
    public partial class updateTheUserRequestTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StudentDirection",
                table: "UserRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "StudentFaculty",
                table: "UserRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "StudentGrade",
                table: "UserRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StudentGroup",
                table: "UserRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StudentDirection",
                table: "UserRequests");

            migrationBuilder.DropColumn(
                name: "StudentFaculty",
                table: "UserRequests");

            migrationBuilder.DropColumn(
                name: "StudentGrade",
                table: "UserRequests");

            migrationBuilder.DropColumn(
                name: "StudentGroup",
                table: "UserRequests");
        }
    }
}
