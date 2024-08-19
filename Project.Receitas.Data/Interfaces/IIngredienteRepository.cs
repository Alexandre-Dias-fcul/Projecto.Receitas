using Projecto.Receitas.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Receitas.Data.Interfaces
{
    public interface IIngredienteRepository:IRepository<Ingrediente>
    {
        public List<Ingrediente> GetAllPesquisa(string texto, int offset, int fetch);
        public int NumeroTotalDeIngredientes(string texto);
    }
}
