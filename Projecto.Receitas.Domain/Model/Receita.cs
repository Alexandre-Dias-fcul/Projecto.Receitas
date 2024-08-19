using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projecto.Receitas.Domain.Model
{
    public class Receita
    {
        public int ReceitaId { get; set; }
        public string Titulo { get; set; }
        public string? ImagemUrl {  get; set; }
        public string ModoDePreparacao { get; set; }
        public Tempo TempoDePreparacao { get; set; }
        public TipoDeEstado Estado { get; set; }
        public GrauDeDificuldade Dificuldade {  get; set; }
        public List<Item> Items { get; set; }
        public Categoria Categoria { get; set; }
        public Utilizador Utilizador { get; set; }
        public List<Avaliacao> Avaliacoes { get; set;}
        public List<Comentario> Comentarios { get; set; }
        public List<Favorito> Favoritos { get; set; }

        public Receita(int receitaId) 
        {
            ReceitaId = receitaId;
        }
        public Receita(string titulo,string? imagemUrl,string modoDePreparacao,Tempo tempoDePreparacao,TipoDeEstado estado,
                       GrauDeDificuldade dificuldade,Categoria categoria,Utilizador utilizador)
        {
            ValidarTitulo(titulo);
            Titulo = titulo;
            ValidarImagemUrl(imagemUrl);
            ImagemUrl = imagemUrl;
            ValidarModoDePreparacao(modoDePreparacao);
            ModoDePreparacao = modoDePreparacao;
            TempoDePreparacao = tempoDePreparacao;
            Estado = estado;
            Dificuldade = dificuldade;
            Categoria = categoria;
            Utilizador = utilizador;
        }

        public Receita(int receitaId,string titulo,string? imagemUrl, string modoDePreparacao, Tempo tempoDePreparacao, TipoDeEstado estado,
                       GrauDeDificuldade dificuldade, Categoria categoria,Utilizador utilizador)
               :this(titulo,imagemUrl,modoDePreparacao,tempoDePreparacao,estado,dificuldade,categoria,utilizador)
        { 
            ReceitaId = receitaId;
        }

        private static void ValidarTitulo(string titulo) 
        {
            if(string.IsNullOrEmpty(titulo)) 
            {
                throw new ArgumentNullException("O titulo não pode ser vazio.");
            }
            else if(titulo.Length>200) 
            {
                throw new ArgumentNullException("O titulo não pode ter mais de 200 caracteres.");
            }
        }

        private static void ValidarImagemUrl(string ImagemUrl)
        {
            if (ImagemUrl != null && ImagemUrl.Length>300)
            {
                throw new ArgumentNullException("A url da imagem não pode ter mais de 300 caracteres.");
            }
        }

        private static void ValidarModoDePreparacao(string modoDePreparacao) 
        {
            if (string.IsNullOrEmpty(modoDePreparacao))
            {
                throw new ArgumentNullException("O modo de preparação não pode ser vazio.");
            }
            else if (modoDePreparacao.Length>3500)
            {
                throw new ArgumentNullException("O modo de preparação não pode ter mais de 3500 caracteres.");
            }
        }
    }
}
