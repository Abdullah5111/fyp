using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyProfile.Migrations
{
    public partial class InitialCreate3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CompanyInsights",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyEmail = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    EstablishedSince = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OrdersCompleted = table.Column<int>(type: "int", nullable: true),
                    EstimatedRevenue = table.Column<double>(type: "float", nullable: true),
                    ProductsSold = table.Column<int>(type: "int", nullable: true),
                    SatisfiedCustomers = table.Column<int>(type: "int", nullable: true),
                    CustomerGrowthPercentage = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyInsights", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyInsights_Companies_CompanyEmail",
                        column: x => x.CompanyEmail,
                        principalTable: "Companies",
                        principalColumn: "Email",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyInsights_CompanyEmail",
                table: "CompanyInsights",
                column: "CompanyEmail",
                unique: true,
                filter: "[CompanyEmail] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyInsights");
        }
    }
}
