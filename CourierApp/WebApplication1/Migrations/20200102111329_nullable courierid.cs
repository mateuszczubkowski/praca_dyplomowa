using Microsoft.EntityFrameworkCore.Migrations;

namespace CourierApp.WebApp.Migrations
{
    public partial class nullablecourierid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Packages_Couriers_CourierId",
                table: "Packages");

            migrationBuilder.AlterColumn<int>(
                name: "CourierId",
                table: "Packages",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Packages_Couriers_CourierId",
                table: "Packages",
                column: "CourierId",
                principalTable: "Couriers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Packages_Couriers_CourierId",
                table: "Packages");

            migrationBuilder.AlterColumn<int>(
                name: "CourierId",
                table: "Packages",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Packages_Couriers_CourierId",
                table: "Packages",
                column: "CourierId",
                principalTable: "Couriers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
