using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class SeAgregaColaborador : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rut",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "Colaborador",
                columns: table => new
                {
                    Rut = table.Column<string>(nullable: false),
                    DV = table.Column<string>(nullable: true),
                    Nombres = table.Column<string>(nullable: true),
                    ApellidoPat = table.Column<string>(nullable: true),
                    ApellidoMat = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Fono = table.Column<string>(nullable: true),
                    ContactoEmerg = table.Column<string>(nullable: true),
                    EmailCorp = table.Column<string>(nullable: true),
                    FonoCorp = table.Column<string>(nullable: true),
                    Direccion = table.Column<string>(nullable: true),
                    CargoId = table.Column<int>(nullable: false),
                    FotoUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colaborador", x => x.Rut);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Colaborador");

            migrationBuilder.AddColumn<string>(
                name: "Rut",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
