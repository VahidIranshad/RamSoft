using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RamSoft.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Firstmigraton : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Jira");

            migrationBuilder.CreateTable(
                name: "EntityLogDbSet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChangeType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorID = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LastEditDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastEditorID = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityLogDbSet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "States",
                schema: "Jira",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorID = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LastEditDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastEditorID = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "RowVersion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jira_States", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaskBoard",
                schema: "Jira",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DefaultStatesId = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorID = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LastEditDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastEditorID = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "RowVersion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jira_TaskBoard", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskBoard_States_DefaultStatesId",
                        column: x => x.DefaultStatesId,
                        principalSchema: "Jira",
                        principalTable: "States",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TaskBoardStates",
                schema: "Jira",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaskBoardId = table.Column<int>(type: "int", nullable: false),
                    StatesId = table.Column<int>(type: "int", nullable: false),
                    OrderShow = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorID = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LastEditDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastEditorID = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "RowVersion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jira_TaskBoardStates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskBoardStates_States_StatesId",
                        column: x => x.StatesId,
                        principalSchema: "Jira",
                        principalTable: "States",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TaskBoardStates_TaskBoard_TaskBoardId",
                        column: x => x.TaskBoardId,
                        principalSchema: "Jira",
                        principalTable: "TaskBoard",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                schema: "Jira",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    Deadline = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TaskBoardId = table.Column<int>(type: "int", nullable: false),
                    StatesId = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorID = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LastEditDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastEditorID = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "RowVersion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jira_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasks_States_StatesId",
                        column: x => x.StatesId,
                        principalSchema: "Jira",
                        principalTable: "States",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tasks_TaskBoard_TaskBoardId",
                        column: x => x.TaskBoardId,
                        principalSchema: "Jira",
                        principalTable: "TaskBoard",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                schema: "Jira",
                table: "States",
                columns: new[] { "Id", "CreateDate", "CreatorID", "IsDeleted", "LastEditDate", "LastEditorID", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 9, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", false, new DateTime(2024, 9, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", "To Do" },
                    { 2, new DateTime(2024, 9, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", false, new DateTime(2024, 9, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", "In Progress" },
                    { 3, new DateTime(2024, 9, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", false, new DateTime(2024, 9, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", "Review" },
                    { 4, new DateTime(2024, 9, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", false, new DateTime(2024, 9, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", "Done" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskBoard_DefaultStatesId",
                schema: "Jira",
                table: "TaskBoard",
                column: "DefaultStatesId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskBoardStates_StatesId",
                schema: "Jira",
                table: "TaskBoardStates",
                column: "StatesId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskBoardStates_TaskBoardId",
                schema: "Jira",
                table: "TaskBoardStates",
                column: "TaskBoardId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_StatesId",
                schema: "Jira",
                table: "Tasks",
                column: "StatesId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_TaskBoardId",
                schema: "Jira",
                table: "Tasks",
                column: "TaskBoardId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EntityLogDbSet");

            migrationBuilder.DropTable(
                name: "TaskBoardStates",
                schema: "Jira");

            migrationBuilder.DropTable(
                name: "Tasks",
                schema: "Jira");

            migrationBuilder.DropTable(
                name: "TaskBoard",
                schema: "Jira");

            migrationBuilder.DropTable(
                name: "States",
                schema: "Jira");
        }
    }
}
