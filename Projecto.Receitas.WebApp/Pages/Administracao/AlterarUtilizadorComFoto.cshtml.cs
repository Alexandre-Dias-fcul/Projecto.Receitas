using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Projecto.Receitas.Domain.Model;
using Projecto.Receitas.Services.Interfaces;
using Projecto.Receitas.Services.Services;
using Projecto.Receitas.WebApp.ViewModel;

namespace Projecto.Receitas.WebApp.Pages.Administracao
{
    public class AlterarUtilizadorComFotoModel : PageModel
    {
        private IUtilizadorService _utilizadorService;
        public AlterarUtilizadorComFotoModel(IUtilizadorService utilizadorService) 
        { 
            _utilizadorService = utilizadorService;
        }
        public List<SelectListItem> Generos { get; set; }
        public List<SelectListItem> Permissoes { get; set; }

        [BindProperty]
        public UtilizadorViewModelId Utilizador { get; set; }


        [BindProperty]
        public int IdPagina {  get; set; }

        [BindProperty]
        public string? Texto { get; set; }
        public IActionResult OnGet(int idUser,int idPagina = 0, string? texto = "")
        {
            Utilizador utilizador = _utilizadorService.GetById(idUser);

            if (utilizador == null) 
            { 
                return NotFound();
            }

            Utilizador = new UtilizadorViewModelId()
            {
                UtilizadorId = utilizador.UtilizadorId,
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

            Permissoes = new List<SelectListItem>()
            {
                new SelectListItem{ Text="Selecione uma opção",Value=""},
                new SelectListItem{ Text=Permissao.Administrador.ToString(),Value=((int)Permissao.Administrador).ToString() },
                new SelectListItem{ Text=Permissao.Utilizador.ToString(),Value=((int)Permissao.Utilizador).ToString() }
            };

            IdPagina = idPagina;

            Texto = texto;

            return Page();
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            { 
                Utilizador utilizador = new Utilizador(Utilizador.UtilizadorId, Utilizador.PrimeiroNome, Utilizador.NomesDoMeio,
                                         Utilizador.UltimoNome, Utilizador.Genero, Utilizador.FotoUrl,Utilizador.DataDeNascimento,
                                         Utilizador.Nacionalidade, Utilizador.Permissoes);

                _utilizadorService.Update(utilizador);

                return RedirectToPage("/Administracao/VerUtilizadores", new { id = IdPagina , texto = Texto});
            }

            Generos = new List<SelectListItem>()
            {
                new SelectListItem{  Text="Selecione uma opção",Value=((int)TipoDeGenero.SemSeleccao).ToString()},
                new SelectListItem{  Text=TipoDeGenero.Feminino.ToString(), Value=((int)TipoDeGenero.Feminino).ToString()},
                new SelectListItem{  Text=TipoDeGenero.Masculino.ToString(), Value=((int)TipoDeGenero.Masculino).ToString()}
            };

            Permissoes = new List<SelectListItem>()
            {
                new SelectListItem{ Text="Selecione uma opção",Value=""},
                new SelectListItem{ Text=Permissao.Administrador.ToString(),Value=((int)Permissao.Administrador).ToString() },
                new SelectListItem{ Text=Permissao.Utilizador.ToString(),Value=((int)Permissao.Utilizador).ToString() }
            };

            return Page();
        }
    }
}
