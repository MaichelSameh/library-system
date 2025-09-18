using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace library_system.Migrations
{
    /// <inheritdoc />
    public partial class Micheal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ReturnedAt",
                table: "Borrows",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReturnedAt",
                table: "Borrows");
        }
    }
}
