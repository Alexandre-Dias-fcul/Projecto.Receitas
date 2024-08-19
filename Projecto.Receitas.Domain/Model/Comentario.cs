using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projecto.Receitas.Domain.Model
{
    public class Comentario
    {
        public int ComentarioId { get; set; }
        public string Mensagem { get; set; }
        public DateTime DataDaPublicacao { get; set; }
        public Receita Receita { get; set; }
        public Utilizador Utilizador { get; set; }

        public Comentario(string mensagem,DateTime dataDaPublicacao,Receita receita,Utilizador utilizador) 
        {
            ValidarMensagem(mensagem);
            Mensagem = mensagem;
            DataDaPublicacao = dataDaPublicacao;
            Receita = receita;
            Utilizador = utilizador;
        }

        public Comentario(int comentarioId,string mensagem, DateTime dataDaPublicacao, Receita receita, Utilizador utilizador)
            :this(mensagem,dataDaPublicacao,receita,utilizador)
        {
            ComentarioId = comentarioId;
        }

        private static void ValidarMensagem(string mensagem) 
        {
            if(string.IsNullOrEmpty(mensagem)) 
            {
                throw new ArgumentException("A mensagem não pode ser vazia.");
            }
            else if(mensagem.Length>3500) 
            {
                throw new ArgumentException("A mensagem não pode ter mais de 3500 caracteres.");
            }
        }
    }
}
