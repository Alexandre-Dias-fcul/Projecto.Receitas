using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Project.Receitas.Data.Interfaces;
using Projecto.Receitas.Domain.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Receitas.Data.Repositories
{
    public class ComentarioRepository : IComentarioRepository
    {
        private static IConfiguration _configuration;

        public ComentarioRepository(IConfiguration configuration) 
        { 
            _configuration = configuration;
        }

        public bool Add(Comentario comentario)
        {
            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = " INSERT INTO Comentarios (Mensagem,DataDaPublicacao,ReceitaId,UtilizadorId) " +
                                   " VALUES (@mensagem,@data,@receitaId,@utilizadorId)";

                    cmd.CommandText = query;
                    cmd.Connection = conexao;

                    cmd.Parameters.Add("@mensagem",SqlDbType.NVarChar);
                    cmd.Parameters["@mensagem"].Value = comentario.Mensagem;
                    cmd.Parameters.Add("@data",SqlDbType.DateTime);
                    cmd.Parameters["@data"].Value = comentario.DataDaPublicacao;
                    cmd.Parameters.Add("@receitaId", SqlDbType.Int);
                    cmd.Parameters["@receitaId"].Value = comentario.Receita.ReceitaId;
                    cmd.Parameters.Add("@utilizadorId", SqlDbType.Int);
                    cmd.Parameters["@utilizadorId"].Value = comentario.Utilizador.UtilizadorId;


                    if (conexao.State != ConnectionState.Open)
                        conexao.Open();

                    int affectedRows = cmd.ExecuteNonQuery();

                    return affectedRows == 1;
                }
            }
        }

        public bool Delete(int id)
        {
            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = "DELETE FROM Comentarios WHERE ComentarioId=@id;";

                    cmd.CommandText = query;
                    cmd.Connection = conexao;

                    cmd.Parameters.Add("@id", SqlDbType.Int);
                    cmd.Parameters["@id"].Value = id; 

                    if (conexao.State != ConnectionState.Open)
                        conexao.Open();

                    int affectedRows = cmd.ExecuteNonQuery();

                    return affectedRows == 1;
                }
            }
        }

        public bool Delete(Comentario comentario)
        {
             return Delete(comentario.ComentarioId);
        }

        public List<Comentario> GetAll()
        {
            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = "SELECT * FROM Comentarios;";

                    cmd.CommandText = query;
                    cmd.Connection = conexao;

                    if (conexao.State != ConnectionState.Open)
                        conexao.Open();

                    List<Comentario> comentarios = new List<Comentario>();

                    SqlDataReader reader = cmd.ExecuteReader();

                    while(reader.Read()) 
                    {
                        Comentario comentario = new Comentario(Convert.ToInt32(reader["ComentarioId"]), reader["Mensagem"].ToString(),
                                               DateTime.Parse(reader["DataDaPublicacao"].ToString()),
                                               new Receita(Convert.ToInt32(reader["ReceitaId"])),
                                                new Utilizador(Convert.ToInt32(reader["UtilizadorId"])));


                        comentarios.Add(comentario);
                    }

                    return comentarios;
                }
            }
        }

        public Comentario GetById(int id)
        {
            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = "SELECT * FROM Comentarios WHERE ComentarioId=@id;";

                    cmd.CommandText = query;
                    cmd.Connection = conexao;

                    cmd.Parameters.Add("@id",SqlDbType.Int);
                    cmd.Parameters["@id"].Value = id;

                    if (conexao.State != ConnectionState.Open)
                        conexao.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    if(reader.Read())
                    {
                        Comentario comentario = new Comentario(Convert.ToInt32(reader["ComentarioId"]), reader["Mensagem"].ToString(),
                                               DateTime.Parse(reader["DataDaPublicacao"].ToString()), 
                                               new Receita(Convert.ToInt32(reader["ReceitaId"])),
                                                new Utilizador(Convert.ToInt32(reader["UtilizadorId"])));

                        return comentario;
                    }
                    else 
                    {
                        return null;
                    }
                }
            }
        }

        public List<Comentario> GetByReceitaId(int id)
        {
            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = "SELECT * FROM Comentarios WHERE ReceitaId=@id;";

                    cmd.CommandText = query;
                    cmd.Connection = conexao;

                    if (conexao.State != ConnectionState.Open)
                        conexao.Open();

                    cmd.Parameters.Add("@id",SqlDbType.Int);
                    cmd.Parameters["@id"].Value = id;

                    List<Comentario> comentarios = new List<Comentario>();

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Comentario comentario = new Comentario(Convert.ToInt32(reader["ComentarioId"]), reader["Mensagem"].ToString(),
                                               DateTime.Parse(reader["DataDaPublicacao"].ToString()),
                                               new Receita(Convert.ToInt32(reader["ReceitaId"])),
                                                new Utilizador(Convert.ToInt32(reader["UtilizadorId"])));


                        comentarios.Add(comentario);
                    }

                    return comentarios;
                }
            }
        }
        public List<Comentario> GetByUtilizadorId(int id)
        {
            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = "SELECT * FROM Comentarios WHERE UtilizadorId=@id;";

                    cmd.CommandText = query;
                    cmd.Connection = conexao;

                    if (conexao.State != ConnectionState.Open)
                        conexao.Open();

                    cmd.Parameters.Add("@id", SqlDbType.Int);
                    cmd.Parameters["@id"].Value = id;

                    List<Comentario> comentarios = new List<Comentario>();

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Comentario comentario = new Comentario(Convert.ToInt32(reader["ComentarioId"]), reader["Mensagem"].ToString(),
                                               DateTime.Parse(reader["DataDaPublicacao"].ToString()),
                                               new Receita(Convert.ToInt32(reader["ReceitaId"])),
                                                new Utilizador(Convert.ToInt32(reader["UtilizadorId"])));


                        comentarios.Add(comentario);
                    }

                    return comentarios;
                }
            }
        }
        public bool Update(Comentario comentario)
        {
            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = " UPDATE Comentarios SET Mensagem=@mensagem, DataDaPublicacao=@data, ReceitaId=@receitaId, " +
                                   " UtilizadorId=@utilizadorId WHERE ComentarioId=@comentarioId;";

                    cmd.CommandText = query;
                    cmd.Connection = conexao;

                    cmd.Parameters.Add("@mensagem", SqlDbType.Int);
                    cmd.Parameters["@mensagem"].Value =comentario.Mensagem;
                    cmd.Parameters.Add("@data", SqlDbType.DateTime);
                    cmd.Parameters["@data"].Value =comentario.DataDaPublicacao;
                    cmd.Parameters.Add("@receitaId", SqlDbType.Int);
                    cmd.Parameters["@receitaId"].Value = comentario.Receita.ReceitaId;
                    cmd.Parameters.Add("@utilizadorId", SqlDbType.Int);
                    cmd.Parameters["@utilizadorId"].Value=comentario.Utilizador.UtilizadorId;
                    cmd.Parameters.Add("@comentarioId", SqlDbType.Int);
                    cmd.Parameters["@comentarioId"].Value =comentario.ComentarioId;


                    if (conexao.State != ConnectionState.Open)
                        conexao.Open();

                    int affectedRows = cmd.ExecuteNonQuery();

                    return affectedRows == 1;
                }
            }
        }
    }
}
