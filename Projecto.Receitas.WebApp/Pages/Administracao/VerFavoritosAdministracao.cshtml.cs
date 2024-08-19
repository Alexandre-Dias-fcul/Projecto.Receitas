using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Projecto.Receitas.Domain.Model;
using Projecto.Receitas.Services.Interfaces;

namespace Projecto.Receitas.WebApp.Pages.Administracao
{
    public class VerFavoritosAdministracaoModel : PageModel
    {

        private IFavoritoService _favoritoService;

        private IUtilizadorService _utilizadorService;

        private IReceitaService _receitaService;
        public VerFavoritosAdministracaoModel(IFavoritoService favoritoService,IUtilizadorService utilizadorService,
                                             IReceitaService receitaService) 
        { 
            _favoritoService = favoritoService; 
            _utilizadorService = utilizadorService;
            _receitaService = receitaService;
        }
        public List<Favorito> Favoritos { get; set; }
        public Utilizador Utilizador { get; set; }
        public int IdPagina { get; set; }
        public string? Texto { get; set; }
        public IActionResult OnGet(int idUser, int idPagina = 0, string? texto = "")
        {
            Utilizador = _utilizadorService.GetById(idUser);

            if (Utilizador == null)
            {
                return NotFound();
            }

            Favoritos = _favoritoService.GetByUtilizadorId(idUser);

            foreach (Favorito favorito in Favoritos) 
            {
                favorito.Receita = _receitaService.GetById(favorito.Receita.ReceitaId);
            }

            IdPagina = idPagina;

            Texto = texto;

            return Page();
        }
    }
}
