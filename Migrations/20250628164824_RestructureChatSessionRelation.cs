using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace JFjewelery.Migrations
{
    /// <inheritdoc />
    public partial class RestructureChatSessionRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ChatSessions",
                table: "ChatSessions");

            migrationBuilder.DropIndex(
                name: "IX_ChatSessions_CustomerId",
                table: "ChatSessions");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ChatSessions");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChatSessions",
                table: "ChatSessions",
                column: "CustomerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ChatSessions",
                table: "ChatSessions");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ChatSessions",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChatSessions",
                table: "ChatSessions",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ChatSessions_CustomerId",
                table: "ChatSessions",
                column: "CustomerId");
        }
    }
}
