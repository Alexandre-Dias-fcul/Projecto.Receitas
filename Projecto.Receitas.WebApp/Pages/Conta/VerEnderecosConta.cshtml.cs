using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Projecto.Receitas.Domain.Model;
using Projecto.Receitas.Services.Interfaces;

namespace Projecto.Receitas.WebApp.Pages.Conta
{
    public class VerEnderecosContaModel : PageModel
    {
        private IUtilizadorService _utilizadorService;

        private IEnderecoService _enderecoService;

        private ValidarSessao _validarSessao;
        public Utilizador Utilizador { get; set; }
        public List<Endereco> Enderecos { get; set; }

        public string Codigo { get; set; }

        public VerEnderecosContaModel(IUtilizadorService utilizadorService, IEnderecoService enderecoService ,
               ValidarSessao validarSessao)
        {
            _utilizadorService= utilizadorService;
            _enderecoService= enderecoService;
            _validarSessao = validarSessao;
        }
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

            Enderecos =_enderecoService.GetAllByUtilizadorId((int)utilizadorId);

            Codigo = _validarSessao.Encriptar((int)utilizadorId);

            return Page();
        }
    }
}
