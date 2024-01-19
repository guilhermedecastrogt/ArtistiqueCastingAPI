using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArtistiqueCastingAPI.Migrations
{
    /// <inheritdoc />
    public partial class CategorySlugNull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubCategory_Category_CategorySlug",
                table: "SubCategory");

            migrationBuilder.AlterColumn<string>(
                name: "CategorySlug",
                table: "SubCategory",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_SubCategory_Category_CategorySlug",
                table: "SubCategory",
                column: "CategorySlug",
                principalTable: "Category",
                principalColumn: "Slug");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubCategory_Category_CategorySlug",
                table: "SubCategory");

            migrationBuilder.AlterColumn<string>(
                name: "CategorySlug",
                table: "SubCategory",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

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
