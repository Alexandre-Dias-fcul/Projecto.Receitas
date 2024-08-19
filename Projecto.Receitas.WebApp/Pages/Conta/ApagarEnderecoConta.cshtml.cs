using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Projecto.Receitas.Domain.Model;
using Projecto.Receitas.Services.Interfaces;

namespace Projecto.Receitas.WebApp.Pages.Conta
{
    public class ApagarEnderecoContaModel : PageModel
    {
        private IEnderecoService _enderecoService;
        private IUtilizadorEnderecoService _utilizadorEnderecoService;
        private IUtilizadorService _utilizadorService;

        private ValidarSessao _validarSessao;

        public ApagarEnderecoContaModel(IEnderecoService enderecoService, IUtilizadorEnderecoService utilizadorEnderecoService
            , IUtilizadorService utilizadorService , ValidarSessao validarSessao)
        {
            _enderecoService= enderecoService;
            _utilizadorEnderecoService= utilizadorEnderecoService;
            _utilizadorService= utilizadorService;
            _validarSessao=validarSessao;
        }

        public IActionResult OnGet(int idEndereco,string codigo)
        {
            if(!_validarSessao.Validar(codigo)) 
            {
                return BadRequest();
            }

            int? utilizadorId = HttpContext.Session.GetInt32("UserId");

            if (utilizadorId == null)
            {
                return BadRequest();
            }

            Utilizador utilizador = _utilizadorService.GetById((int)utilizadorId);
            Endereco endereco = _enderecoService.GetById(idEndereco);

            if (utilizador == null || endereco ==null) 
            {
                return NotFound();
            }

            UtilizadorEndereco utilizadorEndereco = new UtilizadorEndereco(utilizador,endereco);

            _utilizadorEnderecoService.Delete(utilizadorEndereco);
            _enderecoService.Delete(idEndereco);

            return RedirectToPage("/Conta/VerEnderecosConta");
        }
    }
}
