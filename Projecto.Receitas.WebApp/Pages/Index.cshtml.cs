using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Projecto.Receitas.Domain.Model;
using Projecto.Receitas.Services.Interfaces;
using Projecto.Receitas.Services.Services;
using Projecto.Receitas.WebApp.ViewModel;

namespace Projecto.Receitas.WebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private IReceitaService _receitaService;

        private IAvaliacaoService _avaliacaoService;

        public IndexModel(ILogger<IndexModel> logger,IReceitaService receitaService,IAvaliacaoService avaliacaoService)
        {
            _logger = logger;
            _receitaService = receitaService;
            _avaliacaoService = avaliacaoService;
        }

        public List<ReceitaViewModelAvaliacao> Receitas { get; set; }
        public int Id {  get; set; }
        public int  MaximoId{ get; set; }
        public List<Receita> ReceitasTop3 { get; set; }

        public IActionResult OnGet(string texto="",int id=1)
        {

           if(id<1) 
           {
                id=1;
           }

           int numeroTotalDeReceitas = _receitaService.NumeroDeReceitasValidas(texto);

           if(numeroTotalDeReceitas%6 != 0) 
           {
                MaximoId = (int)Math.Floor((decimal)numeroTotalDeReceitas/6)+1;
           }
           else 
           {
                MaximoId = numeroTotalDeReceitas/6; 
           }

           List<Receita> receitas = _receitaService.GetReceitas(id*6,texto);

           Receitas = new List<ReceitaViewModelAvaliacao>();

           foreach (Receita receita in receitas) 
           {
                ReceitaViewModelAvaliacao receitaViewModelAvaliacao = new ReceitaViewModelAvaliacao()
                {
                    Receita = receita,
                    Avaliacao = _avaliacaoService.MediaDoValorByReceita(receita.ReceitaId)
                };

                Receitas.Add(receitaViewModelAvaliacao);
           }

           id++;

           Id=id;

           ReceitasTop3 = _avaliacaoService.GetTop3Raking();

           return Page();
        }
    }
}
