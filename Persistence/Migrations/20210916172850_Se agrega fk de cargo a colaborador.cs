using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class Seagregafkdecargoacolaborador : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CargoId",
                table: "Colaborador",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Colaborador_CargoId",
                table: "Colaborador",
                column: "CargoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Colaborador_Cargo_CargoId",
                table: "Colaborador",
                column: "CargoId",
                principalTable: "Cargo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Colaborador_Cargo_CargoId",
                table: "Colaborador");

            migrationBuilder.DropIndex(
                name: "IX_Colaborador_CargoId",
                table: "Colaborador");

            migrationBuilder.AlterColumn<int>(
                name: "CargoId",
                table: "Colaborador",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
