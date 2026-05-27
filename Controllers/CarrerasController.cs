using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AsistenciaGR.Data;
using AsistenciaGR.Models;

namespace AsistenciaGR.Controllers
{
    public class CarrerasController : Controller
    {
        private readonly AsistenciaGRContext _context;
        private readonly Microsoft.Extensions.Logging.ILogger<CarrerasController> _logger;

        public CarrerasController(AsistenciaGRContext context, Microsoft.Extensions.Logging.ILogger<CarrerasController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Carreras
        public async Task<IActionResult> Index()
        {
            return View(await _context.Carreras.ToListAsync());
        }

        // GET: Carreras/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carreras = await _context.Carreras
                .FirstOrDefaultAsync(m => m.CaId == id);
            if (carreras == null)
            {
                return NotFound();
            }

            return View(carreras);
        }

        // GET: Carreras/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Carreras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CaId,CaDenominacion")] Carreras carreras)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _logger.LogInformation("Attempting to create Carrera: {Denom}", carreras.CaDenominacion);
                    _context.Add(carreras);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation("Created Carrera with Id {Id}", carreras.CaId);
                    // If this is an AJAX request (fetch), return JSON so client can handle redirect
                    if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                    {
                        return Json(new { success = true, redirectUrl = Url.Action(nameof(Index)) });
                    }
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    // Surface exception to the model state so it is visible in the view
                    _logger.LogError(ex, "Error saving Carrera");
                    ModelState.AddModelError(string.Empty, "Error saving data: " + ex.Message);
                }
            }

            // If we got here something failed — collect model state errors for debugging/display
            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).Where(m => !string.IsNullOrEmpty(m)).ToList();
            if (errors.Any())
            {
                ViewData["ModelErrors"] = errors;
            }

            // If AJAX request, return errors as JSON for easier client-side handling
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return Json(new { success = false, errors });
            }

            // Provide a detailed dump of ModelState and model for easier debugging in the view
            try
            {
                var details = new System.Text.StringBuilder();
                details.AppendLine("ModelState errors:");
                foreach (var kv in ModelState)
                {
                    details.AppendLine($"Key: {kv.Key}");
                    foreach (var err in kv.Value.Errors)
                    {
                        details.AppendLine(" - " + err.ErrorMessage + " | " + err.Exception?.Message);
                    }
                }
                details.AppendLine("\nModel values:");
                details.AppendLine($"CaId: {carreras?.CaId}");
                details.AppendLine($"CaDenominacion: {carreras?.CaDenominacion}");
                ViewData["ModelErrorsDetailed"] = details.ToString();
            }
            catch { }

            return View(carreras);
        }

        // GET: Carreras/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carreras = await _context.Carreras.FindAsync(id);
            if (carreras == null)
            {
                return NotFound();
            }
            return View(carreras);
        }

        // POST: Carreras/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CaId,CaDenominacion")] Carreras carreras)
        {
            if (id != carreras.CaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carreras);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarrerasExists(carreras.CaId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(carreras);
        }

        // GET: Carreras/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carreras = await _context.Carreras
                .FirstOrDefaultAsync(m => m.CaId == id);
            if (carreras == null)
            {
                return NotFound();
            }

            return View(carreras);
        }

        // POST: Carreras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var carreras = await _context.Carreras.FindAsync(id);
            if (carreras != null)
            {
                _context.Carreras.Remove(carreras);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarrerasExists(int id)
        {
            return _context.Carreras.Any(e => e.CaId == id);
        }
    }
}
