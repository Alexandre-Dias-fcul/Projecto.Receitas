using Projecto.Receitas.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Receitas.Data.Interfaces
{
    public interface IMedidaRepository:IRepository<Medida>
    {
        public List<Medida> GetAllPesquisa(string texto, int offset, int fetch);
        public int NumeroTotalDeMedidas(string texto);
    }
}
