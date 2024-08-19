using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Projecto.Receitas.Domain.Model;
using Projecto.Receitas.Services.Interfaces;
using Projecto.Receitas.Services.Services;
using Projecto.Receitas.WebApp.ViewModel;

namespace Projecto.Receitas.WebApp.Pages.Conta
{
    public class AlterarDadosPessoaisSemFotoModel : PageModel
    {
        private IUtilizadorService _utilizadorService;
        public AlterarDadosPessoaisSemFotoModel(IUtilizadorService utilizadorService)
        { 
            _utilizadorService = utilizadorService;
        }

        [BindProperty]
        public UtilizadorViewModelFoto Utilizador { get; set; }
        public List<SelectListItem> Generos { get; set; }
        public List<SelectListItem> Permissoes { get; set; }

        public string FotoUrl {  get; set; }

        public IActionResult OnGet()
        {
            int? utilizadorId = HttpContext.Session.GetInt32("UserId");

            if (utilizadorId == null)
            {
                return BadRequest();
            }

            Utilizador utilizador = _utilizadorService.GetById((int)utilizadorId);

            if (utilizador == null) 
            {
                return NotFound();
            }

            FotoUrl = utilizador.FotoUrl;

            Utilizador = new UtilizadorViewModelFoto
            {
                UtilizadorId = (int)utilizadorId,
                PrimeiroNome = utilizador.PrimeiroNome,
                NomesDoMeio = utilizador.NomesDoMeio,
                UltimoNome = utilizador.UltimoNome,
                Genero = utilizador.Genero,
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                if(Utilizador.Imagem!=null && Utilizador.Imagem.Length>0) 
                {
                    string nomeDaImagem = "Utilizador_" + Guid.NewGuid();

                    if (Utilizador.Imagem.FileName.Contains(".jpg"))
                        nomeDaImagem += ".jpg";
                    else if (Utilizador.Imagem.FileName.Contains(".gif"))
                        nomeDaImagem += ".gif";
                    else if (Utilizador.Imagem.FileName.Contains(".png"))
                        nomeDaImagem += ".png";
                    else if (Utilizador.Imagem.FileName.Contains(".pdf"))
                        nomeDaImagem += ".pdf";
                    else
                        nomeDaImagem += ".tmp";

                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/uploads-users", nomeDaImagem);

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await Utilizador.Imagem.CopyToAsync(stream);
                    }

                    Utilizador utilizador = new Utilizador(Utilizador.UtilizadorId, Utilizador.PrimeiroNome, Utilizador.NomesDoMeio,
                                         Utilizador.UltimoNome, Utilizador.Genero, nomeDaImagem, Utilizador.DataDeNascimento,
                                         Utilizador.Nacionalidade, Utilizador.Permissoes);

                    _utilizadorService.Update(utilizador);

                    return RedirectToPage("/Conta/VerDadosPessoais");
                }
                else 
                {
                    Utilizador utilizador = new Utilizador(Utilizador.UtilizadorId, Utilizador.PrimeiroNome, Utilizador.NomesDoMeio,
                                         Utilizador.UltimoNome, Utilizador.Genero, null, Utilizador.DataDeNascimento,
                                         Utilizador.Nacionalidade, Utilizador.Permissoes);

                    _utilizadorService.Update(utilizador);

                    return RedirectToPage("/Conta/VerDadosPessoais");
                }                
            }

            Generos = new List<SelectListItem>()
            {
                new SelectListItem{  Text="Selecione uma opção",Value=((int)TipoDeGenero.SemSeleccao).ToString()},
                new SelectListItem{  Text=TipoDeGenero.Feminino.ToString(), Value=((int)TipoDeGenero.Feminino).ToString()},
                new SelectListItem{  Text=TipoDeGenero.Masculino.ToString(), Value=((int)TipoDeGenero.Masculino).ToString()}
            };

            Utilizador utilizadorFoto = _utilizadorService.GetById(Utilizador.UtilizadorId);

            FotoUrl = utilizadorFoto.FotoUrl;

            return Page();
        }
    }
}
