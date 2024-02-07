using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExcursionOrganizer.Data.Migrations
{
    public partial class excursionOrganizer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Transport",
                table: "Excursions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Transport",
                table: "Excursions");
        }
    }
}
