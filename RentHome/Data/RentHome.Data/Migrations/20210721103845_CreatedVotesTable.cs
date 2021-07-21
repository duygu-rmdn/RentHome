namespace RentHome.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class CreatedVotesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Votes_AspNetUsers_Userid",
                table: "Votes");

            migrationBuilder.RenameColumn(
                name: "Userid",
                table: "Votes",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Votes_Userid",
                table: "Votes",
                newName: "IX_Votes_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Votes_AspNetUsers_UserId",
                table: "Votes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Votes_AspNetUsers_UserId",
                table: "Votes");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Votes",
                newName: "Userid");

            migrationBuilder.RenameIndex(
                name: "IX_Votes_UserId",
                table: "Votes",
                newName: "IX_Votes_Userid");

            migrationBuilder.AddForeignKey(
                name: "FK_Votes_AspNetUsers_Userid",
                table: "Votes",
                column: "Userid",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
