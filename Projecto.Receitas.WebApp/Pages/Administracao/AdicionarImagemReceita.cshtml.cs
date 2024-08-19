using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Projecto.Receitas.Domain.Model;
using Projecto.Receitas.Services.Interfaces;
using Projecto.Receitas.Services.Services;

namespace Projecto.Receitas.WebApp.Pages.Administracao
{
    public class AdicionarImagemReceitaModel : PageModel
    {
        private IReceitaService _receitaService;

        private ICategoriaService _categoriaService;

        private IUtilizadorService _utilizadorService;
        public AdicionarImagemReceitaModel(IReceitaService receitaService, ICategoriaService categoriaService,
            IUtilizadorService utilizadorService) 
        { 
            _receitaService = receitaService;
            _categoriaService = categoriaService;
            _utilizadorService = utilizadorService;
        }

        [BindProperty]
        public int IdReceita { get; set; }

        [BindProperty]
        public IFormFile? Imagem { get; set; }

        [BindProperty]
        public int IdPagina { get; set; }

        [BindProperty]
        public string? Texto { get; set; }

        public IActionResult OnGet(int idReceita, int idPagina = 0, string? texto = "")
        {
            Receita receita = _receitaService.GetById(idReceita);

            if (receita == null)
            {
                return NotFound();
            }

            IdReceita = idReceita;

            IdPagina = idPagina;

            Texto = texto;

            if (!string.IsNullOrEmpty(receita.ImagemUrl))
            {
                return BadRequest();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync() 
        {
            if (ModelState.IsValid)
            {
                if (Imagem != null && Imagem.Length > 0)
                {
                    string nomeDaImagem = "Receita_" + Guid.NewGuid();
                    if (Imagem.FileName.Contains(".jpg"))
                        nomeDaImagem += ".jpg";
                    else if (Imagem.FileName.Contains(".gif"))
                        nomeDaImagem += ".gif";
                    else if (Imagem.FileName.Contains(".png"))
                        nomeDaImagem += ".png";
                    else if (Imagem.FileName.Contains(".pdf"))
                        nomeDaImagem += ".pdf";
                    else
                        nomeDaImagem += ".tmp";

                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/uploads-receitas", nomeDaImagem);

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await Imagem.CopyToAsync(stream);
                    }

                    Receita receita = _receitaService.GetById(IdReceita);

                    if (receita == null)
                    {
                        return NotFound();
                    }

                    Receita receitaAtualizar =
                            new Receita(receita.ReceitaId, receita.Titulo, nomeDaImagem, receita.ModoDePreparacao, receita.TempoDePreparacao
                        , receita.Estado, receita.Dificuldade, _categoriaService.GetById(receita.Categoria.CategoriaId),
                        _utilizadorService.GetById(receita.Utilizador.UtilizadorId));

                    _receitaService.Update(receitaAtualizar);

                    return RedirectToPage("/Administracao/VerReceita", new { idReceita = IdReceita,  idPagina = IdPagina, texto = Texto });
                }
                else 
                {
                    Receita receita = _receitaService.GetById(IdReceita);

                    if (receita == null)
                    {
                        return NotFound();
                    }

                    Receita receitaAtualizar = new Receita(receita.ReceitaId, receita.Titulo, null, receita.ModoDePreparacao,
                     receita.TempoDePreparacao, receita.Estado, receita.Dificuldade, _categoriaService.GetById(receita.Categoria.CategoriaId),
                   _utilizadorService.GetById(receita.Utilizador.UtilizadorId));

                    _receitaService.Update(receitaAtualizar);

                    return RedirectToPage("/Administracao/VerReceita", new { idReceita = IdReceita, idPagina = IdPagina, texto = Texto });
                }
            }

            return Page();
        }
    }
}
