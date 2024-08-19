using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Projecto.Receitas.Domain.Model;
using Projecto.Receitas.Services.Interfaces;

namespace Projecto.Receitas.WebApp.Pages.Administracao
{
    public class AdicionarFotoUtilizadorModel : PageModel
    {
        private IUtilizadorService _utilizadorService;

        public AdicionarFotoUtilizadorModel(IUtilizadorService utilizadorService) 
        { 
            _utilizadorService = utilizadorService;
        }

        [BindProperty]
        public int IdUser { get; set; }

        [BindProperty]
        public IFormFile? Foto { get; set; }

        [BindProperty]
        public int IdPagina {  get; set; }

        [BindProperty]
        public string? Texto {  get; set; }

        public IActionResult OnGet(int idUser, int idPagina = 0, string? texto = "")
        {
            Utilizador utilizador = _utilizadorService.GetById(idUser);

            if (utilizador == null)
            {
                return NotFound();
            }

            IdPagina = idPagina;

            Texto = texto;  

            if (!string.IsNullOrEmpty(utilizador.FotoUrl))
            {
                return BadRequest();
            }

            IdUser = idUser;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid) 
            {
                if (Foto!=null && Foto.Length>0) 
                {
                    string nomeDaImagem = "Utilizador_" + Guid.NewGuid();

                    if (Foto.FileName.Contains(".jpg"))
                        nomeDaImagem += ".jpg";
                    else if (Foto.FileName.Contains(".gif"))
                        nomeDaImagem += ".gif";
                    else if (Foto.FileName.Contains(".png"))
                        nomeDaImagem += ".png";
                    else if (Foto.FileName.Contains(".pdf"))
                        nomeDaImagem += ".pdf";
                    else
                        nomeDaImagem += ".tmp";

                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/uploads-users", nomeDaImagem);

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await Foto.CopyToAsync(stream);
                    }

                    Utilizador utilizador = _utilizadorService.GetById(IdUser);

                    if (utilizador == null)
                    {
                        return NotFound();
                    }

                    Utilizador utilizadorAtualizar = new Utilizador(utilizador.UtilizadorId, utilizador.PrimeiroNome, utilizador.NomesDoMeio,
                                        utilizador.UltimoNome, utilizador.Genero, nomeDaImagem, utilizador.DataDeNascimento,
                                        utilizador.Nacionalidade, utilizador.Permissoes);

                    _utilizadorService.Update(utilizadorAtualizar);

                    return RedirectToPage("/Administracao/VerUtilizadores", new { id = IdPagina, texto = Texto });
                }
                else 
                {
                    Utilizador utilizador = _utilizadorService.GetById(IdUser);

                    if (utilizador == null)
                    {
                        return NotFound();
                    }

                    Utilizador utilizadorAtualizar = new Utilizador(utilizador.UtilizadorId, utilizador.PrimeiroNome, utilizador.NomesDoMeio,
                                        utilizador.UltimoNome, utilizador.Genero,null, utilizador.DataDeNascimento,
                                        utilizador.Nacionalidade, utilizador.Permissoes);

                    _utilizadorService.Update(utilizadorAtualizar);

                    return RedirectToPage("/Administracao/VerUtilizadores", new { id = IdPagina, texto = Texto });
                }
            }

            return Page();
        }
    }
}
