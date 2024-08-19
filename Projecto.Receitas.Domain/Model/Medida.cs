using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projecto.Receitas.Domain.Model
{
    public class Medida
    {
        public int MedidaId { get; set; }
        public string MedidaNome { get; set; }
        public List<Item> Items { get; set; }

        public Medida(int medidaId) 
        { 
            MedidaId = medidaId;
        }

        public Medida(string medidaNome) 
        {
            ValidarMedidaNome(medidaNome);
            MedidaNome = medidaNome;
        }

        public Medida(int medidaId,string medidaNome):this(medidaNome) 
        {
            ValidarMedidaNome(medidaNome);
            MedidaId = medidaId;
        }

        private static void ValidarMedidaNome(string medidaNome)
        {
            if (string.IsNullOrEmpty(medidaNome))
            {
                throw new ArgumentNullException("A medida não pode ser vazia.");
            }
            else if (medidaNome.Length>50)
            {
                throw new ArgumentException("O nome da medida não pode ter mais de 100 caracteres.");
            }
        }
    }
}
