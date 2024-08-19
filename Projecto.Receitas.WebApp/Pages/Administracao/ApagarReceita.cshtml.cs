using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Projecto.Receitas.Domain.Model;
using Projecto.Receitas.Services.Interfaces;
using Projecto.Receitas.Services.Services;

namespace Projecto.Receitas.WebApp.Pages.Administracao
{
    public class ApagarReceitaModel : PageModel
    {
        private IReceitaService _receitaService;

        public ApagarReceitaModel(IReceitaService receitaService) 
        { 
            _receitaService = receitaService;
        }
        public IActionResult OnGet(int idReceita,int idPagina = 0,string? texto= "")
        {
            Receita receita =_receitaService.GetById(idReceita);

            if(receita == null) 
            {   
                return NotFound();
            }

            _receitaService.Delete(receita);

            return RedirectToPage("/Administracao/VerReceitas" , new { id=idPagina,texto = texto});
        }
    }
}
