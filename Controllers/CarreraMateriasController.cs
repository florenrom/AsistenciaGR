
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using AsistenciaGR.Models;
using AsistenciaGR.Data;

public class CarreraMateriasController : Controller
{
    private readonly AsistenciaGRContext _context;

    public CarreraMateriasController(AsistenciaGRContext context)
    {
        _context = context;
    }

    // GET: CARRERAS_MATERIASS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.CarrerasMaterias.ToListAsync());
    }

    // GET: CARRERAS_MATERIASS/Details/5
    public async Task<IActionResult> Details(int? camaid)
    {
        if (camaid == null)
        {
            return NotFound();
        }

        var carreras_materias = await _context.CarrerasMaterias
            .FirstOrDefaultAsync(m => m.CaMaId == camaid);
        if (carreras_materias == null)
        {
            return NotFound();
        }

        return View(carreras_materias);
    }

    // GET: CARRERAS_MATERIASS/Create
    public IActionResult Create()
    {
        ViewData["CaId"] = new SelectList(_context.Carreras, "CaId", "CaDenominacion");
        ViewData["MaId"] = new SelectList(_context.Materias, "MaId", "MaDenominacion");
        return View();
    }

    // POST: CARRERAS_MATERIASS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("CaMaId,CaMaDenominacion,CaId,Carreras,MaId,Materias,Inscripciones")] CarreraMateria carreras_materias)
    {
        if (ModelState.IsValid)
        {
            _context.Add(carreras_materias);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        // repopulate selects on error
        ViewData["CaId"] = new SelectList(_context.Carreras, "CaId", "CaDenominacion", carreras_materias.CaId);
        ViewData["MaId"] = new SelectList(_context.Materias, "MaId", "MaDenominacion", carreras_materias.MaId);
        return View(carreras_materias);
    }

    // GET: CARRERAS_MATERIASS/Edit/5
    public async Task<IActionResult> Edit(int? camaid)
    {
        if (camaid == null)
        {
            return NotFound();
        }

        var carreras_materias = await _context.CarrerasMaterias.FindAsync(camaid);
        if (carreras_materias == null)
        {
            return NotFound();
        }
        return View(carreras_materias);
    }

    // POST: CARRERAS_MATERIASS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? camaid, [Bind("CaMaId,CaMaDenominacion,CaId,Carreras,MaId,Materias,Inscripciones")] CarreraMateria carreras_materias)
    {
        if (camaid != carreras_materias.CaMaId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(carreras_materias);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Carreras_MateriasExists(carreras_materias.CaMaId))
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
        return View(carreras_materias);
    }

    // GET: CARRERAS_MATERIASS/Delete/5
    public async Task<IActionResult> Delete(int? camaid)
    {
        if (camaid == null)
        {
            return NotFound();
        }

        var carreras_materias = await _context.CarrerasMaterias
            .FirstOrDefaultAsync(m => m.CaMaId == camaid);
        if (carreras_materias == null)
        {
            return NotFound();
        }

        return View(carreras_materias);
    }

    // POST: CARRERAS_MATERIASS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? camaid)
    {
        var carreras_materias = await _context.CarrerasMaterias.FindAsync(camaid);
        if (carreras_materias != null)
        {
            _context.CarrerasMaterias.Remove(carreras_materias);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool Carreras_MateriasExists(int? camaid)
    {
        return _context.CarrerasMaterias.Any(e => e.CaMaId == camaid);
    }
}
