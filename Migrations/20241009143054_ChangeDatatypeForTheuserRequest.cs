using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cabinet_Prototype.Migrations
{
    /// <inheritdoc />
    public partial class ChangeDatatypeForTheuserRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StudentGroup",
                table: "UserRequests");

            migrationBuilder.AddColumn<decimal>(
                name: "StudentGroupNumber",
                table: "UserRequests",
                type: "decimal(20,0)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<decimal>(
                name: "GroupNumber",
                table: "Groups",
                type: "decimal(20,0)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StudentGroupNumber",
                table: "UserRequests");

            migrationBuilder.AddColumn<string>(
                name: "StudentGroup",
                table: "UserRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "GroupNumber",
                table: "Groups",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,0)");
        }
    }
}
