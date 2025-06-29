using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace JFjewelery.Migrations
{
    /// <inheritdoc />
    public partial class SeedingScenatioOcasion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Steps",
                columns: new[] { "Id", "Name", "NextStepId", "QuestionText", "ScenarioId" },
                values: new object[] { 12, "Type of jewelry", null, "What type of jewelry are you interested in?", 2 });

            migrationBuilder.InsertData(
                table: "Options",
                columns: new[] { "Id", "Content", "FilterJson", "Name", "StepId" },
                values: new object[,]
                {
                    { 61, "Classic or statement pieces to enhance any look.", "{\"Types\":[\"Ring\"]}", "Ring", 12 },
                    { 62, "From delicate chains to bold pendants.", "{\"Types\":[\"Necklace\"]}", "Necklace", 12 },
                    { 63, "Perfect for layering or wearing solo.", "{\"Types\":[\"Bracelet\"]}", "Bracelet", 12 },
                    { 64, "Studs, hoops, or chandeliers to match any outfit.", "{\"Types\":[\"Earrings\"]}", "Earrings", 12 },
                    { 65, "We’ll show a mix of beautiful jewelry types.", "{}", "No preference", 12 }
                });

            migrationBuilder.InsertData(
                table: "Steps",
                columns: new[] { "Id", "Name", "NextStepId", "QuestionText", "ScenarioId" },
                values: new object[] { 11, "Metal", 12, "What metal do you prefer?", 2 });

            migrationBuilder.InsertData(
                table: "Options",
                columns: new[] { "Id", "Content", "FilterJson", "Name", "StepId" },
                values: new object[,]
                {
                    { 56, "Warm and timeless, perfect for any skin tone.", "{\"Metals\":[\"Gold\"]}", "Gold", 11 },
                    { 57, "Cool, elegant, and versatile for all occasions.", "{\"Metals\":[\"Silver\"]}", "Silver", 11 },
                    { 58, "Rare and durable, ideal for luxury lovers.", "{\"Metals\":[\"Platinum\"]}", "Platinum", 11 },
                    { 59, "Edgy and bold, great for making a statement.", "{\"MetalColors\":[\"Dark\",\"Oxidized\"]}", "Dark metal / Oxidized", 11 },
                    { 60, "We’ll show you a mix of beautiful metal options.", "{}", "No preference", 11 }
                });

            migrationBuilder.InsertData(
                table: "Steps",
                columns: new[] { "Id", "Name", "NextStepId", "QuestionText", "ScenarioId" },
                values: new object[] { 10, "Clothing", 11, "What style of clothing do you plan to wear?", 2 });

            migrationBuilder.InsertData(
                table: "Options",
                columns: new[] { "Id", "Content", "FilterJson", "Name", "StepId" },
                values: new object[,]
                {
                    { 51, "Perfect for formal events and glamorous evenings.", "{\"Styles\":[\"Elegant\",\"Luxury\"],\"Stones\":[\"Diamond\",\"Sapphire\"],\"StoneSizes\":[\"Medium\",\"Large\"],\"Metals\":[\"Gold\",\"Platinum\"]}", "Evening dress", 10 },
                    { 52, "Discreet and refined jewelry for a professional look.", "{\"Styles\":[\"Minimalist\",\"Classic\"],\"StoneColors\":[\"White\",\"Blue\",\"Black\"],\"StoneSizes\":[\"Small\"],\"Metals\":[\"Silver\",\"White Gold\"]}", "Business style", 10 },
                    { 53, "Comfortable and easy-to-match accessories.", "{\"Styles\":[\"Casual\",\"Simple\"],\"StoneColors\":[\"Neutral\",\"Light Blue\"],\"StoneSizes\":[\"Small\",\"Medium\"],\"Metals\":[\"Silver\"]}", "Casual everyday", 10 },
                    { 54, "Stylish and versatile pieces that elevate your outfit.", "{\"Styles\":[\"Smart\",\"Modern\"],\"StoneColors\":[\"Green\",\"Beige\"],\"StoneSizes\":[\"Medium\"],\"Metals\":[\"Rose Gold\",\"Gold\"]}", "Smart casual", 10 },
                    { 55, "Let us surprise you with something universally flattering.", "{\"Styles\":[\"Universal\",\"Classic\"],\"StoneSizes\":[\"Medium\"],\"Metals\":[\"Silver\",\"Gold\"]}", "Not sure / Doesn’t matter", 10 }
                });

            migrationBuilder.InsertData(
                table: "Steps",
                columns: new[] { "Id", "Name", "NextStepId", "QuestionText", "ScenarioId" },
                values: new object[] { 9, "Mood", 10, "What mood do you want to express", 2 });

            migrationBuilder.InsertData(
                table: "Options",
                columns: new[] { "Id", "Content", "FilterJson", "Name", "StepId" },
                values: new object[,]
                {
                    { 46, "Bold and colorful pieces that draw attention.", "{\"Styles\":[\"Bold\",\"Colorful\"],\"StoneColors\":[\"Red\",\"Yellow\",\"Turquoise\"],\"StoneSizes\":[\"Large\"],\"Metals\":[\"Gold\",\"Rose Gold\"]}", "Bright and noticeable", 9 },
                    { 47, "Simple, clean, and graceful designs.", "{\"Styles\":[\"Minimalist\",\"Elegant\"],\"StoneColors\":[\"White\",\"Blue\",\"Black\"],\"StoneSizes\":[\"Small\"],\"Metals\":[\"Silver\",\"White Gold\"]}", "Subtle and elegant", 9 },
                    { 48, "Delicate pieces with gentle tones and curves.", "{\"Styles\":[\"Romantic\",\"Vintage\"],\"StoneColors\":[\"Pink\",\"Peach\",\"Purple\"],\"StoneShapes\":[\"Heart\",\"Oval\"],\"StoneSizes\":[\"Medium\"],\"Metals\":[\"Rose Gold\"]}", "Soft and romantic", 9 },
                    { 49, "Statement jewelry for special moments.", "{\"Styles\":[\"Luxury\",\"Statement\"],\"Stones\":[\"Diamond\",\"Ruby\",\"Emerald\"],\"StoneColors\":[\"White\",\"Red\",\"Green\"],\"StoneSizes\":[\"Large\"],\"Metals\":[\"Platinum\",\"Gold\"]}", "Luxurious and striking", 9 },
                    { 50, "Classic designs that suit any look.", "{\"Styles\":[\"Classic\",\"Universal\"],\"StoneColors\":[\"Neutral\",\"White\",\"Blue\"],\"StoneSizes\":[\"Medium\"],\"Metals\":[\"Silver\",\"Gold\"]}", "Versatile for any situation", 9 }
                });

            migrationBuilder.InsertData(
                table: "Steps",
                columns: new[] { "Id", "Name", "NextStepId", "QuestionText", "ScenarioId" },
                values: new object[] { 8, "Occasion", 9, "What is the occasion?", 2 });

            migrationBuilder.InsertData(
                table: "Options",
                columns: new[] { "Id", "Content", "FilterJson", "Name", "StepId" },
                values: new object[,]
                {
                    { 41, "Elegant and timeless styles for your special day.", "{\"Styles\":[\"Classic\",\"Elegant\"],\"Metals\":[\"Gold\",\"Platinum\"],\"Stones\":[\"Diamond\"],\"StoneTypes\":[\"Precious\"],\"StoneColors\":[\"White\"],\"StoneShapes\":[\"Round\",\"Princess\"],\"StoneSizes\":[\"Medium\",\"Large\"]}", "Wedding", 8 },
                    { 42, "Colorful and joyful jewelry to celebrate you.", "{\"Styles\":[\"Playful\",\"Modern\"],\"Metals\":[\"Silver\",\"Gold\"],\"Stones\":[\"Amethyst\",\"Topaz\",\"Citrine\"],\"StoneTypes\":[\"Semi-Precious\"],\"StoneColors\":[\"Purple\",\"Blue\",\"Yellow\"],\"StoneShapes\":[\"Oval\",\"Heart\"],\"StoneSizes\":[\"Medium\"]}", "Birthday", 8 },
                    { 43, "Minimalistic and professional designs for a refined look.", "{\"Styles\":[\"Minimalist\",\"Modern\",\"Elegant\"],\"Metals\":[\"Silver\",\"White Gold\"],\"StoneColors\":[\"White\",\"Black\",\"Blue\"],\"StoneSizes\":[\"Small\"],\"StoneTypes\":[\"Precious\",\"Semi-Precious\"]}", "Corporate event", 8 },
                    { 44, "Charming pieces to make hearts flutter.", "{\"Styles\":[\"Romantic\",\"Vintage\"],\"Metals\":[\"Rose Gold\",\"Gold\"],\"Stones\":[\"Ruby\",\"Sapphire\",\"Diamond\"],\"StoneColors\":[\"Red\",\"Pink\"],\"StoneShapes\":[\"Heart\",\"Oval\"],\"StoneSizes\":[\"Medium\"]}", "Romantic evening", 8 },
                    { 45, "Unique and expressive styles that reflect you.", "{\"Styles\":[\"Boho\",\"Art Deco\",\"Modern\"],\"Metals\":[\"Silver\",\"Gold\",\"Mixed\"],\"Stones\":[\"Obsidian\",\"Labradorite\",\"Turquoise\"],\"StoneTypes\":[\"Organic\",\"Semi-Precious\"],\"StoneColors\":[\"Black\",\"Blue\",\"Green\"],\"StoneSizes\":[\"Medium\",\"Large\"]}", "Just want to treat myself", 8 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "Id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "Id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "Id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "Id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "Id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "Id",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "Id",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "Id",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "Id",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "Id",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "Id",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "Id",
                keyValue: 64);

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "Id",
                keyValue: 65);

            migrationBuilder.DeleteData(
                table: "Steps",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Steps",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Steps",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Steps",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Steps",
                keyColumn: "Id",
                keyValue: 12);
        }
    }
}
