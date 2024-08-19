using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Projecto.Receitas.Domain.Model;
using Projecto.Receitas.Services.Interfaces;
using Projecto.Receitas.Services.Services;
using Projecto.Receitas.WebApp.ViewModel;

namespace Projecto.Receitas.WebApp.Pages.Administracao
{
    public class AlterarCategoriaModel : PageModel
    {
        private ICategoriaService _categoriaService;

        public AlterarCategoriaModel(ICategoriaService categoriaService) 
        { 
            _categoriaService= categoriaService;
        }

        [BindProperty]
        public  CategoriaViewModelId Categoria {  get; set; }

        [BindProperty]
        public int IdPagina { get; set; }

        [BindProperty]
        public string? Texto { get; set; }
        public IActionResult OnGet(int idCategoria,int idPagina=0,string? texto="")
        {
            Categoria categoria = _categoriaService.GetById(idCategoria);

            if (categoria==null) 
            {
                return NotFound();
            }

            Categoria = new CategoriaViewModelId
            { CategoriaId = categoria.CategoriaId, CategoriaNome = categoria.CategoriaNome };

            IdPagina = idPagina;

            Texto=texto;

            return Page();
        }

        public IActionResult OnPost() 
        {
            if (ModelState.IsValid) 
            {
                Categoria categoria = new Categoria(Categoria.CategoriaId, Categoria.CategoriaNome);

                _categoriaService.Update(categoria);

                return RedirectToPage("/Administracao/VerCategorias" , new { id=IdPagina, texto=Texto });
            }

            return Page();
        }
    }
}
