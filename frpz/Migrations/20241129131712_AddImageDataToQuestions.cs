﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace frpz.Migrations
{
    /// <inheritdoc />
    public partial class AddImageDataToQuestions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "ImageData",
                table: "Questions",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageData",
                table: "Questions");
        }
    }
}
