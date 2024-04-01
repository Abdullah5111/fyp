using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyProfile.Migrations
{
    public partial class InitialCreate4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "CompanyProfiles");

            migrationBuilder.RenameColumn(
                name: "Country",
                table: "CompanyProfiles",
                newName: "Location");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Location",
                table: "CompanyProfiles",
                newName: "Country");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "CompanyProfiles",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
