using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Migrations
{
    public partial class ChangeSliderClassLang : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title_Ru",
                table: "Sliders",
                newName: "TitleRu");

            migrationBuilder.RenameColumn(
                name: "Title_Az",
                table: "Sliders",
                newName: "TitleAz");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TitleRu",
                table: "Sliders",
                newName: "Title_Ru");

            migrationBuilder.RenameColumn(
                name: "TitleAz",
                table: "Sliders",
                newName: "Title_Az");
        }
    }
}
