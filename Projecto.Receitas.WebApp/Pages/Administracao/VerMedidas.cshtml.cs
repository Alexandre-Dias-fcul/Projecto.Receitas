using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Projecto.Receitas.Domain.Model;
using Projecto.Receitas.Services.Interfaces;
using Projecto.Receitas.Services.Services;

namespace Projecto.Receitas.WebApp.Pages.Administracao
{
    public class VerMedidasModel : PageModel
    {
        private IMedidaService _medidaService;
        public VerMedidasModel(IMedidaService medidaService) 
        { 
            _medidaService = medidaService;
        }
        public int Id { get; set; }
        public int MaximoId { get; set; }
        public string? Texto { get; set; }
        public int NumeroTotalDeMedidas { get; set; }
        public List<Medida> Medidas { get; set; }
        public IActionResult OnGet(int id=0,string? texto="")
        {
            if (id<0)
            {
                id = 0;
            }

            NumeroTotalDeMedidas = _medidaService.NumeroTotalDeMedidas(texto);

            MaximoId = (int)Math.Floor((decimal)NumeroTotalDeMedidas/5);

            Medidas = _medidaService.GetAllPesquisa(texto, id*5, 5);

            Id= id;

            Texto = texto;

            return Page();
        }
    }
}
