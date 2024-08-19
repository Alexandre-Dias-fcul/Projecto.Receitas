using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Projecto.Receitas.Domain.Model;
using Projecto.Receitas.Services.Interfaces;

namespace Projecto.Receitas.WebApp.Pages.Administracao
{
    public class ApagarFotoUtilizadorModel : PageModel
    {
        private IUtilizadorService _utilizadorService;

        public ApagarFotoUtilizadorModel(IUtilizadorService utilizadorService) 
        {
            _utilizadorService = utilizadorService;
        }

        public IActionResult OnGet(int idUser,int idPagina=0, string? texto="")
        {
            Utilizador utilizador = _utilizadorService.GetById(idUser);

            if (utilizador == null)
            {
                return NotFound();
            }

            if (string.IsNullOrEmpty(utilizador.FotoUrl))
            {
                return BadRequest();
            }

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/uploads-users", utilizador.FotoUrl);

            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

            utilizador.FotoUrl = null;

            _utilizadorService.Update(utilizador);

            return RedirectToPage("/Administracao/VerUtilizadores", new { id = idPagina, texto = texto });
        }
    }
}
