using Projecto.Receitas.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Receitas.Data.Interfaces
{
    public interface IContactoRepository:IRepository<Contacto>
    {
        public List<Contacto> GetByUtilizadorId(int id);
    }
}
