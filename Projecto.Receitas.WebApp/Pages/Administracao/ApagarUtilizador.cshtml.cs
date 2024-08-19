using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Projecto.Receitas.Domain.Model;
using Projecto.Receitas.Services.Interfaces;
using Projecto.Receitas.Services.Services;

namespace Projecto.Receitas.WebApp.Pages.Administracao
{
    public class ApagarUtilizadorModel : PageModel
    {
        private IUtilizadorService _utilizadorService;

        public ApagarUtilizadorModel(IUtilizadorService utilizadorService) 
        {
            _utilizadorService = utilizadorService;
        }

        public IActionResult OnGet(int idUser,int idPagina = 0, string? texto = "")
        {
            Utilizador utilizador = _utilizadorService.GetById(idUser);

            if(utilizador == null) 
            {
                return NotFound();
            }

            _utilizadorService.Delete(idUser);

            return RedirectToPage("/Administracao/VerUtilizadores", new { id = idPagina , texto = texto});
        }
    }
}
