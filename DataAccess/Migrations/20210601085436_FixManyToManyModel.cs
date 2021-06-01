using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class FixManyToManyModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DivisionEmployeeDivision_Id",
                table: "EmployeePositions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DivisionEmployeeEmployee_Id",
                table: "EmployeePositions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePositions_DivisionEmployeeEmployee_Id_DivisionEmployeeDivision_Id",
                table: "EmployeePositions",
                columns: new[] { "DivisionEmployeeEmployee_Id", "DivisionEmployeeDivision_Id" });

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeePositions_DivisionEmployeesModel_DivisionEmployeeEmployee_Id_DivisionEmployeeDivision_Id",
                table: "EmployeePositions",
                columns: new[] { "DivisionEmployeeEmployee_Id", "DivisionEmployeeDivision_Id" },
                principalTable: "DivisionEmployeesModel",
                principalColumns: new[] { "Employee_Id", "Division_Id" },
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeePositions_DivisionEmployeesModel_DivisionEmployeeEmployee_Id_DivisionEmployeeDivision_Id",
                table: "EmployeePositions");

            migrationBuilder.DropIndex(
                name: "IX_EmployeePositions_DivisionEmployeeEmployee_Id_DivisionEmployeeDivision_Id",
                table: "EmployeePositions");

            migrationBuilder.DropColumn(
                name: "DivisionEmployeeDivision_Id",
                table: "EmployeePositions");

            migrationBuilder.DropColumn(
                name: "DivisionEmployeeEmployee_Id",
                table: "EmployeePositions");
        }
    }
}
