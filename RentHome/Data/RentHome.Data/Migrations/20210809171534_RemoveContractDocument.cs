namespace RentHome.Data.Migrations
{
    using System;

    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class RemoveContractDocument : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContractDocument",
                table: "Contracts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "ContractDocument",
                table: "Contracts",
                type: "varbinary(max)",
                maxLength: 4194304,
                nullable: false,
                defaultValue: new byte[0]);
        }
    }
}
