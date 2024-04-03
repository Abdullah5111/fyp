using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApplyForJob.Migrations
{
    public partial class InitialCreate7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Resume",
                table: "JobApplications");

            migrationBuilder.AddColumn<int>(
                name: "ResumeId",
                table: "JobApplications",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResumeId",
                table: "JobApplications");

            migrationBuilder.AddColumn<byte[]>(
                name: "Resume",
                table: "JobApplications",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }
    }
}
