using Microsoft.EntityFrameworkCore.Migrations;

namespace taskWebapi.Migrations
{
    public partial class delid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "EmployeeDepartments");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "EmployeeDepartments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
