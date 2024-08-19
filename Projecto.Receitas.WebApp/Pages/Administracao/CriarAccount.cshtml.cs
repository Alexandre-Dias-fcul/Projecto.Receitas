using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Win32;
using Projecto.Receitas.Domain.Model;
using Projecto.Receitas.Services.Interfaces;
using Projecto.Receitas.Services.Services;
using Projecto.Receitas.WebApp.ViewModel;

namespace Projecto.Receitas.WebApp.Pages.Administracao
{
    public class CriarAccountModel : PageModel
    {
        private IAccountService _accountService;

        private IUtilizadorService _utilizadorService;

        private IPasswordHasher<IndexModel> _passwordHasher;

        public CriarAccountModel(IAccountService accountService,IUtilizadorService utilizadorService,
            IPasswordHasher<IndexModel> passwordHasher) 
        { 
            _accountService = accountService;
            _utilizadorService = utilizadorService;
            _passwordHasher = passwordHasher;
        }

        public string MensagemErro {  get; set; }

        public Utilizador Utilizador { get; set; }

        [BindProperty]
        public AccountViewModel Account { get; set; }

        [BindProperty]
        public int IdPagina { get; set; }

        [BindProperty]
        public string? Texto { get; set; }
        public IActionResult OnGet(int idUser,int idPagina = 0, string? texto = "")
        {
            Utilizador = _utilizadorService.GetById(idUser);

            if (Utilizador == null) 
            {
                return NotFound();
            }

            Account = new AccountViewModel();
            Account.UtilizadorId = idUser;
            MensagemErro = string.Empty;
            IdPagina = idPagina;
            Texto = texto;

            return Page();
        }

        public IActionResult OnPost() 
        {
            if (ModelState.IsValid)
            { 
                Account account = new Account(Account.Username, _passwordHasher.HashPassword(null, Account.Password), 
                                  _utilizadorService.GetById(Account.UtilizadorId));

                if (_accountService.Add(account)) 
                {
                    return RedirectToPage("/Administracao/VerAccount", 
                                          new { idUser = account.Utilizador.UtilizadorId, idPagina = IdPagina , texto = Texto});
                }
                else 
                {
                    MensagemErro = " Erro: não foi possivel criar a conta.";
                }
            }

            Utilizador = _utilizadorService.GetById(Account.UtilizadorId);

            return Page();
        }
    }
}
