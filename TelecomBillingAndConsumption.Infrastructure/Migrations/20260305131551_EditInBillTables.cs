using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TelecomBillingAndConsumption.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EditInBillTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_Subscribers_SubscriberId",
                table: "Bills");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Bills_BillId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_BillId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Bills_SubscriberId_BillingMonth_BillingYear",
                table: "Bills");

            migrationBuilder.DropIndex(
                name: "IX_BillDetails_BillId",
                table: "BillDetails");

            migrationBuilder.DropColumn(
                name: "BillingMonth",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "BillingYear",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "CallCost",
                table: "BillDetails");

            migrationBuilder.DropColumn(
                name: "DataCost",
                table: "BillDetails");

            migrationBuilder.DropColumn(
                name: "ExtraCharge",
                table: "BillDetails");

            migrationBuilder.DropColumn(
                name: "RoamingCharge",
                table: "BillDetails");

            migrationBuilder.DropColumn(
                name: "SmsCost",
                table: "BillDetails");

            migrationBuilder.AddColumn<string>(
                name: "Month",
                table: "Bills",
                type: "nvarchar(7)",
                maxLength: 7,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "DataMB",
                table: "BillDetails",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "OffPeakCalls",
                table: "BillDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PeakCalls",
                table: "BillDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Sms",
                table: "BillDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UsageRecordId1",
                table: "BillDetails",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Payments_BillId",
                table: "Payments",
                column: "BillId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bills_SubscriberId",
                table: "Bills",
                column: "SubscriberId");

            migrationBuilder.CreateIndex(
                name: "IX_BillDetails_BillId",
                table: "BillDetails",
                column: "BillId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BillDetails_UsageRecordId1",
                table: "BillDetails",
                column: "UsageRecordId1");

            migrationBuilder.AddForeignKey(
                name: "FK_BillDetails_UsageRecords_UsageRecordId1",
                table: "BillDetails",
                column: "UsageRecordId1",
                principalTable: "UsageRecords",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_Subscribers_SubscriberId",
                table: "Bills",
                column: "SubscriberId",
                principalTable: "Subscribers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Bills_BillId",
                table: "Payments",
                column: "BillId",
                principalTable: "Bills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillDetails_UsageRecords_UsageRecordId1",
                table: "BillDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Bills_Subscribers_SubscriberId",
                table: "Bills");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Bills_BillId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_BillId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Bills_SubscriberId",
                table: "Bills");

            migrationBuilder.DropIndex(
                name: "IX_BillDetails_BillId",
                table: "BillDetails");

            migrationBuilder.DropIndex(
                name: "IX_BillDetails_UsageRecordId1",
                table: "BillDetails");

            migrationBuilder.DropColumn(
                name: "Month",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "DataMB",
                table: "BillDetails");

            migrationBuilder.DropColumn(
                name: "OffPeakCalls",
                table: "BillDetails");

            migrationBuilder.DropColumn(
                name: "PeakCalls",
                table: "BillDetails");

            migrationBuilder.DropColumn(
                name: "Sms",
                table: "BillDetails");

            migrationBuilder.DropColumn(
                name: "UsageRecordId1",
                table: "BillDetails");

            migrationBuilder.AddColumn<int>(
                name: "BillingMonth",
                table: "Bills",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BillingYear",
                table: "Bills",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "CallCost",
                table: "BillDetails",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "DataCost",
                table: "BillDetails",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ExtraCharge",
                table: "BillDetails",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "RoamingCharge",
                table: "BillDetails",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "SmsCost",
                table: "BillDetails",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_Payments_BillId",
                table: "Payments",
                column: "BillId");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_SubscriberId_BillingMonth_BillingYear",
                table: "Bills",
                columns: new[] { "SubscriberId", "BillingMonth", "BillingYear" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BillDetails_BillId",
                table: "BillDetails",
                column: "BillId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_Subscribers_SubscriberId",
                table: "Bills",
                column: "SubscriberId",
                principalTable: "Subscribers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Bills_BillId",
                table: "Payments",
                column: "BillId",
                principalTable: "Bills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
