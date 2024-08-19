using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Projecto.Receitas.Domain.Model;
using Projecto.Receitas.Services.Interfaces;
using Projecto.Receitas.Services.Services;

namespace Projecto.Receitas.WebApp.Pages.Receitas
{
    public class FinalizarAdicionarImagemModel : PageModel
    {
        private IReceitaService _receitaService;

        private ICategoriaService _categoriaService;

        private IUtilizadorService _utilizadorService;
        private ValidarSessao _validarSessao {  get; set; }

        public FinalizarAdicionarImagemModel(IReceitaService receitaService,ICategoriaService categoriaService, 
            IUtilizadorService utilizadorService, ValidarSessao validarSessao) 
        {
            _receitaService = receitaService;
            _categoriaService = categoriaService;
            _utilizadorService = utilizadorService;
            _validarSessao = validarSessao;
        }

        [BindProperty]
        public int Id { get; set; }

        [BindProperty]
        public IFormFile? Imagem { get; set; }
        
        [BindProperty]
        public string Codigo { get; set; }

        public IActionResult OnGet(int id, string codigo)
        {
            if (!_validarSessao.Validar(codigo)) 
            { 
                  return BadRequest();
            }

            Receita receita = _receitaService.GetById(id);

            if(receita == null) 
            {
                return NotFound();
            }

            Id = id;

            int? utilizadorId = HttpContext.Session.GetInt32("UserId");

            if(utilizadorId == null) 
            {
                return BadRequest();
            }

            Codigo = codigo;

            if(!string.IsNullOrEmpty(receita.ImagemUrl)) 
            {
                return RedirectToPage("/Receitas/FinalizarAdicionarReceita", new { id = Id , codigo = Codigo});
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                    if(Imagem != null && Imagem.Length > 0) 
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

                        Receita receita = _receitaService.GetById(Id);

                        if(receita == null) 
                        {
                            return NotFound();
                        }

                        Receita receitaAtualizar = 
                            new Receita(receita.ReceitaId,receita.Titulo, nomeDaImagem,receita.ModoDePreparacao,receita.TempoDePreparacao
                        ,receita.Estado,receita.Dificuldade, _categoriaService.GetById(receita.Categoria.CategoriaId), 
                        _utilizadorService.GetById(receita.Utilizador.UtilizadorId));

                        _receitaService.Update(receitaAtualizar);

                        return RedirectToPage("/Receitas/FinalizarAdicionarReceita", new { id = Id , codigo = Codigo });
                    }
                    else 
                    {
                        Receita receita = _receitaService.GetById(Id);

                        if (receita == null)
                        {
                            return NotFound();
                        }

                        Receita receitaAtualizar = new Receita(receita.ReceitaId, receita.Titulo, null, receita.ModoDePreparacao,
                         receita.TempoDePreparacao, receita.Estado, receita.Dificuldade, _categoriaService.GetById(receita.Categoria.CategoriaId),
                       _utilizadorService.GetById(receita.Utilizador.UtilizadorId));

                        _receitaService.Update(receitaAtualizar);

                         return RedirectToPage("/Receitas/FinalizarAdicionarReceita", new { id = Id, codigo = Codigo });
                    }
            }

            return Page();
        }
    }
}
