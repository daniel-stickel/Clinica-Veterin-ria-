using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.ComponentModel.DataAnnotations;
using ClinicaVeterinaria.Models;

namespace ClinicaVeterinaria.Models
{
    public class Animal
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "O nome é obrigatório")]
        public string Nome {  get; set; }
        public string Raca { get; set; }
        public string Cor {  get; set; }
        public decimal Peso { get; set; }
        [Display(Name = "Sexo do Animal")]
        public TipoSexo Sexo { get; set; }
        public string? ObservacoesMedicas { get; set; }
        public string? FotoUrl { get; set; }
        public int TutorId { get; set; }
        public Tutor? Tutor { get; set; }
        public ICollection<Consulta>? Consultas { get; set; }


    }
}
