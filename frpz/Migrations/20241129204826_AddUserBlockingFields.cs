using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace frpz.Migrations
{
    /// <inheritdoc />
    public partial class AddUserBlockingFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "BlockedUntil",
                table: "Users",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FailedAttempts",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BlockedUntil",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FailedAttempts",
                table: "Users");
        }
    }
}
