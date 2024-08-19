using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Projecto.Receitas.Domain.Model;
using Projecto.Receitas.Services.Interfaces;
using Projecto.Receitas.Services.Services;

namespace Projecto.Receitas.WebApp.Pages.Conta
{
    public class VerContactosContaModel : PageModel
    {
        private IUtilizadorService _utilizadorService;

        private IContactoService _contactoService;

        private ValidarSessao _validarSessao;
        public VerContactosContaModel(IUtilizadorService utilizadorService,IContactoService contactoService,
               ValidarSessao validarSessao) 
        { 
            _utilizadorService = utilizadorService;
            _contactoService = contactoService;
            _validarSessao = validarSessao;
        }
        public Utilizador Utilizador { get; set; }
        public List<Contacto> Contactos { get; set; }
        public string Codigo { get; set; }
        public IActionResult OnGet()
        {
            int? utilizadorId = HttpContext.Session.GetInt32("UserId");

            if (utilizadorId == null)
            {
                return BadRequest();
            }

            Utilizador = _utilizadorService.GetById((int)utilizadorId);

            if (Utilizador == null) 
            {
                return BadRequest();
            }

            Contactos = _contactoService.GetByUtilizadorId((int)utilizadorId);

            Codigo = _validarSessao.Encriptar((int)utilizadorId);

            return Page();
        }
    }
}
