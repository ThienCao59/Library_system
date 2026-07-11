using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdentityReportService.Migrations
{
    /// <inheritdoc />
    public partial class AddFido2WebAuthn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsWebAuthnEnabled",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "FidoStoredCredentials",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescriptorId = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PublicKey = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    UserHandle = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    SignatureCounter = table.Column<long>(type: "bigint", nullable: false),
                    CredType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AaGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FidoStoredCredentials", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FidoStoredCredentials");

            migrationBuilder.DropColumn(
                name: "IsWebAuthnEnabled",
                table: "Users");
        }
    }
}
