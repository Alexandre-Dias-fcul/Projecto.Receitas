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
    public class EnderecoRepository : IEnderecoRepository
    {
        private static IConfiguration _configuration;

        public EnderecoRepository(IConfiguration configuration) 
        {
            _configuration = configuration;
        }
        public bool Add(Endereco endereco)
        {
            using(SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"))) 
            {
                using(SqlCommand cmd = new SqlCommand()) 
                {
                    string query = "INSERT INTO Enderecos (Rua,CodigoPostal,Localidade,Pais) " +
                                   " VALUES (@rua,@codigo,@localidade,@pais) ";

                    cmd.CommandText = query;
                    cmd.Connection = conexao;

                    cmd.Parameters.Add("@rua",SqlDbType.NVarChar);
                    cmd.Parameters["@rua"].Value = endereco.Rua;
                    cmd.Parameters.Add("@CodigoPostal", SqlDbType.NVarChar);
                    cmd.Parameters["@CodigoPostal"].Value = endereco.CodigoPostal;
                    cmd.Parameters.Add("@localidade", SqlDbType.NVarChar);
                    cmd.Parameters["@localidade"].Value = endereco.Localidade;
                    cmd.Parameters.Add("@pais", SqlDbType.NVarChar);
                    cmd.Parameters["@pais"].Value = endereco.Pais;

                    if (conexao.State != ConnectionState.Open)
                    {
                        conexao.Open();
                    };
                    
                    int affectRows = cmd.ExecuteNonQuery();

                    return affectRows == 1;
                }
            }
        }

        public int AddId(Endereco endereco)
        {
            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = "INSERT INTO Enderecos (Rua,CodigoPostal,Localidade,Pais) " +
                                   " VALUES (@rua,@codigo,@localidade,@pais); SELECT SCOPE_IDENTITY();";

                    cmd.CommandText = query;
                    cmd.Connection = conexao;

                    cmd.Parameters.Add("@rua", SqlDbType.NVarChar);
                    cmd.Parameters["@rua"].Value = endereco.Rua;
                    cmd.Parameters.Add("@codigo", SqlDbType.NVarChar);
                    cmd.Parameters["@codigo"].Value = endereco.CodigoPostal;
                    cmd.Parameters.Add("@localidade", SqlDbType.NVarChar);
                    cmd.Parameters["@localidade"].Value = endereco.Localidade;
                    cmd.Parameters.Add("@pais", SqlDbType.NVarChar);
                    cmd.Parameters["@pais"].Value = endereco.Pais;

                    if (conexao.State != ConnectionState.Open)
                    {
                        conexao.Open();
                    };

                    int id = Convert.ToInt32(cmd.ExecuteScalar());

                    return id;
                }
            }
        }

        public bool Delete(int id)
        {
            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = "DELETE FROM Enderecos WHERE EnderecoId=@id;";

                    cmd.CommandText = query;
                    cmd.Connection = conexao;

                    cmd.Parameters.Add("@id", SqlDbType.Int);
                    cmd.Parameters["@id"].Value = id;
                    

                    if (conexao.State != ConnectionState.Open)
                    {
                        conexao.Open();
                    };

                    int deletedRows = cmd.ExecuteNonQuery();

                    return deletedRows == 1;
                }
            }
        }

        public bool Delete(Endereco endereco)
        {
            return Delete(endereco.EnderecoId);
        }

        public List<Endereco> GetAll()
        {
            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = "SELECT * FROM Enderecos;";

                    cmd.CommandText = query;
                    cmd.Connection = conexao;

                    if (conexao.State != ConnectionState.Open)
                    {
                        conexao.Open();
                    };

                    List<Endereco> enderecos = new List<Endereco>();

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read()) 
                    {
                        Endereco endereco = new Endereco(Convert.ToInt32(reader["EnderecoId"]), reader["Rua"].ToString(),
                               reader["CodigoPostal"].ToString(), reader["Localidade"].ToString(), reader["Pais"].ToString());

                        enderecos.Add(endereco);
                    }

                    return enderecos;   
                }
            }
        }

        public Endereco GetById(int id)
        {
            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = "SELECT * FROM Enderecos WHERE EnderecoId=@id";

                    cmd.CommandText = query;
                    cmd.Connection = conexao;

                    cmd.Parameters.Add("@id",SqlDbType.Int);
                    cmd.Parameters["@id"].Value = id;

                    if (conexao.State != ConnectionState.Open)
                    {
                        conexao.Open();
                    };

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        Endereco endereco = new Endereco(Convert.ToInt32(reader["EnderecoId"]), reader["Rua"].ToString(),
                               reader["CodigoPostal"].ToString(), reader["Localidade"].ToString(), reader["Pais"].ToString());

                        return endereco;
                    }
                    else 
                    {
                        return null;
                    } 
                }
            }
        }

        public bool Update(Endereco endereco)
        {
            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = " UPDATE Enderecos SET Rua=@rua, CodigoPostal = @codigoPostal, Localidade = @localidade, " +
                                   " Pais = @pais WHERE EnderecoId=@id; ";

                    cmd.CommandText = query;
                    cmd.Connection = conexao;

                    cmd.Parameters.Add("@rua", SqlDbType.NVarChar);
                    cmd.Parameters["@rua"].Value = endereco.Rua;
                    cmd.Parameters.Add("@CodigoPostal", SqlDbType.NVarChar);
                    cmd.Parameters["@CodigoPostal"].Value = endereco.CodigoPostal;
                    cmd.Parameters.Add("@localidade", SqlDbType.NVarChar);
                    cmd.Parameters["@localidade"].Value = endereco.Localidade;
                    cmd.Parameters.Add("@pais", SqlDbType.NVarChar);
                    cmd.Parameters["@pais"].Value = endereco.Pais;
                    cmd.Parameters.Add("@id", SqlDbType.Int);
                    cmd.Parameters["@id"].Value = endereco.EnderecoId;

                    if (conexao.State != ConnectionState.Open)
                    {
                        conexao.Open();
                    };

                    int affectRows = cmd.ExecuteNonQuery();

                    return affectRows == 1;
                }
            }
        }
    }
}
