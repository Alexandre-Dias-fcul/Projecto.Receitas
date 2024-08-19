using Projecto.Receitas.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projecto.Receitas.Services.Interfaces
{
    public interface IAccountService:IService<Account>
    {
        public Account GetByUtilizadorId(int id);
        public bool UpdateByUtilizadorId(Account account);
        public bool DeleteByUtilizadorId(int id);
        public bool DeleteByUtilizadorId(Account account);
        public Account GetByUsername(string username);
    }
}
