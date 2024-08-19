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
    public class IngredienteRepository : IIngredienteRepository
    {
        private static IConfiguration _configuration;

        public IngredienteRepository(IConfiguration configuration) 
        {
            _configuration = configuration;
        }
        public bool Add(Ingrediente ingrediente)
        {
            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"))) 
            {
                using (SqlCommand cmd = new SqlCommand()) 
                {
                    string query = "INSERT INTO Ingredientes (IngredienteNome) VALUES (@nome)";

                    cmd.CommandText = query;
                    cmd.Connection = conexao;

                    cmd.Parameters.Add("@nome",SqlDbType.NVarChar);
                    cmd.Parameters["@nome"].Value = ingrediente.IngredienteNome;

                    if (conexao.State != ConnectionState.Open ) 
                    {
                        conexao.Open();
                    }

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
                    string query = "DELETE FROM Ingredientes WHERE IngredienteId=@id;";

                    cmd.CommandText = query;
                    cmd.Connection = conexao;

                    cmd.Parameters.Add("@id", SqlDbType.Int);
                    cmd.Parameters["@id"].Value = id;

                    if (conexao.State != ConnectionState.Open)
                    {
                        conexao.Open();
                    }

                    int affectedRows = cmd.ExecuteNonQuery();

                    return affectedRows == 1;
                }
            }
        }

        public bool Delete(Ingrediente ingrediente)
        {
            return Delete(ingrediente.IngredienteId);
        }

        public List<Ingrediente> GetAll()
        {
            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = "SELECT * FROM Ingredientes;";

                    cmd.CommandText = query;
                    cmd.Connection = conexao;

                    if (conexao.State != ConnectionState.Open)
                    {
                        conexao.Open();
                    }
                    
                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Ingrediente> ingredientes = new List<Ingrediente>();

                    while (reader.Read()) 
                    {
                        Ingrediente ingrediente = new Ingrediente(Convert.ToInt32(reader["IngredienteId"]),
                            reader["IngredienteNome"].ToString());

                         ingredientes.Add(ingrediente);
                    }
            
                    return ingredientes;
                }
            }
        }

        public int NumeroTotalDeIngredientes(string texto) 
        {
            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = $"SELECT Count(*) FROM Ingredientes WHERE IngredienteNome LIKE '%{texto}' OR " +
                                   $" IngredienteNome LIKE '%{texto}%' OR IngredienteNome LIKE '{texto}%'";

                    cmd.CommandText = query;
                    cmd.Connection = conexao;

                    if (conexao.State != ConnectionState.Open)
                        conexao.Open();

                    int numeroDeIngredientes = (int)cmd.ExecuteScalar();

                    return numeroDeIngredientes;
                }
            }
        }

        public List<Ingrediente> GetAllPesquisa(string texto, int offset, int fetch) 
        {
            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = $"SELECT * FROM Ingredientes WHERE IngredienteNome LIKE '%{texto}' OR IngredienteNome LIKE '%{texto}%'" +
                                   $" OR IngredienteNome LIKE '{texto}%' ORDER BY IngredienteNome " +
                                   $" OffSET {offset} ROWS FETCH FIRST {fetch} ROWS ONLY;";

                    cmd.CommandText = query;
                    cmd.Connection = conexao;

                    if (conexao.State != ConnectionState.Open)
                    {
                        conexao.Open();
                    }

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Ingrediente> ingredientes = new List<Ingrediente>();

                    while (reader.Read())
                    {
                        Ingrediente ingrediente = new Ingrediente(Convert.ToInt32(reader["IngredienteId"]),
                            reader["IngredienteNome"].ToString());

                        ingredientes.Add(ingrediente);
                    }

                    return ingredientes;
                }
            }
        }

        public Ingrediente GetById(int id)
        {
            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = "SELECT * FROM Ingredientes Where IngredienteId=@id";

                    cmd.CommandText = query;
                    cmd.Connection = conexao;

                    cmd.Parameters.Add("@id", SqlDbType.Int);
                    cmd.Parameters["@id"].Value = id;

                    if (conexao.State != ConnectionState.Open)
                    {
                        conexao.Open();
                    }

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        Ingrediente ingrediente = new Ingrediente(Convert.ToInt32(reader["IngredienteId"]),
                           reader["IngredienteNome"].ToString());

                        return ingrediente;
                    }
                    else 
                    {
                        return null;
                    }
                }
            }
        }

        public bool Update(Ingrediente ingrediente)
        {
            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = "UPDATE Ingredientes SET IngredienteNome=@nome WHERE IngredienteId=@id";

                    cmd.CommandText = query;
                    cmd.Connection = conexao;

                    cmd.Parameters.Add("@nome", SqlDbType.NVarChar);
                    cmd.Parameters["@nome"].Value = ingrediente.IngredienteNome;
                    cmd.Parameters.Add("@id", SqlDbType.Int);
                    cmd.Parameters["@id"].Value = ingrediente.IngredienteId;

                    if (conexao.State != ConnectionState.Open)
                    {
                        conexao.Open();
                    }

                    int affectedRows = cmd.ExecuteNonQuery();

                    return affectedRows == 1;
                }
            }
        }
    }
}
