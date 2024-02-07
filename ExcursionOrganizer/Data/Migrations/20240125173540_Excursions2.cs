using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExcursionOrganizer.Data.Migrations
{
    public partial class Excursions2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Excursions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Excursions");
        }
    }
}
