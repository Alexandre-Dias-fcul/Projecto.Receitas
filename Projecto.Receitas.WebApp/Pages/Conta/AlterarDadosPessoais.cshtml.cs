using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Projecto.Receitas.Domain.Model;
using Projecto.Receitas.Services.Interfaces;
using Projecto.Receitas.Services.Services;
using Projecto.Receitas.WebApp.ViewModel;

namespace Projecto.Receitas.WebApp.Pages.Conta
{
    public class AlterarDadosPessoaisModel : PageModel
    {
        private IUtilizadorService _utilizadorService;

        public AlterarDadosPessoaisModel(IUtilizadorService utilizadorService) 
        {
            _utilizadorService = utilizadorService;
        }

        public IActionResult OnGet()
        {
            int? utilizadorId = HttpContext.Session.GetInt32("UserId");

            if (utilizadorId == null)
            {
                return BadRequest();
            }

            Utilizador utilizador = _utilizadorService.GetById((int)utilizadorId);

            if (utilizador == null) 
            { 
                return NotFound();
            }

            if (utilizador.FotoUrl==string.Empty)
            {
                return RedirectToPage("/Conta/AlterarDadosPessoaisSemFoto");
                
            }
            else 
            {
                return RedirectToPage("/Conta/AlterarDadosPessoaisComFoto");
            }
        }
    }
}
