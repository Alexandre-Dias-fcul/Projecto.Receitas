using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Projecto.Receitas.Domain.Model;
using Projecto.Receitas.Services.Interfaces;
using Projecto.Receitas.WebApp.ViewModel;

namespace Projecto.Receitas.WebApp.Pages.Administracao
{
    public class AlterarReceitaSemFotoModel : PageModel
    {
        private IReceitaService _receitaService;

        private ICategoriaService _categoriaService;

        private IUtilizadorService _utilizadorService;

        public AlterarReceitaSemFotoModel(IReceitaService receitaService, ICategoriaService categoriaService, IUtilizadorService utilizadorService)
        {
            _receitaService = receitaService;
            _categoriaService = categoriaService;
            _utilizadorService = utilizadorService;
        }

        public List<SelectListItem> Tempos { get; set; }

        public List<SelectListItem> Graus { get; set; }

        public List<SelectListItem> Estados { get; set; }

        public List<SelectListItem> Categorias { get; set; }

        [BindProperty]
        public ReceitaViewModelSemFotoId Receita { get; set; }

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

            Receita = new ReceitaViewModelSemFotoId()
            {
                ReceitaId = receita.ReceitaId,
                Titulo = receita.Titulo,
                ModoDePreparacao = receita.ModoDePreparacao,
                TempoDePreparacao = receita.TempoDePreparacao,
                Estado = receita.Estado,
                Dificuldade = receita.Dificuldade,
                CategoriaId = receita.Categoria.CategoriaId,
                UtilizadorId = receita.Utilizador.UtilizadorId
            };

            Tempos = new List<SelectListItem>()
            {
                new SelectListItem{ Text="Selecione uma opção.",Value=""},
                new SelectListItem{ Text=((int)Tempo.Minutos5).ToString()+" minutos", Value=((int)Tempo.Minutos5).ToString() },
                new SelectListItem{ Text=((int)Tempo.Minutos10).ToString()+" minutos", Value=((int)Tempo.Minutos10).ToString() },
                new SelectListItem{ Text=((int)Tempo.QuartoHora).ToString()+" minutos", Value=((int)Tempo.QuartoHora).ToString() },
                new SelectListItem{ Text=((int)Tempo.MeiaHora).ToString()+" minutos", Value=((int)Tempo.MeiaHora).ToString() },
                new SelectListItem{ Text=((int)Tempo.Minutos45).ToString()+" minutos", Value=((int)Tempo.Minutos45).ToString() },
                new SelectListItem{ Text=((int)Tempo.Hora).ToString()+" minutos", Value=((int)Tempo.Hora).ToString() },
                new SelectListItem{ Text=((int)Tempo.HoraEMeia).ToString()+" minutos", Value=((int)Tempo.HoraEMeia).ToString() },
                new SelectListItem{ Text=((int)Tempo.DuasHoras).ToString()+" minutos", Value=((int)Tempo.DuasHoras).ToString() }
            };

            Graus = new List<SelectListItem>()
            {
                new SelectListItem{ Text="Selecione uma opção.",Value=""},
                new SelectListItem{ Text = "Muito Fácil" ,Value=((int)GrauDeDificuldade.MuitoFacil).ToString() },
                new SelectListItem{ Text = "Fácil " ,Value=((int)GrauDeDificuldade.Facil).ToString() },
                new SelectListItem{ Text = "Médio",Value=((int)GrauDeDificuldade.Medio).ToString() },
                new SelectListItem{ Text = "Difícil" ,Value=((int)GrauDeDificuldade.Dificil).ToString() },
                new SelectListItem{ Text = "Muito Difícil",Value=((int)GrauDeDificuldade.MuitoDificil).ToString() }
            };

            Estados = new List<SelectListItem>()
            {
                new SelectListItem{ Text="Selecione uma opção.",Value=""},
                new SelectListItem{ Text="Válida",Value=((int)TipoDeEstado.Valida).ToString() },
                new SelectListItem{ Text="Inválida",Value=((int)TipoDeEstado.Invalida).ToString() },
                new SelectListItem{ Text="Pendente",Value=((int)TipoDeEstado.Pendente).ToString() },
                new SelectListItem{ Text="Em Análise",Value=((int)TipoDeEstado.Analise).ToString() }
            };

            List<Categoria> categorias = _categoriaService.GetAll();

            Categorias = new List<SelectListItem> { new SelectListItem { Text ="Selecione uma opção.", Value="" } };

            foreach (var categoria in categorias)
            {

                Categorias.Add(new SelectListItem { Text = categoria.CategoriaNome, Value=categoria.CategoriaId.ToString() });

            }

            IdPagina = idPagina;

            Texto=texto;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                if (Receita.Imagem != null && Receita.Imagem.Length > 0)
                {
                    string nomeDaImagem = "Receita_" + Guid.NewGuid();

                    if (Receita.Imagem.FileName.Contains(".jpg"))
                        nomeDaImagem += ".jpg";
                    else if (Receita.Imagem.FileName.Contains(".gif"))
                        nomeDaImagem += ".gif";
                    else if (Receita.Imagem.FileName.Contains(".png"))
                        nomeDaImagem += ".png";
                    else if (Receita.Imagem.FileName.Contains(".pdf"))
                        nomeDaImagem += ".pdf";
                    else
                        nomeDaImagem += ".tmp";

                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/uploads-receitas", nomeDaImagem);

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await Receita.Imagem.CopyToAsync(stream);
                    }

                    Receita receita = new Receita(Receita.ReceitaId, Receita.Titulo, nomeDaImagem, Receita.ModoDePreparacao,
                    (Tempo)Receita.TempoDePreparacao, (TipoDeEstado)Receita.Estado, (GrauDeDificuldade)Receita.Dificuldade,
                    _categoriaService.GetById(Receita.CategoriaId), _utilizadorService.GetById(Receita.UtilizadorId));

                    _receitaService.Update(receita);

                    return RedirectToPage("/Administracao/VerReceitas", new { id = IdPagina, texto = Texto });
                }
                else 
                {
                    Receita receita = new Receita(Receita.ReceitaId, Receita.Titulo, null, Receita.ModoDePreparacao,
                   (Tempo)Receita.TempoDePreparacao, (TipoDeEstado)Receita.Estado, (GrauDeDificuldade)Receita.Dificuldade,
                   _categoriaService.GetById(Receita.CategoriaId), _utilizadorService.GetById(Receita.UtilizadorId));

                    _receitaService.Update(receita);

                    return RedirectToPage("/Administracao/VerReceitas", new { id = IdPagina, texto = Texto });
                }
               
            }

            Tempos = new List<SelectListItem>()
            {
                new SelectListItem{ Text="Selecione uma opção.",Value=""},
                new SelectListItem{ Text=((int)Tempo.Minutos5).ToString()+" minutos", Value=((int)Tempo.Minutos5).ToString() },
                new SelectListItem{ Text=((int)Tempo.Minutos10).ToString()+" minutos", Value=((int)Tempo.Minutos10).ToString() },
                new SelectListItem{ Text=((int)Tempo.QuartoHora).ToString()+" minutos", Value=((int)Tempo.QuartoHora).ToString() },
                new SelectListItem{ Text=((int)Tempo.MeiaHora).ToString()+" minutos", Value=((int)Tempo.MeiaHora).ToString() },
                new SelectListItem{ Text=((int)Tempo.Minutos45).ToString()+" minutos", Value=((int)Tempo.Minutos45).ToString() },
                new SelectListItem{ Text=((int)Tempo.Hora).ToString()+" minutos", Value=((int)Tempo.Hora).ToString() },
                new SelectListItem{ Text=((int)Tempo.HoraEMeia).ToString()+" minutos", Value=((int)Tempo.HoraEMeia).ToString() },
                new SelectListItem{ Text=((int)Tempo.DuasHoras).ToString()+" minutos", Value=((int)Tempo.DuasHoras).ToString() }
            };

            Graus = new List<SelectListItem>()
            {
                new SelectListItem{ Text="Selecione uma opção.",Value=""},
                new SelectListItem{ Text = "Muito Fácil" ,Value=((int)GrauDeDificuldade.MuitoFacil).ToString() },
                new SelectListItem{ Text = "Fácil " ,Value=((int)GrauDeDificuldade.Facil).ToString() },
                new SelectListItem{ Text = "Médio",Value=((int)GrauDeDificuldade.Medio).ToString() },
                new SelectListItem{ Text = "Difícil" ,Value=((int)GrauDeDificuldade.Dificil).ToString() },
                new SelectListItem{ Text = "Muito Difícil",Value=((int)GrauDeDificuldade.MuitoDificil).ToString() }
            };

            Estados = new List<SelectListItem>()
            {
                new SelectListItem{ Text="Selecione uma opção.",Value=""},
                new SelectListItem{ Text="Válida",Value=((int)TipoDeEstado.Valida).ToString() },
                new SelectListItem{ Text="Inválida",Value=((int)TipoDeEstado.Invalida).ToString() },
                new SelectListItem{ Text="Pendente",Value=((int)TipoDeEstado.Pendente).ToString() },
                new SelectListItem{ Text="Em Análise",Value=((int)TipoDeEstado.Analise).ToString() }
            };

            List<Categoria> categorias = _categoriaService.GetAll();

            Categorias = new List<SelectListItem> { new SelectListItem { Text ="Selecione uma opção.", Value="" } };

            foreach (var categoria in categorias)
            {
                Categorias.Add(new SelectListItem { Text = categoria.CategoriaNome, Value=categoria.CategoriaId.ToString() });
            }

            return Page();
        }
    }
}
