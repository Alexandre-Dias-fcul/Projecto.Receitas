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
    public class UtilizadorService : IUtilizadorService
    {
        private IUtilizadorRepository _utilizadorRepository;

        private IAccountService _accountService;

        private IContactoService _contactoService;

        private IUtilizadorEnderecoService _utilizadorEnderecoService;

        private IReceitaService _receitaService;

        private IFavoritoService _favoritoService;

        private IAvaliacaoService _avaliacaoService;

        private IComentarioService _comentarioService;

        public UtilizadorService(IUtilizadorRepository utilizadorRepository,IAccountService accountService,
               IContactoService contactoService,IUtilizadorEnderecoService utilizadorEnderecoService,
               IReceitaService receitaService, IFavoritoService favoritoService, IAvaliacaoService avaliacaoService,
               IComentarioService comentarioService) 
        {
            _utilizadorRepository=utilizadorRepository;
            _accountService=accountService;
            _contactoService=contactoService;
            _utilizadorEnderecoService = utilizadorEnderecoService;
            _receitaService =receitaService;
            _favoritoService=favoritoService;
            _avaliacaoService=avaliacaoService;
            _comentarioService = comentarioService;
        }

        public bool Add(Utilizador utilizador)
        {
            ValidarUtilizador(utilizador);
            return _utilizadorRepository.Add(utilizador);
        }

        public int AddId(Utilizador utilizador)
        {
            ValidarUtilizador(utilizador);
            return _utilizadorRepository.AddId(utilizador);
        }

        public bool Delete(int id)
        {
            Account account = _accountService.GetByUtilizadorId(id);

            List<Contacto> contactos = _contactoService.GetByUtilizadorId(id);

            List<int> ints = _utilizadorEnderecoService.GetByUtilizadorId(id);

            int NumeroDeReceitas = _receitaService.TotalDeReceitasByUtilizadorId(id);

            List<Avaliacao> avaliacoes = _avaliacaoService.GetByUtilizadorId(id);

            int NumeroDeFavoritos = _favoritoService.NumeroTotalFavoritosByUtilizadorId(id);

            List<Comentario> comentarios = _comentarioService.GetByUtilizadorId(id);

            Utilizador utilizadorExiste = _utilizadorRepository.GetById(id);


            if (account==null && contactos.Count == 0 && ints.Count == 0 && NumeroDeReceitas == 0 
                && avaliacoes.Count == 0 && NumeroDeFavoritos == 0 && comentarios.Count == 0 && utilizadorExiste!=null)
            {
                if (!string.IsNullOrEmpty(utilizadorExiste.FotoUrl))
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/uploads-users", utilizadorExiste.FotoUrl);

                    if (File.Exists(filePath))
                    {
                         File.Delete(filePath);
                    }
                }

                return _utilizadorRepository.Delete(id);
            }
            else 
            {
                return false;
            }
        }

        public bool Delete(Utilizador utilizador)
        {
            if (utilizador is null)
            {
                throw new ArgumentNullException("Utilizador não pode ser nulo.");
            }

            Account account = _accountService.GetByUtilizadorId(utilizador.UtilizadorId);

            List<Contacto> contactos = _contactoService.GetByUtilizadorId(utilizador.UtilizadorId);

            List<int> ints = _utilizadorEnderecoService.GetByUtilizadorId(utilizador.UtilizadorId);

            int NumeroDeReceitas = _receitaService.TotalDeReceitasByUtilizadorId(utilizador.UtilizadorId);

            List<Avaliacao> avaliacoes = _avaliacaoService.GetByUtilizadorId(utilizador.UtilizadorId);

            int NumeroDeFavoritos = _favoritoService.NumeroTotalFavoritosByUtilizadorId(utilizador.UtilizadorId);

            List<Comentario> comentarios = _comentarioService.GetByUtilizadorId(utilizador.UtilizadorId);

            Utilizador utilizadorExiste = _utilizadorRepository.GetById(utilizador.UtilizadorId);

            if (account==null && contactos.Count == 0 && ints.Count == 0 && NumeroDeReceitas == 0
               && avaliacoes.Count == 0 && NumeroDeFavoritos == 0 && comentarios.Count == 0 && utilizadorExiste!=null)
            {
                if (!string.IsNullOrEmpty(utilizadorExiste.FotoUrl))
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/uploads-users", utilizadorExiste.FotoUrl);

                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }
                }

                return _utilizadorRepository.Delete(utilizador);
            }
            else
            {
                return false;
            }
        }

        public List<Utilizador> GetAll()
        {
            return _utilizadorRepository.GetAll();
        }

        public List<Utilizador> GetAllPesquisa(string texto, int offset, int fetch)
        {
            return _utilizadorRepository.GetAllPesquisa(texto,offset,fetch);
        }

        public int NumeroTotalDeUtilizadores(string texto)
        {
            return _utilizadorRepository.NumeroTotalDeUtilizadores(texto);
        }

        public Utilizador GetById(int id)
        {
            return _utilizadorRepository.GetById(id);
        }

        public bool Update(Utilizador utilizador)
        {
            ValidarUtilizador(utilizador);

            Utilizador utilizadorExiste = _utilizadorRepository.GetById(utilizador.UtilizadorId);

            if (utilizadorExiste != null) 
            {
                return _utilizadorRepository.Update(utilizador);
            }
            else 
            {
                return false;
            }
        }

        private static void ValidarUtilizador (Utilizador utilizador) 
        {
            if (utilizador is null)
            {
                throw new ArgumentNullException("Utilizador não pode ser nulo.");
            }
            else if (string.IsNullOrEmpty(utilizador.PrimeiroNome))
            {
                throw new ArgumentNullException("Primeiro nome não pode ser vazio.");
            }
            else if (utilizador.PrimeiroNome.Length>150)
            {
                throw new ArgumentNullException("Primeiro nome não pode ter mais de 150 caracteres.");
            }
            else if (string.IsNullOrEmpty(utilizador.UltimoNome))
            {
                throw new ArgumentNullException("Ultimo nome não pode ser vazio.");
            }
            else if (utilizador.UltimoNome.Length>150)
            {
                throw new ArgumentNullException("Ultimo nome não pode ter mais de 150 caracteres.");
            } 
            else if (utilizador.NomesDoMeio != null && utilizador.NomesDoMeio.Length>500)
            {
                throw new ArgumentNullException("Os nomes do meio não podem ter mais de 500 caracteres.");
            }
            else if (utilizador.FotoUrl!=null && utilizador.FotoUrl.Length>300)
            {
                throw new ArgumentNullException("A url não pode ter mais de 300 caracteres.");
            }
            else if (utilizador.Nacionalidade!=null && utilizador.Nacionalidade.Length>250)
            {
                throw new ArgumentNullException(" A nacionalidade não pode ter mais de 250 caracteres.");
            }
        }
    }
}
