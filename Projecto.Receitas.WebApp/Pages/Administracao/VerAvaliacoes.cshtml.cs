using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Projecto.Receitas.Domain.Model;
using Projecto.Receitas.Services.Interfaces;

namespace Projecto.Receitas.WebApp.Pages.Administracao
{
    public class VerAvaliacoesModel : PageModel
    {
        private IAvaliacaoService _avaliacaoService;

        private IUtilizadorService _utilizadorService;

        private IReceitaService _receitaService;
        public Utilizador Utilizador { get; set; }
        public List<Avaliacao> Avaliacoes { get; set; }
        public int IdPagina { get; set; }
        public string? Texto { get; set; }

        public VerAvaliacoesModel(IAvaliacaoService avaliacaoService, IUtilizadorService utilizadorService,
            IReceitaService receitaService)
        {
            _avaliacaoService = avaliacaoService;
            _utilizadorService = utilizadorService;
            _receitaService=receitaService;
        }

        public IActionResult OnGet(int idUser, int idPagina = 0, string? texto = "")
        {
            Utilizador = _utilizadorService.GetById(idUser);

            if (Utilizador == null)
            {
                return NotFound();
            }

            Avaliacoes = _avaliacaoService.GetByUtilizadorId(idUser);

            foreach(Avaliacao avaliacao in Avaliacoes) 
            {
                avaliacao.Receita = _receitaService.GetById(avaliacao.Receita.ReceitaId);
            }

            IdPagina = idPagina;

            Texto = texto;

            return Page();
        }
    }
}
