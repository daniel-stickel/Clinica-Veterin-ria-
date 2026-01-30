using Microsoft.AspNetCore.Hosting; 
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClinicaVeterinaria.Data;
using ClinicaVeterinaria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicaVeterinaria.Controllers
{
    public class AnimaisController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public AnimaisController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Animais.Include(a => a.Tutor);
            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Animais == null)
            {
                return NotFound();
            }

            var animal = await _context.Animais
                .Include(a => a.Tutor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (animal == null)
            {
                return NotFound();
            }

            return View(animal);
        }

        public IActionResult Create()
        {

            ViewData["TutorId"] = new SelectList(_context.Tutores, "Id", "Nome");
            return View();
        }

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
                    ModelState.AddModelError("", "Você não pode agendar uma consulta no passad. Escolha uma data futura.");
                }
                else
                {
                    bool horarioOcupado = _context.Consultas.Any(c => c.DataHora == dataCompleta);

                    if (horarioOcupado)
                    {
                        ModelState.AddModelError("", "Este horário já está reservado. Por favor escolha outro.");
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

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Animais == null)
            {
                return NotFound();
            }

            var animal = await _context.Animais.FindAsync(id);
            if (animal == null)
            {
                return NotFound();
            }
            ViewData["TutorId"] = new SelectList(_context.Tutores, "Id", "Nome", animal.TutorId);
            return View(animal);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Raca,Cor,Peso,Sexo,ObservacoesMedicas,FotoUrl,TutorId")] Animal animal)
        {
            if (id != animal.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(animal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnimalExists(animal.Id))
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
            ViewData["TutorId"] = new SelectList(_context.Tutores, "Id", "Nome", animal.TutorId);
            return View(animal);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Animais == null)
            {
                return NotFound();
            }

            var animal = await _context.Animais
                .Include(a => a.Tutor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (animal == null)
            {
                return NotFound();
            }

            return View(animal);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Animais == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Animais'  is null.");
            }
            var animal = await _context.Animais.FindAsync(id);
            if (animal != null)
            {
                _context.Animais.Remove(animal);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnimalExists(int id)
        {
            return (_context.Animais?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}