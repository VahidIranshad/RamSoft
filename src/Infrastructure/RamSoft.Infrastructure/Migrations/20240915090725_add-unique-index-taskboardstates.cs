using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RamSoft.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class adduniqueindextaskboardstates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TaskBoardStates_TaskBoardId",
                schema: "Jira",
                table: "TaskBoardStates");

            migrationBuilder.CreateIndex(
                name: "IX_TaskBoardStates_TaskBoardId_StatesId",
                schema: "Jira",
                table: "TaskBoardStates",
                columns: new[] { "TaskBoardId", "StatesId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TaskBoardStates_TaskBoardId_StatesId",
                schema: "Jira",
                table: "TaskBoardStates");

            migrationBuilder.CreateIndex(
                name: "IX_TaskBoardStates_TaskBoardId",
                schema: "Jira",
                table: "TaskBoardStates",
                column: "TaskBoardId");
        }
    }
}
