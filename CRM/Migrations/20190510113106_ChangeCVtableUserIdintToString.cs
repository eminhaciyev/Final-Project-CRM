using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Migrations
{
    public partial class ChangeCVtableUserIdintToString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CVs_AspNetUsers_UserId1",
                table: "CVs");

            migrationBuilder.DropIndex(
                name: "IX_CVs_UserId1",
                table: "CVs");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "CVs");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "CVs",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_CVs_UserId",
                table: "CVs",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CVs_AspNetUsers_UserId",
                table: "CVs",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CVs_AspNetUsers_UserId",
                table: "CVs");

            migrationBuilder.DropIndex(
                name: "IX_CVs_UserId",
                table: "CVs");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "CVs",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "CVs",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CVs_UserId1",
                table: "CVs",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_CVs_AspNetUsers_UserId1",
                table: "CVs",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
