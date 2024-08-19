using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Win32;
using Projecto.Receitas.Domain.Model;
using Projecto.Receitas.Services.Interfaces;

namespace Projecto.Receitas.WebApp.Pages.Conta
{
    public class NovaPasswordContaModel : PageModel
    {
        private IAccountService _accountService;

        private IUtilizadorService _utilizadorService;

        private IPasswordHasher<IndexModel> _passwordHasher;

        public NovaPasswordContaModel(IAccountService accountService,IUtilizadorService utilizadorService,
                                      IPasswordHasher<IndexModel> passwordHasher) 
        {
            _accountService = accountService;
            _utilizadorService = utilizadorService;
            _passwordHasher = passwordHasher;
        }

        [BindProperty]
        public string Password { get; set; }

        [BindProperty]
        public string PasswordConfirm { get; set; }

        [BindProperty]
        public int UtilizadorId { get; set; }

        public string MensagemErro {  get; set; }

        public Utilizador Utilizador {  get; set; } 
        public IActionResult OnGet(string senha)
        {
            int? utilizadorId = HttpContext.Session.GetInt32("UserId");

            if (utilizadorId == null)
            {
                return BadRequest();
            }

            if (!TempData.ContainsKey("Senha")) 
            {
                return BadRequest();
            }
            else 
            {
                if (senha != (string?)TempData["Senha"]) 
                {
                    return BadRequest();
                }
            }

            Utilizador = _utilizadorService.GetById((int)utilizadorId);

            if(Utilizador == null) 
            {
                return BadRequest();
            }

            UtilizadorId = (int)utilizadorId;

            MensagemErro =string.Empty;

            return Page();
        }

        public IActionResult OnPost() 
        {
            if (ModelState.IsValid) 
            {
                if (Password == PasswordConfirm) 
                {
                    Account account = _accountService.GetByUtilizadorId(UtilizadorId);

                    if (account == null) 
                    {
                        return NotFound();
                    }

                    account.Password = _passwordHasher.HashPassword(null, Password);

                    _accountService.UpdateByUtilizadorId(account);

                    return RedirectToPage("/Conta/VerDefinicoesConta");
                }
                else 
                {
                    MensagemErro = "As passwords não coincidem.";
                }
            }

            Utilizador = _utilizadorService.GetById(UtilizadorId);

            return Page();
        }
    }
}
