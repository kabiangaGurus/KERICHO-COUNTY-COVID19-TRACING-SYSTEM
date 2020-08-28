using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Covid19Tracing.Models;
using Fuela.DBContext;

namespace Covid19Tracing.Controllers
{
    public class SuspectedCasesController : Controller
    {
        private readonly ApplicationDBContext _context;

        public SuspectedCasesController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: SuspectedCases
        public async Task<IActionResult> Index()
        {
            return View(await _context.SuspectedCases.ToListAsync());
        }

        // GET: SuspectedCases/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suspectedCases = await _context.SuspectedCases
                .FirstOrDefaultAsync(m => m.ID == id);
            if (suspectedCases == null)
            {
                return NotFound();
            }

            return View(suspectedCases);
        }

        // GET: SuspectedCases/Create
        public IActionResult Create(int niID)
        {
            ViewBag.Nid=niID;
            return View();
        }

        // POST: SuspectedCases/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,P_patient_id,Full_names,S_patient_id,Phone_number")] SuspectedCases suspectedCases)
        {
            if (ModelState.IsValid)
            {
                _context.Add(suspectedCases);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(suspectedCases);
        }

        // GET: SuspectedCases/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suspectedCases = await _context.SuspectedCases.FindAsync(id);
            if (suspectedCases == null)
            {
                return NotFound();
            }
            return View(suspectedCases);
        }

        // POST: SuspectedCases/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,P_patient_id,Full_names,S_patient_id,Phone_number")] SuspectedCases suspectedCases)
        {
            if (id != suspectedCases.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(suspectedCases);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SuspectedCasesExists(suspectedCases.ID))
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
            return View(suspectedCases);
        }

        // GET: SuspectedCases/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suspectedCases = await _context.SuspectedCases
                .FirstOrDefaultAsync(m => m.ID == id);
            if (suspectedCases == null)
            {
                return NotFound();
            }

            return View(suspectedCases);
        }

        // POST: SuspectedCases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var suspectedCases = await _context.SuspectedCases.FindAsync(id);
            _context.SuspectedCases.Remove(suspectedCases);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SuspectedCasesExists(int id)
        {
            return _context.SuspectedCases.Any(e => e.ID == id);
        }
    }
}
