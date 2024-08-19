using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Projecto.Receitas.Domain.Model;
using Projecto.Receitas.Services.Interfaces;
using Projecto.Receitas.Services.Services;
using Projecto.Receitas.WebApp.ViewModel;

namespace Projecto.Receitas.WebApp.Pages.Conta
{
    public class AlterarDadosPessoaisComFotoModel : PageModel
    {
        private IUtilizadorService _utilizadorService;
        public AlterarDadosPessoaisComFotoModel(IUtilizadorService utilizadorService) 
        { 
            _utilizadorService = utilizadorService;
        }

        [BindProperty]
        public UtilizadorViewModelId Utilizador { get; set; }
        public List<SelectListItem> Generos { get; set; }
        public List<SelectListItem> Permissoes { get; set; }
        public IActionResult OnGet()
        {
            int? utilizadorId = HttpContext.Session.GetInt32("UserId");

            if (utilizadorId == null)
            {
                return BadRequest();
            }

            Utilizador utilizador = _utilizadorService.GetById((int)utilizadorId);

            if(utilizador == null) 
            { 
                return NotFound();
            }

            Utilizador = new UtilizadorViewModelId
            {
                UtilizadorId = (int)utilizadorId,
                PrimeiroNome = utilizador.PrimeiroNome,
                NomesDoMeio = utilizador.NomesDoMeio,
                UltimoNome = utilizador.UltimoNome,
                Genero = utilizador.Genero,
                FotoUrl = utilizador.FotoUrl,
                DataDeNascimento = utilizador.DataDeNascimento,
                Nacionalidade = utilizador.Nacionalidade,
                Permissoes = utilizador.Permissoes
            };

            Generos = new List<SelectListItem>()
            {
                new SelectListItem{  Text="Selecione uma opção",Value=((int)TipoDeGenero.SemSeleccao).ToString()},
                new SelectListItem{  Text=TipoDeGenero.Feminino.ToString(), Value=((int)TipoDeGenero.Feminino).ToString()},
                new SelectListItem{  Text=TipoDeGenero.Masculino.ToString(), Value=((int)TipoDeGenero.Masculino).ToString()}
            };

            return Page();
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                Utilizador utilizador = new Utilizador(Utilizador.UtilizadorId, Utilizador.PrimeiroNome, Utilizador.NomesDoMeio,
                                         Utilizador.UltimoNome, Utilizador.Genero, Utilizador.FotoUrl, Utilizador.DataDeNascimento,
                                         Utilizador.Nacionalidade, Utilizador.Permissoes);

                _utilizadorService.Update(utilizador);

                return RedirectToPage("/Conta/VerDadosPessoais");
            }

            Generos = new List<SelectListItem>()
            {
                new SelectListItem{  Text="Selecione uma opção",Value=((int)TipoDeGenero.SemSeleccao).ToString()},
                new SelectListItem{  Text=TipoDeGenero.Feminino.ToString(), Value=((int)TipoDeGenero.Feminino).ToString()},
                new SelectListItem{  Text=TipoDeGenero.Masculino.ToString(), Value=((int)TipoDeGenero.Masculino).ToString()}
            };


            return Page();
        }
    }
}
