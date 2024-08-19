using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Projecto.Receitas.Domain.Model;
using Projecto.Receitas.Services.Interfaces;
using Projecto.Receitas.Services.Services;

namespace Projecto.Receitas.WebApp.Pages.Administracao
{
    public class VerComentariosAdministracaoModel : PageModel
    {
        private IComentarioService _comentarioService;

        private IUtilizadorService _utilizadorService;

        private IReceitaService _receitaService;
        public  VerComentariosAdministracaoModel(IComentarioService comentarioService,IUtilizadorService utilizadorService,
                IReceitaService receitaService)
        {
            _comentarioService = comentarioService;
            _utilizadorService = utilizadorService;
            _receitaService = receitaService;
        }

        public Utilizador Utilizador { get; set; }
        public List<Comentario> Comentarios { get; set; }
        public int IdPagina { get; set; }
        public string? Texto { get; set; }

        public IActionResult OnGet(int idUser, int idPagina = 0, string? texto = "")
        {
            Utilizador = _utilizadorService.GetById(idUser);

            if (Utilizador == null)
            {
                return NotFound();
            }

            Comentarios = _comentarioService.GetByUtilizadorId(idUser);

            foreach(Comentario comentario in Comentarios) 
            { 
                comentario.Receita = _receitaService.GetById(comentario.Receita.ReceitaId);
            }

            IdPagina = idPagina;

            Texto = texto;

            return Page();
        }
    }
}
