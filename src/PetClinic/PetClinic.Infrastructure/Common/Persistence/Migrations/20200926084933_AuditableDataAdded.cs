namespace PetClinic.Infrastructure.Common.Persistence.Migrations
{
    using System;
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AuditableDataAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "PetStatus",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "PetStatus",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "PetStatus",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "PetStatus",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Pets",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Pets",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "Pets",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "Pets",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "OfficeRooms",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "OfficeRooms",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "OfficeRooms",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "OfficeRooms",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Doctors",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Doctors",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "Doctors",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "Doctors",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Clients",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Clients",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "Clients",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "Clients",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Appointments",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Appointments",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "Appointments",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "Appointments",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "PetStatus");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "PetStatus");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "PetStatus");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "PetStatus");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "OfficeRooms");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "OfficeRooms");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "OfficeRooms");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "OfficeRooms");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "Appointments");
        }
    }
}
