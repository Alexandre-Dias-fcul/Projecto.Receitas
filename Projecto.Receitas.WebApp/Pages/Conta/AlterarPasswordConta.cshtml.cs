using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Projecto.Receitas.Domain.Model;
using Projecto.Receitas.Services.Interfaces;

namespace Projecto.Receitas.WebApp.Pages.Conta
{
    public class AlterarPasswordContaModel : PageModel
    {
        private IAccountService _accountService;

        private IUtilizadorService _utilizadorService;

        private IPasswordHasher<IndexModel> _passwordHasher;

        public AlterarPasswordContaModel(IAccountService accountService, IUtilizadorService utilizadorService,
             IPasswordHasher<IndexModel> passwordHasher)
        {
            _accountService = accountService;
            _utilizadorService = utilizadorService;
            _passwordHasher = passwordHasher;
        }

        [BindProperty]
        public string Password { get; set; }

        [BindProperty]
        public int UtilizadorId { get; set; }
        public string MensagemErro { get; set; }
        public Utilizador Utilizador { get; set; }
        public IActionResult OnGet()
        {
            int? utilizadorId = HttpContext.Session.GetInt32("UserId");

            if (utilizadorId == null)
            {
                return BadRequest();
            }

            Utilizador = _utilizadorService.GetById((int)utilizadorId);

            if (Utilizador == null)
            {
                return BadRequest();
            }

            UtilizadorId = (int)utilizadorId;

            MensagemErro = string.Empty;

            return Page();
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                Account account = _accountService.GetByUtilizadorId(UtilizadorId);

                if (account == null) 
                {
                    return NotFound();
                }

                var result = _passwordHasher.VerifyHashedPassword(null, account.Password,Password);

                if (result == PasswordVerificationResult.Success)
                {
                    Random rnd = new Random();
                    TempData["Senha"]=rnd.Next(10000).ToString();
                    return RedirectToPage("/Conta/NovaPasswordConta", new { senha = TempData["Senha"] });
                }
                else
                {
                    MensagemErro = "Password inválida";
                }
            }

            Utilizador = _utilizadorService.GetById(UtilizadorId);

            return Page();
        }
    }
}
