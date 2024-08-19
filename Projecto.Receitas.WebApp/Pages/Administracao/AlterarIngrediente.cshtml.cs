using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Projecto.Receitas.Domain.Model;
using Projecto.Receitas.Services.Interfaces;
using Projecto.Receitas.Services.Services;
using Projecto.Receitas.WebApp.ViewModel;

namespace Projecto.Receitas.WebApp.Pages.Administracao
{
    public class AlterarIngredienteModel : PageModel
    {    
        private IIngredienteService _ingredienteService;

        public AlterarIngredienteModel(IIngredienteService ingredienteService) 
        { 
            _ingredienteService = ingredienteService;
        }

        [BindProperty]
        public IngredienteViewModelId Ingrediente {  get; set; }

        [BindProperty]
        public int IdPagina { get; set; }

        [BindProperty]
        public string? Texto { get; set; }
        public IActionResult OnGet(int idIngrediente,int idPagina=0,string? texto="")
        {
            Ingrediente ingrediente = _ingredienteService.GetById(idIngrediente);

            if (ingrediente == null) 
            {
                return NotFound();
            }

            Ingrediente = new IngredienteViewModelId()
            {
                IngredienteId = ingrediente.IngredienteId,
                IngredienteNome  = ingrediente.IngredienteNome
            };

            IdPagina = idPagina;

            Texto = texto;

            return Page();
        }

        public IActionResult OnPost() 
        {
            if (ModelState.IsValid) 
            {
                Ingrediente ingrediente = new Ingrediente(Ingrediente.IngredienteId, Ingrediente.IngredienteNome);
                
                _ingredienteService.Update(ingrediente);

                return RedirectToPage("/Administracao/VerIngredientes", new { id = IdPagina ,texto = Texto });
            }

            return Page();
        }
    }
}
