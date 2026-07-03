using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AsistenciaGR.Migrations
{
    /// <inheritdoc />
    public partial class asgr3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "AsPorcentaje",
                table: "Asistencias",
                type: "decimal(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AsPorcentaje",
                table: "Asistencias");
        }
    }
}
