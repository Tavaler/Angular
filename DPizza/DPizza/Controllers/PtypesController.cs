using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DPizza.Models.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DPizza.Controllers
{
    public class PtypesController : Controller
    {
        private readonly DpizzaContext _context;

        public PtypesController(DpizzaContext context)
        {
            _context = context;
        }

        // GET: Ptypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Ptype.ToListAsync());
        }

        // GET: Ptypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ptype = await _context.Ptype
                .FirstOrDefaultAsync(m => m.PtypeId == id);
            if (ptype == null)
            {
                return NotFound();
            }

            return View(ptype);
        }

        // GET: Ptypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ptypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PtypeId,PtypeName")] Ptype ptype)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ptype);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ptype);
        }

        // GET: Ptypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ptype = await _context.Ptype.FindAsync(id);
            if (ptype == null)
            {
                return NotFound();
            }
            return View(ptype);
        }

        // POST: Ptypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PtypeId,PtypeName")] Ptype ptype)
        {
            if (id != ptype.PtypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ptype);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PtypeExists(ptype.PtypeId))
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
            return View(ptype);
        }

        // GET: Ptypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ptype = await _context.Ptype
                .FirstOrDefaultAsync(m => m.PtypeId == id);
            if (ptype == null)
            {
                return NotFound();
            }

            return View(ptype);
        }

        // POST: Ptypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ptype = await _context.Ptype.FindAsync(id);
            _context.Ptype.Remove(ptype);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PtypeExists(int id)
        {
            return _context.Ptype.Any(e => e.PtypeId == id);
        }
    }
}
