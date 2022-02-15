using Microsoft.EntityFrameworkCore.Migrations;

namespace taskWebapi.Migrations
{
    public partial class addid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "EmployeeDepartments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "EmployeeDepartments");
        }
    }
}
