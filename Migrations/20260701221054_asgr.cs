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
                name: "Cohorte",
                columns: table => new
                {
                    CoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CoAnio = table.Column<int>(type: "int", nullable: false),
                    CoEstado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cohorte", x => x.CoId);
                });

            migrationBuilder.CreateTable(
                name: "Materias",
                columns: table => new
                {
                    MaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaDenominacion = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    MaModalidad = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    MaCantModulos = table.Column<int>(type: "int", nullable: false),
                    CaMaId = table.Column<int>(type: "int", nullable: false),
                    AsId = table.Column<int>(type: "int", nullable: false),
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
                    RoDenominacion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoId);
                });

            migrationBuilder.CreateTable(
                name: "Carreras",
                columns: table => new
                {
                    CaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CaDenominacion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MateriaMaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carreras", x => x.CaId);
                    table.ForeignKey(
                        name: "FK_Carreras_Materias_MateriaMaId",
                        column: x => x.MateriaMaId,
                        principalTable: "Materias",
                        principalColumn: "MaId");
                });

            migrationBuilder.CreateTable(
                name: "CarreraCohorte",
                columns: table => new
                {
                    CaCoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CaId = table.Column<int>(type: "int", nullable: false),
                    CoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarreraCohorte", x => x.CaCoId);
                    table.ForeignKey(
                        name: "FK_CarreraCohorte_Carreras_CaId",
                        column: x => x.CaId,
                        principalTable: "Carreras",
                        principalColumn: "CaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarreraCohorte_Cohorte_CoId",
                        column: x => x.CoId,
                        principalTable: "Cohorte",
                        principalColumn: "CoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    UsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsApellido = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UsNombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UsDni = table.Column<int>(type: "int", nullable: false),
                    UsEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UsContrasena = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoId = table.Column<int>(type: "int", nullable: false),
                    CaCoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.UsId);
                    table.ForeignKey(
                        name: "FK_Usuarios_CarreraCohorte_CaCoId",
                        column: x => x.CaCoId,
                        principalTable: "CarreraCohorte",
                        principalColumn: "CaCoId");
                    table.ForeignKey(
                        name: "FK_Usuarios_Roles_RoId",
                        column: x => x.RoId,
                        principalTable: "Roles",
                        principalColumn: "RoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CarrerasMaterias",
                columns: table => new
                {
                    CaMaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CaId = table.Column<int>(type: "int", nullable: false),
                    MaId = table.Column<int>(type: "int", nullable: false),
                    UsuarioUsId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarrerasMaterias", x => x.CaMaId);
                    table.ForeignKey(
                        name: "FK_CarrerasMaterias_Carreras_CaId",
                        column: x => x.CaId,
                        principalTable: "Carreras",
                        principalColumn: "CaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarrerasMaterias_Materias_MaId",
                        column: x => x.MaId,
                        principalTable: "Materias",
                        principalColumn: "MaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarrerasMaterias_Usuarios_UsuarioUsId",
                        column: x => x.UsuarioUsId,
                        principalTable: "Usuarios",
                        principalColumn: "UsId");
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
                    MateriasMaId = table.Column<int>(type: "int", nullable: true),
                    UsuarioUsId = table.Column<int>(type: "int", nullable: true)
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
                    table.ForeignKey(
                        name: "FK_Asistencias_Usuarios_UsuarioUsId",
                        column: x => x.UsuarioUsId,
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
                    CaMaId = table.Column<int>(type: "int", nullable: false),
                    UsuariosUsId = table.Column<int>(type: "int", nullable: true),
                    Carreras_MateriasCaMaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inscripciones", x => x.InId);
                    table.ForeignKey(
                        name: "FK_Inscripciones_CarrerasMaterias_Carreras_MateriasCaMaId",
                        column: x => x.Carreras_MateriasCaMaId,
                        principalTable: "CarrerasMaterias",
                        principalColumn: "CaMaId");
                    table.ForeignKey(
                        name: "FK_Inscripciones_Usuarios_UsuariosUsId",
                        column: x => x.UsuariosUsId,
                        principalTable: "Usuarios",
                        principalColumn: "UsId");
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
                name: "IX_Asistencias_UsuarioUsId",
                table: "Asistencias",
                column: "UsuarioUsId");

            migrationBuilder.CreateIndex(
                name: "IX_CarreraCohorte_CaId",
                table: "CarreraCohorte",
                column: "CaId");

            migrationBuilder.CreateIndex(
                name: "IX_CarreraCohorte_CoId",
                table: "CarreraCohorte",
                column: "CoId");

            migrationBuilder.CreateIndex(
                name: "IX_Carreras_MateriaMaId",
                table: "Carreras",
                column: "MateriaMaId");

            migrationBuilder.CreateIndex(
                name: "IX_CarrerasMaterias_CaId",
                table: "CarrerasMaterias",
                column: "CaId");

            migrationBuilder.CreateIndex(
                name: "IX_CarrerasMaterias_MaId",
                table: "CarrerasMaterias",
                column: "MaId");

            migrationBuilder.CreateIndex(
                name: "IX_CarrerasMaterias_UsuarioUsId",
                table: "CarrerasMaterias",
                column: "UsuarioUsId");

            migrationBuilder.CreateIndex(
                name: "IX_Inscripciones_Carreras_MateriasCaMaId",
                table: "Inscripciones",
                column: "Carreras_MateriasCaMaId");

            migrationBuilder.CreateIndex(
                name: "IX_Inscripciones_UsuariosUsId",
                table: "Inscripciones",
                column: "UsuariosUsId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_CaCoId",
                table: "Usuarios",
                column: "CaCoId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_RoId",
                table: "Usuarios",
                column: "RoId");
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
                name: "CarreraCohorte");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Carreras");

            migrationBuilder.DropTable(
                name: "Cohorte");

            migrationBuilder.DropTable(
                name: "Materias");
        }
    }
}
