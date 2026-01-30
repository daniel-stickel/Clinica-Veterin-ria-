using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClinicaVeterinaria.Data;
using ClinicaVeterinaria.Models;

namespace ClinicaVeterinaria.Controllers
{
    public class ConsultasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ConsultasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Consultas
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Consultas.Include(c => c.Animal);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Consultas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Consultas == null)
            {
                return NotFound();
            }

            var consulta = await _context.Consultas
                .Include(c => c.Animal)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (consulta == null)
            {
                return NotFound();
            }

            return View(consulta);
        }

        // GET: Consultas/Create
        public IActionResult Create()
        {
            ViewData["AnimalId"] = new SelectList(_context.Animais, "Id", "Nome");

            ViewBag.HorariosDisponiveis = new SelectList(Consulta.ObterHorariosPadrao());
            
            return View();
        }

        // POST: Consultas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int AnimalId, DateTime dataEscolhida, string horaEscolhida, string Motivo)
        {
            if (string.IsNullOrEmpty(horaEscolhida))
            {
                ModelState.AddModelError("", "Por favor, selecione um horário.");
            }
            else
            {

                TimeSpan hora = TimeSpan.Parse(horaEscolhida);
                DateTime dataCompleta = dataEscolhida.Date + hora;

                if (dataCompleta < DateTime.Now)
                {
                    ModelState.AddModelError("", "Você não pode agendar uma consulta no passado! Escolha uma data futura.");
                }
                else
                {
                    bool horarioOcupado = _context.Consultas.Any(c => c.DataHora == dataCompleta);

                    if (horarioOcupado)
                    {
                        ModelState.AddModelError("", "Este horário já está reservado! Por favor escolha outro.");
                    }
                    else
                    {
                        Consulta novaConsulta = new Consulta();
                        novaConsulta.AnimalId = AnimalId;
                        novaConsulta.DataHora = dataCompleta;
                        novaConsulta.Motivo = Motivo;


                        if (ModelState.IsValid)
                        {
                            _context.Add(novaConsulta);
                            await _context.SaveChangesAsync();
                            return RedirectToAction(nameof(Index));
                        }
                    }
                }
            }

            ViewData["AnimalId"] = new SelectList(_context.Animais, "Id", "Nome", AnimalId);

            ViewBag.HorariosDisponiveis = new SelectList(Consulta.ObterHorariosPadrao(), horaEscolhida);

            return View();
        }



        // GET: Consultas/Edit/5
        // GET: Consultas/Edit/5
        // GET: Consultas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var consulta = await _context.Consultas.FindAsync(id);
            if (consulta == null) return NotFound();

            ViewData["AnimalId"] = new SelectList(_context.Animais, "Id", "Nome", consulta.AnimalId);

            ViewBag.HorariosDisponiveis = new SelectList(Consulta.ObterHorariosPadrao());

            return View(consulta);
        }

        // POST: Consultas/Edit/5
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, int AnimalId, DateTime dataEscolhida, string horaEscolhida, string Motivo, string Diagnostico)
        {
            TimeSpan hora = TimeSpan.Parse(horaEscolhida);
            DateTime dataCompleta = dataEscolhida.Date + hora;

            var consultaBanco = await _context.Consultas.FindAsync(id);

            if (consultaBanco == null) return NotFound();

            consultaBanco.AnimalId = AnimalId;
            consultaBanco.Motivo = Motivo;
            consultaBanco.DataHora = dataCompleta;

            consultaBanco.Diagnostico = Diagnostico;

            try
            {
                _context.Update(consultaBanco);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Consultas.Any(e => e.Id == id)) return NotFound();
                else throw;
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Consultas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Consultas == null)
            {
                return NotFound();
            }

            var consulta = await _context.Consultas
                .Include(c => c.Animal)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (consulta == null)
            {
                return NotFound();
            }

            return View(consulta);
        }

        // POST: Consultas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Consultas == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Consultas'  is null.");
            }
            var consulta = await _context.Consultas.FindAsync(id);
            if (consulta != null)
            {
                _context.Consultas.Remove(consulta);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConsultaExists(int id)
        {
          return (_context.Consultas?.Any(e => e.Id == id)).GetValueOrDefault();
        }

    }
}
