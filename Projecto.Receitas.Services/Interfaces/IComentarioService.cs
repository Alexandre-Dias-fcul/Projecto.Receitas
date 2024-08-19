using Projecto.Receitas.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projecto.Receitas.Services.Interfaces
{
    public interface IComentarioService:IService<Comentario>
    {
        public List<Comentario> GetByReceitaId(int id);
        public List<Comentario> GetByUtilizadorId(int id);
    }
}
