using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Migrations
{
    public partial class ChangeWhyChooseUsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description_Az",
                table: "WhyChooseUs",
                maxLength: 600,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description_Ru",
                table: "WhyChooseUs",
                maxLength: 600,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title_Az",
                table: "WhyChooseUs",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title_Ru",
                table: "WhyChooseUs",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description_Az",
                table: "WhyChooseUs");

            migrationBuilder.DropColumn(
                name: "Description_Ru",
                table: "WhyChooseUs");

            migrationBuilder.DropColumn(
                name: "Title_Az",
                table: "WhyChooseUs");

            migrationBuilder.DropColumn(
                name: "Title_Ru",
                table: "WhyChooseUs");
        }
    }
}
