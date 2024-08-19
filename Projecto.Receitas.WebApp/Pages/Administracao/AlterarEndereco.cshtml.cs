using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Project.Receitas.Data.Interfaces;
using Project.Receitas.Data.Repositories;
using Projecto.Receitas.Domain.Model;
using Projecto.Receitas.Services.Interfaces;
using Projecto.Receitas.Services.Services;
using Projecto.Receitas.WebApp.ViewModel;

namespace Projecto.Receitas.WebApp.Pages.Administracao
{
    public class AlterarEnderecoModel : PageModel
    {
        private IUtilizadorService _utilizadorService;

        private IEnderecoService _enderecoService;

        public AlterarEnderecoModel(IUtilizadorService utilizadorService, IEnderecoService enderecoService)
        {
            _utilizadorService = utilizadorService;
            _enderecoService = enderecoService;
        }
        public Utilizador Utilizador { get; set; }

        [BindProperty]
        public int IdPagina {  get; set; }

        [BindProperty]
        public EnderecoViewModelId Endereco {  get; set; }

        [BindProperty]
        public string? Texto { get; set; }
        public IActionResult OnGet(int idEndereco,int idUser,int idPagina=0, string? texto="")
        {
            Utilizador = _utilizadorService.GetById(idUser);

            if(Utilizador == null) 
            { 
                return NotFound();
            }

            Endereco endereco = _enderecoService.GetById(idEndereco);

            if (endereco == null) 
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
                Endereco endereco = new Endereco(Endereco.EnderecoId, Endereco.Rua,Endereco.CodigoPostal,Endereco.Localidade,
                                    Endereco.Pais);

                _enderecoService.Update(endereco);

                return RedirectToPage("/Administracao/VerEnderecos", 
                             new { idUser = Endereco.UtilizadorId, idPagina = IdPagina, texto = Texto});
            }

            Utilizador = _utilizadorService.GetById(Endereco.UtilizadorId);

            return Page();
        }
    }
}
