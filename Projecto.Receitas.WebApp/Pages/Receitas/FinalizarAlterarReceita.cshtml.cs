using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Projecto.Receitas.Domain.Model;
using Projecto.Receitas.Services.Interfaces;

namespace Projecto.Receitas.WebApp.Pages.Receitas
{
    public class FinalizarAlterarReceitaModel : PageModel
    {
        private IReceitaService _receitaService;

        public FinalizarAlterarReceitaModel(IReceitaService receitaService)
        {
            _receitaService = receitaService;
        }
        public IActionResult OnGet(int id,string codigo)
        {
            Receita receita = _receitaService.GetById(id);

            if (receita == null)
            {
                return NotFound();
            }

            if (receita.ImagemUrl==string.Empty)
            {
                return RedirectToPage("/Receitas/FinalizarAlterarReceitaSemImagem", new { id= id , codigo = codigo});
            }
            else
            {
                return RedirectToPage("/Receitas/FinalizarAlterarReceitaComImagem", new { id = id , codigo = codigo});
            
            }
        }
    }
}
