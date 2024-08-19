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
    public class AccountService : IAccountService
    {
        private IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository) 
        { 
            _accountRepository = accountRepository;
        }
        public bool Add(Account account)
        {
            ValidarAccount(account);

            Account AccountJaExiste = _accountRepository.GetByUtilizadorId(account.Utilizador.UtilizadorId);

            if (AccountJaExiste==null)
            {
                Account AccountUserNameJaExiste = _accountRepository.GetByUsername(account.Username);

                if (AccountUserNameJaExiste==null) 
                { 
                    return _accountRepository.Add(account);
                }
            }

            return false;
        }

        public bool Delete(int id)
        {
            Account accountExiste = _accountRepository.GetById(id);

            if (accountExiste !=null) 
            {
                return _accountRepository.Delete(id);
            }
            else 
            {
                return false;
            }
           
        }

        public bool Delete(Account account)
        {
            if(account is null) 
            {
                throw new ArgumentNullException("Account não pode ser nulo.");
            }

            return _accountRepository.Delete(account);
        }

        public List<Account> GetAll()
        {
            return _accountRepository.GetAll();
        }

        public Account GetById(int id)
        {
            return _accountRepository.GetById(id);
        }

        public bool Update(Account account)
        {
            ValidarAccount(account);
            return _accountRepository.Update(account); 
        }

        public Account GetByUtilizadorId(int id)
        {
            return _accountRepository.GetByUtilizadorId(id);
        }

        public bool UpdateByUtilizadorId(Account account)
        {    
            ValidarAccount(account);
            return _accountRepository.UpdateByUtilizadorId(account);
        }

        public bool DeleteByUtilizadorId(int id)
        {
            Account accountExiste = _accountRepository.GetByUtilizadorId(id);

            if (accountExiste!=null) 
            {
                return _accountRepository.DeleteByUtilizadorId(id);
            }
            else 
            {
                return false;
            }  
        }

        public bool DeleteByUtilizadorId(Account account)
        {
            if(account is null) 
            {
                throw new ArgumentNullException("Account não pode ser nulo.");
            }

            Account accountExiste = _accountRepository.GetByUtilizadorId(account.Utilizador.UtilizadorId);

            if(accountExiste!=null) 
            {
                return _accountRepository.DeleteByUtilizadorId(account);
            }
            else 
            {
                return false;
            }
            
        }

        public Account GetByUsername(string username)
        {
            if(username is null) 
            {
                throw new ArgumentNullException("username não pode ser nulo.");
            }

            return _accountRepository.GetByUsername(username);
        }

        private static void ValidarAccount(Account account) 
        {
            if (account is null) 
            {
                throw new ArgumentNullException("Account não pode ser nulo.");
            }
            else if (string.IsNullOrEmpty(account.Username)) 
            {
                throw new ArgumentNullException("Username não pode ser vazio.");
            }
            else if (account.Username.Length>250) 
            {
                throw new ArgumentNullException("Username pode ter no máximo de 250 caracteres.");
            }
            else if (string.IsNullOrEmpty(account.Password))
            {
                throw new ArgumentNullException("Password não pode ser vazia.");
            }
            else if (account.Password.Length>250) 
            {
                throw new ArgumentNullException("Password pode ter no máximo de 250 caracteres.");
            }
        }
    }
}
