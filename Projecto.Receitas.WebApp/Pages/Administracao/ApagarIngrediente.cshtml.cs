using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Projecto.Receitas.Domain.Model;
using Projecto.Receitas.Services.Interfaces;
using Projecto.Receitas.Services.Services;

namespace Projecto.Receitas.WebApp.Pages.Administracao
{
    public class ApagarIngredienteModel : PageModel
    {    
        private IIngredienteService _ingredienteService;

        private IItemService _itemService;

        public ApagarIngredienteModel(IIngredienteService ingredienteService,IItemService itemService) 
        {
            _ingredienteService = ingredienteService;
            _itemService = itemService;
        }
        public IActionResult OnGet(int idIngrediente,int idPagina = 0, string? texto = "")
        {
            Ingrediente ingrediente = _ingredienteService.GetById(idIngrediente);

            if(ingrediente == null) 
            {
                return NotFound();
            }

            List<Item> items = _itemService.GetByIngredienteId(idIngrediente);

            if (items.Count==0) 
            {
                _ingredienteService.Delete(idIngrediente);
            }

            return RedirectToPage("/Administracao/VerIngredientes",new { id = idPagina, texto = texto });
        }
    }
}
