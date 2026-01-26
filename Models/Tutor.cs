using System.ComponentModel.DataAnnotations;

namespace ClinicaVeterinaria.Models
{
    public class Tutor
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Telefone { get; set; }
        public ICollection<Animal>? Animals { get; set; }

    }
}
