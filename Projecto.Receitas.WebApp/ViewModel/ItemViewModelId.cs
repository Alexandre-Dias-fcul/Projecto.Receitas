using Projecto.Receitas.Domain.Model;
using System.ComponentModel.DataAnnotations;

namespace Projecto.Receitas.WebApp.ViewModel
{
    public class ItemViewModelId
    {
        public int ItemId { get; set; }
        [Required(ErrorMessage = "A quantidade é obrigatória.")]
        public double Quantidade { get; set; }

        [Required(ErrorMessage = "A medida é obrigatória.")]
        public int MedidaId { get; set; }
        public int ReceitaId { get; set; }
        public int IngredienteId {  get; set; }
    }
}
