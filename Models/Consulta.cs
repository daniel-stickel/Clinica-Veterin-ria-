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


        public static List<string> ObterHorariosPadrao()
        {
            var horarios = new List<string>();

            TimeSpan inicio =new TimeSpan(9,0,0);
            TimeSpan fim = new TimeSpan(17,0,0);
            TimeSpan intervalo = new TimeSpan(0,30,0);

            var horaAtual = inicio;

            while(horaAtual <= fim)
            {
                bool horarioAlmoco = (horaAtual.Hours == 12) || (horaAtual.Hours == 13 && horaAtual.Minutes < 30);

                if (!horarioAlmoco)
                {
                    horarios.Add(horaAtual.ToString(@"hh\:mm"));
                }
                horaAtual = horaAtual.Add(intervalo);
            }
            return horarios;
        }

    }
}
