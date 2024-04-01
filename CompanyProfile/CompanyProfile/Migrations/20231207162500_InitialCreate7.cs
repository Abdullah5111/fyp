using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyProfile.Migrations
{
    public partial class InitialCreate7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CompanyReviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyEmail = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EmployeeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReviewText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyReviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyReviews_Companies_CompanyEmail",
                        column: x => x.CompanyEmail,
                        principalTable: "Companies",
                        principalColumn: "Email",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyReviews_CompanyEmail",
                table: "CompanyReviews",
                column: "CompanyEmail");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyReviews");
        }
    }
}
