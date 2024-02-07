﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExcursionOrganizer.Data.Migrations
{
    public partial class exc2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Excursions_AspNetUsers_UserId",
                table: "Excursions");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Excursions",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Excursions_AspNetUsers_UserId",
                table: "Excursions",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Excursions_AspNetUsers_UserId",
                table: "Excursions");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Excursions",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Excursions_AspNetUsers_UserId",
                table: "Excursions",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
