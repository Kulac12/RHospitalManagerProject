using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class AddPolyclinicFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_User_PoliklinikId",
                table: "User",
                column: "PoliklinikId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Polyclinic_PoliklinikId",
                table: "User",
                column: "PoliklinikId",
                principalTable: "Polyclinic",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Polyclinic_PoliklinikId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_PoliklinikId",
                table: "User");
        }
    }
}
