using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Projecto.Receitas.Domain.Model;
using Projecto.Receitas.Services.Interfaces;

namespace Projecto.Receitas.WebApp.Pages.Administracao
{
    public class AlterarReceitaModel : PageModel
    {
        private IReceitaService _receitaService;

        public AlterarReceitaModel(IReceitaService receitaService)
        {
            _receitaService = receitaService;
        }
        public IActionResult OnGet(int idReceita, int idPagina = 0, string? texto = "")
        {
            Receita receita = _receitaService.GetById(idReceita);

            if (receita == null)
            {
                return NotFound();
            }

            if (receita.ImagemUrl==string.Empty)
            {
                return RedirectToPage("/Administracao/AlterarReceitaSemFoto",
                                new { idReceita = idReceita, idPagina = idPagina, texto = texto });
            }
            else
            {
                return RedirectToPage("/Administracao/AlterarReceitaComFoto",
                             new { idReceita = idReceita, idPagina = idPagina, texto = texto });
            }
        }
    }
}
