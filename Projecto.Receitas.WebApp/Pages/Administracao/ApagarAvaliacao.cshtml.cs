using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Projecto.Receitas.Domain.Model;
using Projecto.Receitas.Services.Interfaces;
using Projecto.Receitas.Services.Services;
using Projecto.Receitas.WebApp.Pages.Receitas;

namespace Projecto.Receitas.WebApp.Pages.Administracao
{
    public class ApagarAvaliacaoModel : PageModel
    {    
        private IAvaliacaoService _avaliacaoService;

        public ApagarAvaliacaoModel(IAvaliacaoService avaliacaoService) 
        {
            _avaliacaoService = avaliacaoService;
        }
        public IActionResult  OnGet(int idReceita, int idUser, int idPagina= 0, string? texto = "")
        {
            Avaliacao avaliacao = _avaliacaoService.GetByPrimaryKey(idReceita,idUser);

            if (avaliacao == null) 
            {
                return NotFound();
            }

            _avaliacaoService.Delete(avaliacao);

            return RedirectToPage("/Administracao/VerAvaliacoes", new { idUser = idUser, idPagina = idPagina, texto = texto });
        }
    }
}
