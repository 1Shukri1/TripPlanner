using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExcursionOrganizer.Data.Migrations
{
    public partial class excursionOrganizer3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExcursionUser_AspNetUsers_UserId",
                table: "ExcursionUser");

            migrationBuilder.DropForeignKey(
                name: "FK_ExcursionUser_Excursions_ExcursionId",
                table: "ExcursionUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExcursionUser",
                table: "ExcursionUser");

            migrationBuilder.RenameTable(
                name: "ExcursionUser",
                newName: "ExcursionUsers");

            migrationBuilder.RenameIndex(
                name: "IX_ExcursionUser_UserId",
                table: "ExcursionUsers",
                newName: "IX_ExcursionUsers_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ExcursionUser_ExcursionId",
                table: "ExcursionUsers",
                newName: "IX_ExcursionUsers_ExcursionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExcursionUsers",
                table: "ExcursionUsers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ExcursionUsers_AspNetUsers_UserId",
                table: "ExcursionUsers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExcursionUsers_Excursions_ExcursionId",
                table: "ExcursionUsers",
                column: "ExcursionId",
                principalTable: "Excursions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExcursionUsers_AspNetUsers_UserId",
                table: "ExcursionUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_ExcursionUsers_Excursions_ExcursionId",
                table: "ExcursionUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExcursionUsers",
                table: "ExcursionUsers");

            migrationBuilder.RenameTable(
                name: "ExcursionUsers",
                newName: "ExcursionUser");

            migrationBuilder.RenameIndex(
                name: "IX_ExcursionUsers_UserId",
                table: "ExcursionUser",
                newName: "IX_ExcursionUser_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ExcursionUsers_ExcursionId",
                table: "ExcursionUser",
                newName: "IX_ExcursionUser_ExcursionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExcursionUser",
                table: "ExcursionUser",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ExcursionUser_AspNetUsers_UserId",
                table: "ExcursionUser",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExcursionUser_Excursions_ExcursionId",
                table: "ExcursionUser",
                column: "ExcursionId",
                principalTable: "Excursions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
