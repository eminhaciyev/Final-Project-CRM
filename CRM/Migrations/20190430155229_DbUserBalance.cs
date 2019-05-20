using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Migrations
{
    public partial class DbUserBalance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserBalance_AspNetUsers_UserId",
                table: "UserBalance");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserBalance",
                table: "UserBalance");

            migrationBuilder.RenameTable(
                name: "UserBalance",
                newName: "UserBalances");

            migrationBuilder.RenameIndex(
                name: "IX_UserBalance_UserId",
                table: "UserBalances",
                newName: "IX_UserBalances_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserBalances",
                table: "UserBalances",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserBalances_AspNetUsers_UserId",
                table: "UserBalances",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserBalances_AspNetUsers_UserId",
                table: "UserBalances");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserBalances",
                table: "UserBalances");

            migrationBuilder.RenameTable(
                name: "UserBalances",
                newName: "UserBalance");

            migrationBuilder.RenameIndex(
                name: "IX_UserBalances_UserId",
                table: "UserBalance",
                newName: "IX_UserBalance_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserBalance",
                table: "UserBalance",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserBalance_AspNetUsers_UserId",
                table: "UserBalance",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
