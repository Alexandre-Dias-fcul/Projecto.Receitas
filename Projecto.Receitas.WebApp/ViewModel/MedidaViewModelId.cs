using System.ComponentModel.DataAnnotations;

namespace Projecto.Receitas.WebApp.ViewModel
{
    public class MedidaViewModelId
    {
        public int MedidaId { get; set; }

        [Required(ErrorMessage = "O nome da medida é obrigatório.")]
        [StringLength(250, ErrorMessage = "O nome da medida tem no máximo 100 caracteres.")]
        public string MedidaNome { get; set; }
    }
}
