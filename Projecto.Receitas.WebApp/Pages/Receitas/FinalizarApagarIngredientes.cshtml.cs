using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Projecto.Receitas.Domain.Model;
using Projecto.Receitas.Services.Interfaces;
using Projecto.Receitas.Services.Services;

namespace Projecto.Receitas.WebApp.Pages.Receitas
{
    public class FinalizarApagarIngredientesModel : PageModel
    {
        private IItemService _itemService;
        private ValidarSessao _validarSessao;

        public FinalizarApagarIngredientesModel(IItemService itemService,ValidarSessao validarSessao) 
        {
            _itemService = itemService;
            _validarSessao = validarSessao;
        }
        public IActionResult OnGet(int idItem,string codigo)
        {
            if(!_validarSessao.Validar(codigo)) 
            {
                return BadRequest();
            }

            Item item = _itemService.GetById(idItem);

            if(item == null) 
            {
                return NotFound();
            }

            _itemService.Delete(idItem);

            return RedirectToPage("/Receitas/FinalizarAdicionarReceita", new { id = item.Receita.ReceitaId, codigo = codigo });
        }
    }
}
