using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Projecto.Receitas.Domain.Model;
using Projecto.Receitas.Services.Interfaces;
using Projecto.Receitas.Services.Services;
using Projecto.Receitas.WebApp.ViewModel;

namespace Projecto.Receitas.WebApp.Pages.Receitas
{
    public class AdicionarComentarioModel : PageModel
    {
        private IReceitaService _receitaService;

        private IAvaliacaoService _avaliacaoService;

        private IComentarioService _comentarioService;

        private IUtilizadorService _utilizadorService;

        private ValidarSessao _validarSessao;

        public AdicionarComentarioModel(IReceitaService receitaService,IAvaliacaoService avaliacaoService,
               IComentarioService comentarioService, IUtilizadorService utilizadorService, ValidarSessao validarSessao)
        {
            _avaliacaoService = avaliacaoService;
            _receitaService = receitaService;
            _comentarioService = comentarioService;
            _utilizadorService = utilizadorService;
            _validarSessao = validarSessao;
        }

        public Receita Receita { get; set; }
        public double AvaliacaoMedia { get; set; }

        [BindProperty]
        public ComentarioViewModel Comentario {  get; set; }

        [BindProperty]
        public string Codigo { get; set; }
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

            if (utilizador==null) 
            {
                return BadRequest();
            }

            Receita = _receitaService.GetById(id);

            if(Receita == null) 
            {
                return NotFound();
            }

            if (Receita.Estado !=TipoDeEstado.Valida) 
            {
                return BadRequest();
            }

            Comentario = new ComentarioViewModel()
            {
                ReceitaId = id,
                UtilizadorId = (int)utilizadorId
            };

            AvaliacaoMedia = _avaliacaoService.MediaDoValorByReceita(id);

            Codigo = codigo;

            return Page();
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid) 
            {
 
                Comentario comentario = new Comentario(Comentario.Mensagem, DateTime.Now,
                                        _receitaService.GetById(Comentario.ReceitaId),
                                        _utilizadorService.GetById(Comentario.UtilizadorId));

                _comentarioService.Add(comentario);
                
                return RedirectToPage("/Detalhes", new { id = Comentario.ReceitaId });
            }

            Receita = _receitaService.GetById(Comentario.ReceitaId);

            AvaliacaoMedia = _avaliacaoService.MediaDoValorByReceita(Comentario.ReceitaId);

            return Page();
        }
    }
}
