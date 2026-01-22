using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nfse.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddInvoiceDraftServiceDescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ServiceDescription",
                table: "InvoiceDrafts",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ServiceDescription",
                table: "InvoiceDrafts");
        }
    }
}
