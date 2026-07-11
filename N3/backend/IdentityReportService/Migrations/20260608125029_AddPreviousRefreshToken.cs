using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdentityReportService.Migrations
{
    /// <inheritdoc />
    public partial class AddPreviousRefreshToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PreviousRefreshToken",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PreviousRefreshToken",
                table: "Users");
        }
    }
}
