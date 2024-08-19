using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Projecto.Receitas.Domain.Model;
using Projecto.Receitas.Services.Interfaces;
using Projecto.Receitas.Services.Services;

namespace Projecto.Receitas.WebApp.Pages.Administracao
{
    public class ApagarImagemReceitaModel : PageModel
    {
        private IReceitaService _receitaService;

        public ApagarImagemReceitaModel(IReceitaService receitaService) 
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

            if (string.IsNullOrEmpty(receita.ImagemUrl))
            {
                return BadRequest();
            }

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/uploads-receitas", receita.ImagemUrl);

            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

            receita.ImagemUrl = "";

            _receitaService.Update(receita);

            return RedirectToPage("/Administracao/VerReceita", new { idReceita = idReceita, idPagina = idPagina, texto = texto });
        }
    }
}
