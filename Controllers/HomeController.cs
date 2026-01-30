using ClinicaVeterinaria.Data;
using ClinicaVeterinaria.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ClinicaVeterinaria.Controllers

{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {

            int totalAnimais = _context.Animais.Count();

            int consultasHoje = _context.Consultas
                .Where(c => c.DataHora.Date == DateTime.Today)
                .Count();

            int atendimentosRealizados = _context.Consultas
                .Where(c => c.Diagnostico != null)
                .Count();

            ViewBag.TotalAnimais = totalAnimais;
            ViewBag.ConsultasHoje = consultasHoje;
            ViewBag.AtendimentosRealizados = atendimentosRealizados;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
