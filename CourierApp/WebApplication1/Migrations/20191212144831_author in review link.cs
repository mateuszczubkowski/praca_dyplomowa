using Microsoft.EntityFrameworkCore.Migrations;

namespace CourierApp.WebApp.Migrations
{
    public partial class authorinreviewlink : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "ReviewLinks",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Author",
                table: "ReviewLinks");
        }
    }
}
