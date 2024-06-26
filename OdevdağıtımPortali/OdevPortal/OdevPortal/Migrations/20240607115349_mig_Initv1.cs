using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OdevPortal.Migrations
{
    public partial class mig_Initv1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "odevId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_odevId",
                table: "AspNetUsers",
                column: "odevId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_odevs_odevId",
                table: "AspNetUsers",
                column: "odevId",
                principalTable: "odevs",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_odevs_odevId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_odevId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "odevId",
                table: "AspNetUsers");
        }
    }
}
