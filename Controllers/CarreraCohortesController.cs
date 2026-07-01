
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AsistenciaGR.Models;
using AsistenciaGR.Data;

public class CarreraCohortesController : Controller
{
    private readonly AsistenciaGRContext _context;

    public CarreraCohortesController(AsistenciaGRContext context)
    {
        _context = context;
    }

    // GET: CARRERACOHORTES
    public async Task<IActionResult> Index()    
    {
        return View(await _context.CarreraCohorte.ToListAsync());
    }

    // GET: CARRERACOHORTES/Details/5
    public async Task<IActionResult> Details(int? cacoid)
    {
        if (cacoid == null)
        {
            return NotFound();
        }

        var carreracohorte = await _context.CarreraCohorte
            .FirstOrDefaultAsync(m => m.CaCoId == cacoid);
        if (carreracohorte == null)
        {
            return NotFound();
        }

        return View(carreracohorte);
    }

    // GET: CARRERACOHORTES/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: CARRERACOHORTES/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("CaCoId,CaId,CoId,Carrera,Cohorte")] CarreraCohorte carreracohorte)
    {
        if (ModelState.IsValid)
        {
            _context.Add(carreracohorte);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(carreracohorte);
    }

    // GET: CARRERACOHORTES/Edit/5
    public async Task<IActionResult> Edit(int? cacoid)
    {
        if (cacoid == null)
        {
            return NotFound();
        }

        var carreracohorte = await _context.CarreraCohorte.FindAsync(cacoid);
        if (carreracohorte == null)
        {
            return NotFound();
        }
        return View(carreracohorte);
    }

    // POST: CARRERACOHORTES/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? cacoid, [Bind("CaCoId,CaId,CoId,Carrera,Cohorte")] CarreraCohorte carreracohorte)
    {
        if (cacoid != carreracohorte.CaCoId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(carreracohorte);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarreraCohorteExists(carreracohorte.CaCoId))
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
        return View(carreracohorte);
    }

    // GET: CARRERACOHORTES/Delete/5
    public async Task<IActionResult> Delete(int? cacoid)
    {
        if (cacoid == null)
        {
            return NotFound();
        }

        var carreracohorte = await _context.CarreraCohorte
            .FirstOrDefaultAsync(m => m.CaCoId == cacoid);
        if (carreracohorte == null)
        {
            return NotFound();
        }

        return View(carreracohorte);
    }

    // POST: CARRERACOHORTES/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? cacoid)
    {
        var carreracohorte = await _context.CarreraCohorte.FindAsync(cacoid);
        if (carreracohorte != null)
        {
            _context.CarreraCohorte.Remove(carreracohorte);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool CarreraCohorteExists(int? cacoid)
    {
        return _context.CarreraCohorte.Any(e => e.CaCoId == cacoid);
    }
}
