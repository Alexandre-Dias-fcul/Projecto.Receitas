using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projecto.Receitas.Domain.Model
{
    public class Item
    {
        public int ItemId { get; set; }
        public double Quantidade {  get; set; }
        public Receita Receita { get; set; }
        public Ingrediente Ingrediente { get; set; }
        public Medida Medida { get; set; }
        public Item(int itemId) 
        {
            ItemId = itemId;
        }

        public Item(double quantidade,Medida medida, Receita receita, Ingrediente ingrediente)
        {
            Quantidade=quantidade;
            Medida=medida;
            Receita=receita;
            Ingrediente=ingrediente;
        }

        public Item(int itemId,double quantidade,Medida medida, Receita receita, Ingrediente ingrediente)
            :this(quantidade,medida,receita,ingrediente)
        { 
            ItemId = itemId;
        }
    }
}
