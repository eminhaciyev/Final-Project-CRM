using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Migrations
{
    public partial class ChangeComplaintUserIdIntToString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Complaints_AspNetUsers_UserId1",
                table: "Complaints");

            migrationBuilder.DropIndex(
                name: "IX_Complaints_UserId1",
                table: "Complaints");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Complaints");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Complaints",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_Complaints_UserId",
                table: "Complaints",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Complaints_AspNetUsers_UserId",
                table: "Complaints",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Complaints_AspNetUsers_UserId",
                table: "Complaints");

            migrationBuilder.DropIndex(
                name: "IX_Complaints_UserId",
                table: "Complaints");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Complaints",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "Complaints",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Complaints_UserId1",
                table: "Complaints",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Complaints_AspNetUsers_UserId1",
                table: "Complaints",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
