using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AsistenciaGR.Data;
using AsistenciaGR.Models;

namespace AsistenciaGR.Controllers
{
    public class Carreras_MateriasController : Controller
    {
        private readonly AsistenciaGRContext _context;

        public Carreras_MateriasController(AsistenciaGRContext context)
        {
            _context = context;
        }

        // GET: Carreras_Materias
        public async Task<IActionResult> Index()
        {
            return View(await _context.Carreras_Materias.ToListAsync());
        }

        // GET: Carreras_Materias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carreras_Materias = await _context.Carreras_Materias
                .FirstOrDefaultAsync(m => m.CaMaId == id);
            if (carreras_Materias == null)
            {
                return NotFound();
            }

            return View(carreras_Materias);
        }

        // GET: Carreras_Materias/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Carreras_Materias/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CaMaId,CaId,MaId")] Carreras_Materias carreras_Materias)
        {
            if (ModelState.IsValid)
            {
                _context.Add(carreras_Materias);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(carreras_Materias);
        }

        // GET: Carreras_Materias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carreras_Materias = await _context.Carreras_Materias.FindAsync(id);
            if (carreras_Materias == null)
            {
                return NotFound();
            }
            return View(carreras_Materias);
        }

        // POST: Carreras_Materias/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CaMaId,CaId,MaId")] Carreras_Materias carreras_Materias)
        {
            if (id != carreras_Materias.CaMaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carreras_Materias);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Carreras_MateriasExists(carreras_Materias.CaMaId))
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
            return View(carreras_Materias);
        }

        // GET: Carreras_Materias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carreras_Materias = await _context.Carreras_Materias
                .FirstOrDefaultAsync(m => m.CaMaId == id);
            if (carreras_Materias == null)
            {
                return NotFound();
            }

            return View(carreras_Materias);
        }

        // POST: Carreras_Materias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var carreras_Materias = await _context.Carreras_Materias.FindAsync(id);
            if (carreras_Materias != null)
            {
                _context.Carreras_Materias.Remove(carreras_Materias);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Carreras_MateriasExists(int id)
        {
            return _context.Carreras_Materias.Any(e => e.CaMaId == id);
        }
    }
}
