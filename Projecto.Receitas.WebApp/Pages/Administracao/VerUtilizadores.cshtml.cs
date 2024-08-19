using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Projecto.Receitas.Domain.Model;
using Projecto.Receitas.Services.Interfaces;
using Projecto.Receitas.Services.Services;

namespace Projecto.Receitas.WebApp.Pages.Administracao
{
    public class VerUtilizadoresModel : PageModel
    {
        private IUtilizadorService _utilizadorService;
        public VerUtilizadoresModel(IUtilizadorService utilizadorService) 
        { 
            _utilizadorService = utilizadorService;
        }
        public int Id { get; set; }
        public int MaximoId { get; set; }
        public string? Texto { get; set; }
        public int NumeroTotalDeUtilizadores { get; set; }
        public List<Utilizador> Utilizadores { get; set; }
        public IActionResult OnGet(string? texto = "",int id=0)
        {
            if (id<0)
            {
                id = 0;
            }

            NumeroTotalDeUtilizadores = _utilizadorService.NumeroTotalDeUtilizadores(texto);

            MaximoId = (int)Math.Floor((decimal)NumeroTotalDeUtilizadores/5);

            Utilizadores = _utilizadorService.GetAllPesquisa(texto, id*5, 5);

            Id= id;

            Texto = texto;

            return Page();
        }
    }
}
