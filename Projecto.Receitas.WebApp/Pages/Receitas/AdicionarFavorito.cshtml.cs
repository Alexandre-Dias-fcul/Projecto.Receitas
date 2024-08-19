using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Projecto.Receitas.Domain.Model;
using Projecto.Receitas.Services.Interfaces;
using Projecto.Receitas.Services.Services;

namespace Projecto.Receitas.WebApp.Pages.Receitas
{
    public class AdicionarFavoritoModel : PageModel
    {
        private IFavoritoService _favoritoService;
        private IUtilizadorService _utilizadorService;
        private IReceitaService _receitaService;
        private ValidarSessao _validarSessao;
        public AdicionarFavoritoModel(IFavoritoService favoritoService,IUtilizadorService utilizadorService,
            IReceitaService receitaService,ValidarSessao validarSessao) 
        {
            _favoritoService = favoritoService;
            _utilizadorService = utilizadorService;
            _receitaService = receitaService;
            _validarSessao = validarSessao;
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

            Receita receita = _receitaService.GetById(id);

            if (receita == null)
            {
                return NotFound();
            }

            if (receita.Estado!=TipoDeEstado.Valida) 
            {
                return BadRequest();
            }

            Favorito favorito = new Favorito(receita,utilizador);

            _favoritoService.Add(favorito);

            return RedirectToPage("/Receitas/VerFavoritos");
        }
    }
}
