using ClinicaVeterinaria.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ClinicaVeterinaria.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Tutor> Tutures { get; set; }
        public DbSet<Animal> Animais { get; set; }
        public DbSet<Consulta> Consultas { get; set; }
    }
}
