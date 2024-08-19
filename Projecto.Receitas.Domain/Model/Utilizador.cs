using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Projecto.Receitas.Domain.Model
{
    public class Utilizador
    {
        public int UtilizadorId {  get; set; }
        public string PrimeiroNome { get; set; }
        public string? NomesDoMeio { get; set; }
        public string UltimoNome { get; set; }
        public TipoDeGenero? Genero {  get; set; }
        public string? FotoUrl { get; set; }
        public DateTime? DataDeNascimento { get; set; }
        public string? Nacionalidade { get; set; }
        public Permissao Permissoes { get; set; }
        public List<Contacto> Contactos { get; set; }
        public List<Endereco> Enderecos { get; set; }
        public Account Account { get; set; }
        public List<Avaliacao> Avaliacoes { get; set;}
        public List<Comentario> Comentarios { get; set; }
        public List<Favorito> Favoritos { get; set; }
        public List<Receita> Receitas { get; set; }

        public Utilizador(int utilizadorId) 
        {
            UtilizadorId=utilizadorId;
        }

        public Utilizador(string primeiroNome, string? nomesDoMeio, string ultimoNome,
                         TipoDeGenero? genero, string? fotoUrl, DateTime? dataNascimento, string? nacionalidade,
                         Permissao permissoes)
        {
            ValidarNome(primeiroNome);
            PrimeiroNome = primeiroNome;
            ValidarNomesDoMeio(nomesDoMeio);
            NomesDoMeio = nomesDoMeio;
            ValidarNome(ultimoNome);
            UltimoNome =ultimoNome;
            Genero = genero;
            ValidarFotoUrl(fotoUrl);
            FotoUrl = fotoUrl;
            DataDeNascimento = dataNascimento;
            ValidarNacionalidade(nacionalidade);
            Nacionalidade = nacionalidade;
            Permissoes = permissoes;
        }

        public Utilizador(int utilizadorId,string primeiroNome,string? nomesDoMeio,string ultimoNome,
                         TipoDeGenero? genero,string? fotoUrl,DateTime? dataNascimento,string? nacionalidade,
                         Permissao permissoes):this(primeiroNome,nomesDoMeio,ultimoNome,genero,fotoUrl,dataNascimento,nacionalidade,
                                                    permissoes)
        {
            UtilizadorId = utilizadorId;
        }

        private static void ValidarNome(string? nome) 
        {
            if (string.IsNullOrEmpty(nome))
            {
                throw new ArgumentNullException(" A proriedade não pode ser vazia.");
            }
            else if (nome.Length>150)
            {
                throw new ArgumentNullException("A propriedade não pode ter mais de 150 caracteres.");
            }
        }

        private static void ValidarNomesDoMeio(string? nome)
        {
            if (nome!=null && nome.Length>500)
            {
                throw new ArgumentNullException("Os nomes do meio não podem ter mais de 500 caracteres.");
            }
        }

        private static void ValidarFotoUrl(string? fotoUrl) 
        {
            if (fotoUrl!=null && fotoUrl.Length>300)
            {
                throw new ArgumentNullException("A url não pode ter mais de 300 caracteres.");
            }
        }

        private static void ValidarNacionalidade(string? nacionalidade) 
        {
            if(nacionalidade !=null && nacionalidade.Length>250) 
            {
                throw new ArgumentNullException(" A nacionalidade não pode ter mais de 250 caracteres.");
            }
        }
    }
}
