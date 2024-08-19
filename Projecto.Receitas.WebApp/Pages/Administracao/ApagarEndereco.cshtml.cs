using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Project.Receitas.Data.Interfaces;
using Project.Receitas.Data.Repositories;
using Projecto.Receitas.Domain.Model;
using Projecto.Receitas.Services.Interfaces;
using Projecto.Receitas.Services.Services;

namespace Projecto.Receitas.WebApp.Pages.Administracao
{
    public class ApagarEnderecoModel : PageModel
    {
        private IEnderecoService _enderecoService;
        private IUtilizadorEnderecoService _utilizadorEnderecoService;
        private IUtilizadorService _utilizadorService;

        public ApagarEnderecoModel(IEnderecoService enderecoService,IUtilizadorEnderecoService utilizadorEnderecoService
            ,IUtilizadorService utilizadorService) 
        {
            _enderecoService= enderecoService;
            _utilizadorEnderecoService= utilizadorEnderecoService;
            _utilizadorService= utilizadorService;
        }
        public IActionResult OnGet(int idEndereco, int idUser,int idPagina=0,string? texto = "")
        {
            Utilizador utilizador = _utilizadorService.GetById(idUser);

            Endereco endereco = _enderecoService.GetById(idEndereco);

            if (utilizador == null || endereco == null) 
            {  
                return NotFound();
            }

            UtilizadorEndereco utilizadorEndereco = new UtilizadorEndereco(utilizador,endereco);

            _utilizadorEnderecoService.Delete(utilizadorEndereco);
            _enderecoService.Delete(idEndereco);

            return RedirectToPage("/Administracao/VerEnderecos", new { idUser = idUser ,idPagina = idPagina, texto = texto});
        }
    }
}
