using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JFjewelery.Migrations
{
    /// <inheritdoc />
    public partial class StoneColor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StoneColor",
                table: "Characteristics",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Characteristics",
                keyColumn: "Id",
                keyValue: 1,
                column: "StoneColor",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StoneColor",
                table: "Characteristics");
        }
    }
}
