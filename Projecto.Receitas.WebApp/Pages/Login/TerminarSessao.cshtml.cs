using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Projecto.Receitas.Domain.Model;
using System.Net.NetworkInformation;

namespace Projecto.Receitas.WebApp.Pages.Login
{
    public class TerminarSessaoModel : PageModel
    {
        public async Task<IActionResult> OnGet()
        {
            HttpContext.Session.Remove("Permissoes");
            HttpContext.Session.Remove("Nome");
            HttpContext.Session.Remove("UserId");
            HttpContext.Session.Remove("Key");
            HttpContext.Session.Remove("IV");

            await HttpContext.SignOutAsync();

            return RedirectToPage("/Login/IniciarSessao");
        }
    }
}
