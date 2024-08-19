using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projecto.Receitas.Services.Interfaces
{
    public interface IService<TEntity>
    {
        public List<TEntity> GetAll();
        public TEntity GetById(int id);
        public bool Add(TEntity entity);
        public bool Update(TEntity entity);
        public bool Delete(int id);
        public bool Delete(TEntity entity);
    }
}
