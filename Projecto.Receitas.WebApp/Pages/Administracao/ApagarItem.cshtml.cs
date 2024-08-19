using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Project.Receitas.Data.Interfaces;
using Projecto.Receitas.Domain.Model;
using Projecto.Receitas.Services.Interfaces;
using Projecto.Receitas.Services.Services;

namespace Projecto.Receitas.WebApp.Pages.Administracao
{
    public class ApagarItemModel : PageModel
    {
        private IItemService _itemService;

        public ApagarItemModel(IItemService itemService) 
        { 
            _itemService = itemService;
        }
        public IActionResult OnGet(int idItem,int idPagina = 0, string? texto = "")
        {
            Item item = _itemService.GetById(idItem);

            if(item == null) 
            {
                return NotFound();
            }
            
            _itemService.Delete(idItem);

            return RedirectToPage("/Administracao/VerReceita", 
                              new { idReceita = item.Receita.ReceitaId ,idPagina = idPagina, texto=texto});
        }
    }
}
