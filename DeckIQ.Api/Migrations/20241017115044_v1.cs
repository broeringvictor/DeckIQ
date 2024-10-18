using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeckIQ.Api.Migrations
{
    /// <inheritdoc />
    public partial class v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "category",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "NVARCHAR(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "NVARCHAR(300)", maxLength: 300, nullable: true),
                    UserId = table.Column<string>(type: "NVARCHAR(160)", maxLength: 160, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FlashCard",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Question = table.Column<string>(type: "NVARCHAR(300)", maxLength: 300, nullable: false),
                    Answer = table.Column<string>(type: "NVARCHAR(300)", maxLength: 300, nullable: false),
                    IncorrectAnswerA = table.Column<string>(type: "NVARCHAR(300)", maxLength: 300, nullable: true),
                    IncorrectAnswerB = table.Column<string>(type: "NVARCHAR(300)", maxLength: 300, nullable: true),
                    IncorrectAnswerC = table.Column<string>(type: "NVARCHAR(300)", maxLength: 300, nullable: true),
                    IncorrectAnswerD = table.Column<string>(type: "NVARCHAR(300)", maxLength: 300, nullable: true),
                    CardImage = table.Column<string>(type: "NVARCHAR(300)", maxLength: 300, nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CategoryId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<string>(type: "VARCHAR(160)", maxLength: 160, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlashCard", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FlashCard_category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FlashCard_CategoryId",
                table: "FlashCard",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FlashCard");

            migrationBuilder.DropTable(
                name: "category");
        }
    }
}
