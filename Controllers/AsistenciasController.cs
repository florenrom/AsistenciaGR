using AsistenciaGR.Data;
using AsistenciaGR.DTO;
using AsistenciaGR.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static AsistenciaGR.Models.AsistenciaGlobalViewModel;

namespace AsistenciaGR.Controllers
{
    public class AsistenciasController : Controller
    {
        private readonly AsistenciaGRContext _context;

        public AsistenciasController(AsistenciaGRContext context)
        {
            _context = context;
        }

        // GET: Asistencias
        public async Task<IActionResult> Index(int? selectedCarreraId, int? selectedMateriaId)
        {
            var carreras = await _context.Carreras
                .Select(c => new CarreraDetalleDto
                {
                    CaId = c.CaId,
                    CaDenominacion = c.CaDenominacion,
                    CarreraMateriasCount = c.CarreraMaterias != null ? c.CarreraMaterias.Count() : 0,
                    CarreraCohortesCount = c.CarreraCohortes != null ? c.CarreraCohortes.Count() : 0,
                })
                .ToListAsync();

            var materias = await _context.Materias
                .Select(m => new MateriaDetalleDto
                {
                    MaId = m.MaId,
                    MaDenominacion = m.MaDenominacion,
                    MaModalidad = m.MaModalidad,
                    MaCantModulos = m.MaCantModulos,
                    CarreraMateriasCount = m.CarreraMaterias != null ? m.CarreraMaterias.Count() : 0
                })
                .ToListAsync();

            var dto = new HomeIndexDto
            {
                Carreras = carreras,
                Materias = materias,
                SelectedCarreraId = selectedCarreraId,
                SelectedMateriaId = selectedMateriaId
            };

            return View(dto);
        }

        //GET: Asistencias/Asistencia
        //Vista estática para toma de asistencia(diseño)
        public async Task<IActionResult> Asistencia(int? CaMaId)
        {
            var model = new AsistenciaFormViewModel();
            if (CaMaId == null)
            {
                return View(model);
            }

            model.CaMaId = CaMaId;

            // find role 'Estudiante'
            var role = await _context.Roles.FirstOrDefaultAsync(r => r.RoDenominacion == "Estudiante");
            if (role == null)
            {
                return View(model);
            }

            // find users inscribed to this Carreras_Materias via Inscripciones
            var estudiantes = await _context.Inscripciones
                .Where(i => i.CaMaId == CaMaId)
                .Select(i => i.Usuarios)
                .Where(u => u != null && u.RoId == role.RoId)
                .Select(u => new { u.UsId, FullName = ((u.UsApellido ?? "") + " " + (u.UsNombre ?? "")).Trim() })
                .ToListAsync();

            foreach (var s in estudiantes)
            {
                model.Rows.Add(new AsistenciaRowViewModel { UsId = s.UsId, FullName = s.FullName });
            }

            var caMaName = await _context.CarrerasMaterias
                .Where(cm => cm.CaMaId == CaMaId)
                .FirstOrDefaultAsync();
            ViewData["CaMaDenominacion"] = caMaName;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Asistencia(AsistenciaFormViewModel model)
        {
            if (model.CaMaId == null)
            {
                ModelState.AddModelError(string.Empty, "Debe seleccionar una carrera/materia.");
                return View(model);
            }

            var carreraMateria = await _context.CarrerasMaterias.FindAsync(model.CaMaId.Value);

            foreach (var row in model.Rows)
            {
                var presente = row.Modulos != null && row.Modulos.Any(x => x);

                var entity = new Asistencia
                {
                    AsFecha = DateTime.Now,
                    AsPresente = presente,
                    AsJustificacion = row.AsJustificacion,
                    UsId = row.UsId,
                };

                _context.Asistencias.Add(entity);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Asistencias/AsistenciaGlobal
        // Vista estática para asistencia global (diseño)

        public async Task<IActionResult> AsistenciaGlobal(int? CaMaId)
        {
            var model = new AsistenciaGlobalViewModel();

            if (CaMaId == null)
            {
                return View(model);
            }

            model.CaMaId = CaMaId;

            // Buscar el rol Estudiante
            var role = await _context.Roles
                .FirstOrDefaultAsync(r => r.RoDenominacion == "Estudiante");

            if (role == null)
            {
                return View(model);
            }

            // Buscar los alumnos inscriptos en esa materia
            // Una sola consulta para traer alumnos y sus IDs
            var estudiantes = await _context.Inscripciones
                .Where(i => i.CaMaId == CaMaId)
                .Select(i => i.Usuarios)
                .Where(u => u != null && u.RoId == role.RoId)
                .ToListAsync();

            var usIdsInscritos = estudiantes
                .Select(u => u.UsId)
                .ToList(); // ya está en memoria, no va a la BD de nuevo

            // Paso 2: traer las asistencias de esos alumnos
            var todasLasAsistencias = await _context.Asistencias
            .Where(a => a.UsId.HasValue && usIdsInscritos.Contains(a.UsId.Value))
            .ToListAsync();

            model.Fechas = todasLasAsistencias
            .Where(a => a.AsFecha.HasValue)
            .Select(a => a.AsFecha.Value.Date)
            .Distinct()
            .OrderBy(f => f)
            .ToList();

            // Armar una fila por alumno
            foreach (var alumno in estudiantes)
            {
                var asistenciasAlumno = todasLasAsistencias
                    .Where(a => a.UsId == alumno.UsId)
                    .ToList();

                var asistenciaPorFecha = model.Fechas
                .ToDictionary(
                fecha => fecha,
                fecha => asistenciasAlumno
                .Any(a => a.AsFecha.HasValue && a.AsFecha.Value.Date == fecha && a.AsPresente)
                );

                int presentes = asistenciaPorFecha.Values.Count(v => v);
                int total = model.Fechas.Count;
                decimal porcentaje = total > 0 ? Math.Round((decimal)presentes / total * 100, 1) : 0;

                model.Rows.Add(new AsistenciaGlobalRowViewModel
                {
                    UsId = alumno.UsId,
                    FullName = $"{alumno.UsApellido} {alumno.UsNombre}",
                    AsistenciaPorFecha = asistenciaPorFecha,
                    PorcentajeAsistencia = porcentaje
                });
            }

            return View(model);
        }

        private bool AsistenciaExists(int id)
        {
            return _context.Asistencias.Any(e => e.AsId == id);
        }

        
    }
}