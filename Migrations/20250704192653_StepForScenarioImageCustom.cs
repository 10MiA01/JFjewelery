using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JFjewelery.Migrations
{
    /// <inheritdoc />
    public partial class StepForScenarioImageCustom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Steps",
                columns: new[] { "Id", "Name", "NextStepId", "QuestionText", "ScenarioId" },
                values: new object[] { 19, "Get photo", null, "Please send a photo.", 4 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Steps",
                keyColumn: "Id",
                keyValue: 19);
        }
    }
}
