using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using Projecto.Receitas.Domain.Model;
using Projecto.Receitas.Services.Interfaces;

namespace Projecto.Receitas.WebApp.Pages.Administracao
{
    public class ApagarComentarioModel : PageModel
    {
        private IComentarioService _comentarioService;

        public ApagarComentarioModel(IComentarioService comentarioService) 
        {
            _comentarioService = comentarioService;
        }

        public IActionResult OnGet(int idComentario, int idUser, int idPagina = 0, string? texto = "")
        {
            Comentario comentario = _comentarioService.GetById(idComentario);

            if(comentario == null) 
            {
                return NotFound();
            }

            _comentarioService.Delete(idComentario);

            return RedirectToPage("/Administracao/VerComentariosAdministracao", 
                             new { idUser = idUser, idPagina = idPagina, texto = texto });
        }
    }
}
