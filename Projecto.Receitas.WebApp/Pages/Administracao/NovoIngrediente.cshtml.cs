using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Projecto.Receitas.Domain.Model;
using Projecto.Receitas.Services.Interfaces;
using Projecto.Receitas.Services.Services;
using Projecto.Receitas.WebApp.ViewModel;

namespace Projecto.Receitas.WebApp.Pages.Administracao
{
    public class NovoIngredienteModel : PageModel
    {
        private IIngredienteService _ingredienteService;

        public NovoIngredienteModel(IIngredienteService ingredienteService) 
        { 
            _ingredienteService = ingredienteService;
        }

        [BindProperty]
        public IngredienteViewModel Ingrediente { get; set; }

        [BindProperty]
        public int IdPagina { get; set; }

        [BindProperty]
        public string? Texto { get; set; }
        public void OnGet(int idPagina=0,string? texto = "")
        {
            IdPagina = idPagina;
            Texto = texto;
        }

        public IActionResult OnPost() 
        {
            if (ModelState.IsValid) 
            {
                Ingrediente ingrediente = new Ingrediente(Ingrediente.IngredienteNome);

                _ingredienteService.Add(ingrediente);

                return RedirectToPage("/Administracao/VerIngredientes", new { id=IdPagina , texto=Texto } );
            }

            return Page();
        }
    }
}
