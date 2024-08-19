using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Receitas.Data.Interfaces
{
    public interface IRepository<TEntity>
    {
        public List<TEntity> GetAll();
        public TEntity GetById(int id);
        public bool Add(TEntity entity);
        public bool Update(TEntity entity);
        public bool Delete(int id);
        public bool Delete(TEntity entity);
    }
}
