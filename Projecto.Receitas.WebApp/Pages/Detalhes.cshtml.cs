using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Projecto.Receitas.Domain.Model;
using Projecto.Receitas.Services.Interfaces;
using Projecto.Receitas.Services.Services;

namespace Projecto.Receitas.WebApp.Pages
{
    public class DetalhesModel : PageModel
    {
        private IReceitaService _receitaService;

        private IItemService _itemService;

        private IIngredienteService _ingredienteService;

        private IAvaliacaoService _avaliacaoService;

        private IMedidaService _medidaService;

        private ValidarSessao _validarSessao;

        public DetalhesModel(IReceitaService receitaService,IItemService itemService
            ,IIngredienteService ingredienteService,IAvaliacaoService avaliacaoService,
            IMedidaService medidaService,ValidarSessao validarSessao) 
        {
            _receitaService = receitaService;
            _itemService = itemService;
            _ingredienteService = ingredienteService;
            _avaliacaoService = avaliacaoService;
            _medidaService = medidaService;
            _validarSessao = validarSessao;
        }
        public double Avaliacao { get; set; }
        public Receita Receita { get; set; }
        public List<Item> Itens { get; set; }
        public string Codigo { get; set; }
        public IActionResult OnGet(int id)
        {
            Receita = _receitaService.GetById(id);

            if(Receita == null) 
            {
                return NotFound();
            }

            if (Receita.Estado!=TipoDeEstado.Valida) 
            {
                return BadRequest();
            }

            Avaliacao = _avaliacaoService.MediaDoValorByReceita(id);

            Itens = _itemService.GetByReceitaId(id);

            foreach (Item item in Itens)
            {
                item.Ingrediente =_ingredienteService.GetById(item.Ingrediente.IngredienteId);
                item.Medida = _medidaService.GetById(item.Medida.MedidaId);
            }
        
            if(HttpContext.Session.GetInt32("UserId") != null)
            {
                Codigo = _validarSessao.Encriptar((int)HttpContext.Session.GetInt32("UserId"));
            }

            return Page();
        }
    }
}
