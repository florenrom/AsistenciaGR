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
                name: "CarrerasMaterias",
                columns: table => new
                {
                    CaMaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CaMaDenominacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CaId = table.Column<int>(type: "int", nullable: false),
                    CarrerasCaId = table.Column<int>(type: "int", nullable: true),
                    MaId = table.Column<int>(type: "int", nullable: false),
                    MateriasMaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarrerasMaterias", x => x.CaMaId);
                    table.ForeignKey(
                        name: "FK_CarrerasMaterias_Carreras_CarrerasCaId",
                        column: x => x.CarrerasCaId,
                        principalTable: "Carreras",
                        principalColumn: "CaId");
                    table.ForeignKey(
                        name: "FK_CarrerasMaterias_Materias_MateriasMaId",
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
                    RolesRoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.UsId);
                    table.ForeignKey(
                        name: "FK_Usuarios_Roles_RolesRoId",
                        column: x => x.RolesRoId,
                        principalTable: "Roles",
                        principalColumn: "RoId");
                });

            migrationBuilder.CreateTable(
                name: "Asistencias",
                columns: table => new
                {
                    AsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AsFecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AsPresente = table.Column<bool>(type: "bit", nullable: false),
                    AsJustificacion = table.Column<bool>(type: "bit", nullable: false),
                    UsId = table.Column<int>(type: "int", nullable: true),
                    MaId = table.Column<int>(type: "int", nullable: true),
                    CaMaId = table.Column<int>(type: "int", nullable: true),
                    MateriasMaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Asistencias", x => x.AsId);
                    table.ForeignKey(
                        name: "FK_Asistencias_CarrerasMaterias_CaMaId",
                        column: x => x.CaMaId,
                        principalTable: "CarrerasMaterias",
                        principalColumn: "CaMaId");
                    table.ForeignKey(
                        name: "FK_Asistencias_Materias_MateriasMaId",
                        column: x => x.MateriasMaId,
                        principalTable: "Materias",
                        principalColumn: "MaId");
                    table.ForeignKey(
                        name: "FK_Asistencias_Usuarios_UsId",
                        column: x => x.UsId,
                        principalTable: "Usuarios",
                        principalColumn: "UsId");
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
                    table.ForeignKey(
                        name: "FK_Inscripciones_CarrerasMaterias_CaMaId",
                        column: x => x.CaMaId,
                        principalTable: "CarrerasMaterias",
                        principalColumn: "CaMaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Inscripciones_Usuarios_UsId",
                        column: x => x.UsId,
                        principalTable: "Usuarios",
                        principalColumn: "UsId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Asistencias_CaMaId",
                table: "Asistencias",
                column: "CaMaId");

            migrationBuilder.CreateIndex(
                name: "IX_Asistencias_MateriasMaId",
                table: "Asistencias",
                column: "MateriasMaId");

            migrationBuilder.CreateIndex(
                name: "IX_Asistencias_UsId",
                table: "Asistencias",
                column: "UsId");

            migrationBuilder.CreateIndex(
                name: "IX_CarrerasMaterias_CarrerasCaId",
                table: "CarrerasMaterias",
                column: "CarrerasCaId");

            migrationBuilder.CreateIndex(
                name: "IX_CarrerasMaterias_MateriasMaId",
                table: "CarrerasMaterias",
                column: "MateriasMaId");

            migrationBuilder.CreateIndex(
                name: "IX_Inscripciones_CaMaId",
                table: "Inscripciones",
                column: "CaMaId");

            migrationBuilder.CreateIndex(
                name: "IX_Inscripciones_UsId",
                table: "Inscripciones",
                column: "UsId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_RolesRoId",
                table: "Usuarios",
                column: "RolesRoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Asistencias");

            migrationBuilder.DropTable(
                name: "Inscripciones");

            migrationBuilder.DropTable(
                name: "CarrerasMaterias");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Carreras");

            migrationBuilder.DropTable(
                name: "Materias");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
