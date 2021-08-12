using Microsoft.EntityFrameworkCore.Migrations;

namespace RentHome.Data.Migrations
{
    public partial class IsPublicFieldToThePropertyTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Test",
                table: "Properties");

            migrationBuilder.AddColumn<bool>(
                name: "IsPublic",
                table: "Properties",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPublic",
                table: "Properties");

            migrationBuilder.AddColumn<int>(
                name: "Test",
                table: "Properties",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
