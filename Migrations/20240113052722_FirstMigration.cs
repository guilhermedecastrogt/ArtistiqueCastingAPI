using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArtistiqueCastingAPI.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Slug = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Slug);
                });

            migrationBuilder.CreateTable(
                name: "Casting",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategorySlug = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Casting", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Casting_Category_CategorySlug",
                        column: x => x.CategorySlug,
                        principalTable: "Category",
                        principalColumn: "Slug");
                });

            migrationBuilder.CreateTable(
                name: "SubCategory",
                columns: table => new
                {
                    Slug = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategorySlug = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCategory", x => x.Slug);
                    table.ForeignKey(
                        name: "FK_SubCategory_Category_CategorySlug",
                        column: x => x.CategorySlug,
                        principalTable: "Category",
                        principalColumn: "Slug",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Casting_CategorySlug",
                table: "Casting",
                column: "CategorySlug");

            migrationBuilder.CreateIndex(
                name: "IX_SubCategory_CategorySlug",
                table: "SubCategory",
                column: "CategorySlug");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Casting");

            migrationBuilder.DropTable(
                name: "SubCategory");

            migrationBuilder.DropTable(
                name: "Category");
        }
    }
}
