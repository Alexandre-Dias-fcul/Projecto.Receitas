using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Projecto.Receitas.Domain.Model;
using Projecto.Receitas.Services.Interfaces;
using Projecto.Receitas.Services.Services;
using Projecto.Receitas.WebApp.ViewModel;

namespace Projecto.Receitas.WebApp.Pages.Conta
{
    public class NovoEnderecoContaModel : PageModel
    {
        private IEnderecoService _enderecoService;

        private IUtilizadorService _utilizadorService;
        public NovoEnderecoContaModel(IEnderecoService enderecoService,IUtilizadorService userService) 
        {
            _enderecoService = enderecoService;
            _utilizadorService = userService;
        }

        [BindProperty]
        public EnderecoViewModel Endereco { get; set; }

        public Utilizador Utilizador { get; set; }
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

            Endereco = new EnderecoViewModel(){ UtilizadorId =  (int) utilizadorId };

            return Page();
        }

        public IActionResult OnPost() 
        {
            if (ModelState.IsValid) 
            {
                Endereco endereco = new Endereco(Endereco.Rua, Endereco.CodigoPostal, Endereco.Localidade, Endereco.Pais);

                _enderecoService.AddEndereco(endereco, Endereco.UtilizadorId);

                return RedirectToPage("/Conta/VerEnderecosConta");
            }

            Utilizador = _utilizadorService.GetById(Endereco.UtilizadorId);

            return Page();
        }
    }
}
