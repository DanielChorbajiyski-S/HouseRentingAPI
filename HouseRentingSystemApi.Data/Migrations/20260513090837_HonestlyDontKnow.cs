using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HouseRentingSystemApi.Data.Migrations
{
    /// <inheritdoc />
    public partial class HonestlyDontKnow : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "PricePerMonth",
                table: "Houses",
                type: "decimal(8,2)",
                precision: 8,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.UpdateData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: 4,
                column: "UserId",
                value: "85ac7242-1ea1-41b3-9638-423dc79e5ac8");

            migrationBuilder.UpdateData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: 5,
                column: "UserId",
                value: "85ac7242-1ea1-41b3-9638-423dc79e5ac8");

            migrationBuilder.UpdateData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: 6,
                column: "UserId",
                value: "85ac7242-1ea1-41b3-9638-423dc79e5ac8");

            migrationBuilder.UpdateData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: 7,
                column: "UserId",
                value: "85ac7242-1ea1-41b3-9638-423dc79e5ac8");

            migrationBuilder.UpdateData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: 8,
                column: "UserId",
                value: "85ac7242-1ea1-41b3-9638-423dc79e5ac8");

            migrationBuilder.UpdateData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: 9,
                column: "UserId",
                value: "85ac7242-1ea1-41b3-9638-423dc79e5ac8");

            migrationBuilder.UpdateData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: 10,
                column: "UserId",
                value: "85ac7242-1ea1-41b3-9638-423dc79e5ac8");

            migrationBuilder.UpdateData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: 11,
                column: "UserId",
                value: "85ac7242-1ea1-41b3-9638-423dc79e5ac8");

            migrationBuilder.UpdateData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: 12,
                column: "UserId",
                value: "85ac7242-1ea1-41b3-9638-423dc79e5ac8");

            migrationBuilder.UpdateData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: 13,
                column: "UserId",
                value: "85ac7242-1ea1-41b3-9638-423dc79e5ac8");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "PricePerMonth",
                table: "Houses",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)",
                oldPrecision: 8,
                oldScale: 2);

            migrationBuilder.UpdateData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: 4,
                column: "UserId",
                value: "df26734f-abc3-4aba-b5de-bea3da51c483");

            migrationBuilder.UpdateData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: 5,
                column: "UserId",
                value: "df26734f-abc3-4aba-b5de-bea3da51c483");

            migrationBuilder.UpdateData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: 6,
                column: "UserId",
                value: "df26734f-abc3-4aba-b5de-bea3da51c483");

            migrationBuilder.UpdateData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: 7,
                column: "UserId",
                value: "df26734f-abc3-4aba-b5de-bea3da51c483");

            migrationBuilder.UpdateData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: 8,
                column: "UserId",
                value: "df26734f-abc3-4aba-b5de-bea3da51c483");

            migrationBuilder.UpdateData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: 9,
                column: "UserId",
                value: "df26734f-abc3-4aba-b5de-bea3da51c483");

            migrationBuilder.UpdateData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: 10,
                column: "UserId",
                value: "df26734f-abc3-4aba-b5de-bea3da51c483");

            migrationBuilder.UpdateData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: 11,
                column: "UserId",
                value: "df26734f-abc3-4aba-b5de-bea3da51c483");

            migrationBuilder.UpdateData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: 12,
                column: "UserId",
                value: "df26734f-abc3-4aba-b5de-bea3da51c483");

            migrationBuilder.UpdateData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: 13,
                column: "UserId",
                value: "df26734f-abc3-4aba-b5de-bea3da51c483");
        }
    }
}
