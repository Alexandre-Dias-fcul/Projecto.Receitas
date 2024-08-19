﻿using Projecto.Receitas.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Receitas.Data.Interfaces
{
    public interface IComentarioRepository:IRepository<Comentario>
    {
        public List<Comentario> GetByReceitaId(int id);
        public List<Comentario> GetByUtilizadorId(int id);
    }
}
