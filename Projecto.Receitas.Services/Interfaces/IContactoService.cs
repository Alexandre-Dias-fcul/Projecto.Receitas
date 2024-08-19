using Projecto.Receitas.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projecto.Receitas.Services.Interfaces
{
    public interface IContactoService:IService<Contacto>
    {
        public List<Contacto> GetByUtilizadorId(int id);
    }
}
