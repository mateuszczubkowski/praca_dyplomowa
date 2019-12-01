using Microsoft.EntityFrameworkCore.Migrations;

namespace CourierApp.WebApp.Migrations.CourierAppIdentityDb
{
    public partial class courierid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CourierId",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CourierId",
                table: "AspNetUsers");
        }
    }
}
