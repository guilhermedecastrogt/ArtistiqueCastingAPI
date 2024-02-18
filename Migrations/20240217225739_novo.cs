using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArtistiqueCastingAPI.Migrations
{
    /// <inheritdoc />
    public partial class novo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IconFontAlwesome",
                table: "Category",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IconFontAlwesome",
                table: "Category");
        }
    }
}
