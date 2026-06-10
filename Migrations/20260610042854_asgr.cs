using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AsistenciaGR.Migrations
{
    /// <inheritdoc />
    public partial class asgr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Carreras",
                columns: table => new
                {
                    CaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CaDenominacion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carreras", x => x.CaId);
                });

            migrationBuilder.CreateTable(
                name: "Inscripciones",
                columns: table => new
                {
                    InId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsId = table.Column<int>(type: "int", nullable: false),
                    CaMaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inscripciones", x => x.InId);
                });

            migrationBuilder.CreateTable(
                name: "Materias",
                columns: table => new
                {
                    MaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaDenominacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaModalidad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaCantHoras = table.Column<int>(type: "int", nullable: false),
                    CaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materias", x => x.MaId);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoDenominacion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoId);
                });

            migrationBuilder.CreateTable(
                name: "Carreras_Materias",
                columns: table => new
                {
                    CaMaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CaMaDenominacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CaId = table.Column<int>(type: "int", nullable: false),
                    CarrerasCaId = table.Column<int>(type: "int", nullable: true),
                    MaId = table.Column<int>(type: "int", nullable: false),
                    MateriasMaId = table.Column<int>(type: "int", nullable: true),
                    InscripcionesInId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carreras_Materias", x => x.CaMaId);
                    table.ForeignKey(
                        name: "FK_Carreras_Materias_Carreras_CarrerasCaId",
                        column: x => x.CarrerasCaId,
                        principalTable: "Carreras",
                        principalColumn: "CaId");
                    table.ForeignKey(
                        name: "FK_Carreras_Materias_Inscripciones_InscripcionesInId",
                        column: x => x.InscripcionesInId,
                        principalTable: "Inscripciones",
                        principalColumn: "InId");
                    table.ForeignKey(
                        name: "FK_Carreras_Materias_Materias_MateriasMaId",
                        column: x => x.MateriasMaId,
                        principalTable: "Materias",
                        principalColumn: "MaId");
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    UsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsApellido = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UsNombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UsDNI = table.Column<int>(type: "int", nullable: false),
                    RoId = table.Column<int>(type: "int", nullable: false),
                    RolesRoId = table.Column<int>(type: "int", nullable: true),
                    InscripcionesInId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.UsId);
                    table.ForeignKey(
                        name: "FK_Usuarios_Inscripciones_InscripcionesInId",
                        column: x => x.InscripcionesInId,
                        principalTable: "Inscripciones",
                        principalColumn: "InId");
                    table.ForeignKey(
                        name: "FK_Usuarios_Roles_RolesRoId",
                        column: x => x.RolesRoId,
                        principalTable: "Roles",
                        principalColumn: "RoId");
                });

            migrationBuilder.CreateTable(
                name: "Asistencia",
                columns: table => new
                {
                    AsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AsFecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AsPresente = table.Column<bool>(type: "bit", nullable: false),
                    AsJustificacion = table.Column<bool>(type: "bit", nullable: false),
                    UsId = table.Column<int>(type: "int", nullable: true),
                    MaId = table.Column<int>(type: "int", nullable: true),
                    UsuarioUsId = table.Column<int>(type: "int", nullable: true),
                    MateriasMaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Asistencia", x => x.AsId);
                    table.ForeignKey(
                        name: "FK_Asistencia_Materias_MateriasMaId",
                        column: x => x.MateriasMaId,
                        principalTable: "Materias",
                        principalColumn: "MaId");
                    table.ForeignKey(
                        name: "FK_Asistencia_Usuarios_UsuarioUsId",
                        column: x => x.UsuarioUsId,
                        principalTable: "Usuarios",
                        principalColumn: "UsId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Asistencia_MateriasMaId",
                table: "Asistencia",
                column: "MateriasMaId");

            migrationBuilder.CreateIndex(
                name: "IX_Asistencia_UsuarioUsId",
                table: "Asistencia",
                column: "UsuarioUsId");

            migrationBuilder.CreateIndex(
                name: "IX_Carreras_Materias_CarrerasCaId",
                table: "Carreras_Materias",
                column: "CarrerasCaId");

            migrationBuilder.CreateIndex(
                name: "IX_Carreras_Materias_InscripcionesInId",
                table: "Carreras_Materias",
                column: "InscripcionesInId");

            migrationBuilder.CreateIndex(
                name: "IX_Carreras_Materias_MateriasMaId",
                table: "Carreras_Materias",
                column: "MateriasMaId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_InscripcionesInId",
                table: "Usuarios",
                column: "InscripcionesInId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_RolesRoId",
                table: "Usuarios",
                column: "RolesRoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Asistencia");

            migrationBuilder.DropTable(
                name: "Carreras_Materias");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Carreras");

            migrationBuilder.DropTable(
                name: "Materias");

            migrationBuilder.DropTable(
                name: "Inscripciones");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
