using Project.Receitas.Data.Interfaces;
using Project.Receitas.Data.Repositories;
using Projecto.Receitas.Domain.Model;
using Projecto.Receitas.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projecto.Receitas.Services.Services
{
    public class ContactoService : IContactoService
    {
        private IContactoRepository _contactoRepository;

        public ContactoService(IContactoRepository contactoRepository)
        {
            _contactoRepository=contactoRepository;
        }

        public bool Add(Contacto contacto)
        {
            ValidarContacto(contacto);

            List<Contacto> contactos = _contactoRepository.GetByUtilizadorId(contacto.Utilizador.UtilizadorId);

            bool jaExiste = false;

            foreach (Contacto cont in contactos) 
            {
                if (contacto.Tipo == cont.Tipo) 
                {
                    jaExiste = true;
                }
            }

            if (!jaExiste) 
            {
                return _contactoRepository.Add(contacto);
            }
            else 
            {
                return false;
            }   
        }

        public bool Delete(int id)
        {
            Contacto contactoExiste = _contactoRepository.GetById(id);

            if (contactoExiste!=null) 
            { 
                return _contactoRepository.Delete(id);
            }
            else 
            {
                return false;
            }
        }

        public bool Delete(Contacto contacto)
        {
            ValidarContacto(contacto);

            Contacto contactoExiste = _contactoRepository.GetById(contacto.ContactoId);

            if(contactoExiste !=null) 
            {
                return _contactoRepository.Delete(contacto);
            }
            else 
            {
                return false;
            }
        }

        public List<Contacto> GetAll()
        {
            return _contactoRepository.GetAll();
        }

        public Contacto GetById(int id)
        {
            return _contactoRepository.GetById(id);
        }

        public List<Contacto> GetByUtilizadorId(int id)
        {
            return _contactoRepository.GetByUtilizadorId(id);
        }

        public bool Update(Contacto contacto)
        {
            ValidarContacto(contacto);

            Contacto contactoExiste = _contactoRepository.GetById(contacto.ContactoId);

            if(contactoExiste != null) 
            {
                return _contactoRepository.Update(contacto);
            }
            else 
            { 
                return false; 
            }   
        }

        public static void ValidarContacto(Contacto contacto) 
        {
            if (contacto is null) 
            {
                throw new ArgumentNullException("Contacto não pode ser nulo.");
            }
            else if(string.IsNullOrEmpty(contacto.Valor)) 
            {
                throw new ArgumentNullException("A propriedade valor não pode ser vazia.");
            }
            else if (contacto.Valor.Length>300)
            {
                throw new ArgumentNullException(" A propriedade valor não pode ter mais de 300 caracteres.");
            }
            else if(contacto.Tipo==TipoDeContacto.Email && !ValidarEmail(contacto.Valor))
            {
                throw new ArgumentException("O email é inválido");
            }
            else if(contacto.Tipo==TipoDeContacto.Telefone  && contacto.Valor.Length!=9) 
            {
                throw new ArgumentNullException("O número de telefone tem no máximo 9 digitos.");
            }
            else if(contacto.Tipo==TipoDeContacto.Telemovel && contacto.Valor.Length!=9) 
            {
                throw new ArgumentNullException("O número de telemóvel tem no máximo 9 digitos.");
            }
            else if(contacto.Tipo==TipoDeContacto.Telefone  && !int.TryParse(contacto.Valor,out int resultado))
            {
                throw new ArgumentNullException("O número de telefone á formado por 9 digitos.");
            }
            else if (contacto.Tipo==TipoDeContacto.Telemovel  && !int.TryParse(contacto.Valor, out int resultado2))
            {
                throw new ArgumentNullException("O número de telemovel é formado por 9 digitos.");
            }
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
