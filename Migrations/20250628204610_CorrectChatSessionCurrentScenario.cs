using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JFjewelery.Migrations
{
    /// <inheritdoc />
    public partial class CorrectChatSessionCurrentScenario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Options_Steps_StepId",
                table: "Options");

            migrationBuilder.DropForeignKey(
                name: "FK_Steps_Scenarios_ScenarioId",
                table: "Steps");

            migrationBuilder.AlterColumn<int>(
                name: "ScenarioId",
                table: "Steps",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "StepId",
                table: "Options",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Options_Steps_StepId",
                table: "Options",
                column: "StepId",
                principalTable: "Steps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Steps_Scenarios_ScenarioId",
                table: "Steps",
                column: "ScenarioId",
                principalTable: "Scenarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Options_Steps_StepId",
                table: "Options");

            migrationBuilder.DropForeignKey(
                name: "FK_Steps_Scenarios_ScenarioId",
                table: "Steps");

            migrationBuilder.AlterColumn<int>(
                name: "ScenarioId",
                table: "Steps",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "StepId",
                table: "Options",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Options_Steps_StepId",
                table: "Options",
                column: "StepId",
                principalTable: "Steps",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Steps_Scenarios_ScenarioId",
                table: "Steps",
                column: "ScenarioId",
                principalTable: "Scenarios",
                principalColumn: "Id");
        }
    }
}
