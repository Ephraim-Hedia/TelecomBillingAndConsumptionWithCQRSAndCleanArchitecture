using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TelecomBillingAndConsumption.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTariffRuleTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TariffRules_UsageType_IsRoaming_IsPeak_EffectiveFrom",
                table: "TariffRules");

            migrationBuilder.DropColumn(
                name: "EffectiveFrom",
                table: "TariffRules");

            migrationBuilder.DropColumn(
                name: "EffectiveTo",
                table: "TariffRules");

            migrationBuilder.CreateIndex(
                name: "IX_TariffRules_UsageType_IsRoaming_IsPeak",
                table: "TariffRules",
                columns: new[] { "UsageType", "IsRoaming", "IsPeak" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TariffRules_UsageType_IsRoaming_IsPeak",
                table: "TariffRules");

            migrationBuilder.AddColumn<DateTime>(
                name: "EffectiveFrom",
                table: "TariffRules",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "EffectiveTo",
                table: "TariffRules",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TariffRules_UsageType_IsRoaming_IsPeak_EffectiveFrom",
                table: "TariffRules",
                columns: new[] { "UsageType", "IsRoaming", "IsPeak", "EffectiveFrom" });
        }
    }
}
