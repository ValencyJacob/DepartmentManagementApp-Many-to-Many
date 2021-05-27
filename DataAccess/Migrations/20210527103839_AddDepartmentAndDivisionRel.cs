using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class AddDepartmentAndDivisionRel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DepartmentDivisionsModel",
                columns: table => new
                {
                    Department_Id = table.Column<int>(type: "int", nullable: false),
                    Division_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentDivisionsModel", x => new { x.Division_Id, x.Department_Id });
                    table.ForeignKey(
                        name: "FK_DepartmentDivisionsModel_Departments_Department_Id",
                        column: x => x.Department_Id,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DepartmentDivisionsModel_Divisions_Division_Id",
                        column: x => x.Division_Id,
                        principalTable: "Divisions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentDivisionsModel_Department_Id",
                table: "DepartmentDivisionsModel",
                column: "Department_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DepartmentDivisionsModel");

            migrationBuilder.DropTable(
                name: "Departments");
        }
    }
}
