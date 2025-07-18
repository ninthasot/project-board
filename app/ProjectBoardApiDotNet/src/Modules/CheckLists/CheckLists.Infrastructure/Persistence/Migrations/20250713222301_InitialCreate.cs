﻿//<auto-generated>
using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CheckLists.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "checklist");

            migrationBuilder.CreateTable(
                name: "CheckLists",
                schema: "checklist",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CardId = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckLists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CheckListItems",
                schema: "checklist",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CheckListId = table.Column<Guid>(type: "uuid", nullable: false),
                    Content = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    IsCompleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckListItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CheckListItems_CheckLists_CheckListId",
                        column: x => x.CheckListId,
                        principalSchema: "checklist",
                        principalTable: "CheckLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CheckListItems_CheckListId",
                schema: "checklist",
                table: "CheckListItems",
                column: "CheckListId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckListItems_IsCompleted",
                schema: "checklist",
                table: "CheckListItems",
                column: "IsCompleted");

            migrationBuilder.CreateIndex(
                name: "IX_CheckLists_CardId",
                schema: "checklist",
                table: "CheckLists",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckLists_Title",
                schema: "checklist",
                table: "CheckLists",
                column: "Title");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CheckListItems",
                schema: "checklist");

            migrationBuilder.DropTable(
                name: "CheckLists",
                schema: "checklist");
        }
    }
}
