using System.ComponentModel.DataAnnotations;

namespace Projecto.Receitas.WebApp.ViewModel
{
    public class AvaliacaoViewModel
    {
        [Required(ErrorMessage ="Tém de selecionar um valor.")]
        public int Valor {  get; set; }
        public int ReceitaId { get; set; }
        public int UtilizadorId { get; set; }
    }
}
