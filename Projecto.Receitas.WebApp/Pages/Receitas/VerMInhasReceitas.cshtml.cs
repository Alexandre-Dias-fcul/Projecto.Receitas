using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Projecto.Receitas.Domain.Model;
using Projecto.Receitas.Services.Interfaces;
using Projecto.Receitas.Services.Services;

namespace Projecto.Receitas.WebApp.Pages.Receitas
{
    public class VerMInhasReceitasModel : PageModel
    {
        private IReceitaService _receitaService;

        private ValidarSessao _validarSessao;
        public VerMInhasReceitasModel(IReceitaService receitaService,ValidarSessao validarSessao)
        {
            _receitaService=receitaService;
            _validarSessao=validarSessao;
        }

        public int MaximoId {  get; set; }

        public int Id { get; set; }

        public List<Receita> Receitas {  get; set; }

        public string Codigo { get; set; }

        public IActionResult OnGet(int id=1)
        {
            int? utilizadorId = HttpContext.Session.GetInt32("UserId");

            if (utilizadorId == null)
            {
                return BadRequest();
            }

            if(id<1) 
            {
                id=1;
            }

            int totalDeMinhasReceitas = _receitaService.TotalDeReceitasByUtilizadorId((int)utilizadorId);

            if (totalDeMinhasReceitas%8 !=0)
            {
                MaximoId = (int)Math.Floor((decimal)totalDeMinhasReceitas/8)+1;
            }
            else
            {
                MaximoId = totalDeMinhasReceitas/8;
            }

            Receitas =_receitaService.GetReceitasByUtilizadorId(id*8,(int)utilizadorId);

            id++;

            Id= id;

            Codigo = _validarSessao.Encriptar((int)utilizadorId);

            return Page();
        }
    }
}
