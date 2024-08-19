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
    public class UtilizadorEnderecoService : IUtilizadorEnderecoService
    {
        private IUtilizadorEnderecoRepository _utilizadorEnderecoRepository;

        public UtilizadorEnderecoService(IUtilizadorEnderecoRepository utilizadorEnderecoRepository) 
        {
            _utilizadorEnderecoRepository = utilizadorEnderecoRepository;
        }
        public bool Add(UtilizadorEndereco utilizadorEndereco)
        {
            ValidaUtilizadorEndereco(utilizadorEndereco);
            return _utilizadorEnderecoRepository.Add(utilizadorEndereco);
        }

        public bool Delete(UtilizadorEndereco utilizadorEndereco)
        {
            ValidaUtilizadorEndereco(utilizadorEndereco);
            return _utilizadorEnderecoRepository.Delete(utilizadorEndereco);
        }

        public List<int> GetByUtilizadorId(int id)
        {
            return _utilizadorEnderecoRepository.GetByUtilizadorId(id);
        }

        private static void ValidaUtilizadorEndereco(UtilizadorEndereco utilizadorEndereco) 
        {
            if(utilizadorEndereco is null) 
            {
                throw new ArgumentNullException("UtilizadorEndereco não pode ser nulo.");
            }
        }
    }
}
