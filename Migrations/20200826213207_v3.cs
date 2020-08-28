using Microsoft.EntityFrameworkCore.Migrations;

namespace Covid19Tracing.Migrations
{
    public partial class v3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Rental_owners",
                table: "Rental_owners");

            migrationBuilder.RenameTable(
                name: "Rental_owners",
                newName: "Administration");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Administration",
                table: "Administration",
                column: "staff_no");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Administration",
                table: "Administration");

            migrationBuilder.RenameTable(
                name: "Administration",
                newName: "Rental_owners");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rental_owners",
                table: "Rental_owners",
                column: "staff_no");
        }
    }
}
