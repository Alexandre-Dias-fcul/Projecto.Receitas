using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Projecto.Receitas.Domain.Model;
using Projecto.Receitas.Services.Interfaces;

namespace Projecto.Receitas.WebApp.Pages.Administracao
{
    public class VerReceitaModel : PageModel
    {
        private IReceitaService _receitaService;

        private IItemService _itemService;

        private IIngredienteService _ingredienteService;

        private IAvaliacaoService _avaliacaoService;

        private IMedidaService _medidaService;

        public VerReceitaModel(IReceitaService receitaService, IItemService itemService
            , IIngredienteService ingredienteService, IAvaliacaoService avaliacaoService,
            IMedidaService medidaService)
        {
            _receitaService = receitaService;
            _itemService = itemService;
            _ingredienteService = ingredienteService;
            _avaliacaoService = avaliacaoService;
            _medidaService = medidaService;
        }

        public double Avaliacao { get; set; }
        public Receita Receita { get; set; }
        public List<Item> Itens { get; set; }
        public int IdPagina { get; set; }
        public string? Texto { get; set; }
        public IActionResult OnGet(int idReceita, int idPagina = 0, string? texto = "")
        {
            Receita = _receitaService.GetById(idReceita);

            if (Receita == null)
            {
                return NotFound();
            }

            Avaliacao = _avaliacaoService.MediaDoValorByReceita(idReceita);

            Itens = _itemService.GetByReceitaId(idReceita);

            foreach (Item item in Itens)
            {
                item.Ingrediente =_ingredienteService.GetById(item.Ingrediente.IngredienteId);
                item.Medida = _medidaService.GetById(item.Medida.MedidaId);
            }

            IdPagina = idPagina;

            Texto = texto;

            return Page();
        }
    }
}
