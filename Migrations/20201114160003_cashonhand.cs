using Microsoft.EntityFrameworkCore.Migrations;

namespace MegaHack5.Migrations
{
    public partial class cashonhand : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "CashOnHand",
                table: "Planning",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CashOnHand",
                table: "Planning");
        }
    }
}
