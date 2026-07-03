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
            // Support alternate input names coming from the view/form (e.g. SelectedCarreraId/SelectedMateriaId)
            if (!selectedCarreraId.HasValue)
            {
                var s = (Request.HasFormContentType ? Request.Form["SelectedCarreraId"].FirstOrDefault() : null) ?? Request.Query["SelectedCarreraId"].FirstOrDefault();
                if (!string.IsNullOrEmpty(s) && int.TryParse(s, out var v1)) selectedCarreraId = v1;
            }
            if (!selectedMateriaId.HasValue)
            {
                var s2 = (Request.HasFormContentType ? Request.Form["SelectedMateriaId"].FirstOrDefault() : null) ?? Request.Query["SelectedMateriaId"].FirstOrDefault();
                if (!string.IsNullOrEmpty(s2) && int.TryParse(s2, out var v2)) selectedMateriaId = v2;
            }
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

            var modelDto = new HomeIndexDto
            {
                Carreras = carreras,
                Materias = materias,
                SelectedCarreraId = selectedCarreraId,
                SelectedMateriaId = selectedMateriaId
            };

            // If both Carrera and Materia were selected, resolve the corresponding CaMaId
            if (selectedCarreraId.HasValue && selectedMateriaId.HasValue)
            {
                var caMa = await _context.CarrerasMaterias
                    .FirstOrDefaultAsync(cm => cm.CaId == selectedCarreraId.Value && cm.MaId == selectedMateriaId.Value);

                if (caMa != null)
                {
                    // redirect to Asistencia action with the resolved CaMaId
                    return RedirectToAction(nameof(Asistencia), new { CaMaId = caMa.CaMaId });
                }

                // if no matching CarreraMateria found, add model error and show index with message
                ModelState.AddModelError(string.Empty, "No existe una relación Carrera-Materia para la selección realizada.");
            }

            return View(modelDto);
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

            // find role 'Estudiante' (case-insensitive)
            var role = await _context.Roles.FirstOrDefaultAsync(r => r.RoDenominacion.ToLower() == "estudiante");
            if (role == null)
            {
                // no role configured; return empty model
                return View(model);
            }

            // find users inscribed to this Carreras_Materias via Inscripciones — ensure we load Usuario and its Rol
            var estudiantes = await (from i in _context.Inscripciones
                                     join u in _context.Usuarios on i.UsId equals u.UsId
                                     where i.CaMaId == CaMaId && u.RoId == role.RoId
                                     select new { u.UsId, FullName = ((u.UsApellido ?? "") + " " + (u.UsNombre ?? "")).Trim() })
                                    .ToListAsync();

            // determine number of modules for this materia
            int maCantModulos = 1; // default
            var caMa = await _context.CarrerasMaterias.FirstOrDefaultAsync(cm => cm.CaMaId == CaMaId);
            if (caMa != null)
            {
                var materia = await _context.Materias.FindAsync(caMa.MaId);
                if (materia != null)
                {
                    if (materia.MaCantModulos.HasValue && materia.MaCantModulos.Value > 0)
                    {
                        maCantModulos = materia.MaCantModulos.Value;
                    }
                    else
                    {
                        maCantModulos = 1;
                    }
                }
            }

            foreach (var s in estudiantes)
            {
                var row = new AsistenciaRowViewModel { UsId = s.UsId, FullName = s.FullName };
                // initialize Modulos list according to MaCantModulos
                row.Modulos = Enumerable.Range(0, maCantModulos).Select(_ => false).ToList();
                model.Rows.Add(row);
            }

            ViewData["MaCantModulos"] = maCantModulos;
            model.ModuleCount = maCantModulos;

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

            int moduleCount = model.ModuleCount > 0 ? model.ModuleCount : 1;
            foreach (var row in model.Rows)
            {
                var checkedCount = row.Modulos != null ? row.Modulos.Count(x => x) : 0;
                var presente = checkedCount > 0;
                decimal porcentaje = 0;
                if (moduleCount > 0)
                {
                    porcentaje = Math.Round((decimal)checkedCount / moduleCount * 100, 1);
                }

                var entity = new Asistencia
                {
                    AsFecha = DateTime.Now,
                    AsPresente = presente,
                    AsJustificacion = row.AsJustificacion,
                    UsId = row.UsId,
                    AsPorcentaje = porcentaje
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

            // Buscar el rol Estudiante (case-insensitive)
            var role = await _context.Roles
                .FirstOrDefaultAsync(r => r.RoDenominacion.ToLower() == "estudiante");

            if (role == null)
            {
                return View(model);
            }

            // Buscar los alumnos inscriptos en esa materia (únicos)
            var estudiantes = await (from i in _context.Inscripciones
                                     join u in _context.Usuarios on i.UsId equals u.UsId
                                     where i.CaMaId == CaMaId && u.RoId == role.RoId
                                     select u)
                                    .Distinct()
                                    .ToListAsync();

            var usIdsInscritos = estudiantes.Select(u => u.UsId).ToList();

            // Paso 2: traer las asistencias de esos alumnos filtradas por la materia
            var todasLasAsistencias = await _context.Asistencias
                .Where(a => a.CaMaId == CaMaId && a.UsId.HasValue && usIdsInscritos.Contains(a.UsId.Value))
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

                // Para cada fecha buscamos la asistencia correspondiente y usamos AsPorcentaje si está disponible
                var asistenciaPorFecha = new Dictionary<DateTime, decimal>();
                decimal sumaPorcentajes = 0m;
                foreach (var fecha in model.Fechas)
                {
                    var registro = asistenciasAlumno
                        .FirstOrDefault(a => a.AsFecha.HasValue && a.AsFecha.Value.Date == fecha);
                    decimal pct = 0m;
                    if (registro != null)
                    {
                        if (registro.AsPorcentaje.HasValue)
                        {
                            pct = registro.AsPorcentaje.Value;
                        }
                        else
                        {
                            pct = registro.AsPresente ? 100m : 0m;
                        }
                    }
                    asistenciaPorFecha[fecha] = pct;
                    sumaPorcentajes += pct;
                }

                int totalFechas = model.Fechas.Count;
                decimal promedio = totalFechas > 0 ? Math.Round(sumaPorcentajes / totalFechas, 1) : 0m;

                model.Rows.Add(new AsistenciaGlobalRowViewModel
                {
                    UsId = alumno.UsId,
                    FullName = $"{alumno.UsApellido} {alumno.UsNombre}",
                    AsistenciaPorFecha = asistenciaPorFecha,
                    PorcentajeAsistencia = promedio
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