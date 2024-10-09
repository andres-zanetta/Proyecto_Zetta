using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proyecto_Zetta.DB.Migrations
{
    /// <inheritdoc />
    public partial class inicio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Localidad = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Telefono = table.Column<long>(type: "bigint", maxLength: 15, nullable: false),
                    Mail = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Aceptado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Formas_De_Pago",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Aceptado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Formas_De_Pago", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Instaladores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    Actividad = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Dni = table.Column<int>(type: "int", maxLength: 12, nullable: false),
                    Telefono = table.Column<long>(type: "bigint", maxLength: 15, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Aceptado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instaladores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Obras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Estado = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Materiales = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    FechaAlta = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaBaja = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AnexarServicio = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    InstaladorId = table.Column<int>(type: "int", nullable: false),
                    Aceptado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Obras", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Obras_Instaladores_InstaladorId",
                        column: x => x.InstaladorId,
                        principalTable: "Instaladores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Presupuestos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Estado = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    Insumos = table.Column<long>(type: "bigint", maxLength: 30, nullable: false),
                    ManodeObra = table.Column<long>(type: "bigint", maxLength: 30, nullable: false),
                    PrecioFinal = table.Column<long>(type: "bigint", maxLength: 30, nullable: false),
                    ObraId = table.Column<int>(type: "int", nullable: false),
                    ClienteId = table.Column<int>(type: "int", nullable: false),
                    Forma_de_PagoId = table.Column<int>(type: "int", nullable: false),
                    Aceptado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Presupuestos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Presupuestos_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Presupuestos_Formas_De_Pago_Forma_de_PagoId",
                        column: x => x.Forma_de_PagoId,
                        principalTable: "Formas_De_Pago",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Presupuestos_Obras_ObraId",
                        column: x => x.ObraId,
                        principalTable: "Obras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Mantenimientos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Estado = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FechaVisita = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PresupuestoId = table.Column<int>(type: "int", nullable: false),
                    Aceptado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mantenimientos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mantenimientos_Presupuestos_PresupuestoId",
                        column: x => x.PresupuestoId,
                        principalTable: "Presupuestos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Seguimientos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Estado = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Mantenimiento_SN = table.Column<bool>(type: "bit", nullable: false),
                    ObraId = table.Column<int>(type: "int", nullable: false),
                    MantenimientoId = table.Column<int>(type: "int", nullable: false),
                    Aceptado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seguimientos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Seguimientos_Mantenimientos_MantenimientoId",
                        column: x => x.MantenimientoId,
                        principalTable: "Mantenimientos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Seguimientos_Obras_ObraId",
                        column: x => x.ObraId,
                        principalTable: "Obras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "Cliente_Apellido_Nombre",
                table: "Clientes",
                columns: new[] { "Apellido", "Nombre" });

            migrationBuilder.CreateIndex(
                name: "Instalador_Apellido_Nombre",
                table: "Instaladores",
                columns: new[] { "Apellido", "Nombre" });

            migrationBuilder.CreateIndex(
                name: "IX_Mantenimientos_PresupuestoId",
                table: "Mantenimientos",
                column: "PresupuestoId");

            migrationBuilder.CreateIndex(
                name: "IX_Obras_InstaladorId",
                table: "Obras",
                column: "InstaladorId");

            migrationBuilder.CreateIndex(
                name: "IX_Presupuestos_ClienteId",
                table: "Presupuestos",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Presupuestos_Forma_de_PagoId",
                table: "Presupuestos",
                column: "Forma_de_PagoId");

            migrationBuilder.CreateIndex(
                name: "IX_Presupuestos_ObraId",
                table: "Presupuestos",
                column: "ObraId");

            migrationBuilder.CreateIndex(
                name: "IX_Seguimientos_MantenimientoId",
                table: "Seguimientos",
                column: "MantenimientoId");

            migrationBuilder.CreateIndex(
                name: "IX_Seguimientos_ObraId",
                table: "Seguimientos",
                column: "ObraId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Seguimientos");

            migrationBuilder.DropTable(
                name: "Mantenimientos");

            migrationBuilder.DropTable(
                name: "Presupuestos");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Formas_De_Pago");

            migrationBuilder.DropTable(
                name: "Obras");

            migrationBuilder.DropTable(
                name: "Instaladores");
        }
    }
}
