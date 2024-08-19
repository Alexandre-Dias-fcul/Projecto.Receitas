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
    public class FavoritoRepository : IFavoritoRepository
    {
        private static IConfiguration _configuration;

        public FavoritoRepository(IConfiguration configuration) 
        {
            _configuration = configuration;
        }
        public bool Add(Favorito favorito)
        {
            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = " INSERT INTO Favoritos (ReceitaId,UtilizadorId) " +
                                   " VALUES (@receitaId,@utilizadorId); ";

                    cmd.CommandText = query;
                    cmd.Connection = conexao;

                    cmd.Parameters.Add("@receitaId", SqlDbType.Int);
                    cmd.Parameters["@receitaId"].Value = favorito.Receita.ReceitaId;
                    cmd.Parameters.Add("@utilizadorId", SqlDbType.Int);
                    cmd.Parameters["@utilizadorId"].Value = favorito.Utilizador.UtilizadorId;

                    if (conexao.State != ConnectionState.Open)
                        conexao.Open();

                    int affectedRows = cmd.ExecuteNonQuery();

                    return affectedRows == 1;
                }
            }
        }

        public bool Delete(Favorito favorito)
        {
            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = " DELETE FROM Favoritos WHERE ReceitaId=@receitaId AND UtilizadorId=@utilizadorId;";

                    cmd.CommandText = query;
                    cmd.Connection = conexao;

                    cmd.Parameters.Add("@receitaId", SqlDbType.Int);
                    cmd.Parameters["@receitaId"].Value = favorito.Receita.ReceitaId;
                    cmd.Parameters.Add("@utilizadorId", SqlDbType.Int);
                    cmd.Parameters["@utilizadorId"].Value = favorito.Utilizador.UtilizadorId;

                    if (conexao.State != ConnectionState.Open)
                        conexao.Open();

                    int affectedRows = cmd.ExecuteNonQuery();

                    return affectedRows == 1;
                }
            }
        }

        public Favorito GetByPrimaryKey(int idReceita, int idUser)
        {
            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = "SELECT * FROM Favoritos WHERE ReceitaId=@receitaId AND UtilizadorId=@utilizadorId;";

                    cmd.CommandText = query;
                    cmd.Connection = conexao;

                    cmd.Parameters.Add("@receitaId", SqlDbType.Int);
                    cmd.Parameters["@receitaId"].Value = idReceita ;

                    cmd.Parameters.Add("@utilizadorId", SqlDbType.Int);
                    cmd.Parameters["@utilizadorId"].Value = idUser;

                    if (conexao.State != ConnectionState.Open) 
                    {
                        conexao.Open();
                    }    

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {

                        Favorito favoritoARetornar = new Favorito(new Receita(Convert.ToInt32(reader["ReceitaId"])), 
                                                            new Utilizador(Convert.ToInt32(reader["UtilizadorId"])));

                        return favoritoARetornar;
                    }
                    else
                    {
                        return null;
                    }  
                }
            }
        }

        public List<int> GetByUtilizadorId(int top,int id)
        {
            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = $"SELECT TOP {top}  * FROM Favoritos WHERE UtilizadorId=@id";

                    cmd.CommandText = query;
                    cmd.Connection = conexao;

                    cmd.Parameters.Add("@id", SqlDbType.Int);
                    cmd.Parameters["@id"].Value = id;

                    if (conexao.State != ConnectionState.Open)
                        conexao.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<int> receitaIds = new List<int>();

                    while (reader.Read())
                    {
                        
                        int receitaId = Convert.ToInt32(reader["ReceitaId"]);

                        receitaIds.Add(receitaId);
                        
                    }

                    return receitaIds;
                }
            }
        }

        public List<Favorito> GetByUtilizadorId(int id)
        {
            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = $"SELECT * FROM Favoritos WHERE UtilizadorId=@id";

                    cmd.CommandText = query;
                    cmd.Connection = conexao;

                    cmd.Parameters.Add("@id", SqlDbType.Int);
                    cmd.Parameters["@id"].Value = id;

                    if (conexao.State != ConnectionState.Open)
                        conexao.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Favorito> favoritos = new List<Favorito>();

                    while (reader.Read())
                    {

                        Favorito favorito = new Favorito(new Receita(Convert.ToInt32(reader["ReceitaId"])),
                                                               new Utilizador(Convert.ToInt32(reader["UtilizadorId"])));

                        favoritos.Add(favorito);

                    }

                    return favoritos;
                }
            }
        }

        public List<Favorito> GetByReceitaId(int id) 
        {
            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = $"SELECT * FROM Favoritos WHERE ReceitaId=@id";

                    cmd.CommandText = query;
                    cmd.Connection = conexao;

                    cmd.Parameters.Add("@id", SqlDbType.Int);
                    cmd.Parameters["@id"].Value = id;

                    if (conexao.State != ConnectionState.Open)
                        conexao.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Favorito> favoritos = new List<Favorito>();

                    while (reader.Read())
                    {

                        Favorito favorito = new Favorito(new Receita(Convert.ToInt32(reader["ReceitaId"])),
                                                               new Utilizador(Convert.ToInt32(reader["UtilizadorId"])));

                        favoritos.Add(favorito);
                    }

                    return favoritos;
                }
            }
        }
        public int NumeroTotalFavoritosByUtilizadorId(int id)
        {
            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = $"SELECT Count(*) FROM Favoritos WHERE UtilizadorId=@id";

                    cmd.CommandText = query;
                    cmd.Connection = conexao;

                    cmd.Parameters.Add("@id", SqlDbType.Int);
                    cmd.Parameters["@id"].Value = id;

                    if (conexao.State != ConnectionState.Open)
                        conexao.Open();

                    int numeroDeFavoritos = (int)cmd.ExecuteScalar();

                    return numeroDeFavoritos;
                }
            }
        }
    }
}
