using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AsistenciaGR.Migrations
{
    /// <inheritdoc />
    public partial class asgr2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Asistencia_Usuarios_UsuarioUsId",
                table: "Asistencia");

            migrationBuilder.DropForeignKey(
                name: "FK_Carreras_Materias_Inscripciones_InscripcionesInId",
                table: "Carreras_Materias");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Inscripciones_InscripcionesInId",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_InscripcionesInId",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Carreras_Materias_InscripcionesInId",
                table: "Carreras_Materias");

            migrationBuilder.DropColumn(
                name: "InscripcionesInId",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "InscripcionesInId",
                table: "Carreras_Materias");

            migrationBuilder.RenameColumn(
                name: "UsuarioUsId",
                table: "Asistencia",
                newName: "CaMaId");

            migrationBuilder.RenameIndex(
                name: "IX_Asistencia_UsuarioUsId",
                table: "Asistencia",
                newName: "IX_Asistencia_CaMaId");

            migrationBuilder.CreateIndex(
                name: "IX_Inscripciones_CaMaId",
                table: "Inscripciones",
                column: "CaMaId");

            migrationBuilder.CreateIndex(
                name: "IX_Inscripciones_UsId",
                table: "Inscripciones",
                column: "UsId");

            migrationBuilder.CreateIndex(
                name: "IX_Asistencia_UsId",
                table: "Asistencia",
                column: "UsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Asistencia_Carreras_Materias_CaMaId",
                table: "Asistencia",
                column: "CaMaId",
                principalTable: "Carreras_Materias",
                principalColumn: "CaMaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Asistencia_Usuarios_UsId",
                table: "Asistencia",
                column: "UsId",
                principalTable: "Usuarios",
                principalColumn: "UsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Inscripciones_Carreras_Materias_CaMaId",
                table: "Inscripciones",
                column: "CaMaId",
                principalTable: "Carreras_Materias",
                principalColumn: "CaMaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Inscripciones_Usuarios_UsId",
                table: "Inscripciones",
                column: "UsId",
                principalTable: "Usuarios",
                principalColumn: "UsId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Asistencia_Carreras_Materias_CaMaId",
                table: "Asistencia");

            migrationBuilder.DropForeignKey(
                name: "FK_Asistencia_Usuarios_UsId",
                table: "Asistencia");

            migrationBuilder.DropForeignKey(
                name: "FK_Inscripciones_Carreras_Materias_CaMaId",
                table: "Inscripciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Inscripciones_Usuarios_UsId",
                table: "Inscripciones");

            migrationBuilder.DropIndex(
                name: "IX_Inscripciones_CaMaId",
                table: "Inscripciones");

            migrationBuilder.DropIndex(
                name: "IX_Inscripciones_UsId",
                table: "Inscripciones");

            migrationBuilder.DropIndex(
                name: "IX_Asistencia_UsId",
                table: "Asistencia");

            migrationBuilder.RenameColumn(
                name: "CaMaId",
                table: "Asistencia",
                newName: "UsuarioUsId");

            migrationBuilder.RenameIndex(
                name: "IX_Asistencia_CaMaId",
                table: "Asistencia",
                newName: "IX_Asistencia_UsuarioUsId");

            migrationBuilder.AddColumn<int>(
                name: "InscripcionesInId",
                table: "Usuarios",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InscripcionesInId",
                table: "Carreras_Materias",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_InscripcionesInId",
                table: "Usuarios",
                column: "InscripcionesInId");

            migrationBuilder.CreateIndex(
                name: "IX_Carreras_Materias_InscripcionesInId",
                table: "Carreras_Materias",
                column: "InscripcionesInId");

            migrationBuilder.AddForeignKey(
                name: "FK_Asistencia_Usuarios_UsuarioUsId",
                table: "Asistencia",
                column: "UsuarioUsId",
                principalTable: "Usuarios",
                principalColumn: "UsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Carreras_Materias_Inscripciones_InscripcionesInId",
                table: "Carreras_Materias",
                column: "InscripcionesInId",
                principalTable: "Inscripciones",
                principalColumn: "InId");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Inscripciones_InscripcionesInId",
                table: "Usuarios",
                column: "InscripcionesInId",
                principalTable: "Inscripciones",
                principalColumn: "InId");
        }
    }
}
