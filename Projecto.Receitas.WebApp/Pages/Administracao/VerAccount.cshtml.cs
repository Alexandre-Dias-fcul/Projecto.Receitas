using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Projecto.Receitas.Domain.Model;
using Projecto.Receitas.Services.Interfaces;
using Projecto.Receitas.Services.Services;

namespace Projecto.Receitas.WebApp.Pages.Administracao
{
    public class VerAccountModel : PageModel
    {
        private IAccountService _accountService;

        private IUtilizadorService _utilizadorService;
        public VerAccountModel(IAccountService accountService,IUtilizadorService utilizadorService) 
        {
            _accountService = accountService;
            _utilizadorService = utilizadorService;
        }
        public Account Account {  get; set; }
        public Utilizador Utilizador {  get; set; } 
        public int IdPagina { get; set; }

        public string? Texto {  get; set; }

        public IActionResult OnGet(int idUser,int idPagina = 0, string? texto = "")
        {
            Utilizador = _utilizadorService.GetById(idUser);

            if(Utilizador == null) 
            {
                return NotFound();
            }

            Account=_accountService.GetByUtilizadorId(idUser);

            IdPagina = idPagina;

            Texto = texto;

            return Page();
        }
    }
}
