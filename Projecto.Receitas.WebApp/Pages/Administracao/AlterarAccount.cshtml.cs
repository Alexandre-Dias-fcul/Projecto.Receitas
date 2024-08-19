using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Projecto.Receitas.Domain.Model;
using Projecto.Receitas.Services.Interfaces;
using Projecto.Receitas.Services.Services;
using Projecto.Receitas.WebApp.ViewModel;

namespace Projecto.Receitas.WebApp.Pages.Administracao
{
    public class AlterarAccountModel : PageModel
    {
        private IAccountService _accountService;

        private IUtilizadorService _utilizadorService;

        private IPasswordHasher<IndexModel> _passwordHasher;

        public AlterarAccountModel(IUtilizadorService utilizadorService,IAccountService accountService,
                                  IPasswordHasher<IndexModel> passwordHasher) 
        {
            _utilizadorService = utilizadorService; 
            _accountService = accountService;
            _passwordHasher = passwordHasher;
        }

        public Utilizador Utilizador { get; set; }

        [BindProperty]
        public AccountViewModel Account { get; set; }

        [BindProperty]
        public int IdPagina {  get; set; }

        [BindProperty]
        public string? Texto { get; set; }
        public IActionResult OnGet(int idUser,int idPagina = 0, string? texto = "")
        {

            Utilizador = _utilizadorService.GetById(idUser);

            if (Utilizador == null)
            {
                return NotFound();
            }

            Account account = _accountService.GetByUtilizadorId(idUser);

            if(account == null) 
            {
                return NotFound();
            }

            Account = new AccountViewModel()
            {
                Username = account.Username,
                Password = account.Password,
                UtilizadorId = account.Utilizador.UtilizadorId
            };

            IdPagina = idPagina;

            Texto = texto;

            return Page();
        }

        public IActionResult OnPost() 
        { 
            if(ModelState.IsValid) 
            {
                Account account = new Account(Account.Username,_passwordHasher.HashPassword(null,Account.Password), _utilizadorService.GetById(Account.UtilizadorId));

                _accountService.UpdateByUtilizadorId(account);

                return RedirectToPage("/Administracao/VerAccount", 
                                      new { idUser = account.Utilizador.UtilizadorId ,idPagina=IdPagina ,texto = Texto});
            }

            Utilizador = _utilizadorService.GetById(Account.UtilizadorId);

            return Page();
        }
    }
}
