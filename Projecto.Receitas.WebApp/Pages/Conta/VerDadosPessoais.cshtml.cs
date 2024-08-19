using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Projecto.Receitas.Domain.Model;
using Projecto.Receitas.Services.Interfaces;
using Projecto.Receitas.Services.Services;

namespace Projecto.Receitas.WebApp.Pages.Conta
{
    public class VerDadosPessoaisModel : PageModel
    {
        private IUtilizadorService _utilizadorService;
        public VerDadosPessoaisModel(IUtilizadorService utilizadorService) 
        {
            _utilizadorService = utilizadorService;
        }
        public Utilizador Utilizador { get; set; }  
        public IActionResult OnGet()
        {
            int? utilizadorId = HttpContext.Session.GetInt32("UserId");

            if(utilizadorId == null) 
            {
                return BadRequest();
            }

            Utilizador = _utilizadorService.GetById((int)utilizadorId);

            if (Utilizador==null) 
            {
                return BadRequest();
            }

            return Page();
        }
    }
}
