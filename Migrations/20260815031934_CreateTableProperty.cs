using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PropiedadesMinimalAPI.Migrations
{
    /// <inheritdoc />
    public partial class CreateTableProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Property",
                columns: table => new
                {
                    IdPropiedad = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombrePropiedad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ubicacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Activa = table.Column<bool>(type: "bit", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Property", x => x.IdPropiedad);
                });

            migrationBuilder.InsertData(
                table: "Property",
                columns: new[] { "IdPropiedad", "Activa", "Descripcion", "FechaCreacion", "NombrePropiedad", "Ubicacion" },
                values: new object[,]
                {
                    { 1, true, "Hermosa casa con jardín y alberca", new DateTime(2026, 8, 4, 21, 19, 34, 158, DateTimeKind.Local).AddTicks(9337), "Casa las palmas", "Tuxtla Gutierrez" },
                    { 2, true, "Cómodo departamento cerca del parque central", new DateTime(2026, 8, 6, 21, 19, 34, 158, DateTimeKind.Local).AddTicks(9356), "Departamento centro", "Tuxtla Gutierrez" },
                    { 3, false, "Amplia casa en zona residencial privada", new DateTime(2026, 7, 30, 21, 19, 34, 158, DateTimeKind.Local).AddTicks(9358), "Casa residencial", "Tuxtla Gutierrez" },
                    { 4, true, "Terreno de 500m2 con vista al bosque", new DateTime(2026, 8, 9, 21, 19, 34, 158, DateTimeKind.Local).AddTicks(9360), "Terreno el vergel", "Chiapa de Corzo" },
                    { 5, true, "Casa de dos pisos con terraza", new DateTime(2026, 7, 25, 21, 19, 34, 158, DateTimeKind.Local).AddTicks(9362), "Casa san miguel", "San Cristobal" },
                    { 6, false, "Departamento nuevo, totalmente equipado", new DateTime(2026, 8, 11, 21, 19, 34, 158, DateTimeKind.Local).AddTicks(9363), "Departamento moderno", "Tuxtla Gutierrez" },
                    { 7, true, "Propiedad rural con jardín y huerta", new DateTime(2026, 8, 2, 21, 19, 34, 158, DateTimeKind.Local).AddTicks(9365), "Casa campo", "Chiapa de Corzo" },
                    { 8, true, "Local en zona de alto tránsito peatonal", new DateTime(2026, 8, 7, 21, 19, 34, 158, DateTimeKind.Local).AddTicks(9366), "Local comercial", "Tuxtla Gutierrez" },
                    { 9, false, "Casa estilo colonial con 3 recámaras", new DateTime(2026, 7, 20, 21, 19, 34, 158, DateTimeKind.Local).AddTicks(9368), "Casa colonial", "San Cristobal" },
                    { 10, true, "Terreno con acceso principal para negocio", new DateTime(2026, 8, 13, 21, 19, 34, 158, DateTimeKind.Local).AddTicks(9369), "Terreno comercial", "Tuxtla Gutierrez" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Property");
        }
    }
}
