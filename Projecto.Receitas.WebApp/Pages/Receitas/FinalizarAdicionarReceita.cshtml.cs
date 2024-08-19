using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Projecto.Receitas.Domain.Model;
using Projecto.Receitas.Services.Interfaces;
using Projecto.Receitas.Services.Services;

namespace Projecto.Receitas.WebApp.Pages.Receitas
{
    public class FinalizarAdicionarReceitaModel : PageModel
    {
        private IReceitaService _receitaService;

        private IItemService _itemService;

        private IIngredienteService _ingredienteService;

        private IMedidaService _medidaService;

        private ValidarSessao _validarSessao; 
        public FinalizarAdicionarReceitaModel(IReceitaService receitaService,IItemService itemService
            ,IIngredienteService ingredienteService,IMedidaService medidaService, ValidarSessao validarSessao) 
        {
            _receitaService = receitaService;
            _itemService = itemService;
            _ingredienteService = ingredienteService;
            _medidaService = medidaService;
            _validarSessao = validarSessao;
        }
        public Receita Receita { get; set; }
        public List<Item> Itens { get; set; }
        public string Codigo { get; set; }

        public IActionResult OnGet(int id, string codigo)
        {
            if (!_validarSessao.Validar(codigo)) 
            {
                return BadRequest();
            }

            Receita =_receitaService.GetById(id);

            if(Receita == null) 
            {
                return NotFound();
            }

            Itens = _itemService.GetByReceitaId(id);
            
            foreach(Item item in Itens) 
            {
                item.Ingrediente = _ingredienteService.GetById(item.Ingrediente.IngredienteId);
                item.Medida = _medidaService.GetById(item.Medida.MedidaId);
            }

            Codigo = codigo;

            return Page();
        }
    }
}
