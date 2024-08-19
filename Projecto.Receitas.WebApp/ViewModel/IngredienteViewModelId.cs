using System.ComponentModel.DataAnnotations;

namespace Projecto.Receitas.WebApp.ViewModel
{
    public class IngredienteViewModelId
    {
        public int IngredienteId { get; set; }

        [Required(ErrorMessage = "O nome do ingrediente é obrigatório.")]
        [StringLength(250, ErrorMessage = "O nome do ingrediente tem no máximo 250 caracteres.")]
        public string IngredienteNome { get; set; }
    }
}
