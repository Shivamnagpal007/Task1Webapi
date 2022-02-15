using Microsoft.EntityFrameworkCore.Migrations;

namespace taskWebapi.Migrations
{
    public partial class test1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    depId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    dname = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.depId);
                });

            migrationBuilder.CreateTable(
                name: "Designations",
                columns: table => new
                {
                    dsgId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    dsgname = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Designations", x => x.dsgId);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    empId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ename = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    eadd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    esal = table.Column<int>(type: "int", nullable: false),
                    dsgId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.empId);
                    table.ForeignKey(
                        name: "FK_Employees_Designations_dsgId",
                        column: x => x.dsgId,
                        principalTable: "Designations",
                        principalColumn: "dsgId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeDepartments",
                columns: table => new
                {
                    empId = table.Column<int>(type: "int", nullable: false),
                    depId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeDepartments", x => new { x.empId, x.depId });
                    table.ForeignKey(
                        name: "FK_EmployeeDepartments_Departments_depId",
                        column: x => x.depId,
                        principalTable: "Departments",
                        principalColumn: "depId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeDepartments_Employees_empId",
                        column: x => x.empId,
                        principalTable: "Employees",
                        principalColumn: "empId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDepartments_depId",
                table: "EmployeeDepartments",
                column: "depId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_dsgId",
                table: "Employees",
                column: "dsgId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeDepartments");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Designations");
        }
    }
}
