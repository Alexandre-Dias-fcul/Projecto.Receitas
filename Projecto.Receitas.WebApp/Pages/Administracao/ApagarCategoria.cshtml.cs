using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Projecto.Receitas.Domain.Model;
using Projecto.Receitas.Services.Interfaces;
using Projecto.Receitas.Services.Services;

namespace Projecto.Receitas.WebApp.Pages.Administracao
{
    public class ApagarCategoriaModel : PageModel
    {
        private ICategoriaService _categoriaService ;

        private IReceitaService _receitaService;

        public ApagarCategoriaModel(ICategoriaService categoriaService,IReceitaService receitaService) 
        {
            _categoriaService = categoriaService;
            _receitaService = receitaService;
        }
        public IActionResult OnGet(int idCategoria,int idPagina=0,string? texto="")
        { 
            Categoria categoria = _categoriaService.GetById(idCategoria);

            if(categoria == null) 
            {
                return NotFound();
            }

            List<Receita> receitaList = _receitaService.GetByCategoriaId(idCategoria);

            if (receitaList.Count==0) 
            {
                _categoriaService.Delete(idCategoria);
            }
            
            return RedirectToPage("/Administracao/VerCategorias", new { id =idPagina,texto=texto });
        }
    }
}
