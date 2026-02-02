using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nfse.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddIssuerSequence : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IssuerSequences",
                columns: table => new
                {
                    IssuerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NextDpsNumber = table.Column<long>(type: "bigint", nullable: false),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssuerSequences", x => x.IssuerId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IssuerSequences");
        }
    }
}
