using Project.Receitas.Data.Interfaces;
using Project.Receitas.Data.Repositories;
using Projecto.Receitas.Domain.Model;
using Projecto.Receitas.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projecto.Receitas.Services.Services
{
    public class ItemService : IItemService
    {  
        private IItemRepository _itemRepository;
        public ItemService(IItemRepository itemRepository) 
        { 
            _itemRepository = itemRepository;
      
        }
        public bool Add(Item item)
        {
            ValidarItem(item);

            return _itemRepository.Add(item);
        }

        public bool Delete(int id)
        {    
            Item itemExiste = _itemRepository.GetById(id);

            if (itemExiste != null)
            {
                return _itemRepository.Delete(id);
            }
            else 
            {
                return false;
            }  
        }

        public bool Delete(Item item)
        {
            ValidarItem(item);

            Item itemExiste = _itemRepository.GetById(item.ItemId);

            if (itemExiste != null)
            {
                return _itemRepository.Delete(item);
            }
            else
            {
                return false;
            }
        }

        public List<Item> GetAll()
        {
            return _itemRepository.GetAll();
        }

        public Item GetById(int id)
        {
            return _itemRepository.GetById(id);
        }

        public List<Item> GetByIngredienteId(int id)
        {
            return _itemRepository.GetByIngredienteId(id);
        }

        public List<Item> GetByMedidaId(int id)
        {
            return _itemRepository.GetByMedidaId(id);
        }
        public List<Item> GetByReceitaId(int id)
        {
            return _itemRepository.GetByReceitaId(id);
        }

        public bool Update(Item item)
        {
            ValidarItem(item);

            Item itemExiste = _itemRepository.GetById(item.ItemId);

            if (itemExiste!=null) 
            {
                return _itemRepository.Update(item);
            }
            else 
            {
                return false; 
            }
        }

        private static void ValidarItem(Item item) 
        {
            if(item is null) 
            { 
                throw new ArgumentNullException("O item não pode ser nulo.");
            }
        }
    }
}
