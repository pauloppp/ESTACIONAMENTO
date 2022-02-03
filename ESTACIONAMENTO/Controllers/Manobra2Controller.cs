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
    public class Manobra2Controller : Controller
    {
        private readonly EstacionamentoContext _context;

        public Manobra2Controller(EstacionamentoContext context)
        {
            _context = context;
        }

        // GET: Manobra2
        public async Task<IActionResult> IndexA()
        {
            var estacionamentoContext = _context.Manobras2.Include(m => m.Carro)
                                                                   .Include(m => m.Manobrista)
                                                                   .Where(s => s.Status == "Aberta");

            return View(await estacionamentoContext.ToListAsync());
        }

        // GET: Manobra2
        public async Task<IActionResult> IndexF()
        {

            var estacionamentoContext = _context.Manobras2.Include(m => m.Carro)
                                                          .Include(m => m.Manobrista)
                                                          .Where(s => s.Status == "Fechada");

            return View(await estacionamentoContext.ToListAsync());
        }

        // GET: Manobra2/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manobra2 = await _context.Manobras2
                .Include(m => m.Carro)
                .Include(m => m.Manobrista)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (manobra2 == null)
            {
                return NotFound();
            }

            return View(manobra2);
        }

        // GET: Manobra2/Create
        public IActionResult Create()
        {
            ViewData["Carros"] = new SelectList(_context.Carros, "Id", "Modelo");
            ViewData["Manobristas"] = new SelectList(_context.Manobristas, "Id", "Nome");
            ViewData["Classificacoes"] = new SelectList(_context.Classificacoes.ToList(), "Descricao", "Descricao");
            Manobra2 man = new Manobra2();
            man.Status = "Aberta";
            return View(man);
        }

        // POST: Manobra2/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DataSaida,Classificacao,Status,Valor,CarroId,ManobristaId,Id")] Manobra2 manobra2)
        {
            if (ModelState.IsValid)
            {
                _context.Add(manobra2);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(IndexA));
            }
            ViewData["Carros"] = new SelectList(_context.Carros, "Id", "Modelo");
            ViewData["Manobristas"] = new SelectList(_context.Manobristas, "Id", "Nome");
            ViewData["Classificacoes"] = new SelectList(_context.Classificacoes.ToList(), "Descricao", "Descricao");
            return View(manobra2);
        }

        // GET: Manobra2/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manobra2 = await _context.Manobras2.FindAsync(id);
            if (manobra2 == null)
            {
                return NotFound();
            }
            ViewData["Carros"] = new SelectList(_context.Carros, "Id", "Modelo", manobra2.Carro.Modelo);
            ViewData["Manobristas"] = new SelectList(_context.Manobristas, "Id", "Nome", manobra2.Manobrista.Nome);
            ViewData["Classificacoes"] = new SelectList(_context.Classificacoes.ToList(), "Descricao", "Descricao", manobra2.Classificacao);
            return View(manobra2);
        }

        // POST: Manobra2/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DataSaida,Classificacao,Status,Valor,CarroId,ManobristaId,Id")] Manobra2 manobra2)
        {
            if (id != manobra2.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(manobra2);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Manobra2Exists(manobra2.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(IndexA));
            }
            ViewData["Carros"] = new SelectList(_context.Carros, "Id", "Modelo", manobra2.Carro.Modelo);
            ViewData["Manobristas"] = new SelectList(_context.Manobristas, "Id", "Nome", manobra2.Manobrista.Nome);
            ViewData["Classificacoes"] = new SelectList(_context.Classificacoes.ToList(), "Descricao", "Descricao", manobra2.Classificacao);
            return View(manobra2);
        }

        // GET: Manobra2/Fechar/5
        public async Task<IActionResult> Fechar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manobra2 = await _context.Manobras2.FindAsync(id);
            if (manobra2 == null)
            {
                return NotFound();
            }
            ViewData["CarroId"] = new SelectList(_context.Carros, "Id", "Marca", manobra2.Carro.Marca);
            ViewData["ManobristaId"] = new SelectList(_context.Manobristas, "Id", "Nome", manobra2.Manobrista.Nome);
            return View(manobra2);
        }

        // POST: Manobra2/Fechar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Fechar(int id, [Bind("DataEntrada,DataSaida,Classificacao,Status,Valor,CarroId,ManobristaId,Id")] Manobra2 manobra2)
        {
            if (id != manobra2.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try

                {
                    var vlrMinuto = 1.5;
                    manobra2.DataSaida = DateTime.Now;
                    var minutos = (int)(manobra2.DataSaida - manobra2.DataEntrada).TotalMinutes;

                    manobra2.Valor = ((decimal)(minutos * vlrMinuto));
                    manobra2.Status = "Fechada";
                    manobra2.Classificacao = "Retorno";

                    _context.Update(manobra2);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Manobra2Exists(manobra2.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(IndexA));
            }
            ViewData["CarroId"] = new SelectList(_context.Carros, "Id", "Marca", manobra2.Carro.Marca);
            ViewData["ManobristaId"] = new SelectList(_context.Manobristas, "Id", "Nome", manobra2.Manobrista.Nome);
            return View(manobra2);
        }

        // GET: Manobra2/Recibo/5
        public async Task<IActionResult> Recibo(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manobra2 = await _context.Manobras2.FindAsync(id);
            if (manobra2 == null)
            {
                return NotFound();
            }
            ViewData["CarroId"] = new SelectList(_context.Carros, "Id", "Marca", manobra2.Carro.Marca);
            ViewData["ManobristaId"] = new SelectList(_context.Manobristas, "Id", "Nome", manobra2.Manobrista.Nome);
            return View(manobra2);
        }

        // GET: Manobra2/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manobra2 = await _context.Manobras2
                .Include(m => m.Carro)
                .Include(m => m.Manobrista)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (manobra2 == null)
            {
                return NotFound();
            }

            return View(manobra2);
        }

        // POST: Manobra2/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var manobra2 = await _context.Manobras2.FindAsync(id);
            _context.Manobras2.Remove(manobra2);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Manobra2Exists(int id)
        {
            return _context.Manobras2.Any(e => e.Id == id);
        }
    }
}
