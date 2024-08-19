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
    public class AvaliacaoService : IAvaliacaoService
    {
        private IAvaliacaoRepository _avaliacaoRepository;

        public AvaliacaoService(IAvaliacaoRepository avaliacaoRepository) 
        { 
            _avaliacaoRepository = avaliacaoRepository;
        }
        public bool Add(Avaliacao avaliacao)
        {
            ValidarAvaliacao(avaliacao);

            Avaliacao AvaliacaoExiste = _avaliacaoRepository.GetByPrimaryKey(avaliacao.Receita.ReceitaId,avaliacao.Utilizador.UtilizadorId);

            if (AvaliacaoExiste == null) 
            {
                return _avaliacaoRepository.Add(avaliacao);
            }
            else 
            {
                return false;
            }
        }

        public bool Delete(Avaliacao avaliacao)
        {
            ValidarAvaliacao(avaliacao);

            Avaliacao AvaliacaoExiste = _avaliacaoRepository.GetByPrimaryKey(avaliacao.Receita.ReceitaId,avaliacao.Utilizador.UtilizadorId);

            if(AvaliacaoExiste != null) 
            {
                return _avaliacaoRepository.Delete(avaliacao);
            }
            else 
            {
                return false;
            }
        }

        public Avaliacao GetByPrimaryKey(int idReceita, int idUser)
        {
            return _avaliacaoRepository.GetByPrimaryKey(idReceita,idUser);
        }

        public List<Avaliacao> GetByReceitaId(int id)
        {
            return _avaliacaoRepository.GetByReceitaId(id);
        }

        public List<Avaliacao> GetByUtilizadorId(int id)
        {
            return _avaliacaoRepository.GetByUtilizadorId(id);
        }

        public double MediaDoValorByReceita(int id)
        {
            return _avaliacaoRepository.MediaDoValorByReceita(id);
        }

        public bool Update(Avaliacao avaliacao)
        {
            ValidarAvaliacao(avaliacao);

            Avaliacao AvaliacaoExiste = _avaliacaoRepository.GetByPrimaryKey(avaliacao.Receita.ReceitaId, avaliacao.Utilizador.UtilizadorId);

            if (AvaliacaoExiste != null) 
            {
                return _avaliacaoRepository.Update(avaliacao);
            }
            else 
            {
                return false;
            }  
        }

        private static void ValidarAvaliacao(Avaliacao avaliacao) 
        {
            if (avaliacao is null) 
            {
                throw new ArgumentNullException("A avaliação não pode ser nula.");
            }
        }

        public List<Receita> GetTop3Raking()
        {
            return _avaliacaoRepository.GetTop3Raking();
        }
    }
}
