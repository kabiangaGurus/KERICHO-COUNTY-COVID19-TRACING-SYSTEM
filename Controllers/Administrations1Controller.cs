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
    public class Administrations1Controller : Controller
    {
        private readonly ApplicationDBContext _context;

        public Administrations1Controller(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: Administrations1
        public async Task<IActionResult> Index()
        {
            return View(await _context.Administration.ToListAsync());
        }

        // GET: Administrations1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var administration = await _context.Administration
                .FirstOrDefaultAsync(m => m.staff_no == id);
            if (administration == null)
            {
                return NotFound();
            }

            return View(administration);
        }

        // GET: Administrations1/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Administrations1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("staff_no,Full_names,Phone_number,Email,Password,Role,Department")] Administration administration)
        {
            if (ModelState.IsValid)
            {
                _context.Add(administration);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(administration);
        }

        // GET: Administrations1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var administration = await _context.Administration.FindAsync(id);
            if (administration == null)
            {
                return NotFound();
            }
            return View(administration);
        }

        // POST: Administrations1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("staff_no,Full_names,Phone_number,Email,Password,Role,Department")] Administration administration)
        {
            if (id != administration.staff_no)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(administration);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdministrationExists(administration.staff_no))
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
            return View(administration);
        }

        // GET: Administrations1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var administration = await _context.Administration
                .FirstOrDefaultAsync(m => m.staff_no == id);
            if (administration == null)
            {
                return NotFound();
            }

            return View(administration);
        }

        // POST: Administrations1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var administration = await _context.Administration.FindAsync(id);
            _context.Administration.Remove(administration);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdministrationExists(int id)
        {
            return _context.Administration.Any(e => e.staff_no == id);
        }
    }
}
