using Projecto.Receitas.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projecto.Receitas.Services.Interfaces
{
    public interface IEnderecoService:IService<Endereco>
    {
        public int AddId(Endereco endereco);

        public List<Endereco> GetAllByUtilizadorId(int id);

        public void AddEndereco(Endereco endereco, int utilizadorId);
    }
}
