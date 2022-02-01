using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ESTACIONAMENTO.Dados;
using ESTACIONAMENTO.Models;

namespace ESTACIONAMENTO.Controllers
{
    public class ManobristaController : Controller
    {
        private readonly EstacionamentoContext _context;

        public ManobristaController(EstacionamentoContext context)
        {
            _context = context;
        }

        // GET: Manobrista
        public async Task<IActionResult> Index()
        {
            return View(await _context.Manobristas.ToListAsync());
        }

        // GET: Manobrista/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manobrista = await _context.Manobristas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (manobrista == null)
            {
                return NotFound();
            }

            return View(manobrista);
        }

        // GET: Manobrista/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Manobrista/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,CPF,DataNascimento,Id")] Manobrista manobrista)
        {
            if (ModelState.IsValid)
            {
                _context.Add(manobrista);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(manobrista);
        }

        // GET: Manobrista/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manobrista = await _context.Manobristas.FindAsync(id);
            if (manobrista == null)
            {
                return NotFound();
            }
            return View(manobrista);
        }

        // POST: Manobrista/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Nome,CPF,DataNascimento,Id")] Manobrista manobrista)
        {
            if (id != manobrista.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(manobrista);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ManobristaExists(manobrista.Id))
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
            return View(manobrista);
        }

        // GET: Manobrista/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manobrista = await _context.Manobristas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (manobrista == null)
            {
                return NotFound();
            }

            return View(manobrista);
        }

        // POST: Manobrista/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var manobrista = await _context.Manobristas.FindAsync(id);
            _context.Manobristas.Remove(manobrista);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ManobristaExists(int id)
        {
            return _context.Manobristas.Any(e => e.Id == id);
        }
    }
}
