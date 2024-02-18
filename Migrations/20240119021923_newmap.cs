using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArtistiqueCastingAPI.Migrations
{
    /// <inheritdoc />
    public partial class newmap : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Casting_Category_CategorySlug",
                table: "Casting");

            migrationBuilder.RenameColumn(
                name: "CategorySlug",
                table: "Casting",
                newName: "SubCategorySlug");

            migrationBuilder.RenameIndex(
                name: "IX_Casting_CategorySlug",
                table: "Casting",
                newName: "IX_Casting_SubCategorySlug");

            migrationBuilder.AddForeignKey(
                name: "FK_Casting_SubCategory_SubCategorySlug",
                table: "Casting",
                column: "SubCategorySlug",
                principalTable: "SubCategory",
                principalColumn: "Slug");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Casting_SubCategory_SubCategorySlug",
                table: "Casting");

            migrationBuilder.RenameColumn(
                name: "SubCategorySlug",
                table: "Casting",
                newName: "CategorySlug");

            migrationBuilder.RenameIndex(
                name: "IX_Casting_SubCategorySlug",
                table: "Casting",
                newName: "IX_Casting_CategorySlug");

            migrationBuilder.AddForeignKey(
                name: "FK_Casting_Category_CategorySlug",
                table: "Casting",
                column: "CategorySlug",
                principalTable: "Category",
                principalColumn: "Slug");
        }
    }
}
