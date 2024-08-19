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
    public class CategoriaService : ICategoriaService
    {
        private ICategoriaRepository _categoriaRepository;

        private IReceitaService _receitaService;

        public CategoriaService(ICategoriaRepository categoriaRepository, IReceitaService receitaService) 
        {
            _categoriaRepository = categoriaRepository;
            _receitaService = receitaService;
        }
        public bool Add(Categoria categoria)
        {
            ValidarCategoria(categoria);

            return _categoriaRepository.Add(categoria);
        }

        public bool Delete(int id)
        {
            List<Receita> receitas= _receitaService.GetByCategoriaId(id);

            Categoria categoriaExiste = _categoriaRepository.GetById(id);

            if (receitas.Count==0 && categoriaExiste != null)
            {
                return _categoriaRepository.Delete(id);
            }
            else 
            {
                return false;
            }
        }

        public bool Delete(Categoria categoria)
        {
            if(categoria is null) 
            {
                throw new ArgumentNullException("A categoria não pode ser nula.");
            }

            List<Receita> receitas = _receitaService.GetByCategoriaId(categoria.CategoriaId);

            Categoria categoriaExiste = _categoriaRepository.GetById(categoria.CategoriaId);

            if (receitas.Count==0 && categoriaExiste != null)
            {
                return _categoriaRepository.Delete(categoria);
            }
            else
            {
                return false;
            }
        }

        public List<Categoria> GetAll()
        {
            return _categoriaRepository.GetAll();
        }

        public List<Categoria> GetAllPesquisa(string texto, int offset, int fetch)
        {
            return _categoriaRepository.GetAllPesquisa(texto,offset,fetch);
        }

        public int NumeroTotalDeCategorias(string texto)
        {
            return _categoriaRepository.NumeroTotalDeCategorias(texto);
        }

        public Categoria GetById(int id)
        {
            return _categoriaRepository.GetById(id);
        }

        public bool Update(Categoria categoria)
        {
            ValidarCategoria(categoria);

            Categoria categoriaExiste = _categoriaRepository.GetById(categoria.CategoriaId);

            if(categoriaExiste != null) 
            {
                return _categoriaRepository.Update(categoria);
            }
            else 
            {
                return false;
            }
        }

        private static void ValidarCategoria(Categoria categoria) 
        {
            if(categoria is null) 
            {
                throw new ArgumentNullException("A categoria não pode ser nula.");
            }
            else if(string.IsNullOrEmpty(categoria.CategoriaNome)) 
            {
                throw new ArgumentNullException("A categoria não pode ser vazia.");
            }
            else if(categoria.CategoriaNome.Length>250) 
            {
                throw new ArgumentException("A categoria não pode ter mais de 250 caracteres.");
            }
        }
    }
}
