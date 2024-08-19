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
    public class MedidaRepository : IMedidaRepository
    {
        private static IConfiguration _configuration;

        public MedidaRepository(IConfiguration configuration) 
        {
            _configuration = configuration;
        }
        public bool Add(Medida medida)
        {
            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = "INSERT INTO Medidas (MedidaNome) Values (@nome)";

                    cmd.CommandText = query;
                    cmd.Connection = conexao;

                    cmd.Parameters.Add("@nome", SqlDbType.NVarChar);
                    cmd.Parameters["@nome"].Value = medida.MedidaNome;

                    if (conexao.State != ConnectionState.Open)
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
                    string query = "DELETE FROM Medidas WHERE MedidaId=@id;";

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

        public bool Delete(Medida medida)
        {
            return Delete(medida.MedidaId);
        }

        public List<Medida> GetAll()
        {
            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = "SELECT * FROM Medidas;";

                    cmd.CommandText = query;
                    cmd.Connection = conexao;

                    if (conexao.State != ConnectionState.Open)
                    {
                        conexao.Open();
                    }

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Medida> medidas = new List<Medida>();

                    while (reader.Read())
                    {
                        Medida medida= new Medida(Convert.ToInt32(reader["MedidaId"]),
                            reader["MedidaNome"].ToString());

                        medidas.Add(medida);
                    }

                    return medidas;
                }
            }
        }

        public List<Medida> GetAllPesquisa(string texto, int offset, int fetch)
        {
            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = $"SELECT * FROM Medidas WHERE MedidaNome LIKE '%{texto}' OR MedidaNome LIKE '%{texto}%'" +
                                   $" OR MedidaNome LIKE '{texto}%' ORDER BY MedidaNome " +
                                   $" OffSET {offset} ROWS FETCH FIRST {fetch} ROWS ONLY;";

                    cmd.CommandText = query;
                    cmd.Connection = conexao;

                    if (conexao.State != ConnectionState.Open)
                    {
                        conexao.Open();
                    }

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Medida> medidas = new List<Medida>();

                    while (reader.Read())
                    {
                        Medida medida = new Medida(Convert.ToInt32(reader["MedidaId"]), reader["MedidaNome"].ToString());

                        medidas.Add(medida);
                    }

                    return medidas;
                }
            }
        }

        public int NumeroTotalDeMedidas(string texto)
        {
            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = $"SELECT Count(*) FROM Medidas WHERE MedidaNome LIKE '%{texto}' OR " +
                                   $" MedidaNome LIKE '%{texto}%' OR MedidaNome LIKE '{texto}%'";

                    cmd.CommandText = query;
                    cmd.Connection = conexao;

                    if (conexao.State != ConnectionState.Open)
                        conexao.Open();

                    int numeroDeMedidas = (int)cmd.ExecuteScalar();

                    return numeroDeMedidas;
                }
            }
        }

        public Medida GetById(int id)
        {
            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = "SELECT * FROM Medidas Where MedidaId=@id";

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
                        Medida medida = new Medida(Convert.ToInt32(reader["MedidaId"]),
                            reader["MedidaNome"].ToString());

                        return medida;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public bool Update(Medida medida)
        {
            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = "UPDATE Medidas SET MedidaNome=@nome WHERE MedidaId=@id";

                    cmd.CommandText = query;
                    cmd.Connection = conexao;

                    cmd.Parameters.Add("@nome", SqlDbType.NVarChar);
                    cmd.Parameters["@nome"].Value = medida.MedidaNome;
                    cmd.Parameters.Add("@id", SqlDbType.Int);
                    cmd.Parameters["@id"].Value = medida.MedidaId;

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
