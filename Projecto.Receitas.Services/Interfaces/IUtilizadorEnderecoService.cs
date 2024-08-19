
using Projecto.Receitas.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projecto.Receitas.Services.Interfaces
{
    public interface IUtilizadorEnderecoService
    {
        public bool Add(UtilizadorEndereco utilizadorEndereco);
        public bool Delete(UtilizadorEndereco utilizadorEndereco);
        public List<int> GetByUtilizadorId(int id);
    }
}
