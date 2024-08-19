using Projecto.Receitas.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Receitas.Data.Interfaces
{
    public interface IItemRepository:IRepository<Item>
    {
        public List<Item> GetByReceitaId(int id);
        public List<Item> GetByIngredienteId(int id);
        public List<Item> GetByMedidaId(int id);
    }
}
