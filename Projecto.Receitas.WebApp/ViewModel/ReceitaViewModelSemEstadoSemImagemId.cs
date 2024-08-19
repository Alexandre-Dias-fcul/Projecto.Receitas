using Projecto.Receitas.Domain.Model;
using System.ComponentModel.DataAnnotations;

namespace Projecto.Receitas.WebApp.ViewModel
{
    public class ReceitaViewModelSemEstadoSemImagemId
    {
        public int ReceitaId { get; set; }
        [Required(ErrorMessage = "O titulo é obrigatório.")]
        [StringLength(200, ErrorMessage = "O titulo tem no máximo 200 caracteres.")]
        public string Titulo { get; set; }

        public IFormFile? Imagem { get; set; }

        [Required(ErrorMessage = "O modo de preparação é obrigatório.")]
        [StringLength(3500, ErrorMessage = "O modo de preparação tem no máximo 3500 caracteres.")]
        public string ModoDePreparacao { get; set; }

        [Required(ErrorMessage = "O tempo de preparação é obrigatório.")]
        public Tempo TempoDePreparacao { get; set; }

        [Required(ErrorMessage = "O grau de dificuldade é obrigatório.")]
        public GrauDeDificuldade Dificuldade { get; set; }

        [Required(ErrorMessage = "A categoria é obrigatória.")]
        public int CategoriaId { get; set; }
        public int UtilizadorId { get; set; }
    }
}
