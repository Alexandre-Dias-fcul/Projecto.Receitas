using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Projecto.Receitas.Domain.Model;
using Projecto.Receitas.Services.Interfaces;

namespace Projecto.Receitas.WebApp.Pages.Conta
{
    public class ApagarFotoContaModel : PageModel
    {
        private IUtilizadorService _utilizadorService;

        public ApagarFotoContaModel(IUtilizadorService utilizadorService) 
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
                return BadRequest();
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

            return RedirectToPage("/Conta/VerDadosPessoais");
        }
    }
}
