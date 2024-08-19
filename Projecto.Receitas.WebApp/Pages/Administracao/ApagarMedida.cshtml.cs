using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Projecto.Receitas.Domain.Model;
using Projecto.Receitas.Services.Interfaces;
using Projecto.Receitas.Services.Services;

namespace Projecto.Receitas.WebApp.Pages.Administracao
{
    public class ApagarMedidaModel : PageModel
    {
        private IMedidaService _medidaService;

        private IItemService _itemService;

        public ApagarMedidaModel(IMedidaService medidaService, IItemService itemService) 
        { 
            _medidaService = medidaService;
            _itemService = itemService; 
        }

        public IActionResult OnGet(int idMedida, int idPagina=0 , string? texto="")
        {
            Medida medida = _medidaService.GetById(idMedida);
            
            if (medida == null) 
            {
                return NotFound();
            }

            var items = _itemService.GetByMedidaId(idMedida);

            if (items.Count == 0) 
            {
                _medidaService.Delete(idMedida);
            }

            return RedirectToPage("/Administracao/VerMedidas", new { id = idPagina, texto = texto });
        }
    }
}
