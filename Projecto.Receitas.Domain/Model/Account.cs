using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projecto.Receitas.Domain.Model
{
    public class Account
    {
        public int AccountId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Utilizador Utilizador { get; set; }

        public Account (string username, string password, Utilizador utilizador)
        {
            ValidarUsername(username);
            Username=username;
            ValidarPassword(password);
            Password=password;
            Utilizador=utilizador;
        }

        public Account(int accountId,string username, string password, Utilizador utilizador):this(username,password,utilizador)
        {
            AccountId=accountId;
        }

        private static void ValidarUsername(string username) 
        {
            if (string.IsNullOrEmpty(username)) 
            {
                throw new ArgumentNullException("Username não pode ser vazio.");
            }
            else if (username.Length>250) 
            {
                throw new ArgumentNullException("Username pode ter no máximo de 250 caracteres.");
            }
        }

        private static void ValidarPassword(string password) 
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException("Password não pode ser vazia.");
            }
            else if (password.Length>250)
            {
                throw new ArgumentNullException("Password pode ter no máximo de 250 caracteres.");
            }
        }
    }
}
