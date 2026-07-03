using System;
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
            migrationBuilder.AddColumn<DateTime>(
                name: "InFechaAlta",
                table: "Inscripciones",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InFechaAlta",
                table: "Inscripciones");
        }
    }
}
