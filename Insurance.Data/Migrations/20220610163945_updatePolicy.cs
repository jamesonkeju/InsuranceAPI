using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Insurance.Data.Migrations
{
    public partial class updatePolicy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "CustomerPolicies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "CustomerPolicies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PolicyStartDate",
                table: "CustomerPolicies",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "PolicyStartEndDate",
                table: "CustomerPolicies",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "emailAddress",
                table: "CustomerPolicies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "localGovernment",
                table: "CustomerPolicies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "phoneNumber",
                table: "CustomerPolicies",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "CustomerPolicies");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "CustomerPolicies");

            migrationBuilder.DropColumn(
                name: "PolicyStartDate",
                table: "CustomerPolicies");

            migrationBuilder.DropColumn(
                name: "PolicyStartEndDate",
                table: "CustomerPolicies");

            migrationBuilder.DropColumn(
                name: "emailAddress",
                table: "CustomerPolicies");

            migrationBuilder.DropColumn(
                name: "localGovernment",
                table: "CustomerPolicies");

            migrationBuilder.DropColumn(
                name: "phoneNumber",
                table: "CustomerPolicies");
        }
    }
}
