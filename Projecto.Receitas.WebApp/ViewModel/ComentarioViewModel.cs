using System.ComponentModel.DataAnnotations;

namespace Projecto.Receitas.WebApp.ViewModel
{
    public class ComentarioViewModel
    {
        [Required(ErrorMessage = "Não pode enviar o formulário vazio.")]
        public string Mensagem { get; set; }
        public int ReceitaId { get; set; }
        public int UtilizadorId { get; set; }
    }
}
