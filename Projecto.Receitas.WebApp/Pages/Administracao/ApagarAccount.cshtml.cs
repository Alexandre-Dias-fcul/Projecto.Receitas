using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Identity.Client;
using Projecto.Receitas.Domain.Model;
using Projecto.Receitas.Services.Interfaces;
using Projecto.Receitas.Services.Services;

namespace Projecto.Receitas.WebApp.Pages.Administracao
{
    public class ApagarAccountModel : PageModel
    {
        private IAccountService _accountService;

        private IUtilizadorService _utilizadorService;

        public ApagarAccountModel(IAccountService accountService,IUtilizadorService utilizadorService) 
        {
            _accountService = accountService;
            _utilizadorService = utilizadorService;
        }
        public IActionResult OnGet(int idUser,int idPagina = 0, string? texto = "")
        {

            Utilizador utilizador = _utilizadorService.GetById(idUser);

            if(utilizador == null) 
            { 
                return NotFound();
            }

            _accountService.DeleteByUtilizadorId(idUser);
            
            return RedirectToPage("/Administracao/VerAccount", new { idUser = idUser, idPagina=idPagina , texto = texto});
        }
    }
}
