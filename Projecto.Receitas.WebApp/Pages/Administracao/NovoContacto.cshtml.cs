using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Projecto.Receitas.Domain.Model;
using Projecto.Receitas.Services.Interfaces;
using Projecto.Receitas.Services.Services;
using Projecto.Receitas.WebApp.ViewModel;

namespace Projecto.Receitas.WebApp.Pages.Administracao
{
    public class NovoContactoModel : PageModel
    {
        private IUtilizadorService _utilizadorService;

        private IContactoService _contactoService;

        public NovoContactoModel(IUtilizadorService utilizadorService,IContactoService contactoService) 
        { 
            _utilizadorService = utilizadorService;
            _contactoService = contactoService;
        }
        public Utilizador Utilizador { get; set; }

        [BindProperty]
        public ContactoViewModel Contacto { get; set; }
        public List<SelectListItem> Tipos { get; set; }
        public string MensagemErro { get; set; }

        [BindProperty]
        public int IdPagina { get; set; }

        [BindProperty]
        public string? Texto { get; set; }
        public IActionResult OnGet(int idUser,int idPagina = 0, string? texto = "")
        {

            Utilizador = _utilizadorService.GetById(idUser);

            if(Utilizador == null) 
            {
                return NotFound();
            }

            Contacto = new ContactoViewModel { UtilizadorId = idUser };

            Tipos = new List<SelectListItem>()
            {
                new SelectListItem(){ Text = "Selecione uma opção",Value = ""},
                new SelectListItem(){ Text = TipoDeContacto.Email.ToString() , Value = ((int)TipoDeContacto.Email).ToString()},
                new SelectListItem(){ Text = TipoDeContacto.Telefone.ToString() , Value = ((int)TipoDeContacto.Telefone).ToString()},
                new SelectListItem(){ Text = TipoDeContacto.Telemovel.ToString() , Value = ((int)TipoDeContacto.Telemovel).ToString()}
            };

            MensagemErro = string.Empty;

            IdPagina = idPagina;

            Texto = texto;

            return Page();
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                if((Contacto.Tipo == TipoDeContacto.Email) && ValidarEmail(Contacto.Valor)) 
                {
                    Contacto contacto = new Contacto(Contacto.Tipo, Contacto.Valor, _utilizadorService.GetById(Contacto.UtilizadorId));

                    if (_contactoService.Add(contacto))
                    {
                        return RedirectToPage("/Administracao/VerContactos", 
                            new { idUser = Contacto.UtilizadorId, idPagina = IdPagina , texto =Texto });
                    }
                    else
                    {
                        MensagemErro="Erro:Não foi possivel criar o contacto.";
                    }

                }
                else if ((Contacto.Tipo==TipoDeContacto.Telefone) && (Contacto.Valor.Length==9) && 
                    int.TryParse(Contacto.Valor,out int resultado )) 
                {
                    Contacto contacto = new Contacto(Contacto.Tipo, Contacto.Valor, _utilizadorService.GetById(Contacto.UtilizadorId));

                    if (_contactoService.Add(contacto))
                    {
                        return RedirectToPage("/Administracao/VerContactos", 
                                new { idUser = Contacto.UtilizadorId, idPagina = IdPagina, texto = Texto });
                    }
                    else
                    {
                        MensagemErro="Erro:Não foi possivel criar o contacto.";
                    }
                }
                else if ((Contacto.Tipo==TipoDeContacto.Telemovel) && (Contacto.Valor.Length==9) && 
                       int.TryParse(Contacto.Valor, out int resultado2))
                {
                    Contacto contacto = new Contacto(Contacto.Tipo, Contacto.Valor, _utilizadorService.GetById(Contacto.UtilizadorId));

                    if (_contactoService.Add(contacto))
                    {
                        return RedirectToPage("/Administracao/VerContactos", 
                            new { idUser = Contacto.UtilizadorId, idPagina = IdPagina, texto = Texto });
                    }
                    else
                    {
                        MensagemErro="Erro:Não foi possivel criar o contacto.";
                    }
                }
                else 
                {
                    if (Contacto.Tipo == TipoDeContacto.Email) 
                    {
                        MensagemErro="Erro:Email inválido.";
                    }
                    else if(Contacto.Tipo == TipoDeContacto.Telefone) 
                    {
                        MensagemErro="Erro:Telefone inválido.";
                    }
                    else if (Contacto.Tipo == TipoDeContacto.Telemovel)
                    {
                        MensagemErro="Erro:Telemóvel inválido.";
                    }
                }
            }

            Utilizador = _utilizadorService.GetById(Contacto.UtilizadorId);

            Tipos = new List<SelectListItem>()
            {
                new SelectListItem(){ Text = "Selecione uma opção",Value = ""},
                new SelectListItem(){ Text = TipoDeContacto.Email.ToString() , Value = ((int)TipoDeContacto.Email).ToString()},
                new SelectListItem(){ Text = TipoDeContacto.Telefone.ToString() , Value = ((int)TipoDeContacto.Telefone).ToString()},
                new SelectListItem(){ Text = TipoDeContacto.Telemovel.ToString() , Value = ((int)TipoDeContacto.Telemovel).ToString()}
            };

            return Page();
        }

        private static bool ValidarEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            string[] parts = email.Split('@');
            if (parts.Length != 2)
            {
                return false; // email must have exactly one @ symbol
            }

            string localPart = parts[0];
            string domainPart = parts[1];
            if (string.IsNullOrWhiteSpace(localPart) || string.IsNullOrWhiteSpace(domainPart))
            {
                return false; // local and domain parts cannot be empty
            }

            // check local part for valid characters
            foreach (char c in localPart)
            {
                if (!char.IsLetterOrDigit(c) && c != '.' && c != '_' && c != '-')
                {
                    return false; // local part contains invalid character
                }
            }

            // check domain part for valid format
            if (domainPart.Length < 2 || !domainPart.Contains(".") || domainPart.Split(".").Length !=2 ||  domainPart.EndsWith(".") || domainPart.StartsWith("."))
            {
                return false; // domain part is not a valid format
            }

            return true;
        }
    }
}
