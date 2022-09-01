using Microsoft.EntityFrameworkCore.Migrations;

namespace Cliente.Api.Migrations
{
    public partial class Initialice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "gen");

            migrationBuilder.CreateTable(
                name: "tbPersona",
                schema: "gen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Genero = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Edad = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbPersona", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbCliente",
                schema: "gen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Contrasena = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbCliente", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbCliente_tbPersona_Id",
                        column: x => x.Id,
                        principalSchema: "gen",
                        principalTable: "tbPersona",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbCliente",
                schema: "gen");

            migrationBuilder.DropTable(
                name: "tbPersona",
                schema: "gen");
        }
    }
}
