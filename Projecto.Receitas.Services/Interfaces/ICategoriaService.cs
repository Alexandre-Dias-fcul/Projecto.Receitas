using Projecto.Receitas.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projecto.Receitas.Services.Interfaces
{
    public interface ICategoriaService:IService<Categoria>
    {
        public List<Categoria> GetAllPesquisa(string texto, int offset, int fetch);
        public int NumeroTotalDeCategorias(string texto);
    }
}
