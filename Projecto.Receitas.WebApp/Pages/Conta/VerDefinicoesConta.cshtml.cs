using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Projecto.Receitas.Domain.Model;
using Projecto.Receitas.Services.Interfaces;

namespace Projecto.Receitas.WebApp.Pages.Conta
{
    public class VerDefinicoesContaModel : PageModel
    {
        private IAccountService _accountService;
        private IUtilizadorService _utilizadorService;

        public VerDefinicoesContaModel(IAccountService accountService,IUtilizadorService utilizadorService) 
        { 
            _accountService = accountService;
            _utilizadorService = utilizadorService;
        }

        public Utilizador Utilizador {  get; set; }
        public Account Account { get; set; }
        public IActionResult OnGet()
        {
            int? utilizadorId = HttpContext.Session.GetInt32("UserId");

            if (utilizadorId == null)
            {
                return BadRequest();
            }

            Utilizador = _utilizadorService.GetById((int)utilizadorId);

            if (Utilizador == null)
            {
                return BadRequest();
            }

            Account = _accountService.GetByUtilizadorId((int)utilizadorId);

            return Page();
        }
    }
}
