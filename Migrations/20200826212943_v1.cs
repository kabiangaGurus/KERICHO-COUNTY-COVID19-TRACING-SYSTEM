using Microsoft.EntityFrameworkCore.Migrations;

namespace Covid19Tracing.Migrations
{
    public partial class v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rental_owners",
                columns: table => new
                {
                    staff_no = table.Column<int>(nullable: false),
                    Full_names = table.Column<string>(maxLength: 50, nullable: false),
                    Phone_number = table.Column<int>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    Role = table.Column<int>(nullable: false),
                    Department = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rental_owners", x => x.staff_no);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rental_owners");
        }
    }
}
