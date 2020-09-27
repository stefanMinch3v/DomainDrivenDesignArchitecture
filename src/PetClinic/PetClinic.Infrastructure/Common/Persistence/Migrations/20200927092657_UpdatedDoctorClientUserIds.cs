using Microsoft.EntityFrameworkCore.Migrations;

namespace PetClinic.Infrastructure.Common.Presistence.Migrations
{
    public partial class UpdatedDoctorClientUserIds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OfficeRoomId",
                table: "Appointments");

            migrationBuilder.AddColumn<string>(
                name: "ClientUserId",
                table: "Appointments",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DoctorUserId",
                table: "Appointments",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClientUserId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "DoctorUserId",
                table: "Appointments");

            migrationBuilder.AddColumn<int>(
                name: "OfficeRoomId",
                table: "Appointments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
