using Projecto.Receitas.Domain.Model;
using System.ComponentModel.DataAnnotations;

namespace Projecto.Receitas.WebApp.ViewModel
{
    public class UtilizadorViewModelId
    {
        public int UtilizadorId { get; set; }
        [Required(ErrorMessage = "O primeiro nome é obrigatório")]
        [StringLength(150, ErrorMessage = "O primeiro nome pode ter no máximo 150 caracteres")]
        public string PrimeiroNome { get; set; }

        [StringLength(500, ErrorMessage = " Os nomes do meio não podem ter mais de 500 caracteres.")]
        public string? NomesDoMeio { get; set; }

        [Required(ErrorMessage = "O último nome é obrigatório")]
        [StringLength(150, ErrorMessage = "O último nome pode ter no máximo 150 caracteres")]
        public string UltimoNome { get; set; }
        public TipoDeGenero? Genero { get; set; }

        [StringLength(300, ErrorMessage = " A url não pode ter mais de 300 caracteres.")]
        public string? FotoUrl { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Data inválida.")]
        public DateTime? DataDeNascimento { get; set; }

        [StringLength(300, ErrorMessage = " A nacionalidade não pode ter mais de 250 caracteres.")]
        public string? Nacionalidade { get; set; }

        [Required(ErrorMessage = "As permissões são obrigatórias")]
        public Permissao Permissoes { get; set; }
    }
}
