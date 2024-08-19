using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Projecto.Receitas.Domain.Model;
using Projecto.Receitas.Services.Interfaces;
using Projecto.Receitas.Services.Services;
using Projecto.Receitas.WebApp.ViewModel;

namespace Projecto.Receitas.WebApp.Pages.Receitas
{
    public class VerFavoritosModel : PageModel
    {
        private IFavoritoService _favoritoService;

        private IAvaliacaoService _avaliacaoService;

        private IReceitaService _receitaService;

        private ValidarSessao _validarSessao;

        public VerFavoritosModel(IFavoritoService favoritoService,IAvaliacaoService avaliacaoService, 
                                   IReceitaService receitaService,ValidarSessao validarSessao)
        { 
            _favoritoService = favoritoService;
            _avaliacaoService = avaliacaoService;
            _receitaService = receitaService;
            _validarSessao = validarSessao;
        }
        public List<ReceitaViewModelAvaliacao> Receitas {  get; set; }
        public int Id { get; set; }
        public int MaximoId { get; set; }
        public string Codigo {  get; set; }
        public IActionResult OnGet(int id=1)
        {
            int? utilizadorId = HttpContext.Session.GetInt32("UserId");

            if (utilizadorId == null)
            {
                return BadRequest();
            }

            if (id<1) 
            {
                id=1;
            }

            int numeroTotalDeFavoritos = _favoritoService.NumeroTotalFavoritosByUtilizadorId((int)utilizadorId);

            if (numeroTotalDeFavoritos%6 !=0)
            {
                MaximoId = (int)Math.Floor((decimal)numeroTotalDeFavoritos/6)+1;
            }
            else
            {
                MaximoId = numeroTotalDeFavoritos/6;
            }

            List<int> receitaIds = _favoritoService.GetByUtilizadorId(id*6, (int)utilizadorId);

            List<Receita> receitas = new List<Receita>();

            foreach (int receitaId in receitaIds)
            {
                Receita receita = _receitaService.GetById(receitaId);

                receitas.Add(receita);
            }

            Receitas = new List<ReceitaViewModelAvaliacao>();

            foreach(Receita receita in receitas) 
            {
                ReceitaViewModelAvaliacao receitaViewModelAvaliacao = new ReceitaViewModelAvaliacao()
                {
                    Receita = receita,
                    Avaliacao = _avaliacaoService.MediaDoValorByReceita(receita.ReceitaId)
                };

                Receitas.Add(receitaViewModelAvaliacao);
            }

            id++;

            Id= id;

            Codigo = _validarSessao.Encriptar((int)utilizadorId);

            return Page();
        }
    }
}
