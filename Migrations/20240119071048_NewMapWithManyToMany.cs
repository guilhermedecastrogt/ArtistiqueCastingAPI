using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArtistiqueCastingAPI.Migrations
{
    /// <inheritdoc />
    public partial class NewMapWithManyToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Casting_SubCategory_SubCategorySlug",
                table: "Casting");

            migrationBuilder.DropForeignKey(
                name: "FK_SubCategory_Category_CategorySlug",
                table: "SubCategory");

            migrationBuilder.DropIndex(
                name: "IX_SubCategory_CategorySlug",
                table: "SubCategory");

            migrationBuilder.DropIndex(
                name: "IX_Casting_SubCategorySlug",
                table: "Casting");

            migrationBuilder.DropColumn(
                name: "CategorySlug",
                table: "SubCategory");

            migrationBuilder.AlterColumn<string>(
                name: "SubCategorySlug",
                table: "Casting",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "CastingSubCategory",
                columns: table => new
                {
                    CastingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubCategorySlug = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CastingSubCategory", x => new { x.CastingId, x.SubCategorySlug });
                    table.ForeignKey(
                        name: "FK_CastingSubCategory_Casting_CastingId",
                        column: x => x.CastingId,
                        principalTable: "Casting",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CastingSubCategory_SubCategory_SubCategorySlug",
                        column: x => x.SubCategorySlug,
                        principalTable: "SubCategory",
                        principalColumn: "Slug",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubCategoryCategory",
                columns: table => new
                {
                    CategorySlug = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SubCategorySlug = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCategoryCategory", x => new { x.CategorySlug, x.SubCategorySlug });
                    table.ForeignKey(
                        name: "FK_SubCategoryCategory_Category_CategorySlug",
                        column: x => x.CategorySlug,
                        principalTable: "Category",
                        principalColumn: "Slug",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubCategoryCategory_SubCategory_SubCategorySlug",
                        column: x => x.SubCategorySlug,
                        principalTable: "SubCategory",
                        principalColumn: "Slug",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CastingSubCategory_SubCategorySlug",
                table: "CastingSubCategory",
                column: "SubCategorySlug");

            migrationBuilder.CreateIndex(
                name: "IX_SubCategoryCategory_SubCategorySlug",
                table: "SubCategoryCategory",
                column: "SubCategorySlug");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CastingSubCategory");

            migrationBuilder.DropTable(
                name: "SubCategoryCategory");

            migrationBuilder.AddColumn<string>(
                name: "CategorySlug",
                table: "SubCategory",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "SubCategorySlug",
                table: "Casting",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubCategory_CategorySlug",
                table: "SubCategory",
                column: "CategorySlug");

            migrationBuilder.CreateIndex(
                name: "IX_Casting_SubCategorySlug",
                table: "Casting",
                column: "SubCategorySlug");

            migrationBuilder.AddForeignKey(
                name: "FK_Casting_SubCategory_SubCategorySlug",
                table: "Casting",
                column: "SubCategorySlug",
                principalTable: "SubCategory",
                principalColumn: "Slug");

            migrationBuilder.AddForeignKey(
                name: "FK_SubCategory_Category_CategorySlug",
                table: "SubCategory",
                column: "CategorySlug",
                principalTable: "Category",
                principalColumn: "Slug",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
