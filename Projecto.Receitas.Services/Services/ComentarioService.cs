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
    public class ComentarioService : IComentarioService
    {
        private IComentarioRepository _comentarioRepository;

        public ComentarioService(IComentarioRepository comentarioRepository) 
        {
            _comentarioRepository = comentarioRepository;
        }
        public bool Add(Comentario comentario)
        {
            ValidarComentario(comentario);

            return _comentarioRepository.Add(comentario);
        }

        public bool Delete(int id)
        {
            Comentario comentarioExiste = _comentarioRepository.GetById(id);

            if(comentarioExiste!=null)
            {
                return _comentarioRepository.Delete(id);
            }
            else
            {
                return false;
            }
        }

        public bool Delete(Comentario comentario)
        {
            if(comentario is null) 
            {
                throw new ArgumentNullException("O comentário não pode ser nulo.");
            }

            Comentario comentarioExiste = _comentarioRepository.GetById(comentario.ComentarioId);

            if (comentarioExiste!=null)
            {
                return _comentarioRepository.Delete(comentario);
            }
            else
            {
                return false;
            } 
        }

        public List<Comentario> GetAll()
        {
            return _comentarioRepository.GetAll();
        }

        public Comentario GetById(int id)
        {
            return _comentarioRepository.GetById(id);
        }

        public List<Comentario> GetByReceitaId(int id)
        {
            return _comentarioRepository.GetByReceitaId(id);
        }

        public List<Comentario> GetByUtilizadorId(int id)
        {
            return _comentarioRepository.GetByUtilizadorId(id);
        }

        public bool Update(Comentario comentario)
        {
            ValidarComentario(comentario);

            Comentario comentarioExiste = _comentarioRepository.GetById(comentario.ComentarioId);

            if(comentarioExiste!=null) 
            {
                return _comentarioRepository.Update(comentario);
            }
            else 
            {
                return false;
            }   
        }

        private static void ValidarComentario(Comentario comentario) 
        {
            if (comentario is null) 
            {
                throw new ArgumentNullException("O comentário não pode ser nulo.");
            }
            else if(string.IsNullOrEmpty(comentario.Mensagem)) 
            {
                throw new ArgumentException("A mensagem não pode ser vazia.");
            }
            else if(comentario.Mensagem.Length>3500) 
            {
                throw new ArgumentException("A mensagem não pode ter mais de 3500 caracteres.");
            }
        }
    }
}
