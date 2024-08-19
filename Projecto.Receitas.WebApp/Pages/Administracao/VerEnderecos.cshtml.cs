using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Projecto.Receitas.Domain.Model;
using Projecto.Receitas.Services.Interfaces;
using Projecto.Receitas.Services.Services;

namespace Projecto.Receitas.WebApp.Pages.Administracao
{
    public class VerEnderecosModel : PageModel
    {
        private IUtilizadorService _utilizadorService;

        private IEnderecoService _enderecoService;

        public VerEnderecosModel(IUtilizadorService utilizadorService,IEnderecoService enderecoService) 
        { 
            _utilizadorService = utilizadorService;
            _enderecoService = enderecoService;
        }
        public Utilizador Utilizador { get; set; }
        public List<Endereco> Enderecos { get; set; }
        public int IdPagina { get; set; }
        public string? Texto {  get; set; }
        public IActionResult OnGet(int idUser,int idPagina=0,string? texto="")
        {

            Utilizador = _utilizadorService.GetById(idUser);

            if(Utilizador == null) 
            {
                return NotFound();
            }

            Enderecos =_enderecoService.GetAllByUtilizadorId(idUser);

            IdPagina = idPagina;

            Texto=texto;

            return Page();
        }
    }
}
