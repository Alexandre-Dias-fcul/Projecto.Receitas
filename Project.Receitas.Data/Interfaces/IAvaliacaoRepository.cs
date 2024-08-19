using Projecto.Receitas.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Receitas.Data.Interfaces
{
    public interface IAvaliacaoRepository
    {
        public bool Add(Avaliacao avaliacao);
        public bool Delete(Avaliacao avaliacao);
        public Avaliacao GetByPrimaryKey(int idReceita,int idUser);
        public double MediaDoValorByReceita(int id);
        public List<Avaliacao> GetByReceitaId(int id);
        public List<Avaliacao> GetByUtilizadorId(int id);
        public bool Update(Avaliacao avaliacao);
        public List<Receita> GetTop3Raking();

    }
}
