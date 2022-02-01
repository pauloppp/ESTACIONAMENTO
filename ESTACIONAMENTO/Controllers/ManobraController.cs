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
    public class ManobraController : Controller
    {
        private readonly EstacionamentoContext _context;
       
        public ManobraController(EstacionamentoContext context)
        {
            _context = context;
        }

        // GET: Manobra
        public async Task<IActionResult> Index()
        {
            return View(await _context.Manobras.OrderByDescending(p => p.Carro.Placa).ToListAsync());
        }

        // GET: Manobra/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manobra = await _context.Manobras
                .FirstOrDefaultAsync(m => m.Id == id);
            if (manobra == null)
            {
                return NotFound();
            }

            return View(manobra);
        }

        // GET: Manobra/Create
        public IActionResult Create()
        {
            var carros = _context.Carros.ToList();
            ViewData["Carros"] = new SelectList(carros, "Id", "Modelo");
            var manobristas = _context.Manobristas.ToList();
            ViewData["Manobristas"] = new SelectList(manobristas, "Id", "Nome");
            var classificacao = _context.Classificacoes.ToList();
            ViewData["Classificacoes"] = new SelectList(classificacao, "Descricao", "Descricao");

            return View();
        }

        // POST: Manobra/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Manobra manobra)
        {
            if (ModelState.IsValid)
            {
                _context.Add(manobra);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(manobra);
        }

        // GET: Manobra/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            

            var manobra = await _context.Manobras.FindAsync(id);
            if (manobra == null)
            {
                return NotFound();
            }

            var carros = _context.Carros.ToList();
            ViewData["Carros"] = new SelectList(carros, "Id", "Modelo", manobra.Carro.Modelo);
            var manobristas = _context.Manobristas.ToList();
            ViewData["Manobristas"] = new SelectList(manobristas, "Id", "Nome", manobra.Manobrista.Nome);
            var classificacao = _context.Classificacoes.ToList();
            ViewData["Classificacoes"] = new SelectList(classificacao, "Descricao", "Descricao", manobra.Classificacao);
            return View(manobra);
        }

        // POST: Manobra/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CarroId,ManobristaId,Classificacao,TipoManobra,Id")] Manobra manobra)
        {
            if (id != manobra.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(manobra);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ManobraExists(manobra.Id))
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
            return View(manobra);
        }

        // GET: Manobra/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manobra = await _context.Manobras
                .FirstOrDefaultAsync(m => m.Id == id);
            if (manobra == null)
            {
                return NotFound();
            }

            return View(manobra);
        }

        // POST: Manobra/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var manobra = await _context.Manobras.FindAsync(id);
            _context.Manobras.Remove(manobra);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ManobraExists(int id)
        {
            return _context.Manobras.Any(e => e.Id == id);
        }
    }
}
