using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Projecto.Receitas.Domain.Model;
using Projecto.Receitas.Services.Interfaces;
using Projecto.Receitas.Services.Services;

namespace Projecto.Receitas.WebApp.Pages.Administracao
{
    public class VerCategoriasModel : PageModel
    {
        private ICategoriaService _categoriaService;
        public VerCategoriasModel(ICategoriaService categoriaService) 
        {
            _categoriaService = categoriaService;
        }
        public int Id { get; set; }
        public int MaximoId { get; set; }
        public string? Texto { get; set; }
        public int NumeroTotalDeCategorias { get; set; }
        public List<Categoria> Categorias {  get; set; }
        public IActionResult OnGet(int id = 0,string? texto="")
        {
            if (id<0)
            {
                id = 0;
            }

            NumeroTotalDeCategorias = _categoriaService.NumeroTotalDeCategorias(texto);

            MaximoId = (int)Math.Floor((decimal)NumeroTotalDeCategorias/5);

            Categorias = _categoriaService.GetAllPesquisa(texto, id*5, 5);

            Id= id;

            Texto = texto;

            return Page();
        }
    }
}
