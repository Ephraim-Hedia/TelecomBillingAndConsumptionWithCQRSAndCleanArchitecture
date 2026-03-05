using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TelecomBillingAndConsumption.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class editinbill3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillDetails_UsageRecords_UsageRecordId",
                table: "BillDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_BillDetails_UsageRecords_UsageRecordId1",
                table: "BillDetails");

            migrationBuilder.DropIndex(
                name: "IX_BillDetails_UsageRecordId1",
                table: "BillDetails");

            migrationBuilder.DropColumn(
                name: "UsageRecordId1",
                table: "BillDetails");

            migrationBuilder.AlterColumn<int>(
                name: "UsageRecordId",
                table: "BillDetails",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_BillDetails_UsageRecords_UsageRecordId",
                table: "BillDetails",
                column: "UsageRecordId",
                principalTable: "UsageRecords",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillDetails_UsageRecords_UsageRecordId",
                table: "BillDetails");

            migrationBuilder.AlterColumn<int>(
                name: "UsageRecordId",
                table: "BillDetails",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsageRecordId1",
                table: "BillDetails",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BillDetails_UsageRecordId1",
                table: "BillDetails",
                column: "UsageRecordId1");

            migrationBuilder.AddForeignKey(
                name: "FK_BillDetails_UsageRecords_UsageRecordId",
                table: "BillDetails",
                column: "UsageRecordId",
                principalTable: "UsageRecords",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BillDetails_UsageRecords_UsageRecordId1",
                table: "BillDetails",
                column: "UsageRecordId1",
                principalTable: "UsageRecords",
                principalColumn: "Id");
        }
    }
}
