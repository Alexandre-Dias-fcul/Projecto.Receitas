using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projecto.Receitas.Domain.Model
{
    public class UtilizadorEndereco
    {
        public Utilizador Utilizador { get; set; }
        public Endereco Endereco { get; set; }

        public UtilizadorEndereco(Utilizador utilizador,Endereco endereco) 
        {
            Utilizador = utilizador;
            Endereco = endereco;
        }
    }
}
