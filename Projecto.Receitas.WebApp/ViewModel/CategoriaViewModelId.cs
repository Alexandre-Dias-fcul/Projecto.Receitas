using System.ComponentModel.DataAnnotations;

namespace Projecto.Receitas.WebApp.ViewModel
{
    public class CategoriaViewModelId
    {
        public int CategoriaId { get; set; }
        [Required(ErrorMessage = "O nome da categoria é obrigatório.")]
        [StringLength(250, ErrorMessage = "O nome da categoria tem no máximo 250 caracteres.")]
        public string CategoriaNome { get; set; }
    }
}
