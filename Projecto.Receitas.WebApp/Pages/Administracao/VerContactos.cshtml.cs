using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Projecto.Receitas.Domain.Model;
using Projecto.Receitas.Services.Interfaces;
using Projecto.Receitas.Services.Services;

namespace Projecto.Receitas.WebApp.Pages.Administracao
{
    public class VerContactosModel : PageModel
    {
        private IContactoService _contactoService;

        private IUtilizadorService _utilizadorService;

        public VerContactosModel(IContactoService contactoService,IUtilizadorService utilizadorService)  
        {
            _contactoService = contactoService;
            _utilizadorService = utilizadorService;
        }
        public Utilizador Utilizador { get; set; }
        public List<Contacto> Contactos { get; set; }
        public int IdPagina {  get; set; }
        public string? Texto { get; set; }
        public IActionResult OnGet(int idUser,int idPagina=0, string? texto = "")
        {
            Utilizador = _utilizadorService.GetById(idUser);

            if (Utilizador == null) 
            {
                return NotFound();
            }

            Contactos = _contactoService.GetByUtilizadorId(idUser);

            IdPagina = idPagina;

            Texto = texto;

            return Page();
        }
    }
}
