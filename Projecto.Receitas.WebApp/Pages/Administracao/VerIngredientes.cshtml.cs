using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Projecto.Receitas.Domain.Model;
using Projecto.Receitas.Services.Interfaces;
using Projecto.Receitas.Services.Services;

namespace Projecto.Receitas.WebApp.Pages.Administracao
{
    public class VerIngredientesModel : PageModel
    {
        private IIngredienteService _ingredienteService;
        public VerIngredientesModel(IIngredienteService ingredienteService) 
        {
            _ingredienteService = ingredienteService;
        }
        public int Id {  get; set; }
        public int MaximoId { get; set; }
        public string? Texto { get; set; }
        public int NumeroTotalDeIngredientes { get; set; }
        public List<Ingrediente> Ingredientes { get; set; }
        public IActionResult OnGet(int id=0,string? texto="")
        {
            if (id<0) 
            {
                id = 0;
            }

            NumeroTotalDeIngredientes = _ingredienteService.NumeroTotalDeIngredientes(texto);

            MaximoId = (int)Math.Floor((decimal)NumeroTotalDeIngredientes/5);

            Ingredientes =_ingredienteService.GetAllPesquisa(texto, id*5,5);

            Id= id;

            Texto = texto;

            return Page();
        }
    }
}
