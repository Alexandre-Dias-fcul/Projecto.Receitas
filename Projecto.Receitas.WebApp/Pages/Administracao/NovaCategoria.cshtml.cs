using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Projecto.Receitas.Domain.Model;
using Projecto.Receitas.Services.Interfaces;
using Projecto.Receitas.Services.Services;
using Projecto.Receitas.WebApp.ViewModel;

namespace Projecto.Receitas.WebApp.Pages.Administracao
{
    public class NovaCategoriaModel : PageModel
    {
        private ICategoriaService _categoriaService;
        public NovaCategoriaModel(ICategoriaService categoriaService) 
        { 
            _categoriaService = categoriaService;
        }

        [BindProperty]
        public CategoriaViewModel Categoria { get; set; }

        [BindProperty]
        public int IdPagina { get; set; }

        [BindProperty]
        public string? Texto { get; set; }

        public void OnGet(int idPagina=0, string? texto="")
        {
             IdPagina = idPagina;
             Texto = texto;
        }

        public IActionResult OnPost() 
        {
            if (ModelState.IsValid) 
            {
                Categoria categoria = new Categoria(Categoria.CategoriaNome);

                _categoriaService.Add(categoria);

                return RedirectToPage("/Administracao/VerCategorias", new  { id=IdPagina, texto = Texto});
            }

            return Page();
        }
    }
}
