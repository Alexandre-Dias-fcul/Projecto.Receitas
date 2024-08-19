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
    public class ReceitaService : IReceitaService
    {
        private IReceitaRepository _receitaRepository;

        private IFavoritoService _favoritoService;

        private IAvaliacaoService _avaliacaoService;

        private IComentarioService _comentarioService;

        private IItemService _itemService;

        public ReceitaService(IReceitaRepository receitaRepository,IFavoritoService favoritoService,
            IAvaliacaoService avaliacaoService, IComentarioService comentarioService, IItemService itemService)
        {
            _receitaRepository = receitaRepository;
            _favoritoService = favoritoService;
            _avaliacaoService = avaliacaoService;
            _comentarioService = comentarioService;
            _itemService = itemService;
        }

        public bool Add(Receita receita)
        {
            ValidarReceita(receita);

            return _receitaRepository.Add(receita);
        }

        public int AddId(Receita receita)
        {
            ValidarReceita(receita);

            return _receitaRepository.AddId(receita);
        }

        public bool Delete(int id)
        {
            List<Avaliacao> avaliacao = _avaliacaoService.GetByReceitaId(id);

            List<Favorito> favorito = _favoritoService.GetByReceitaId(id);

            List<Comentario> comentario = _comentarioService.GetByReceitaId(id);

            List<Item> Itens = _itemService.GetByReceitaId(id);

            Receita receitaExiste = _receitaRepository.GetById(id);

            if (avaliacao.Count == 0 && favorito.Count == 0 && comentario.Count == 0 && Itens.Count == 0 && receitaExiste != null)
            {
                if (!string.IsNullOrEmpty(receitaExiste.ImagemUrl))
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/uploads-receitas", receitaExiste.ImagemUrl);

                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }
                }

                return _receitaRepository.Delete(id);
            }
            else
            {
                return false;
            }
        }

        public bool Delete(Receita receita)
        {
            if(receita is null) 
            {
                throw new ArgumentNullException("A receita não pode ser nula.");
            }

            List<Avaliacao> avaliacao = _avaliacaoService.GetByReceitaId(receita.ReceitaId);

            List<Favorito> favorito = _favoritoService.GetByReceitaId(receita.ReceitaId);

            List<Comentario> comentario = _comentarioService.GetByReceitaId(receita.ReceitaId);

            List<Item> Itens = _itemService.GetByReceitaId(receita.ReceitaId);

            Receita receitaExiste = _receitaRepository.GetById(receita.ReceitaId);

            if (avaliacao.Count == 0 && favorito.Count == 0 && comentario.Count == 0 && Itens.Count == 0 && receitaExiste != null)
            {
                if (!string.IsNullOrEmpty(receitaExiste.ImagemUrl))
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/uploads-receitas", receitaExiste.ImagemUrl);

                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }
                }

                return _receitaRepository.Delete(receita);
            }
            else
            {
                return false;
            }
        }

        public List<Receita> GetAll()
        {
            return _receitaRepository.GetAll();
        }

        public List<Receita> GetAllPesquisa(string texto, int offset, int fetch)
        {
            return _receitaRepository.GetAllPesquisa(texto,offset,fetch);
        }

        public int NumeroTotalDeReceitasPesquisa(string texto)
        {
            return _receitaRepository.NumeroTotalDeReceitasPesquisa(texto);
        }

        public List<Receita> GetByCategoriaId(int id)
        {
            return _receitaRepository.GetByCategoriaId(id);
        }

        public Receita GetById(int id)
        {
            return _receitaRepository.GetById(id);
        }

        public List<Receita> GetReceitas(int top, string texto)
        {
            if(string.IsNullOrEmpty(texto)) 
            {
                return GetReceitasValidas(top);
            }
            else 
            {
                return GetReceitasValidasTituloPesquisa(top, texto);
            }
        }

        public List<Receita> GetReceitasValidas(int top)
        {
            return _receitaRepository.GetReceitasValidas(top);
        }

        public List<Receita> GetReceitasValidasTituloPesquisa(int top, string texto)
        {
            return _receitaRepository.GetReceitasValidasTituloPesquisa(top, texto);
        }
        public int NumeroDeReceitasValidas(string texto)
        {
            return _receitaRepository.NumeroDeReceitasValidas(texto);
        }

        public List<Receita> GetReceitasByUtilizadorId(int top, int id)
        {
            return _receitaRepository.GetReceitasByUtilizadorId(top,id);
        }
        public int TotalDeReceitasByUtilizadorId(int id)
        {
            return _receitaRepository.TotalDeReceitasByUtilizadorId(id);
        }
        public bool Update(Receita receita)
        {
            ValidarReceita(receita);

            Receita receitaExiste = _receitaRepository.GetById(receita.ReceitaId);

            if(receitaExiste != null) 
            {
                return _receitaRepository.Update(receita);
            }
            else 
            {
                return false;
            }
        }

        private static void ValidarReceita(Receita receita)
        {
            if(receita is null) 
            {
                throw new ArgumentNullException("A receita não pode ser nula.");
            }
            else if(string.IsNullOrEmpty(receita.Titulo)) 
            {
                throw new ArgumentNullException("O titulo não pode ser vazio.");
            }
            else if(receita.Titulo.Length>200) 
            {
                throw new ArgumentNullException("O titulo não pode ter mais de 200 caracteres.");
            }
            else if(receita.ImagemUrl != null && receita.ImagemUrl.Length>300) 
            {
                throw new ArgumentNullException("A url da imagem não pode ter mais de 300 caracteres.");
            }
            else if (string.IsNullOrEmpty(receita.ModoDePreparacao))
            {
                throw new ArgumentNullException("O modo de preparação não pode ser vazio.");
            }
            else if (receita.ModoDePreparacao.Length>3500) 
            {
                throw new ArgumentNullException("O modo de preparação não pode ter mais de 3500 caracteres.");
            }
        }
    }
}
