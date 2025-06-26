using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace JFjewelery.Migrations
{
    /// <inheritdoc />
    public partial class RestructurizationForFilterAndSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Coating",
                table: "Characteristics");

            migrationBuilder.DropColumn(
                name: "Material",
                table: "Characteristics");

            migrationBuilder.DropColumn(
                name: "MetalColor",
                table: "Characteristics");

            migrationBuilder.DropColumn(
                name: "Purity",
                table: "Characteristics");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "Characteristics");

            migrationBuilder.DropColumn(
                name: "Stone",
                table: "Characteristics");

            migrationBuilder.DropColumn(
                name: "StoneColor",
                table: "Characteristics");

            migrationBuilder.DropColumn(
                name: "StoneCount",
                table: "Characteristics");

            migrationBuilder.DropColumn(
                name: "StoneShape",
                table: "Characteristics");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "Characteristics");

            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "Products",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Characteristics",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AppliesToEntities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppliesToEntities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Metal",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Metal", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stone",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stone", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Coating",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    AppliesToEntityId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coating", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Coating_AppliesToEntities_AppliesToEntityId",
                        column: x => x.AppliesToEntityId,
                        principalTable: "AppliesToEntities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Color",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    AppliesToEntityId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Color", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Color_AppliesToEntities_AppliesToEntityId",
                        column: x => x.AppliesToEntityId,
                        principalTable: "AppliesToEntities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Shape",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    AppliesToEntityId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shape", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shape_AppliesToEntities_AppliesToEntityId",
                        column: x => x.AppliesToEntityId,
                        principalTable: "AppliesToEntities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Size",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    AppliesToEntityId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Size", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Size_AppliesToEntities_AppliesToEntityId",
                        column: x => x.AppliesToEntityId,
                        principalTable: "AppliesToEntities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Type",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    AppliesToEntityId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Type", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Type_AppliesToEntities_AppliesToEntityId",
                        column: x => x.AppliesToEntityId,
                        principalTable: "AppliesToEntities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CharacteristicMetals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CharacteristicId = table.Column<int>(type: "integer", nullable: false),
                    MetalId = table.Column<int>(type: "integer", nullable: false),
                    ShapeId = table.Column<int>(type: "integer", nullable: true),
                    ColorId = table.Column<int>(type: "integer", nullable: true),
                    SizeId = table.Column<int>(type: "integer", nullable: true),
                    TypeId = table.Column<int>(type: "integer", nullable: true),
                    Purity = table.Column<int>(type: "integer", nullable: true),
                    Weight = table.Column<float>(type: "real", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacteristicMetals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CharacteristicMetals_Characteristics_CharacteristicId",
                        column: x => x.CharacteristicId,
                        principalTable: "Characteristics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacteristicMetals_Color_ColorId",
                        column: x => x.ColorId,
                        principalTable: "Color",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CharacteristicMetals_Metal_MetalId",
                        column: x => x.MetalId,
                        principalTable: "Metal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacteristicMetals_Shape_ShapeId",
                        column: x => x.ShapeId,
                        principalTable: "Shape",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CharacteristicMetals_Size_SizeId",
                        column: x => x.SizeId,
                        principalTable: "Size",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CharacteristicMetals_Type_TypeId",
                        column: x => x.TypeId,
                        principalTable: "Type",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CharacteristicStones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CharacteristicId = table.Column<int>(type: "integer", nullable: false),
                    StoneId = table.Column<int>(type: "integer", nullable: false),
                    ShapeId = table.Column<int>(type: "integer", nullable: true),
                    ColorId = table.Column<int>(type: "integer", nullable: true),
                    SizeId = table.Column<int>(type: "integer", nullable: true),
                    TypeId = table.Column<int>(type: "integer", nullable: true),
                    Count = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacteristicStones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CharacteristicStones_Characteristics_CharacteristicId",
                        column: x => x.CharacteristicId,
                        principalTable: "Characteristics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacteristicStones_Color_ColorId",
                        column: x => x.ColorId,
                        principalTable: "Color",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CharacteristicStones_Shape_ShapeId",
                        column: x => x.ShapeId,
                        principalTable: "Shape",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CharacteristicStones_Size_SizeId",
                        column: x => x.SizeId,
                        principalTable: "Size",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CharacteristicStones_Stone_StoneId",
                        column: x => x.StoneId,
                        principalTable: "Stone",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacteristicStones_Type_TypeId",
                        column: x => x.TypeId,
                        principalTable: "Type",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "AppliesToEntities",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Metal" },
                    { 2, "Stone" }
                });

            migrationBuilder.UpdateData(
                table: "Characteristics",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "Manufacturer" },
                values: new object[] { null, "JFjewelery" });

            migrationBuilder.InsertData(
                table: "Metal",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Gold" },
                    { 2, "Silver" },
                    { 3, "Platinum" },
                    { 4, "Titanium" }
                });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "Price",
                value: 5000);

            migrationBuilder.InsertData(
                table: "Stone",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Diamond" },
                    { 2, "Ruby" },
                    { 3, "Sapphire" },
                    { 4, "Emerald" },
                    { 5, "Amethyst" },
                    { 6, "Topaz" },
                    { 7, "Spinel" }
                });

            migrationBuilder.InsertData(
                table: "Coating",
                columns: new[] { "Id", "AppliesToEntityId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Rhodium Plating" },
                    { 2, 1, "Gold Plating" },
                    { 3, 1, "Black Rhodium" },
                    { 4, 1, "Silver Plating" },
                    { 5, 1, "PVD Coating" },
                    { 6, 2, "Irradiation" },
                    { 7, 2, "Heat Treatment" },
                    { 8, 2, "Dyeing" },
                    { 9, 2, "Coating with Film" }
                });

            migrationBuilder.InsertData(
                table: "Color",
                columns: new[] { "Id", "AppliesToEntityId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Silver" },
                    { 2, 1, "Gold" },
                    { 3, 1, "Rose Gold" },
                    { 4, 1, "White Gold" },
                    { 5, 1, "Platinum" },
                    { 6, 1, "Black" },
                    { 7, 2, "Red" },
                    { 8, 2, "Blue" },
                    { 9, 2, "Green" },
                    { 10, 2, "Yellow" },
                    { 11, 2, "Pink" },
                    { 12, 2, "Purple" },
                    { 13, 2, "Orange" },
                    { 14, 2, "White" },
                    { 15, 2, "Black" }
                });

            migrationBuilder.InsertData(
                table: "Shape",
                columns: new[] { "Id", "AppliesToEntityId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Round" },
                    { 2, 1, "Square" },
                    { 3, 1, "Oval" },
                    { 4, 1, "Rectangle" },
                    { 5, 1, "Triangle" },
                    { 6, 2, "Brilliant Cut" },
                    { 7, 2, "Princess Cut" },
                    { 8, 2, "Cushion Cut" },
                    { 9, 2, "Emerald Cut" },
                    { 10, 2, "Marquise Cut" },
                    { 11, 2, "Oval" },
                    { 12, 2, "Round" }
                });

            migrationBuilder.InsertData(
                table: "Size",
                columns: new[] { "Id", "AppliesToEntityId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Small" },
                    { 2, 1, "Medium" },
                    { 3, 1, "Large" },
                    { 4, 2, "Tiny" },
                    { 5, 2, "Regular" },
                    { 6, 2, "Big" }
                });

            migrationBuilder.InsertData(
                table: "Type",
                columns: new[] { "Id", "AppliesToEntityId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Gold" },
                    { 2, 1, "Silver" },
                    { 3, 1, "Platinum" },
                    { 4, 1, "Palladium" },
                    { 5, 1, "Titanium" },
                    { 6, 2, "Star-shaped" },
                    { 7, 2, "Milky" },
                    { 8, 2, "Impure" },
                    { 9, 2, "Pure" },
                    { 10, 2, "Translucent" }
                });

            migrationBuilder.InsertData(
                table: "CharacteristicMetals",
                columns: new[] { "Id", "CharacteristicId", "ColorId", "MetalId", "Purity", "ShapeId", "SizeId", "TypeId", "Weight" },
                values: new object[] { 1, 1, 1, 1, 585, 3, 2, 1, 10f });

            migrationBuilder.InsertData(
                table: "CharacteristicStones",
                columns: new[] { "Id", "CharacteristicId", "ColorId", "Count", "ShapeId", "SizeId", "StoneId", "TypeId" },
                values: new object[,]
                {
                    { 1, 1, 7, 4, 11, 5, 2, null },
                    { 2, 1, 14, 62, 12, 5, 7, 9 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CharacteristicMetals_CharacteristicId",
                table: "CharacteristicMetals",
                column: "CharacteristicId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacteristicMetals_ColorId",
                table: "CharacteristicMetals",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacteristicMetals_MetalId",
                table: "CharacteristicMetals",
                column: "MetalId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacteristicMetals_ShapeId",
                table: "CharacteristicMetals",
                column: "ShapeId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacteristicMetals_SizeId",
                table: "CharacteristicMetals",
                column: "SizeId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacteristicMetals_TypeId",
                table: "CharacteristicMetals",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacteristicStones_CharacteristicId",
                table: "CharacteristicStones",
                column: "CharacteristicId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacteristicStones_ColorId",
                table: "CharacteristicStones",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacteristicStones_ShapeId",
                table: "CharacteristicStones",
                column: "ShapeId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacteristicStones_SizeId",
                table: "CharacteristicStones",
                column: "SizeId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacteristicStones_StoneId",
                table: "CharacteristicStones",
                column: "StoneId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacteristicStones_TypeId",
                table: "CharacteristicStones",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Coating_AppliesToEntityId",
                table: "Coating",
                column: "AppliesToEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Color_AppliesToEntityId",
                table: "Color",
                column: "AppliesToEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Shape_AppliesToEntityId",
                table: "Shape",
                column: "AppliesToEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Size_AppliesToEntityId",
                table: "Size",
                column: "AppliesToEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Type_AppliesToEntityId",
                table: "Type",
                column: "AppliesToEntityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CharacteristicMetals");

            migrationBuilder.DropTable(
                name: "CharacteristicStones");

            migrationBuilder.DropTable(
                name: "Coating");

            migrationBuilder.DropTable(
                name: "Metal");

            migrationBuilder.DropTable(
                name: "Color");

            migrationBuilder.DropTable(
                name: "Shape");

            migrationBuilder.DropTable(
                name: "Size");

            migrationBuilder.DropTable(
                name: "Stone");

            migrationBuilder.DropTable(
                name: "Type");

            migrationBuilder.DropTable(
                name: "AppliesToEntities");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Characteristics");

            migrationBuilder.AddColumn<string>(
                name: "Coating",
                table: "Characteristics",
                type: "character varying(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Material",
                table: "Characteristics",
                type: "character varying(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MetalColor",
                table: "Characteristics",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Purity",
                table: "Characteristics",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Size",
                table: "Characteristics",
                type: "character varying(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Stone",
                table: "Characteristics",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StoneColor",
                table: "Characteristics",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StoneCount",
                table: "Characteristics",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StoneShape",
                table: "Characteristics",
                type: "character varying(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Weight",
                table: "Characteristics",
                type: "real",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Characteristics",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Coating", "Manufacturer", "Material", "MetalColor", "Purity", "Size", "Stone", "StoneColor", "StoneCount", "StoneShape", "Weight" },
                values: new object[] { null, null, "Gold", "Yellow", 585, "Universal", "Ruby", null, 4, "Pear", 10f });
        }
    }
}
