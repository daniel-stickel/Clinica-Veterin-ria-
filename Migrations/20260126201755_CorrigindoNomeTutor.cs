using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicaVeterinaria.Migrations
{
    public partial class CorrigindoNomeTutor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animais_Tutures_TutorId",
                table: "Animais");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tutures",
                table: "Tutures");

            migrationBuilder.RenameTable(
                name: "Tutures",
                newName: "Tutores");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tutores",
                table: "Tutores",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Animais_Tutores_TutorId",
                table: "Animais",
                column: "TutorId",
                principalTable: "Tutores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animais_Tutores_TutorId",
                table: "Animais");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tutores",
                table: "Tutores");

            migrationBuilder.RenameTable(
                name: "Tutores",
                newName: "Tutures");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tutures",
                table: "Tutures",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Animais_Tutures_TutorId",
                table: "Animais",
                column: "TutorId",
                principalTable: "Tutures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
