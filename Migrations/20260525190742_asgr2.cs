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
            migrationBuilder.AddColumn<int>(
                name: "MaId",
                table: "Asistencia",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MateriasMaId",
                table: "Asistencia",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsId",
                table: "Asistencia",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsuariosUsId",
                table: "Asistencia",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Asistencia_MateriasMaId",
                table: "Asistencia",
                column: "MateriasMaId");

            migrationBuilder.CreateIndex(
                name: "IX_Asistencia_UsuariosUsId",
                table: "Asistencia",
                column: "UsuariosUsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Asistencia_Materias_MateriasMaId",
                table: "Asistencia",
                column: "MateriasMaId",
                principalTable: "Materias",
                principalColumn: "MaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Asistencia_Usuarios_UsuariosUsId",
                table: "Asistencia",
                column: "UsuariosUsId",
                principalTable: "Usuarios",
                principalColumn: "UsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Asistencia_Materias_MateriasMaId",
                table: "Asistencia");

            migrationBuilder.DropForeignKey(
                name: "FK_Asistencia_Usuarios_UsuariosUsId",
                table: "Asistencia");

            migrationBuilder.DropIndex(
                name: "IX_Asistencia_MateriasMaId",
                table: "Asistencia");

            migrationBuilder.DropIndex(
                name: "IX_Asistencia_UsuariosUsId",
                table: "Asistencia");

            migrationBuilder.DropColumn(
                name: "MaId",
                table: "Asistencia");

            migrationBuilder.DropColumn(
                name: "MateriasMaId",
                table: "Asistencia");

            migrationBuilder.DropColumn(
                name: "UsId",
                table: "Asistencia");

            migrationBuilder.DropColumn(
                name: "UsuariosUsId",
                table: "Asistencia");
        }
    }
}
