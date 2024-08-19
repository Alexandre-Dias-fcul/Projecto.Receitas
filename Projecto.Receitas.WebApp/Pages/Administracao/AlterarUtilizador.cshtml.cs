using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Projecto.Receitas.Domain.Model;
using Projecto.Receitas.Services.Interfaces;
using Projecto.Receitas.Services.Services;
using Projecto.Receitas.WebApp.ViewModel;

namespace Projecto.Receitas.WebApp.Pages.Administracao
{
    public class AlterarUtilizadorModel : PageModel
    {
        private IUtilizadorService _utilizadorService;

        public AlterarUtilizadorModel(IUtilizadorService utilizadorService) 
        {
            _utilizadorService = utilizadorService;
        }

        public IActionResult OnGet(int idUser,int idPagina = 0, string? texto = "")
        {
            Utilizador utilizador = _utilizadorService.GetById(idUser);

            if (utilizador == null) 
            {
                return NotFound();
            }

            if (utilizador.FotoUrl==string.Empty)
            {
                return RedirectToPage("/Administracao/AlterarUtilizadorSemFoto", new { idUser = idUser,idPagina=idPagina,texto=texto});

            }
            else
            {
                return RedirectToPage("/Administracao/AlterarUtilizadorComFoto", new { idUser = idUser ,idPagina=idPagina,texto=texto});
            }
        }
    }
}
