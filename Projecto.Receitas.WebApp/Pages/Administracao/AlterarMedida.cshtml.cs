using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Projecto.Receitas.Domain.Model;
using Projecto.Receitas.Services.Interfaces;
using Projecto.Receitas.WebApp.ViewModel;

namespace Projecto.Receitas.WebApp.Pages.Administracao
{
    public class AlterarMedidaModel : PageModel
    {
        private IMedidaService _medidaService;

        public AlterarMedidaModel(IMedidaService medidaService) 
        {
            _medidaService=medidaService;
        }

        [BindProperty]
        public MedidaViewModelId Medida {  get; set; }

        [BindProperty]
        public int IdPagina {  get; set; }

        [BindProperty]
        public string? Texto { get; set; }

        public IActionResult OnGet(int idMedida,int idPagina=0,string? texto="")
        {
            Medida medida = _medidaService.GetById(idMedida);

            if (medida==null) 
            {
                return NotFound();
            }

            Medida = new MedidaViewModelId
            {
                MedidaId = medida.MedidaId,
                MedidaNome = medida.MedidaNome
            };

            IdPagina=idPagina;

            Texto = texto;

            return Page();
        }

        public IActionResult OnPost() 
        {
            if (ModelState.IsValid) 
            {
                Medida medida= new Medida(Medida.MedidaId,Medida.MedidaNome);

                _medidaService.Update(medida);

                return RedirectToPage("/Administracao/VerMedidas", new { id = IdPagina , texto = Texto });
            }
            return Page();
        }
    }
}
