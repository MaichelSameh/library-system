using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace library_system.Migrations
{
    /// <inheritdoc />
    public partial class botros : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Tipologys",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "Tipologys",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Tipologys",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Tipologys",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Pubblishers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "Pubblishers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Pubblishers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Pubblishers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Clients",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "Clients",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Clients",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Clients",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Borrows",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "Borrows",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Borrows",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Borrows",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Books",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "Books",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Books",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Books",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Authors",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "Authors",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Authors",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Authors",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tipologys_CreatedBy",
                table: "Tipologys",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Pubblishers_CreatedBy",
                table: "Pubblishers",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_CreatedBy",
                table: "Clients",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Borrows_CreatedBy",
                table: "Borrows",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Books_CreatedBy",
                table: "Books",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Authors_CreatedBy",
                table: "Authors",
                column: "CreatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_Authors_Clients_CreatedBy",
                table: "Authors",
                column: "CreatedBy",
                principalTable: "Clients",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Clients_CreatedBy",
                table: "Books",
                column: "CreatedBy",
                principalTable: "Clients",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Borrows_Clients_CreatedBy",
                table: "Borrows",
                column: "CreatedBy",
                principalTable: "Clients",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_Clients_CreatedBy",
                table: "Clients",
                column: "CreatedBy",
                principalTable: "Clients",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Pubblishers_Clients_CreatedBy",
                table: "Pubblishers",
                column: "CreatedBy",
                principalTable: "Clients",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tipologys_Clients_CreatedBy",
                table: "Tipologys",
                column: "CreatedBy",
                principalTable: "Clients",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Authors_Clients_CreatedBy",
                table: "Authors");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_Clients_CreatedBy",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Borrows_Clients_CreatedBy",
                table: "Borrows");

            migrationBuilder.DropForeignKey(
                name: "FK_Clients_Clients_CreatedBy",
                table: "Clients");

            migrationBuilder.DropForeignKey(
                name: "FK_Pubblishers_Clients_CreatedBy",
                table: "Pubblishers");

            migrationBuilder.DropForeignKey(
                name: "FK_Tipologys_Clients_CreatedBy",
                table: "Tipologys");

            migrationBuilder.DropIndex(
                name: "IX_Tipologys_CreatedBy",
                table: "Tipologys");

            migrationBuilder.DropIndex(
                name: "IX_Pubblishers_CreatedBy",
                table: "Pubblishers");

            migrationBuilder.DropIndex(
                name: "IX_Clients_CreatedBy",
                table: "Clients");

            migrationBuilder.DropIndex(
                name: "IX_Borrows_CreatedBy",
                table: "Borrows");

            migrationBuilder.DropIndex(
                name: "IX_Books_CreatedBy",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Authors_CreatedBy",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Tipologys");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Tipologys");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Tipologys");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Tipologys");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Pubblishers");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Pubblishers");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Pubblishers");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Pubblishers");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Borrows");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Borrows");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Borrows");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Borrows");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Authors");
        }
    }
}
