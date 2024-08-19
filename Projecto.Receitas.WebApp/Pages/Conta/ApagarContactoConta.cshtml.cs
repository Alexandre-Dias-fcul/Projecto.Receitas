using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Projecto.Receitas.Domain.Model;
using Projecto.Receitas.Services.Interfaces;
using Projecto.Receitas.Services.Services;

namespace Projecto.Receitas.WebApp.Pages.Conta
{
    public class ApagarContactoContaModel : PageModel
    {
        private IContactoService _contactoService;

        private ValidarSessao _validarSessao;

        public ApagarContactoContaModel(IContactoService contactoService,ValidarSessao validarSessao) 
        {
            _contactoService = contactoService;
            _validarSessao = validarSessao;
        }

        public IActionResult OnGet(int idContacto, string codigo)
        {
            if (!_validarSessao.Validar(codigo)) 
            {
                return BadRequest();
            }

            Contacto contacto = _contactoService.GetById(idContacto);

            if (contacto == null) 
            { 
                return NotFound();
            }

            _contactoService.Delete(idContacto);

            return RedirectToPage("/Conta/VerContactosConta");
        }
    }
}
