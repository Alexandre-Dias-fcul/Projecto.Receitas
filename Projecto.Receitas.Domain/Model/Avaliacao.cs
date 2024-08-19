using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projecto.Receitas.Domain.Model
{
    public class Avaliacao
    {
        public int Valor { get; set; }
        public Utilizador Utilizador { get; set; }
        public Receita Receita { get; set; }

        public Avaliacao(int valor,Receita receita,Utilizador utilizador) 
        {
            Valor = valor;
            Receita = receita;
            Utilizador = utilizador;
        }
    }
}
