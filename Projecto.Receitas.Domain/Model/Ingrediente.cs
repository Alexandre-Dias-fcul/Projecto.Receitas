using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projecto.Receitas.Domain.Model
{
    public class Ingrediente
    {
        public int IngredienteId { get; set; }
        public string IngredienteNome { get; set; }
        public List<Item> Items { get; set; }

        public Ingrediente(int ingredienteId) 
        {
            IngredienteId = ingredienteId;
        }
        public Ingrediente(string ingredienteNome) 
        { 
            ValidarIngredienteNome(ingredienteNome);

            IngredienteNome = ingredienteNome;
        }

        public Ingrediente(int ingredienteId,string ingredienteNome):this(ingredienteNome)
        { 
            IngredienteId = ingredienteId;
        }

        private static void ValidarIngredienteNome(string ingredienteNome) 
        {
            if(string.IsNullOrEmpty(ingredienteNome)) 
            {
                throw new ArgumentNullException("O nome do ingrediente não pode ser vazio.");
            }
            else if(ingredienteNome.Length>250) 
            {
                throw new ArgumentException("O nome do ingrediente não pode ter mais de 250 caracteres.");
            }
        }
    }
}
