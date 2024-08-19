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
    public class FavoritoService : IFavoritoService
    {
        private IFavoritoRepository _favoritoRepository;

        public FavoritoService(IFavoritoRepository favoritoRepository) 
        { 
            _favoritoRepository = favoritoRepository;
        }
        public bool Add(Favorito favorito)
        {
            ValidarFavorito(favorito);

            Favorito favoritoExiste = _favoritoRepository.GetByPrimaryKey(favorito.Receita.ReceitaId,favorito.Utilizador.UtilizadorId);

            if(favoritoExiste == null) 
            {
                return _favoritoRepository.Add(favorito);
            }
            else 
            {
                return false;
            }
            
        }

        public bool Delete(Favorito favorito)
        {
            ValidarFavorito(favorito);

            Favorito favoritoExiste = _favoritoRepository.GetByPrimaryKey(favorito.Receita.ReceitaId, favorito.Utilizador.UtilizadorId);

            if (favoritoExiste != null) 
            {
                return _favoritoRepository.Delete(favorito);
            }
            else 
            {
                return false;
            }
        }

        public List<int> GetByUtilizadorId(int top, int id)
        {
            return _favoritoRepository.GetByUtilizadorId(top,id);
        }

        public List<Favorito> GetByUtilizadorId(int id)
        {
            return _favoritoRepository.GetByUtilizadorId(id);
        }
        public List<Favorito> GetByReceitaId(int id)
        {
            return _favoritoRepository.GetByReceitaId(id);
        }

        public Favorito GetByPrimaryKey(int idReceita, int idUser)
        {
            return _favoritoRepository.GetByPrimaryKey(idReceita,idUser);
        }

        public int NumeroTotalFavoritosByUtilizadorId(int id)
        {
            return _favoritoRepository.NumeroTotalFavoritosByUtilizadorId(id);
        }

        private static void ValidarFavorito(Favorito favorito) 
        {
            if(favorito is null) 
            {
                throw new ArgumentNullException("O favorito não pode ser nulo.");
            }
        }
    }
}
