using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projecto.Receitas.Domain.Model
{
    public class Favorito
    {
        public Receita Receita { get; set; }
        public Utilizador Utilizador { get; set; }

        public Favorito(Receita receita,Utilizador utilizador) 
        { 
            Receita = receita;
            Utilizador = utilizador;
        }
    }
}
