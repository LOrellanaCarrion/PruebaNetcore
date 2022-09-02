using Microsoft.EntityFrameworkCore.Migrations;

namespace Cuentas.Api.Migrations
{
    public partial class initialize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "cta");

            migrationBuilder.CreateTable(
                name: "tbCuenta",
                schema: "cta",
                columns: table => new
                {
                    NumeroCuenta = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: false),
                    TipoCuenta = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SaldoInicial = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false),
                    IdCliente = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbCuenta", x => x.NumeroCuenta);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbCuenta",
                schema: "cta");
        }
    }
}
