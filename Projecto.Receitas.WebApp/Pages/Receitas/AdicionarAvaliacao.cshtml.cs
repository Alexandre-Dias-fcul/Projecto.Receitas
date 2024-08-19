using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Projecto.Receitas.Domain.Model;
using Projecto.Receitas.Services.Interfaces;
using Projecto.Receitas.Services.Services;
using Projecto.Receitas.WebApp.ViewModel;

namespace Projecto.Receitas.WebApp.Pages.Receitas
{
    public class AdicionarAvaliacaoModel : PageModel
    {
        private IReceitaService _receitaService;

        private IAvaliacaoService _avaliacaoService;

        private IUtilizadorService _utilizadorService;

        private ValidarSessao _validarSessao;
        public AdicionarAvaliacaoModel(IReceitaService receitaService,IAvaliacaoService avaliacaoService,
            IUtilizadorService utilizadorService, ValidarSessao validarSessao) 
        { 
            _receitaService = receitaService;
            _avaliacaoService = avaliacaoService;
            _utilizadorService = utilizadorService;
            _validarSessao = validarSessao;
        }
        public double AvaliacaoMedia { get; set; }
        public Receita Receita { get; set; }

        [BindProperty]
        public AvaliacaoViewModel Avaliacao {  get; set; }

        [BindProperty]
        public string Codigo {  get; set; }

        public IActionResult OnGet(int id ,string codigo)
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

            if (utilizador == null) 
            {
                return BadRequest();
            }

            Receita = _receitaService.GetById(id);

            if (Receita == null)
            {
                return NotFound();
            }

            if(Receita.Estado != TipoDeEstado.Valida) 
            {
                return BadRequest();
            }

            Avaliacao = new AvaliacaoViewModel()
            {
                ReceitaId = id,
                UtilizadorId =(int)utilizadorId
            };

            AvaliacaoMedia = _avaliacaoService.MediaDoValorByReceita(id);

            Codigo = codigo;

            return Page();
        }

        public IActionResult OnPost() 
        {
            if (ModelState.IsValid && Avaliacao.Valor!=0)
            {
                Avaliacao avaliacao = new Avaliacao(Avaliacao.Valor, _receitaService.GetById(Avaliacao.ReceitaId),
                                                          _utilizadorService.GetById(Avaliacao.UtilizadorId));

                Avaliacao AvaliacaoExiste =_avaliacaoService.GetByPrimaryKey(Avaliacao.ReceitaId, Avaliacao.UtilizadorId);

                if (AvaliacaoExiste == null) 
                {
                    _avaliacaoService.Add(avaliacao);
                }
                else 
                {
                    _avaliacaoService.Update(avaliacao);
                }

                return RedirectToPage("/Detalhes", new { id =Avaliacao.ReceitaId });
            }

            AvaliacaoMedia = _avaliacaoService.MediaDoValorByReceita(Avaliacao.ReceitaId);

            Receita = _receitaService.GetById(Avaliacao.ReceitaId);

            return Page();
        }
    }
}
