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
    public class MedidaService : IMedidaService
    {
        private IMedidaRepository _medidaRepository;

        private IItemService _itemService;

        public MedidaService(IMedidaRepository medidaRepository, IItemService itemService)
        {
            _medidaRepository = medidaRepository;
            _itemService=itemService;
        }
        public bool Add(Medida medida)
        {
            ValidarMedida(medida);
            return _medidaRepository.Add(medida);
        }

        public bool Delete(int id)
        {
            List<Item> itens = _itemService.GetByMedidaId(id);

            Medida medidaExiste = _medidaRepository.GetById(id);

            if (itens.Count == 0 && medidaExiste!=null)
            {
                return _medidaRepository.Delete(id);
            }
            else
            {
                return false;
            }
        }

        public bool Delete(Medida medida)
        {
            if(medida is null) 
            {
                throw new ArgumentNullException("A medida não pode ser nula.");
            }

            List<Item> itens = _itemService.GetByMedidaId(medida.MedidaId);

            Medida medidaExiste = _medidaRepository.GetById(medida.MedidaId);

            if (itens.Count == 0 && medidaExiste!=null) 
            {
                return _medidaRepository.Delete(medida);
            }
            else 
            {
                return false;
            }
        }

        public List<Medida> GetAll()
        {
           return _medidaRepository.GetAll();
        }

        public List<Medida> GetAllPesquisa(string texto, int offset, int fetch)
        {
            return _medidaRepository.GetAllPesquisa(texto, offset, fetch);
        }

        public int NumeroTotalDeMedidas(string texto)
        {
            return _medidaRepository.NumeroTotalDeMedidas(texto);
        }

        public Medida GetById(int id)
        {
            return _medidaRepository.GetById(id);
        }

        public bool Update(Medida medida)
        {
            ValidarMedida(medida);

            Medida medidaExiste = _medidaRepository.GetById(medida.MedidaId);

            if (medidaExiste != null) 
            {
                return _medidaRepository.Update(medida);
            }
            else 
            {
                return false;
            }
        }

        private static void ValidarMedida(Medida medida)
        {
            if (medida is null)
            {
                throw new ArgumentNullException("A medida não pode ser nula.");
            }
            else if (string.IsNullOrEmpty(medida.MedidaNome))
            {
                throw new ArgumentNullException("O nome da medida não pode ser vazio.");
            }
            else if (medida.MedidaNome.Length>250)
            {
                throw new ArgumentException("O nome da medida não pode ter mais de 100 caracteres.");
            }
        }
    }
}
