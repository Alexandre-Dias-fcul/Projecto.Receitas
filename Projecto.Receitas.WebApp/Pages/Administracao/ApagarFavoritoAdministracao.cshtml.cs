using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Projecto.Receitas.Domain.Model;
using Projecto.Receitas.Services.Interfaces;

namespace Projecto.Receitas.WebApp.Pages.Administracao
{
    public class ApagarFavoritoAdministracaoModel : PageModel
    {
        private IFavoritoService _favoritoService;

        public ApagarFavoritoAdministracaoModel(IFavoritoService favoritoService) 
        {
            _favoritoService = favoritoService;
        }
        public IActionResult OnGet(int idReceita, int idUser, int idPagina = 0, string? texto = "")
        {
            Favorito favorito = _favoritoService.GetByPrimaryKey(idReceita,idUser);

            if (favorito == null)
            {
                return NotFound();
            }

            _favoritoService.Delete(favorito);

            return RedirectToPage("/Administracao/VerFavoritosAdministracao", new { idUser = idUser, idPagina = idPagina, texto = texto });
        }
    }
}
