using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class mig4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PolyclinicId",
                table: "Appointment",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_PolyclinicId",
                table: "Appointment",
                column: "PolyclinicId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_Polyclinic_PolyclinicId",
                table: "Appointment",
                column: "PolyclinicId",
                principalTable: "Polyclinic",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_Polyclinic_PolyclinicId",
                table: "Appointment");

            migrationBuilder.DropIndex(
                name: "IX_Appointment_PolyclinicId",
                table: "Appointment");

            migrationBuilder.DropColumn(
                name: "PolyclinicId",
                table: "Appointment");
        }
    }
}
