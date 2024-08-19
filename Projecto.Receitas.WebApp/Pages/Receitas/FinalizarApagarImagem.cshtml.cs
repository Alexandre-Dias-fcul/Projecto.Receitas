using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Projecto.Receitas.Domain.Model;
using Projecto.Receitas.Services.Interfaces;

namespace Projecto.Receitas.WebApp.Pages.Receitas
{
    public class FinalizarApagarImagemModel : PageModel
    {
        private IReceitaService _receitaService;

        private ValidarSessao _validarSessao;

        public FinalizarApagarImagemModel(IReceitaService receitaService, ValidarSessao validarSessao)
        {
            _receitaService = receitaService;
            _validarSessao =validarSessao;
        }

        public IActionResult OnGet(int id, string codigo)
        {
            if (!_validarSessao.Validar(codigo)) 
            {
                return BadRequest();
            }

            Receita receita = _receitaService.GetById(id);

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

            return RedirectToPage("/Receitas/FinalizarAdicionarReceita", new { id = id , codigo = codigo });
        }
    }
}
