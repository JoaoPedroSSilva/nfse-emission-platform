using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nfse.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixNaming : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Service Templates",
                table: "Service Templates");

            migrationBuilder.RenameTable(
                name: "Service Templates",
                newName: "ServiceTemplates");

            migrationBuilder.RenameIndex(
                name: "IX_Service Templates_IssuerId_NationalServiceCode",
                table: "ServiceTemplates",
                newName: "IX_ServiceTemplates_IssuerId_NationalServiceCode");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ServiceTemplates",
                table: "ServiceTemplates",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ServiceTemplates",
                table: "ServiceTemplates");

            migrationBuilder.RenameTable(
                name: "ServiceTemplates",
                newName: "Service Templates");

            migrationBuilder.RenameIndex(
                name: "IX_ServiceTemplates_IssuerId_NationalServiceCode",
                table: "Service Templates",
                newName: "IX_Service Templates_IssuerId_NationalServiceCode");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Service Templates",
                table: "Service Templates",
                column: "Id");
        }
    }
}
