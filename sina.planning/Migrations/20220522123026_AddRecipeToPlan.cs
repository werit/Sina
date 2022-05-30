﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace sina.planning.Migrations
{
    public partial class AddRecipeToPlan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "current_flag",
                table: "recipe_schedule");

            migrationBuilder.DropColumn(
                name: "valid_from",
                table: "recipe_schedule");

            migrationBuilder.DropColumn(
                name: "valid_to",
                table: "recipe_schedule");

            migrationBuilder.CreateTable(
                name: "recipe",
                columns: table => new
                {
                    recipe_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_recipe", x => x.recipe_id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "recipe");

            migrationBuilder.AddColumn<char>(
                name: "current_flag",
                table: "recipe_schedule",
                type: "character(1)",
                nullable: false,
                defaultValue: ' ');

            migrationBuilder.AddColumn<DateTime>(
                name: "valid_from",
                table: "recipe_schedule",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "valid_to",
                table: "recipe_schedule",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
