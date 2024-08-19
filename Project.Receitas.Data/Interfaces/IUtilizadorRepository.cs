using Projecto.Receitas.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Receitas.Data.Interfaces
{
    public interface IUtilizadorRepository:IRepository<Utilizador>
    {
        public int AddId(Utilizador utilizador);
        public List<Utilizador> GetAllPesquisa(string texto, int offset, int fetch);
        public int NumeroTotalDeUtilizadores(string texto);
    }
}
