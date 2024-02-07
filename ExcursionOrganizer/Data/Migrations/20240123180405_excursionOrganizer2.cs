using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExcursionOrganizer.Data.Migrations
{
    public partial class excursionOrganizer2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Excursions_AspNetUsers_UserId",
                table: "Excursions");

            migrationBuilder.DropIndex(
                name: "IX_Excursions_UserId",
                table: "Excursions");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Excursions");

            migrationBuilder.CreateTable(
                name: "ExcursionUser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExcursionId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExcursionUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExcursionUser_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExcursionUser_Excursions_ExcursionId",
                        column: x => x.ExcursionId,
                        principalTable: "Excursions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExcursionUser_ExcursionId",
                table: "ExcursionUser",
                column: "ExcursionId");

            migrationBuilder.CreateIndex(
                name: "IX_ExcursionUser_UserId",
                table: "ExcursionUser",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExcursionUser");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Excursions",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Excursions_UserId",
                table: "Excursions",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Excursions_AspNetUsers_UserId",
                table: "Excursions",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
