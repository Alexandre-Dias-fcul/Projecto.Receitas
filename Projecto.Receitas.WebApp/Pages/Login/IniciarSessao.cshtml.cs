using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Projecto.Receitas.Domain.Model;
using Projecto.Receitas.Services.Interfaces;
using Projecto.Receitas.Services.Services;
using Projecto.Receitas.WebApp.ViewModel;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Projecto.Receitas.WebApp.Pages.Login
{
    public class IniciarSessaoModel : PageModel
    {
        private IUtilizadorService _utilizadorService;
        private IAccountService _accountService ;
        private IPasswordHasher<IndexModel> _passwordHasher;

        public IniciarSessaoModel(IUtilizadorService utilizadorService,IAccountService accountService, 
                                  IPasswordHasher<IndexModel> passwordHasher) 
        {
            _utilizadorService = utilizadorService;
            _accountService = accountService;
            _passwordHasher = passwordHasher;
        }

        public string MensagemErro { get; set; }

        [BindProperty]
        public LoginViewModel Login {  get; set; }
        public void OnGet()
        {
            MensagemErro = string.Empty;
        }

        public async Task<IActionResult> OnPost() 
        {
            if(ModelState.IsValid) 
            {
               Account account = _accountService.GetByUsername(Login.Username);
               
               if(account!= null)
               { 
                    var result =_passwordHasher.VerifyHashedPassword(null, account.Password, Login.Password);

                    if (result == PasswordVerificationResult.Success) 
                    {
                        Utilizador utilizador = _utilizadorService.GetById(account.Utilizador.UtilizadorId);

                        HttpContext.Session.SetString("Nome", utilizador.PrimeiroNome+" "+utilizador.UltimoNome);
                        HttpContext.Session.SetInt32("UserId", utilizador.UtilizadorId);
                        HttpContext.Session.SetString("Permissoes", utilizador.Permissoes.ToString());

                        byte[] Key = new byte[16];
                        byte[] IV = new byte[16];

                        using (var rng = new RNGCryptoServiceProvider())
                        {
                            rng.GetBytes(Key);
                            rng.GetBytes(IV);
                        }

                        HttpContext.Session.SetString("Key", Convert.ToBase64String(Key));

                        HttpContext.Session.SetString("IV", Convert.ToBase64String(IV));

                        var userClaims = new List<Claim>()
                        {
                            //define o cookie
                           new Claim(ClaimTypes.Name,utilizador.PrimeiroNome+" "+utilizador.UltimoNome ),
                           new Claim(ClaimTypes.Role,utilizador.Permissoes.ToString()),
                           new Claim(ClaimTypes.NameIdentifier,utilizador.UtilizadorId.ToString())
                        };

                        var minhaIdentity = new ClaimsIdentity(userClaims, "Usuario");
                        var userPrincipal = new ClaimsPrincipal(new[] { minhaIdentity });

                        //cria o cookie
                        await HttpContext.SignInAsync(userPrincipal);

                        return RedirectToPage("/Index");
                    }
                    else
                    {
                        MensagemErro="Inicio de Sessão inválido.";
                    }
                }
                else 
                {
                    MensagemErro="Inicio de Sessão inválido.";
                }
            }

            return Page();
        }
    }
}
