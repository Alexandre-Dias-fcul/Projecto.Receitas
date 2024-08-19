using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Projecto.Receitas.Domain.Model;
using Projecto.Receitas.Services.Interfaces;
using Projecto.Receitas.Services.Services;
using Projecto.Receitas.WebApp.ViewModel;

namespace Projecto.Receitas.WebApp.Pages.Administracao
{
    public class NovoEnderecoModel : PageModel
    {
        private IUtilizadorService _utilizadorService;

        private IEnderecoService _enderecoService;

        public NovoEnderecoModel(IUtilizadorService utilizadorService,IEnderecoService enderecoService) 
        { 
            _utilizadorService = utilizadorService;
            _enderecoService = enderecoService;
        }
        public Utilizador Utilizador { get; set; }

        [BindProperty]
        public EnderecoViewModel Endereco { get; set; }

        [BindProperty]
        public int IdPagina {  get; set; }

        [BindProperty]
        public string? Texto { get; set; }
        public IActionResult OnGet(int idUser,int idPagina = 0, string? texto = "")
        {
            Utilizador = _utilizadorService.GetById(idUser);

            if (Utilizador == null) 
            { 
                return NotFound();
            }

            Endereco = new EnderecoViewModel()
            {
                UtilizadorId = idUser
            };

            IdPagina = idPagina;

            Texto = texto;

            return Page();
        }

        public IActionResult OnPost() 
        {
            if(ModelState.IsValid)
            { 
                Endereco endereco = new Endereco(Endereco.Rua, Endereco.CodigoPostal, Endereco.Localidade, Endereco.Pais);

                _enderecoService.AddEndereco(endereco,Endereco.UtilizadorId); 

                return RedirectToPage("/Administracao/VerEnderecos",
                          new  { idUser = Endereco.UtilizadorId,idPagina = IdPagina, texto = Texto });

            }

            Utilizador = _utilizadorService.GetById(Endereco.UtilizadorId);

            return Page();
        }
    }
}
