using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Projecto.Receitas.Domain.Model;
using Projecto.Receitas.Services.Interfaces;
using Projecto.Receitas.Services.Services;

namespace Projecto.Receitas.WebApp.Pages.Administracao
{
    public class VerReceitasModel : PageModel
    {
        private IReceitaService _receitaService;
        public VerReceitasModel(IReceitaService receitaService) 
        { 
            _receitaService = receitaService;   
        }
        public int Id { get; set; }
        public int MaximoId { get; set; }
        public string? Texto { get; set; }
        public int NumeroTotalDeReceitas { get; set; }
        public List<Receita> Receitas { get; set; }
        public IActionResult OnGet(string? texto = "", int id = 0)
        {
            if (id<0)
            {
                id = 0;
            }

            NumeroTotalDeReceitas = _receitaService.NumeroTotalDeReceitasPesquisa(texto);

            MaximoId = (int)Math.Floor((decimal)NumeroTotalDeReceitas/5);

            Receitas = _receitaService.GetAllPesquisa(texto, id*5, 5);

            Id= id;

            Texto = texto;

            return Page();
        }
    }
}
