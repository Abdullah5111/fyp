using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApplyForJob.Migrations
{
    public partial class InitialCreate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "JobApplications",
                newName: "UserEmail");

            migrationBuilder.AddColumn<string>(
                name: "Applier",
                table: "JobApplications",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ApplyTime",
                table: "JobApplications",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Experience",
                table: "JobApplications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "JobId",
                table: "JobApplications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "JobApplications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<byte[]>(
                name: "Resume",
                table: "JobApplications",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Applier",
                table: "JobApplications");

            migrationBuilder.DropColumn(
                name: "ApplyTime",
                table: "JobApplications");

            migrationBuilder.DropColumn(
                name: "Experience",
                table: "JobApplications");

            migrationBuilder.DropColumn(
                name: "JobId",
                table: "JobApplications");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "JobApplications");

            migrationBuilder.DropColumn(
                name: "Resume",
                table: "JobApplications");

            migrationBuilder.RenameColumn(
                name: "UserEmail",
                table: "JobApplications",
                newName: "Name");
        }
    }
}
