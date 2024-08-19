using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projecto.Receitas.Domain.Model
{
    public class Contacto
    {
        public int ContactoId { get; set; }
        public TipoDeContacto Tipo { get; set; }
        public string Valor {  get; set; }
        public Utilizador Utilizador { get; set; }

        public Contacto(TipoDeContacto tipo,string valor,Utilizador utilizador) 
        {
            Tipo = tipo;

            if (tipo == TipoDeContacto.Email)
            {
                if (ValidarEmail(valor))
                {
                    if (valor.Length>300) 
                    {
                        throw new ArgumentNullException(" A propriedade valor não pode ter mais de 300 caracteres.");
                    }
                    else 
                    {
                        Valor = valor;
                    }
                }
                else
                {
                    throw new ArgumentNullException("O Email é inválido.");
                }
            }
            else if(tipo==TipoDeContacto.Telefone)
            {
               ValidarTelefone(valor);
               Valor = valor; 
            }
            else if (tipo==TipoDeContacto.Telemovel) 
            {
                ValidarTelefone(valor);
                Valor = valor;
            }
            
            Utilizador = utilizador;
        }

        public Contacto(int contactoId, TipoDeContacto tipo, string valor, Utilizador utilizador):this (tipo,valor,utilizador)
        {
            ContactoId = contactoId;
        }

        private static void ValidarTelefone(string valor) 
        {
            if (string.IsNullOrEmpty(valor)) 
            {
                throw new ArgumentNullException("A propriedade valor não pode ser vazia.");
            }
            else if (valor.Length!=9) 
            {
                throw new ArgumentNullException("O número de telefone tem no máximo 9 digitos.");
            }
            else if (!int.TryParse(valor, out int resultado))
            {
                throw new ArgumentNullException("O número de telefone á formado por 9 digitos.");
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
