using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Projecto.Receitas.Domain.Model;
using Projecto.Receitas.Services.Interfaces;
using Projecto.Receitas.Services.Services;
using Projecto.Receitas.WebApp.ViewModel;

namespace Projecto.Receitas.WebApp.Pages.Login
{
    public class RegistarModel : PageModel
    {
        private IUtilizadorService _utilizadorService;

        private IAccountService _accountService;

        private IPasswordHasher<IndexModel> _passwordHasher;
        public RegistarModel(IUtilizadorService utilizadorService,IAccountService accountService, 
            IPasswordHasher<IndexModel> passwordHasher) 
        {
            _utilizadorService = utilizadorService;
            _accountService = accountService;
            _passwordHasher = passwordHasher;
        }
        public string MensagemErro {  get; set; }

        [BindProperty]
        public RegistoViewModel Registo {  get; set; }
        public void OnGet()
        {
            MensagemErro = string.Empty;
        }

        public IActionResult OnPost() 
        {
            if (ModelState.IsValid) 
            {
                Account UserNameJaExiste = _accountService.GetByUsername(Registo.Username);

                if (UserNameJaExiste == null) 
                {
                    Utilizador utilizador = new Utilizador(Registo.PrimeiroNome,null ,Registo.UltimoNome,null,null,null,null,Permissao.Utilizador);

                    int id = _utilizadorService.AddId(utilizador);

                    Account account = new Account(Registo.Username, _passwordHasher.HashPassword(null,Registo.Password),
                                            new Utilizador(id, Registo.PrimeiroNome,null, Registo.UltimoNome,null,null,null,null, Permissao.Utilizador));

                    _accountService.Add(account);

                    return Redirect("/Login/IniciarSessao");
                }
                else 
                {
                    MensagemErro="O Username já existe.";
                }
            } 

            return Page();
        }
    }
}
