using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Projecto.Receitas.Domain.Model;
using Projecto.Receitas.Services.Interfaces;
using Projecto.Receitas.Services.Services;

namespace Projecto.Receitas.WebApp.Pages.Administracao
{
    public class ApagarContactoModel : PageModel
    {
        private IContactoService _contactoService;

        public ApagarContactoModel(IContactoService contactoService) 
        { 
            _contactoService = contactoService; 
        }

        public IActionResult OnGet(int idContacto,int idPagina = 0, string? texto = "")
        {      
            Contacto contacto = _contactoService.GetById(idContacto);

            if(contacto == null) 
            {
                return NotFound();
            }

            _contactoService.Delete(idContacto);
            
            return RedirectToPage("/Administracao/VerContactos", 
                       new {  idUser = contacto.Utilizador.UtilizadorId, idPagina=idPagina, texto=texto});
        }
    }
}
