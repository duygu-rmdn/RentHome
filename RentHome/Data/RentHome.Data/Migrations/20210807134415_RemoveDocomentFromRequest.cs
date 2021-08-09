namespace RentHome.Data.Migrations
{
    using System;

    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class RemoveDocomentFromRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Document",
                table: "Requests");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Document",
                table: "Requests",
                type: "varbinary(max)",
                maxLength: 2097152,
                nullable: false,
                defaultValue: new byte[0]);
        }
    }
}
