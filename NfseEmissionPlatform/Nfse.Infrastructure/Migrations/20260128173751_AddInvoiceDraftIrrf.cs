using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nfse.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddInvoiceDraftIrrf : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "IrrfWithheldAmount",
                table: "InvoiceDrafts",
                type: "decimal(15,2)",
                precision: 15,
                scale: 2,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IrrfWithheldAmount",
                table: "InvoiceDrafts");
        }
    }
}
