using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Projecto.Receitas.Domain.Model;
using Projecto.Receitas.Services.Interfaces;
using Projecto.Receitas.Services.Services;
using Projecto.Receitas.WebApp.ViewModel;

namespace Projecto.Receitas.WebApp.Pages.Administracao
{
    public class NovoUtilizadorModel : PageModel
    {
        private IUtilizadorService _utilizadorService;
        public NovoUtilizadorModel(IUtilizadorService utilizadorService) 
        {
            _utilizadorService = utilizadorService;
        }
        public List<SelectListItem> Generos { get; set; }
        public List<SelectListItem> Permissoes { get; set; }

        [BindProperty]
        public UtilizadorViewModel Utilizador { get; set; }

        [BindProperty]
        public int IdPagina { get; set; }
        [BindProperty]
        public string? Texto { get; set; }
        public void OnGet(int idPagina=0,string? texto= "")
        {   
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
        }

        public async Task<IActionResult> OnPostAsync() 
        {
            if (ModelState.IsValid) 
            {
                if (Utilizador.Imagem!=null && Utilizador.Imagem.Length>0)
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

                    Utilizador utilizador = new Utilizador(Utilizador.PrimeiroNome, Utilizador.NomesDoMeio, Utilizador.UltimoNome,
                                         Utilizador.Genero, nomeDaImagem, Utilizador.DataDeNascimento, Utilizador.Nacionalidade,
                                         Utilizador.Permissoes);

                    _utilizadorService.Add(utilizador);

                    return RedirectToPage("/Administracao/VerUtilizadores",new { id=IdPagina,  texto = Texto });
                }
                else 
                {
                    Utilizador utilizador = new Utilizador(Utilizador.PrimeiroNome, Utilizador.NomesDoMeio, Utilizador.UltimoNome,
                                         Utilizador.Genero, null, Utilizador.DataDeNascimento, Utilizador.Nacionalidade,
                                         Utilizador.Permissoes);

                    _utilizadorService.Add(utilizador);

                    return RedirectToPage("/Administracao/VerUtilizadores", new { id = IdPagina, texto = Texto });
                }
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
