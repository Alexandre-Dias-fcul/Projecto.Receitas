using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Project.Receitas.Data.Interfaces;
using Projecto.Receitas.Domain.Model;
using Projecto.Receitas.Services.Interfaces;
using Projecto.Receitas.WebApp.ViewModel;

namespace Projecto.Receitas.WebApp.Pages.Administracao
{
    public class NovaMedidaModel : PageModel
    {
        private IMedidaService _medidaService;

        public NovaMedidaModel(IMedidaService medidaService) 
        { 
            _medidaService = medidaService;
        }

        [BindProperty]
        public MedidaViewModel Medida {  get; set; }

        [BindProperty]
        public int IdPagina {  get; set; }

        [BindProperty]
        public string? Texto { get; set; }

        public void OnGet(int idPagina=0,string? texto="")
        {
            IdPagina = idPagina;
            Texto = texto;
        }

        public IActionResult OnPost() 
        {
            if (ModelState.IsValid)
            {
                Medida medida = new Medida(Medida.MedidaNome);

                _medidaService.Add(medida);

                return RedirectToPage("/Administracao/VerMedidas" ,new { id = IdPagina, texto = Texto });
            }

            return Page();
        }
    }
}
