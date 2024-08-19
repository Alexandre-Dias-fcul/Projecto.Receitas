using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Projecto.Receitas.Domain.Model;
using Projecto.Receitas.Services.Interfaces;
using Projecto.Receitas.Services.Services;
using Projecto.Receitas.WebApp.ViewModel;

namespace Projecto.Receitas.WebApp.Pages.Conta
{
    public class AlterarEnderecoContaModel : PageModel
    {
        private IEnderecoService _enderecoService;

        private IUtilizadorService _utilizadorService;

        private ValidarSessao _validarSessao;

        public AlterarEnderecoContaModel(IEnderecoService enderecoService, IUtilizadorService utilizadorService,
               ValidarSessao validarSessao) 
        {
            _enderecoService = enderecoService;
            _utilizadorService = utilizadorService;
            _validarSessao = validarSessao;
        }

        [BindProperty]
        public EnderecoViewModelId Endereco { get; set; }

        public Utilizador Utilizador { get; set; }
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

            Endereco endereco = _enderecoService.GetById(idEndereco);

            if (endereco == null) 
            {
                return NotFound();
            }

            Utilizador = _utilizadorService.GetById((int)utilizadorId);

            if (Utilizador == null) 
            { 
                return NotFound();
            }

            Endereco = new EnderecoViewModelId
            {
                EnderecoId = endereco.EnderecoId,
                Rua = endereco.Rua,
                CodigoPostal = endereco.CodigoPostal,
                Localidade = endereco.Localidade,
                Pais = endereco.Pais,
                UtilizadorId = (int)utilizadorId
            };

            return Page();
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                Endereco endereco = new Endereco(Endereco.EnderecoId, Endereco.Rua, Endereco.CodigoPostal, Endereco.Localidade,
                                    Endereco.Pais);

                _enderecoService.Update(endereco);

                return RedirectToPage("/Conta/VerEnderecosConta");
            }

            Utilizador = _utilizadorService.GetById(Endereco.UtilizadorId);

            return Page();
        }
    }
}
