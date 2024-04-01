using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyProfile.Migrations
{
    public partial class InitialCreate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CompanyProfiles_CompanyEmail",
                table: "CompanyProfiles");

            migrationBuilder.AlterColumn<string>(
                name: "CompanyEmail",
                table: "CompanyProfiles",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyProfiles_CompanyEmail",
                table: "CompanyProfiles",
                column: "CompanyEmail",
                unique: true,
                filter: "[CompanyEmail] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CompanyProfiles_CompanyEmail",
                table: "CompanyProfiles");

            migrationBuilder.AlterColumn<string>(
                name: "CompanyEmail",
                table: "CompanyProfiles",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompanyProfiles_CompanyEmail",
                table: "CompanyProfiles",
                column: "CompanyEmail",
                unique: true);
        }
    }
}
