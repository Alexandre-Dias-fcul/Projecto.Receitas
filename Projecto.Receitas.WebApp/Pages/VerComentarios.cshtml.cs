using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Projecto.Receitas.Domain.Model;
using Projecto.Receitas.Services.Interfaces;
using Projecto.Receitas.Services.Services;

namespace Projecto.Receitas.WebApp.Pages
{
    public class VerComentariosModel : PageModel
    {
        private IComentarioService _comentarioService;

        private IReceitaService _receitaService;

        private IAvaliacaoService _avaliacaoService;

        private IUtilizadorService _utilizadorService;

        public ValidarSessao _validarSessao;
        public VerComentariosModel(IComentarioService comentarioService,IReceitaService receitaService,
            IAvaliacaoService avaliacaoService,IUtilizadorService utilizadorService,ValidarSessao validarSessao) 
        { 
            _comentarioService = comentarioService;
            _receitaService = receitaService;
            _avaliacaoService = avaliacaoService;
            _utilizadorService = utilizadorService;
            _validarSessao = validarSessao;
        }
        public List<Comentario> Comentarios { get; set; }
        public Receita Receita { get; set; }
        public double MediaDaAvaliacao { get; set; }
        public string Codigo {  get; set; } 
        public IActionResult OnGet(int id)
        {
            Receita = _receitaService.GetById(id);

            if (Receita == null) 
            {
                return NotFound();
            }

            Comentarios = _comentarioService.GetByReceitaId(id);

            MediaDaAvaliacao = _avaliacaoService.MediaDoValorByReceita(id);

            foreach (Comentario comentario in Comentarios) 
            {
                comentario.Utilizador = _utilizadorService.GetById(comentario.Utilizador.UtilizadorId);
            }

            if (HttpContext.Session.GetInt32("UserId") != null) 
            {
                Codigo = _validarSessao.Encriptar((int)HttpContext.Session.GetInt32("UserId"));
            }

            return Page();
        }
    }
}
