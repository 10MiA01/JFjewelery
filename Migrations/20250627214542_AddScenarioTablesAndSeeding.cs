using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace JFjewelery.Migrations
{
    /// <inheritdoc />
    public partial class AddScenarioTablesAndSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ChatId",
                table: "Customers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "FilterJson",
                table: "ChatSessions",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Count",
                table: "CharacteristicStones",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateTable(
                name: "Scenarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scenarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Steps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    QuestionText = table.Column<string>(type: "text", nullable: false),
                    NextStepId = table.Column<int>(type: "integer", nullable: true),
                    ScenarioId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Steps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Steps_Scenarios_ScenarioId",
                        column: x => x.ScenarioId,
                        principalTable: "Scenarios",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Steps_Steps_NextStepId",
                        column: x => x.NextStepId,
                        principalTable: "Steps",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Options",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: true),
                    FilterJson = table.Column<string>(type: "text", nullable: true),
                    StepId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Options", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Options_Steps_StepId",
                        column: x => x.StepId,
                        principalTable: "Steps",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Scenarios",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Personal form" },
                    { 2, "Custom characteristics" },
                    { 3, "Custom for an event " },
                    { 4, "Custom by picture" },
                    { 5, "Virtual fitting" }
                });

            migrationBuilder.InsertData(
                table: "Steps",
                columns: new[] { "Id", "Name", "NextStepId", "QuestionText", "ScenarioId" },
                values: new object[] { 7, "Power", null, "If your jewelry had a hidden power, what would it help you with?", 1 });

            migrationBuilder.InsertData(
                table: "Options",
                columns: new[] { "Id", "Content", "FilterJson", "Name", "StepId" },
                values: new object[,]
                {
                    { 35, "Helping you see your true path", "{\"StoneTypes\": [\"Quartz\"], \"StoneColors\": [\"Clear\"], \"Styles\": [\"Minimalist\"], \"Description\": \"Clarity and insight\"}", "Inner Clarity", 7 },
                    { 36, "Giving you strength to speak and act boldly", "{\"Stones\": [\"Ruby\"], \"StoneColors\": [\"Red\"], \"Styles\": [\"Bold\"], \"Description\": \"Confidence and assertiveness\"}", "Courage", 7 },
                    { 37, "Guarding your energy and intentions", "{\"Stones\": [\"Obsidian\"], \"StoneColors\": [\"Black\"], \"StoneTypes\": [\"Protective\"], \"Styles\": [\"Mystic\"]}", "Protection", 7 },
                    { 38, "Attracting deep bonds and warmth", "{\"StoneColors\": [\"Pink\"], \"Stones\": [\"Rose Quartz\"], \"Styles\": [\"Romantic\"], \"Description\": \"Emotional connection\"}", "Love & Connection", 7 },
                    { 39, "Sparking ideas and artistic flow", "{\"StoneColors\": [\"Purple\"], \"Stones\": [\"Amethyst\"], \"Styles\": [\"Artistic\"], \"Description\": \"Imagination and inspiration\"}", "Creativity", 7 },
                    { 40, "Guiding you through change with grace", "{\"StoneColors\": [\"Blue\", \"Green\"], \"Stones\": [\"Labradorite\"], \"Styles\": [\"Mystic\"], \"Description\": \"Change and growth\"}", "Transformation", 7 }
                });

            migrationBuilder.InsertData(
                table: "Steps",
                columns: new[] { "Id", "Name", "NextStepId", "QuestionText", "ScenarioId" },
                values: new object[] { 6, "Energy", 7, "What kind of energy would you like your jewelry to amplify", 1 });

            migrationBuilder.InsertData(
                table: "Options",
                columns: new[] { "Id", "Content", "FilterJson", "Name", "StepId" },
                values: new object[,]
                {
                    { 29, "Subtle, intriguing, layered", "{\"StoneTypes\": [\"Obsidian\", \"Amethyst\"], \"StoneColors\": [\"Black\", \"Purple\"], \"Styles\": [\"Mystic\"]}", "Mystery", 6 },
                    { 30, "Bold, fiery, full of emotion", "{\"StoneColors\": [\"Red\"], \"Stones\": [\"Ruby\"], \"Styles\": [\"Dramatic\"], \"MetalColors\": [\"Gold\"]}", "Passion", 6 },
                    { 31, "Airy, carefree, joyful", "{\"StoneColors\": [\"Pink\", \"Sky Blue\"], \"MetalTypes\": [\"Thin\"], \"Styles\": [\"Playful\"]}", "Lightness", 6 },
                    { 32, "Refined, distant, composed", "{\"StoneColors\": [\"White\", \"Blue\"], \"Styles\": [\"Elegant\"], \"MetalColors\": [\"Platinum\", \"Silver\"]}", "Cool Elegance", 6 },
                    { 33, "Fun, whimsical, unexpected", "{\"StoneShapes\": [\"Heart\", \"Star\"], \"Styles\": [\"Fun\"], \"StoneColors\": [\"Multicolor\"]}", "Playfulness", 6 },
                    { 34, "Strong, assertive, unmistakable", "{\"MetalTypes\": [\"Bold\"], \"Stones\": [\"Diamond\"], \"Styles\": [\"Statement\"], \"StoneColors\": [\"Clear\"]}", "Confidence", 6 }
                });

            migrationBuilder.InsertData(
                table: "Steps",
                columns: new[] { "Id", "Name", "NextStepId", "QuestionText", "ScenarioId" },
                values: new object[] { 5, "Scent", 6, "If your ideal piece of jewelry had a scent, what would it be?", 1 });

            migrationBuilder.InsertData(
                table: "Options",
                columns: new[] { "Id", "Content", "FilterJson", "Name", "StepId" },
                values: new object[,]
                {
                    { 23, "Soft, romantic, blooming with charm", "{\"Styles\": [\"Romantic\"], \"StoneTypes\": [\"Semi-Precious\"], \"StoneColors\": [\"Pink\", \"Lavender\"]}", "Floral", 5 },
                    { 24, "Earthy, grounded, with quiet strength", "{\"MetalColors\": [\"Brown\", \"Copper\"], \"StoneShapes\": [\"Oval\", \"Cushion\"], \"StoneTypes\": [\"Natural\"]}", "Woody", 5 },
                    { 25, "Deep, spicy, mysterious", "{\"StoneColors\": [\"Red\", \"Amber\"], \"MetalTypes\": [\"Engraved\"], \"Styles\": [\"Exotic\"]}", "Oriental", 5 },
                    { 26, "Fresh, clean, with a sense of freedom", "{\"StoneColors\": [\"Blue\", \"Aqua\"], \"Styles\": [\"Marine\"], \"MetalColors\": [\"Silver\"]}", "Oceanic", 5 },
                    { 27, "Gentle, nostalgic, subtly elegant", "{\"Styles\": [\"Vintage\"], \"StoneColors\": [\"Peach\", \"White\"], \"MetalShapes\": [\"Soft\"]}", "Powdery", 5 },
                    { 28, "Pure, simple, speaks without a trace", "{\"Styles\": [\"Minimalist\"], \"StoneTypes\": [], \"MetalTypes\": [\"Plain\"], \"StoneColors\": [\"Clear\"]}", "Unscented (Minimalist)", 5 }
                });

            migrationBuilder.InsertData(
                table: "Steps",
                columns: new[] { "Id", "Name", "NextStepId", "QuestionText", "ScenarioId" },
                values: new object[] { 4, "Music", 5, "If your ideal piece of jewelry were music, what would it sound like?", 1 });

            migrationBuilder.InsertData(
                table: "Options",
                columns: new[] { "Id", "Content", "FilterJson", "Name", "StepId" },
                values: new object[,]
                {
                    { 17, "Soulful, intimate, full of depth.", "{\"Styles\":[\"Romantic\",\"Elegant\"],\"Metals\":[\"Rose Gold\"],\"StoneColors\":[\"Deep Blue\",\"Purple\"],\"StoneTypes\":[\"Semi-Precious\"]}", "Smooth Jazz", 4 },
                    { 18, "Modern, vibrant, full of energy.", "{\"Styles\":[\"Modern\",\"Edgy\"],\"MetalColors\":[\"Black\",\"Chrome\"],\"Metals\":[\"Titanium\"],\"Stones\":[\"Cubic Zirconia\"],\"StoneColors\":[\"Neon\"]}", "🎛Electronic Beats", 4 },
                    { 19, "Elegant, timeless, structured.", "{\"Styles\":[\"Classic\"],\"Metals\":[\"Gold\"],\"StoneTypes\":[\"Precious\"],\"Stones\":[\"Diamond\",\"Pearl\"],\"MetalTypes\":[\"Polished\"]}", "Classical Symphony", 4 },
                    { 20, "Gentle, personal, a bit nostalgic.", "{\"Styles\":[\"Vintage\",\"Rustic\"],\"Stones\":[\"Amber\",\"Moonstone\"],\"MetalColors\":[\"Copper\",\"Bronze\"],\"StoneColors\":[\"Soft White\",\"Honey\"]}", "Indie Acoustic", 4 },
                    { 21, "Minimalist, calming, introspective.", "{\"Styles\":[\"Minimalist\"],\"Metals\":[\"Silver\"],\"StoneTypes\":[],\"MetalTypes\":[\"Matte\"],\"WeightMax\":5}", "Ambient Silence", 4 },
                    { 22, "Eclectic, rich with cultural textures.", "{\"Styles\":[\"Ethnic\",\"Boho\"],\"Stones\":[\"Turquoise\",\"Garnet\"],\"StoneColors\":[\"Red\",\"Green\",\"Blue\"],\"Metals\":[\"Mixed\"]}", "World Fusion", 4 }
                });

            migrationBuilder.InsertData(
                table: "Steps",
                columns: new[] { "Id", "Name", "NextStepId", "QuestionText", "ScenarioId" },
                values: new object[] { 3, "Archetype", 4, "Which archetype best represents you?", 1 });

            migrationBuilder.InsertData(
                table: "Options",
                columns: new[] { "Id", "Content", "FilterJson", "Name", "StepId" },
                values: new object[,]
                {
                    { 11, "Seeker of truth and wisdom.", "{\"Styles\":[\"Classic\",\"Minimalist\"],\"MetalTypes\":[\"Brushed\"],\"Stones\":[\"Sapphire\"],\"StoneColors\":[\"Blue\"],\"StoneTypes\":[\"Precious\"],\"Purity\":925}", "The Sage", 3 },
                    { 12, "Driven, focused, protective.", "{\"Styles\":[\"Bold\",\"Military\"],\"MetalTypes\":[\"Matte\"],\"Metals\":[\"Steel\",\"Titanium\"],\"WeightMin\":10,\"StoneTypes\":[\"None\"]}", "🛡The Warrior", 3 },
                    { 13, "Adventurous and independent.", "{\"Styles\":[\"Boho\",\"Rustic\"],\"Metals\":[\"Silver\"],\"StoneShapes\":[\"Raw\"],\"StoneColors\":[\"Green\",\"Brown\"],\"Stones\":[\"Tourmaline\",\"Agate\"]}", "The Explorer", 3 },
                    { 14, "A natural leader and organizer.", "{\"Styles\":[\"Luxury\",\"Formal\"],\"Metals\":[\"Gold\"],\"MetalColors\":[\"Yellow Gold\"],\"Stones\":[\"Diamond\",\"Ruby\"],\"StoneTypes\":[\"Precious\"],\"Purity\":750}", "The Ruler", 3 },
                    { 15, "Imaginative, artistic, visionary.", "{\"Styles\":[\"Artistic\",\"Abstract\"],\"MetalColors\":[\"Mixed\"],\"Stones\":[\"Opal\",\"Amethyst\"],\"StoneColors\":[\"Violet\",\"Iridescent\"],\"StoneTypes\":[\"Semi-Precious\"]}", "The Creator", 3 },
                    { 16, "Insightful, transformative, intuitive.", "{\"Styles\":[\"Mystic\",\"Elegant\"],\"Stones\":[\"Moonstone\",\"Labradorite\"],\"StoneColors\":[\"Grey\",\"Blue\"],\"MetalTypes\":[\"Oxidized\"],\"Description\":\"Spiritual focus\"}", "The Magician", 3 }
                });

            migrationBuilder.InsertData(
                table: "Steps",
                columns: new[] { "Id", "Name", "NextStepId", "QuestionText", "ScenarioId" },
                values: new object[] { 2, "Season", 3, "What is your favorite season?", 1 });

            migrationBuilder.InsertData(
                table: "Options",
                columns: new[] { "Id", "Content", "FilterJson", "Name", "StepId" },
                values: new object[,]
                {
                    { 7, "Fresh, blooming, and full of life.", "{\"Styles\":[\"Floral\",\"Nature\"],\"MetalColors\":[\"Rose Gold\"],\"StoneColors\":[\"Pink\",\"Green\"],\"Stones\":[\"Peridot\",\"Rose Quartz\"],\"StoneTypes\":[\"Semi-Precious\"]}", "Spring", 2 },
                    { 8, "Bright, vibrant, and radiant.", "{\"Styles\":[\"Bold\",\"Tropical\"],\"MetalColors\":[\"Yellow Gold\"],\"Stones\":[\"Topaz\",\"Citrine\"],\"StoneColors\":[\"Yellow\",\"Light Blue\"],\"MetalTypes\":[\"Polished\"]}", "Summer", 2 },
                    { 9, "Warm, rich, and earthy.", "{\"Styles\":[\"Vintage\",\"Boho\"],\"MetalColors\":[\"Copper\",\"Bronze\"],\"Stones\":[\"Garnet\",\"Tiger's Eye\"],\"StoneColors\":[\"Brown\",\"Red\",\"Orange\"],\"MetalTypes\":[\"Matte\"]}", "Autumn", 2 },
                    { 10, "Cool, calm, and sparkling.", "{\"Styles\":[\"Minimalist\",\"Classic\"],\"MetalColors\":[\"White Gold\",\"Silver\"],\"Stones\":[\"Diamond\",\"Sapphire\"],\"StoneColors\":[\"White\",\"Blue\"],\"StoneTypes\":[\"Precious\"],\"Purity\":925}", "Winter", 2 }
                });

            migrationBuilder.InsertData(
                table: "Steps",
                columns: new[] { "Id", "Name", "NextStepId", "QuestionText", "ScenarioId" },
                values: new object[] { 1, "Stone", 2, "If you were a stone, what kind would you be?", 1 });

            migrationBuilder.InsertData(
                table: "Options",
                columns: new[] { "Id", "Content", "FilterJson", "Name", "StepId" },
                values: new object[,]
                {
                    { 1, "Timeless, brilliant, and strong.", "{\"Stones\":[\"Diamond\"],\"StoneColors\":[\"White\"],\"StoneTypes\":[\"Precious\"],\"StoneShapes\":[\"Round\",\"Princess\"],\"StoneSizes\":[\"Medium\",\"Large\"],\"CountMin\":1}", "Diamond", 1 },
                    { 2, "Mysterious, spiritual, and creative.", "{\"Stones\":[\"Amethyst\"],\"StoneColors\":[\"Purple\"],\"StoneTypes\":[\"Semi-Precious\"],\"StoneShapes\":[\"Oval\",\"Marquise\"],\"StoneSizes\":[\"Medium\"],\"CountMin\":1}", "Amethyst", 1 },
                    { 3, "Natural, elegant, and deeply intuitive.", "{\"Stones\":[\"Emerald\"],\"StoneColors\":[\"Green\"],\"StoneTypes\":[\"Precious\"],\"StoneShapes\":[\"Emerald\",\"Cushion\"],\"StoneSizes\":[\"Medium\",\"Large\"],\"CountMin\":1}", "Emerald", 1 },
                    { 4, "Passionate, bold, and full of energy.", "{\"Stones\":[\"Ruby\"],\"StoneColors\":[\"Red\"],\"StoneTypes\":[\"Precious\"],\"StoneShapes\":[\"Heart\",\"Oval\"],\"StoneSizes\":[\"Small\",\"Medium\"],\"CountMin\":1}", "Ruby", 1 },
                    { 5, "Calm, wise, and emotionally deep.", "{\"Stones\":[\"Sapphire\"],\"StoneColors\":[\"Blue\"],\"StoneTypes\":[\"Precious\"],\"StoneShapes\":[\"Round\",\"Cushion\"],\"StoneSizes\":[\"Medium\"],\"CountMin\":1}", "Sapphire", 1 },
                    { 6, "Grounded, edgy, and protective.", "{\"Stones\":[\"Obsidian\"],\"StoneColors\":[\"Black\"],\"StoneTypes\":[\"Organic\"],\"StoneShapes\":[\"Oval\",\"Cabochon\"],\"StoneSizes\":[\"Large\"],\"CountMin\":1}", "Obsidian", 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Options_StepId",
                table: "Options",
                column: "StepId");

            migrationBuilder.CreateIndex(
                name: "IX_Steps_NextStepId",
                table: "Steps",
                column: "NextStepId");

            migrationBuilder.CreateIndex(
                name: "IX_Steps_ScenarioId",
                table: "Steps",
                column: "ScenarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Options");

            migrationBuilder.DropTable(
                name: "Steps");

            migrationBuilder.DropTable(
                name: "Scenarios");

            migrationBuilder.DropColumn(
                name: "ChatId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "FilterJson",
                table: "ChatSessions");

            migrationBuilder.AlterColumn<int>(
                name: "Count",
                table: "CharacteristicStones",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);
        }
    }
}
