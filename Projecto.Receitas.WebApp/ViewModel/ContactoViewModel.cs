using Projecto.Receitas.Domain.Model;
using System.ComponentModel.DataAnnotations;

namespace Projecto.Receitas.WebApp.ViewModel
{
    public class ContactoViewModel
    {
        [Required(ErrorMessage ="O tipo de contacto é obrigatório.")]
        public TipoDeContacto Tipo { get; set; }

        [Required(ErrorMessage ="O valor é obrigatório.")]
        [StringLength(300,ErrorMessage = "O valor tem no máximo 300 caracteres.")]
        public string Valor { get; set; }
        public int UtilizadorId { get; set; }
    }
}
