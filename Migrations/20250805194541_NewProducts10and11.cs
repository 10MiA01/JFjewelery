using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace JFjewelery.Migrations
{
    /// <inheritdoc />
    public partial class NewProducts10and11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Name", "Price", "Quantity" },
                values: new object[,]
                {
                    { 9, 3, "Fox tail", 2000, 10 },
                    { 10, 3, "Queen", 2500, 10 }
                });

            migrationBuilder.InsertData(
                table: "Characteristics",
                columns: new[] { "Id", "Description", "Gender", "Manufacturer", "ProductId", "Style" },
                values: new object[,]
                {
                    { 9, null, "Women", "JFjewelery", 9, "Romantic" },
                    { 10, null, "Both", "JFjewelery", 10, "Vintage" }
                });

            migrationBuilder.InsertData(
                table: "ProductImages",
                columns: new[] { "Id", "FileName", "ProductId" },
                values: new object[,]
                {
                    { 9, "prod_9_front_1.jpg", 9 },
                    { 10, "prod_10_front_1.jpg", 10 }
                });

            migrationBuilder.InsertData(
                table: "CharacteristicMetals",
                columns: new[] { "Id", "CharacteristicId", "ColorId", "MetalId", "Purity", "ShapeId", "SizeId", "TypeId", "Weight" },
                values: new object[,]
                {
                    { 9, 9, 2, 1, 585, null, 2, 1, 15f },
                    { 10, 10, 2, 1, 585, null, 2, 1, 20f }
                });

            migrationBuilder.InsertData(
                table: "CharacteristicStones",
                columns: new[] { "Id", "CharacteristicId", "ColorId", "Count", "ShapeId", "SizeId", "StoneId", "TypeId" },
                values: new object[] { 15, 10, 14, 1, 12, 5, 7, 9 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CharacteristicMetals",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "CharacteristicMetals",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "CharacteristicStones",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Characteristics",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Characteristics",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10);
        }
    }
}
