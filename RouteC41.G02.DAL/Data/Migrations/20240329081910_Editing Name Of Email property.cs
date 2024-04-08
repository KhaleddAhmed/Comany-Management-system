using Microsoft.EntityFrameworkCore.Migrations;

namespace RouteC41.G02.DAL.Data.Migrations
{
    public partial class EditingNameOfEmailproperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Adrress",
                table: "Employees",
                newName: "EmailAddress");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EmailAddress",
                table: "Employees",
                newName: "Adrress");
        }
    }
}
