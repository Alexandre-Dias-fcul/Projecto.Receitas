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
    public class EnderecoService : IEnderecoService
    {
        private IEnderecoRepository _enderecoRepository;

        private IUtilizadorEnderecoService _utilizadorEnderecoService;

        private IUtilizadorService _utilizadorService;

        public EnderecoService(IEnderecoRepository enderecoRepository,
                               IUtilizadorEnderecoService utilizadorEnderecoService,IUtilizadorService utilizadorService)
        {
            _enderecoRepository = enderecoRepository;
            _utilizadorEnderecoService = utilizadorEnderecoService;
            _utilizadorService=utilizadorService;
        }

        public bool Add(Endereco endereco)
        {
            ValidarEndereco(endereco);

            return _enderecoRepository.Add(endereco);
        }

        public void AddEndereco(Endereco endereco, int utilizadorId)
        {
            int id = _enderecoRepository.AddId(endereco);

            UtilizadorEndereco utilizadorEndereco = new UtilizadorEndereco(_utilizadorService.GetById(utilizadorId),
                                                   _enderecoRepository.GetById(id));

            _utilizadorEnderecoService.Add(utilizadorEndereco);

        }

        public int AddId(Endereco endereco)
        {
            ValidarEndereco(endereco);

            return _enderecoRepository.AddId(endereco);
        }

        public bool Delete(int id)
        {
            Endereco enderecoExiste = _enderecoRepository.GetById(id);

            if (enderecoExiste!=null) 
            {
                return _enderecoRepository.Delete(id);
            }
            else 
            {
                return false;
            }
        }

        public bool Delete(Endereco endereco)
        {
            if (endereco is null) 
            {
                throw new ArgumentNullException("Endereço não pode ser nulo.");
            }

            Endereco enderecoExiste = _enderecoRepository.GetById(endereco.EnderecoId);

            if (enderecoExiste!=null)
            {
                return _enderecoRepository.Delete(endereco);
            }
            else
            {
                return false;
            }
        }

        public List<Endereco> GetAll()
        {
            return _enderecoRepository.GetAll();
        }

        public List<Endereco> GetAllByUtilizadorId(int id)
        {
            List<int> enderecosIds = _utilizadorEnderecoService.GetByUtilizadorId(id);

            List<Endereco> Enderecos = new List<Endereco>();

            foreach (int inteiro in enderecosIds)
            {
                Endereco endereco = _enderecoRepository.GetById(inteiro);

                if (endereco != null)
                {
                    Enderecos.Add(endereco);
                }
            }

            return Enderecos;
        }

        public Endereco GetById(int id)
        {
            return _enderecoRepository.GetById(id);
        }

        public bool Update(Endereco endereco)
        {
            ValidarEndereco(endereco);

            Endereco enderecoExiste = _enderecoRepository.GetById(endereco.EnderecoId);

            if (enderecoExiste != null) 
            {
                return _enderecoRepository.Update(endereco);
            }
            else 
            {
                 return false;
            } 
        }

        private static void ValidarEndereco(Endereco endereco) 
        {
            if (endereco is null) 
            {
                throw new ArgumentNullException("Endereço não pode ser nulo.");
            }
            else if (string.IsNullOrEmpty(endereco.Rua)) 
            {
                throw new ArgumentNullException("A rua não pode ser vazia.");
            } 
            else if (string.IsNullOrEmpty(endereco.CodigoPostal)) 
            {
                throw new ArgumentNullException("O código postal não pode ser vazio.");
            }
            else if (string.IsNullOrEmpty(endereco.Localidade)) 
            {
                throw new ArgumentNullException("A localidade não pode ser vazia.");
            }
            else if (string.IsNullOrEmpty(endereco.Pais))
            {
                throw new ArgumentNullException("O país não pode ser vazio.");
            }
            else if (endereco.Rua.Length>300) 
            {
                throw new ArgumentNullException("A rua não pode ter mais de 300 caracteres.");
            }
            else if (endereco.CodigoPostal.Length>50) 
            {
                throw new ArgumentNullException("O código postal não pode ter mais de 50 caracteres.");
            }
            else if (endereco.Localidade.Length>150) 
            {
                throw new ArgumentNullException("A localidade não pode ter mais de 150 caracteres.");
            }
            else if (endereco.Pais.Length>150) 
            {
                throw new ArgumentNullException("O país não pode ter mais de 150 caracteres.");
            }
        }
    }
}
