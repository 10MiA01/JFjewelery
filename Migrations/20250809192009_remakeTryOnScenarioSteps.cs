using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JFjewelery.Migrations
{
    /// <inheritdoc />
    public partial class remakeTryOnScenarioSteps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Steps",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "Name", "NextStepId", "QuestionText" },
                values: new object[] { "Get description", 22, "Please give a discrittion of what you want" });

            migrationBuilder.InsertData(
                table: "Steps",
                columns: new[] { "Id", "Name", "NextStepId", "QuestionText", "ScenarioId" },
                values: new object[] { 22, "Get photo", null, "Please send a photo.", 5 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Steps",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.UpdateData(
                table: "Steps",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "Name", "NextStepId", "QuestionText" },
                values: new object[] { "Get photo", null, "Please send a photo." });
        }
    }
}
