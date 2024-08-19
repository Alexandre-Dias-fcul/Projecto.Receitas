using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Projecto.Receitas.Domain.Model;
using Projecto.Receitas.Services.Interfaces;
using Projecto.Receitas.Services.Services;
using Projecto.Receitas.WebApp.ViewModel;

namespace Projecto.Receitas.WebApp.Pages.Receitas
{
    public class ApagarFavoritoModel : PageModel
    {
        private IFavoritoService _favoritoService;

        private IUtilizadorService _utilizadorService;

        private IReceitaService _receitaService;

        private ValidarSessao _validarSessao;

        public ApagarFavoritoModel(IFavoritoService favoritoService,IUtilizadorService utilizadorService,
            IReceitaService receitaService,ValidarSessao validarsessao) 
        {
            _favoritoService = favoritoService;
            _utilizadorService = utilizadorService;
            _receitaService = receitaService;
            _validarSessao = validarsessao;
        }
        public IActionResult OnGet(int id,string codigo)
        {
            if (!_validarSessao.Validar(codigo)) 
            {
                return BadRequest();
            }

            int? utilizadorId = HttpContext.Session.GetInt32("UserId");

            if (utilizadorId == null) 
            {
                return BadRequest();
            }

            Utilizador utilizador = _utilizadorService.GetById((int)utilizadorId);

            if(utilizador == null) 
            {
                return BadRequest();
            }

            Receita receita = _receitaService.GetById(id);
           
            if (receita == null) 
            {
                return NotFound();
            }

            Favorito favorito = new Favorito(receita,utilizador);

            _favoritoService.Delete(favorito);

            return RedirectToPage("/Receitas/VerFavoritos");
        }
    }
}
