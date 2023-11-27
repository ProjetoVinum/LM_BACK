using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LivroMente.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UrlsDoBook : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UrlBook",
                table: "Book",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UrlImg",
                table: "Book",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UrlBook",
                table: "Book");

            migrationBuilder.DropColumn(
                name: "UrlImg",
                table: "Book");
        }
    }
}
