using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nfse.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddIssuerMunicipalAndTaxProfile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TradeName",
                table: "Issuers",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AddColumn<string>(
                name: "IbgeCityCode7",
                table: "Issuers",
                type: "nvarchar(7)",
                maxLength: 7,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MunicipalRegistration",
                table: "Issuers",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SimplesNationalOption",
                table: "Issuers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SimplesNationalRegime",
                table: "Issuers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SpecialTaxRegime",
                table: "Issuers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IbgeCityCode7",
                table: "Issuers");

            migrationBuilder.DropColumn(
                name: "MunicipalRegistration",
                table: "Issuers");

            migrationBuilder.DropColumn(
                name: "SimplesNationalOption",
                table: "Issuers");

            migrationBuilder.DropColumn(
                name: "SimplesNationalRegime",
                table: "Issuers");

            migrationBuilder.DropColumn(
                name: "SpecialTaxRegime",
                table: "Issuers");

            migrationBuilder.AlterColumn<string>(
                name: "TradeName",
                table: "Issuers",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);
        }
    }
}
