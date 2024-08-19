using Projecto.Receitas.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Receitas.Data.Interfaces
{
    public interface IUtilizadorEnderecoRepository
    {
        public bool Add(UtilizadorEndereco utilizadorEndereco);
        public bool Delete(UtilizadorEndereco utilizadorEndereco);
        public List<int> GetByUtilizadorId(int id);
    }
}
