using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace JFjewelery.Migrations
{
    /// <inheritdoc />
    public partial class SeedingNewProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Characteristics_ProductId",
                table: "Characteristics");

            migrationBuilder.UpdateData(
                table: "CharacteristicMetals",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ColorId", "Weight" },
                values: new object[] { 2, 25f });

            migrationBuilder.UpdateData(
                table: "CharacteristicStones",
                keyColumn: "Id",
                keyValue: 2,
                column: "SizeId",
                value: 4);

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Name", "Price", "Quantity" },
                values: new object[,]
                {
                    { 2, 2, "Stained glass", 1000, 10 },
                    { 3, 2, "Pink treasure", 4000, 10 },
                    { 4, 4, "Lara's wish", 1500, 10 },
                    { 5, 2, "Sweet bird", 2000, 10 },
                    { 6, 2, "Marine rope", 1000, 10 },
                    { 7, 1, "Sea tear", 2000, 10 },
                    { 8, 1, "Demon's eye", 3000, 10 }
                });

            migrationBuilder.InsertData(
                table: "Shape",
                columns: new[] { "Id", "AppliesToEntityId", "Name" },
                values: new object[] { 13, 2, "Rectangle" });

            migrationBuilder.InsertData(
                table: "Stone",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 8, "Pearl" },
                    { 9, "obsydian" }
                });

            migrationBuilder.InsertData(
                table: "Characteristics",
                columns: new[] { "Id", "Description", "Gender", "Manufacturer", "ProductId", "Style" },
                values: new object[,]
                {
                    { 2, null, "Both", "JFjewelery", 2, "Vintage" },
                    { 3, null, "Women", "JFjewelery", 3, "Romantic" },
                    { 4, null, "Women", "JFjewelery", 4, "Minimalistic" },
                    { 5, null, "Women", "JFjewelery", 5, "Classic" },
                    { 6, null, "Women", "JFjewelery", 6, "Minimalism" },
                    { 7, null, "Women", "JFjewelery", 7, "Minimalism" },
                    { 8, null, "Men", "JFjewelery", 8, "Vintage" }
                });

            migrationBuilder.InsertData(
                table: "ProductImages",
                columns: new[] { "Id", "FileName", "ProductId" },
                values: new object[,]
                {
                    { 2, "prod_2_front_1.jpg", 2 },
                    { 3, "prod_3_front_1.jpg", 3 },
                    { 4, "prod_4_front_1.jpg", 4 },
                    { 5, "prod_5_front_1.jpg", 5 },
                    { 6, "prod_6_front_1.jpg", 6 },
                    { 7, "prod_7_front_1.jpg", 7 },
                    { 8, "prod_8_front_1.jpg", 8 }
                });

            migrationBuilder.InsertData(
                table: "CharacteristicMetals",
                columns: new[] { "Id", "CharacteristicId", "ColorId", "MetalId", "Purity", "ShapeId", "SizeId", "TypeId", "Weight" },
                values: new object[,]
                {
                    { 2, 2, 1, 2, 585, null, 2, 2, 10f },
                    { 3, 3, 2, 1, 585, null, 1, 1, 10f },
                    { 4, 4, 2, 1, 585, null, 2, 1, 20f },
                    { 5, 5, 2, 1, 585, 2, 3, 1, 50f },
                    { 6, 6, 2, 1, 585, null, 2, 1, 15f },
                    { 7, 7, 1, 2, 585, null, 2, 2, 10f },
                    { 8, 8, 2, 1, 585, null, 2, 1, 20f }
                });

            migrationBuilder.InsertData(
                table: "CharacteristicStones",
                columns: new[] { "Id", "CharacteristicId", "ColorId", "Count", "ShapeId", "SizeId", "StoneId", "TypeId" },
                values: new object[,]
                {
                    { 3, 2, 14, 4, 13, 4, 1, 9 },
                    { 4, 2, 7, 4, 13, 4, 2, 9 },
                    { 5, 2, 8, 4, 13, 5, 3, 9 },
                    { 6, 2, 12, 4, 13, 5, 5, 9 },
                    { 7, 3, 11, 8, 12, 4, 7, 9 },
                    { 8, 4, 14, 5, 12, 4, 1, 10 },
                    { 9, 5, 11, 2, 13, 6, 3, 9 },
                    { 10, 5, 14, 2, 13, 5, 7, 10 },
                    { 11, 7, 14, 1, 12, 5, 8, 7 },
                    { 12, 7, 14, 1, 12, 4, 1, 10 },
                    { 13, 8, 7, 1, 12, 5, 2, 9 },
                    { 14, 8, 15, 12, 12, 4, 9, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Characteristics_ProductId",
                table: "Characteristics",
                column: "ProductId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Characteristics_ProductId",
                table: "Characteristics");

            migrationBuilder.DeleteData(
                table: "CharacteristicMetals",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "CharacteristicMetals",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "CharacteristicMetals",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "CharacteristicMetals",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "CharacteristicMetals",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "CharacteristicMetals",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "CharacteristicMetals",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "CharacteristicStones",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "CharacteristicStones",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "CharacteristicStones",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "CharacteristicStones",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "CharacteristicStones",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "CharacteristicStones",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "CharacteristicStones",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "CharacteristicStones",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "CharacteristicStones",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "CharacteristicStones",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "CharacteristicStones",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "CharacteristicStones",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Characteristics",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Characteristics",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Characteristics",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Characteristics",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Characteristics",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Characteristics",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Characteristics",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Shape",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Stone",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Stone",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.UpdateData(
                table: "CharacteristicMetals",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ColorId", "Weight" },
                values: new object[] { 1, 10f });

            migrationBuilder.UpdateData(
                table: "CharacteristicStones",
                keyColumn: "Id",
                keyValue: 2,
                column: "SizeId",
                value: 5);

            migrationBuilder.CreateIndex(
                name: "IX_Characteristics_ProductId",
                table: "Characteristics",
                column: "ProductId");
        }
    }
}
