using Microsoft.EntityFrameworkCore.Migrations;

namespace Customers5._0.Migrations
{
    public partial class AddNewColumnInCustomerDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "email",
                table: "customer",
                type: "varchar(150)",
                maxLength: 150,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "customer",
                keyColumn: "id",
                keyValue: 1L,
                column: "email",
                value: "thaina.biudes@gmail.com");

            migrationBuilder.UpdateData(
                table: "customer",
                keyColumn: "id",
                keyValue: 2L,
                column: "email",
                value: "ina_biudes@hotmail.com");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "email",
                table: "customer");
        }
    }
}
