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
    public class Covid_status1Controller : Controller
    {
        private readonly ApplicationDBContext _context;

        public Covid_status1Controller(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: Covid_status1
        public async Task<IActionResult> Index()
        {
            return View(await _context.Covid_status.ToListAsync());
        }

        // GET: Covid_status1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var covid_status = await _context.Covid_status
                .FirstOrDefaultAsync(m => m.ID == id);
            if (covid_status == null)
            {
                return NotFound();
            }

            return View(covid_status);
        }

        // GET: Covid_status1/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Covid_status1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Patients_id,Status,Date")] Covid_status covid_status)
        {
            if (ModelState.IsValid)
            {
                _context.Add(covid_status);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(covid_status);
        }

        // GET: Covid_status1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var covid_status = await _context.Covid_status.FindAsync(id);
            if (covid_status == null)
            {
                return NotFound();
            }
            return View(covid_status);
        }

        // POST: Covid_status1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Patients_id,Status,Date")] Covid_status covid_status)
        {
            if (id != covid_status.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(covid_status);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Covid_statusExists(covid_status.ID))
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
            return View(covid_status);
        }

        // GET: Covid_status1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var covid_status = await _context.Covid_status
                .FirstOrDefaultAsync(m => m.ID == id);
            if (covid_status == null)
            {
                return NotFound();
            }

            return View(covid_status);
        }

        // POST: Covid_status1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var covid_status = await _context.Covid_status.FindAsync(id);
            _context.Covid_status.Remove(covid_status);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Covid_statusExists(int id)
        {
            return _context.Covid_status.Any(e => e.ID == id);
        }
    }
}
