using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArtistiqueCastingAPI.Migrations
{
    /// <inheritdoc />
    public partial class NullableCastingModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Casting_Category_CategorySlug",
                table: "Casting");

            migrationBuilder.AlterColumn<string>(
                name: "CategorySlug",
                table: "Casting",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Casting_Category_CategorySlug",
                table: "Casting",
                column: "CategorySlug",
                principalTable: "Category",
                principalColumn: "Slug");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Casting_Category_CategorySlug",
                table: "Casting");

            migrationBuilder.AlterColumn<string>(
                name: "CategorySlug",
                table: "Casting",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Casting_Category_CategorySlug",
                table: "Casting",
                column: "CategorySlug",
                principalTable: "Category",
                principalColumn: "Slug",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
