using System.ComponentModel.DataAnnotations;

namespace ClinicaVeterinaria.Models
{
    public class Consulta
    {
        public int Id { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime DataHora { get; set; }
        public string Motivo { get; set; }
        public string? Diagnostico { get; set; }
        public int AnimalId { get; set; }   
        public Animal? Animal { get; set; }

    }
}
