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
    public class IngredienteService : IIngredienteService
    {
        private IIngredienteRepository _ingredienteRepository;

        private IItemService _itemService;

        public IngredienteService(IIngredienteRepository ingredienteRepository, IItemService itemService ) 
        {
            _ingredienteRepository = ingredienteRepository;
            _itemService = itemService;
        }
        public bool Add(Ingrediente ingrediente)
        {
            ValidarIngrediente(ingrediente);

            return _ingredienteRepository.Add(ingrediente);
        }

        public bool Delete(int id)
        {
            List<Item> itens = _itemService.GetByIngredienteId(id);

            Ingrediente ingredienteExiste = _ingredienteRepository.GetById(id);

            if (itens.Count == 0 && ingredienteExiste!=null) 
            {
                return _ingredienteRepository.Delete(id);
            }
            else 
            {
                return false;
            }
            
        }

        public bool Delete(Ingrediente ingrediente)
        {
            if (ingrediente is null) 
            {
                throw new ArgumentNullException("O ingrediente não pode ser nulo.");
            }

            List<Item> itens = _itemService.GetByIngredienteId(ingrediente.IngredienteId);

            Ingrediente ingredienteExiste = _ingredienteRepository.GetById(ingrediente.IngredienteId);

            if (itens.Count == 0 && ingredienteExiste!=null)
            {
                return _ingredienteRepository.Delete(ingrediente);
            }
            else
            {
                return false;
            }
        }

        public List<Ingrediente> GetAll()
        {
            return _ingredienteRepository.GetAll();
        }

        public int NumeroTotalDeIngredientes(string texto)
        {
            return _ingredienteRepository.NumeroTotalDeIngredientes(texto);
        }

        public List<Ingrediente> GetAllPesquisa(string texto, int offset, int fetch)
        {
            return _ingredienteRepository.GetAllPesquisa(texto,offset,fetch);
        }

        public Ingrediente GetById(int id)
        {
            return _ingredienteRepository.GetById(id);
        }

        public bool Update(Ingrediente ingrediente)
        {
            ValidarIngrediente(ingrediente);

            Ingrediente ingredienteExiste = _ingredienteRepository.GetById(ingrediente.IngredienteId);

            if(ingredienteExiste !=null) 
            {
                return _ingredienteRepository.Update(ingrediente);
            }
            else 
            {
                return false;
            }
        }

        private static void ValidarIngrediente(Ingrediente ingrediente) 
        {
            if(ingrediente is null) 
            {
                throw new ArgumentNullException("O ingrediente não pode ser nulo.");
            }
            else if(string.IsNullOrEmpty(ingrediente.IngredienteNome)) 
            {
                throw new ArgumentNullException("O nome do ingrediente não pode ser vazio.");
            }
            else if(ingrediente.IngredienteNome.Length>250) 
            {
                throw new ArgumentException("O nome do ingrediente não pode ter mais de 250 caracteres.");
            }
        }
    }
}
