using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace JFjewelery.Migrations
{
    /// <inheritdoc />
    public partial class SeedingScenatioCustom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Scenarios",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Custom for an event");

            migrationBuilder.UpdateData(
                table: "Scenarios",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Custom characteristics");

            migrationBuilder.InsertData(
                table: "Steps",
                columns: new[] { "Id", "Name", "NextStepId", "QuestionText", "ScenarioId" },
                values: new object[] { 18, "Design Style", null, "Do you have a preferred design or motif?", 3 });

            migrationBuilder.InsertData(
                table: "Options",
                columns: new[] { "Id", "Content", "FilterJson", "Name", "StepId" },
                values: new object[,]
                {
                    { 91, "Timeless designs with elegant details.", "{\"Styles\":[\"Classic\"]}", "Classic", 18 },
                    { 92, "Clean lines and minimalist shapes.", "{\"Styles\":[\"Modern\"]}", "Modern", 18 },
                    { 93, "Inspired by antique and retro aesthetics.", "{\"Styles\":[\"Vintage\"]}", "Vintage", 18 },
                    { 94, "Motifs featuring flowers and nature.", "{\"Styles\":[\"Floral\"]}", "Floral", 18 },
                    { 95, "Bold shapes and angular patterns.", "{\"Styles\":[\"Geometric\"]}", "Geometric", 18 },
                    { 96, "I’m open to any design style.", "{}", "No preference", 18 }
                });

            migrationBuilder.InsertData(
                table: "Steps",
                columns: new[] { "Id", "Name", "NextStepId", "QuestionText", "ScenarioId" },
                values: new object[] { 17, "Stone Shape & Size", 18, "What shape and size should the stones be?", 3 });

            migrationBuilder.InsertData(
                table: "Options",
                columns: new[] { "Id", "Content", "FilterJson", "Name", "StepId" },
                values: new object[,]
                {
                    { 86, "Classic round shape, medium size for versatile use.", "{\"StoneShapes\": [\"Round\"], \"StoneSizes\": [\"Regular\"]}", "Round, Medium", 17 },
                    { 87, "Elegant oval shape with large size for statement pieces.", "{\"StoneShapes\": [\"Oval\"], \"StoneSizes\": [\"Large\"]}", "Oval, Large", 17 },
                    { 88, "Square princess cut, small size for subtle sparkle.", "{\"StoneShapes\": [\"Princess\"], \"StoneSizes\": [\"Tiny\"]}", "Princess, Small", 17 },
                    { 89, "A combination of different shapes and sizes for a unique design.", "{\"StoneShapes\": [\"Round\", \"Oval\", \"Princess\"], \"StoneSizes\": [\"Tiny\", \"Regular\", \"Large\"]}", "Mixed shapes and sizes", 17 },
                    { 90, "I’m open to any shape or size.", "{}", "No preference", 17 }
                });

            migrationBuilder.InsertData(
                table: "Steps",
                columns: new[] { "Id", "Name", "NextStepId", "QuestionText", "ScenarioId" },
                values: new object[] { 16, "Add Stones", 17, "Would you like to add any stones?", 3 });

            migrationBuilder.InsertData(
                table: "Options",
                columns: new[] { "Id", "Content", "FilterJson", "Name", "StepId" },
                values: new object[,]
                {
                    { 82, "Classic sparkle with timeless diamonds.", "{\"Stones\": [\"Diamond\"]}", "Yes, add diamonds", 16 },
                    { 83, "Add vibrant color with sapphires, rubies, or emeralds.", "{\"Stones\": [\"Sapphire\", \"Ruby\", \"Emerald\"]}", "Yes, add colored gemstones", 16 },
                    { 84, "Soft elegance with natural pearls.", "{\"Stones\": [\"Pearl\"]}", "Yes, add pearls", 16 },
                    { 85, "Keep it simple and elegant without stones.", "{}", "No stones", 16 }
                });

            migrationBuilder.InsertData(
                table: "Steps",
                columns: new[] { "Id", "Name", "NextStepId", "QuestionText", "ScenarioId" },
                values: new object[] { 15, "Metal Color", 16, "What color or finish should the metal have?", 3 });

            migrationBuilder.InsertData(
                table: "Options",
                columns: new[] { "Id", "Content", "FilterJson", "Name", "StepId" },
                values: new object[,]
                {
                    { 76, "Classic warm tone, timeless and elegant.", "{\"MetalColors\":[\"Yellow\"]}", "Yellow Gold", 15 },
                    { 77, "Modern and sleek silver-like finish.", "{\"MetalColors\":[\"White\"]}", "White Gold", 15 },
                    { 78, "Soft pinkish hue, romantic and trendy.", "{\"MetalColors\":[\"Rose\"]}", "Rose Gold", 15 },
                    { 79, "Subtle, non-reflective surface for understated elegance.", "{\"MetalFinishes\":[\"Matte\"]}", "Matte Finish", 15 },
                    { 80, "Bright and glossy finish for maximum brilliance.", "{\"MetalFinishes\":[\"Polished\"]}", "Polished Shine", 15 },
                    { 81, "Any metal color or finish is fine.", "{}", "No preference", 15 }
                });

            migrationBuilder.InsertData(
                table: "Steps",
                columns: new[] { "Id", "Name", "NextStepId", "QuestionText", "ScenarioId" },
                values: new object[] { 14, "Base Metal", 15, "What metal do you prefer for the base?", 3 });

            migrationBuilder.InsertData(
                table: "Options",
                columns: new[] { "Id", "Content", "FilterJson", "Name", "StepId" },
                values: new object[,]
                {
                    { 71, "Warm and classic, ideal for timeless pieces.", "{\"Metals\": [\"Gold\"]}", "Gold", 14 },
                    { 72, "Bright and elegant, perfect for versatile looks.", "{\"Metals\": [\"Silver\"]}", "Silver", 14 },
                    { 73, "Rare and durable, a symbol of lasting value.", "{\"Metals\": [\"Platinum\"]}", "Platinum", 14 },
                    { 74, "Romantic and modern with a warm tone.", "{\"MetalColors\": [\"Rose\"]}", "Rose Gold", 14 },
                    { 75, "Unique and bold, ideal for statement designs.", "{\"MetalColors\": [\"Oxidized\", \"Dark\"]}", "Oxidized / Dark Metal", 14 }
                });

            migrationBuilder.InsertData(
                table: "Steps",
                columns: new[] { "Id", "Name", "NextStepId", "QuestionText", "ScenarioId" },
                values: new object[] { 13, "Jewelry Type", 14, "What type of jewelry would you like to design?", 3 });

            migrationBuilder.InsertData(
                table: "Options",
                columns: new[] { "Id", "Content", "FilterJson", "Name", "StepId" },
                values: new object[,]
                {
                    { 66, "A timeless piece for any occasion.", "{\"ProductTypes\": [\"Ring\"]}", "Ring", 13 },
                    { 67, "Elegant and expressive centerpiece.", "{\"ProductTypes\": [\"Necklace\"]}", "Necklace", 13 },
                    { 68, "Perfect for adding a touch of sparkle.", "{\"ProductTypes\": [\"Earrings\"]}", "Earrings", 13 },
                    { 69, "Stylish and comfortable for everyday or formal wear.", "{\"ProductTypes\": [\"Bracelet\"]}", "Bracelet", 13 },
                    { 70, "Simple, symbolic, and beautiful.", "{\"ProductTypes\": [\"Pendant\"]}", "Pendant", 13 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "Id",
                keyValue: 66);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "Id",
                keyValue: 67);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "Id",
                keyValue: 68);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "Id",
                keyValue: 69);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "Id",
                keyValue: 70);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "Id",
                keyValue: 71);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "Id",
                keyValue: 72);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "Id",
                keyValue: 73);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "Id",
                keyValue: 74);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "Id",
                keyValue: 75);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "Id",
                keyValue: 76);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "Id",
                keyValue: 77);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "Id",
                keyValue: 78);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "Id",
                keyValue: 79);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "Id",
                keyValue: 80);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "Id",
                keyValue: 81);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "Id",
                keyValue: 82);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "Id",
                keyValue: 83);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "Id",
                keyValue: 84);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "Id",
                keyValue: 85);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "Id",
                keyValue: 86);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "Id",
                keyValue: 87);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "Id",
                keyValue: 88);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "Id",
                keyValue: 89);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "Id",
                keyValue: 90);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "Id",
                keyValue: 91);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "Id",
                keyValue: 92);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "Id",
                keyValue: 93);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "Id",
                keyValue: 94);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "Id",
                keyValue: 95);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "Id",
                keyValue: 96);

            migrationBuilder.DeleteData(
                table: "Steps",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Steps",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Steps",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Steps",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Steps",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Steps",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.UpdateData(
                table: "Scenarios",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Custom characteristics");

            migrationBuilder.UpdateData(
                table: "Scenarios",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Custom for an event ");
        }
    }
}
