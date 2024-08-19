using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Projecto.Receitas.Domain.Model;
using Projecto.Receitas.Services.Interfaces;
using Projecto.Receitas.Services.Services;
using Projecto.Receitas.WebApp.ViewModel;

namespace Projecto.Receitas.WebApp.Pages.Administracao
{
    public class NovoItemModel : PageModel
    {
        private IItemService _itemService;

        private IIngredienteService _ingredienteService;

        private IReceitaService _receitaService;

        private IMedidaService _medidaService;

        public NovoItemModel(IItemService itemService,IIngredienteService ingredienteService,IReceitaService receitaService
            ,IMedidaService medidaService) 
        { 
            _itemService = itemService; 
            _ingredienteService = ingredienteService;
            _receitaService = receitaService;
            _medidaService = medidaService;
        }

        public List<SelectListItem> Medidas { get; set; }

        [BindProperty]
        public ItemViewModel Item {  get; set; }

        public List<SelectListItem> Ingredientes { get; set; }

        [BindProperty]
        public int IdPagina {  get; set; }
        [BindProperty]
        public string? Texto { get; set; }
        public IActionResult OnGet(int idReceita,int idPagina = 0, string? texto = "")
        {
            Receita receita = _receitaService.GetById(idReceita);

            if (receita == null) 
            { 
                return NotFound();
            }

            Item = new ItemViewModel()
            {
                ReceitaId = idReceita
            };

            List<Medida> medidas =_medidaService.GetAll();

            Medidas = new List<SelectListItem>() { new SelectListItem() { Text="Selecione uma opcão.", Value="" } };

            foreach (Medida medida in medidas)
            {
                Medidas.Add(new SelectListItem() { Text = medida.MedidaNome, Value = medida.MedidaId.ToString() });
            }

            List<Ingrediente> ingredientes = _ingredienteService.GetAll();

            Ingredientes = new List<SelectListItem>() { new SelectListItem() { Text="Selecione uma opcão.", Value="" } };

            foreach (Ingrediente ingrediente in ingredientes) 
            {
                Ingredientes.Add(new SelectListItem() { Text =ingrediente.IngredienteNome,Value = ingrediente.IngredienteId.ToString() });
            }

            IdPagina = idPagina;

            Texto = texto;

            return Page();
        }

        public IActionResult OnPost() 
        {
            if(ModelState.IsValid) 
            {
                Item item = new Item(Item.Quantidade,_medidaService.GetById(Item.MedidaId), _receitaService.GetById(Item.ReceitaId),
                                    _ingredienteService.GetById(Item.IngredienteId));

                _itemService.Add(item);

                return RedirectToPage("/Administracao/VerReceita", 
                               new { idReceita = Item.ReceitaId,idPagina = IdPagina ,texto=Texto});
            }

            List<Medida> medidas = _medidaService.GetAll();

            Medidas = new List<SelectListItem>() { new SelectListItem() { Text="Selecione uma opcão.", Value="" } };

            foreach (Medida medida in medidas)
            {
                Medidas.Add(new SelectListItem() { Text = medida.MedidaNome, Value = medida.MedidaId.ToString() });
            }

            List<Ingrediente> ingredientes = _ingredienteService.GetAll();

            Ingredientes = new List<SelectListItem>() { new SelectListItem() { Text="Selecione uma opcão.", Value="" } };

            foreach (Ingrediente ingrediente in ingredientes)
            {
                Ingredientes.Add(new SelectListItem() { Text =ingrediente.IngredienteNome, Value = ingrediente.IngredienteId.ToString() });
            }

            return Page();
        }
    }
}
