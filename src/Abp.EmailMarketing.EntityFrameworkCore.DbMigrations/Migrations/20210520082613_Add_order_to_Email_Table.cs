using Microsoft.EntityFrameworkCore.Migrations;

namespace Abp.EmailMarketing.Migrations
{
    public partial class Add_order_to_Email_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "AppEmail",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "AppEmail");
        }
    }
}
