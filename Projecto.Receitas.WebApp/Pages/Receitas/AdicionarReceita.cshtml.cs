using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Projecto.Receitas.Domain.Model;
using Projecto.Receitas.Services.Interfaces;
using Projecto.Receitas.Services.Services;
using Projecto.Receitas.WebApp.ViewModel;

namespace Projecto.Receitas.WebApp.Pages.Receitas
{
    public class AdicionarReceitaModel : PageModel
    {
        private IReceitaService _receitaService;

        private ICategoriaService _categoriaService;

        private IUtilizadorService _utilizadorService;

        private ValidarSessao _validarSessao;

        public AdicionarReceitaModel(IReceitaService receitaService,ICategoriaService categoriaService,
            IUtilizadorService utilizadorService,ValidarSessao validarSessao) 
        { 
            _receitaService = receitaService;   
            _categoriaService = categoriaService;
            _utilizadorService = utilizadorService;
            _validarSessao = validarSessao;
        }

        public List<SelectListItem> Tempos { get; set; }
        public List<SelectListItem> Graus { get; set; }
        public List<SelectListItem> Categorias { get; set; }

        [BindProperty]
        public ReceitaViewModelSemEstado Receita { get; set; }

        [BindProperty]
        public string Codigo { get; set; }
        public IActionResult OnGet()
        {
            int? utilizadorId = HttpContext.Session.GetInt32("UserId");

            if (utilizadorId == null)
            {
                return NotFound();
            }

            Receita = new ReceitaViewModelSemEstado() { UtilizadorId = (int)utilizadorId };

            Tempos = new List<SelectListItem>()
            {
                new SelectListItem{ Text="Selecione uma op��o.",Value=""},
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
                new SelectListItem{ Text="Selecione uma op��o.",Value=""},
                new SelectListItem{ Text = "Muito F�cil" ,Value=((int)GrauDeDificuldade.MuitoFacil).ToString() },
                new SelectListItem{ Text = "F�cil " ,Value=((int)GrauDeDificuldade.Facil).ToString() },
                new SelectListItem{ Text = "M�dio",Value=((int)GrauDeDificuldade.Medio).ToString() },
                new SelectListItem{ Text = "Dif�cil" ,Value=((int)GrauDeDificuldade.Dificil).ToString() },
                new SelectListItem{ Text = "Muito Dif�cil",Value=((int)GrauDeDificuldade.MuitoDificil).ToString() }
            };

            List<Categoria> categorias = _categoriaService.GetAll();

            Categorias = new List<SelectListItem> { new SelectListItem { Text ="Selecione uma op��o.", Value="" } };

            foreach (var categoria in categorias)
            {
                Categorias.Add(new SelectListItem { Text = categoria.CategoriaNome, Value=categoria.CategoriaId.ToString() });
            }

            Codigo = _validarSessao.Encriptar((int)utilizadorId);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync() 
        {
            if(ModelState.IsValid) 
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

                    Receita receita = new Receita(Receita.Titulo, nomeDaImagem, Receita.ModoDePreparacao, Receita.TempoDePreparacao
                                      ,TipoDeEstado.Pendente, Receita.Dificuldade, _categoriaService.GetById(Receita.CategoriaId),
                                      _utilizadorService.GetById(Receita.UtilizadorId));

                    int id = _receitaService.AddId(receita);

                    return RedirectToPage("/Receitas/FinalizarAdicionarReceita", new { Id = id, codigo = Codigo });
                }
                else 
                {
                    Receita receita = new Receita(Receita.Titulo,null, Receita.ModoDePreparacao, Receita.TempoDePreparacao
                                     , TipoDeEstado.Pendente, Receita.Dificuldade, _categoriaService.GetById(Receita.CategoriaId),
                                     _utilizadorService.GetById(Receita.UtilizadorId));

                    int id = _receitaService.AddId(receita);

                    return RedirectToPage("/Receitas/FinalizarAdicionarReceita", new { Id = id , codigo = Codigo });
                }
            }

            Tempos = new List<SelectListItem>()
            {
                new SelectListItem{ Text="Selecione uma op��o.",Value=""},
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
                new SelectListItem{ Text="Selecione uma op��o.",Value=""},
                new SelectListItem{ Text = "Muito F�cil" ,Value=((int)GrauDeDificuldade.MuitoFacil).ToString() },
                new SelectListItem{ Text = "F�cil " ,Value=((int)GrauDeDificuldade.Facil).ToString() },
                new SelectListItem{ Text = "M�dio",Value=((int)GrauDeDificuldade.Medio).ToString() },
                new SelectListItem{ Text = "Dif�cil" ,Value=((int)GrauDeDificuldade.Dificil).ToString() },
                new SelectListItem{ Text = "Muito Dif�cil",Value=((int)GrauDeDificuldade.MuitoDificil).ToString() }
            };

            List<Categoria> categorias = _categoriaService.GetAll();

            Categorias = new List<SelectListItem> { new SelectListItem { Text ="Selecione uma op��o.", Value="" } };

            foreach (var categoria in categorias)
            {
                Categorias.Add(new SelectListItem { Text = categoria.CategoriaNome, Value=categoria.CategoriaId.ToString() });
            }

            return Page();
        }
    }
}
