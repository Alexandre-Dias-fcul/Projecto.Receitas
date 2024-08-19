using Projecto.Receitas.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projecto.Receitas.Services.Interfaces
{
    public interface IFavoritoService
    {
        public bool Add(Favorito favorito);
        public bool Delete(Favorito favorito);
        public List<Favorito> GetByReceitaId(int id);
        public List<int> GetByUtilizadorId(int top, int id);
        public Favorito GetByPrimaryKey(int idReceita, int idUser);
        public int NumeroTotalFavoritosByUtilizadorId(int id);
        public List<Favorito> GetByUtilizadorId(int id);
    }
}
