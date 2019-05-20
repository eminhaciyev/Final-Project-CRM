using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Migrations
{
    public partial class RemoveRequiredDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Date",
                table: "Blogs",
                nullable: true,
                oldClrType: typeof(string));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Date",
                table: "Blogs",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
