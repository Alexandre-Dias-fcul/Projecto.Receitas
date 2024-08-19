using System.ComponentModel.DataAnnotations;

namespace Projecto.Receitas.WebApp.ViewModel
{
    public class EnderecoViewModel
    {
        [Required(ErrorMessage = "O rua é obrigatória.")]
        [StringLength(300,ErrorMessage ="A rua pode ter no máximo 300 caracteres.")]
        public string Rua { get; set; }

        [Required(ErrorMessage = "O código postal é obrigátório.")]
        [StringLength(50,ErrorMessage ="O código postal pode ter no máximo 50 caracteres.")]
        public string CodigoPostal { get; set; }

        [Required(ErrorMessage = "A localidade é obrigatória.")]
        [StringLength(150, ErrorMessage = "A localidade pode ter no máximo 150 caracteres.")]
        public string Localidade { get; set; }

        [Required(ErrorMessage = "O país é obrigatório.")]
        [StringLength(510, ErrorMessage = "O país pode ter no máximo 150 caracteres.")]
        public string Pais { get; set; }
        public int UtilizadorId { get; set; }
    }
}
