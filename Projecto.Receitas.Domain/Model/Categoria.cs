using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projecto.Receitas.Domain.Model
{
    public class Categoria
    {
        public int CategoriaId { get; set; }
        public string CategoriaNome { get; set; }
        public List<Receita> Receitas { get; set; }

        public Categoria(int categoriaId) 
        {
            CategoriaId = categoriaId;
        }
        public Categoria(string categoriaNome)
        {
            ValidarCategoriaNome(categoriaNome);

            CategoriaNome = categoriaNome;
        }
        public Categoria(int categoriaId,string categoriaNome):this(categoriaNome)
        { 
            CategoriaId = categoriaId;
        }

        private static void ValidarCategoriaNome(string categoriaNome) 
        {
            if(string.IsNullOrEmpty(categoriaNome)) 
            {
                throw new ArgumentException("O nome da categoria não pode ser vazio.");
            }
            else if(categoriaNome.Length>250)
            { 
                throw new ArgumentException("O nome da categoria não pode ter mais de 250 caracteres.");
            }
        }
    } 
}
