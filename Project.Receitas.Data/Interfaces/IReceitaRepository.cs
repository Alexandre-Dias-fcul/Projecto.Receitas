using Projecto.Receitas.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Receitas.Data.Interfaces
{
    public interface IReceitaRepository:IRepository<Receita>
    {
        public List<Receita> GetByCategoriaId(int id);
        public List<Receita> GetReceitasValidas(int top);
        public List<Receita> GetReceitasValidasTituloPesquisa(int top, string texto);
        public int AddId(Receita receita);
        public int NumeroDeReceitasValidas(string texto);
        public List<Receita> GetReceitasByUtilizadorId(int top,int id);
        public int TotalDeReceitasByUtilizadorId(int id);
        public List<Receita> GetAllPesquisa(string texto, int offset, int fetch);
        public int NumeroTotalDeReceitasPesquisa(string texto);
    }
}
