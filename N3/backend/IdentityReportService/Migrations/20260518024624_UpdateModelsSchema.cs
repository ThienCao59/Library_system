using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdentityReportService.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModelsSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "LibraryCards",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "EventLogs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "EventLogs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "EventLogs",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "LibraryCards");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "EventLogs");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "EventLogs");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "EventLogs");
        }
    }
}
