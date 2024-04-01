using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyProfile.Migrations
{
    public partial class InitialCreate5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CompanyDepartments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyEmail = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Department1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Department2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Salary1 = table.Column<double>(type: "float", nullable: false),
                    Role2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Salary2 = table.Column<double>(type: "float", nullable: false),
                    Role3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Salary3 = table.Column<double>(type: "float", nullable: false),
                    Role4 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Salary4 = table.Column<double>(type: "float", nullable: false),
                    Role5 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Salary5 = table.Column<double>(type: "float", nullable: false),
                    Role6 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Salary6 = table.Column<double>(type: "float", nullable: false),
                    Role7 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Salary7 = table.Column<double>(type: "float", nullable: false),
                    Role8 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Salary8 = table.Column<double>(type: "float", nullable: false),
                    Role9 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Salary9 = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyDepartments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyDepartments_Companies_CompanyEmail",
                        column: x => x.CompanyEmail,
                        principalTable: "Companies",
                        principalColumn: "Email",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyDepartments_CompanyEmail",
                table: "CompanyDepartments",
                column: "CompanyEmail",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyDepartments");
        }
    }
}
