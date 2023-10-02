using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LivroMente.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoryBook",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    IsFree = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryBook", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Book",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Author = table.Column<string>(type: "TEXT", nullable: false),
                    Synopsis = table.Column<string>(type: "TEXT", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    Pages = table.Column<int>(type: "INTEGER", nullable: false),
                    PublishingCompany = table.Column<string>(type: "TEXT", nullable: false),
                    Isbn = table.Column<string>(type: "TEXT", nullable: false),
                    Value = table.Column<double>(type: "REAL", nullable: false),
                    Language = table.Column<string>(type: "TEXT", nullable: false),
                    Classification = table.Column<int>(type: "INTEGER", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    CategoryId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Book_CategoryBook_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "CategoryBook",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Book_CategoryId",
                table: "Book",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Book");

            migrationBuilder.DropTable(
                name: "CategoryBook");
        }
    }
}
