using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nfse.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MakeServiceTemplateOptionalInDrafts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_InvoiceDrafts_IssuerId_Status",
                table: "InvoiceDrafts");

            migrationBuilder.AlterColumn<Guid>(
                name: "ServiceTemplateId",
                table: "InvoiceDrafts",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<string>(
                name: "NationalServiceCode",
                table: "InvoiceDrafts",
                type: "nvarchar(6)",
                maxLength: 6,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceDrafts_IssuerId_NationalServiceCode_Status",
                table: "InvoiceDrafts",
                columns: new[] { "IssuerId", "NationalServiceCode", "Status" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_InvoiceDrafts_IssuerId_NationalServiceCode_Status",
                table: "InvoiceDrafts");

            migrationBuilder.DropColumn(
                name: "NationalServiceCode",
                table: "InvoiceDrafts");

            migrationBuilder.AlterColumn<Guid>(
                name: "ServiceTemplateId",
                table: "InvoiceDrafts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceDrafts_IssuerId_Status",
                table: "InvoiceDrafts",
                columns: new[] { "IssuerId", "Status" });
        }
    }
}
