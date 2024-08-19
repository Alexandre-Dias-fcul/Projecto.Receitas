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
    public class UtilizadorEnderecoRepository : IUtilizadorEnderecoRepository
    {
        private static IConfiguration _counfiguration;

        public UtilizadorEnderecoRepository(IConfiguration configuration) 
        {
            _counfiguration = configuration;
        }

        public bool Add(UtilizadorEndereco utilizadorEndereco)
        {
            using (SqlConnection conexao = new SqlConnection(_counfiguration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = "INSERT INTO UtilizadoresEnderecos (UtilizadorId,EnderecoId) " +
                                   " VALUES (@utilizadorId,@enderecoId); ";

                    cmd.CommandText = query;
                    cmd.Connection = conexao;

                    cmd.Parameters.Add("@utilizadorId", SqlDbType.Int);
                    cmd.Parameters["@utilizadorId"].Value =utilizadorEndereco.Utilizador.UtilizadorId ;
                    cmd.Parameters.Add("@enderecoId", SqlDbType.Int);
                    cmd.Parameters["@enderecoId"].Value =utilizadorEndereco.Endereco.EnderecoId;

                    if (conexao.State != ConnectionState.Open)
                        conexao.Open();

                    int affectedRows = cmd.ExecuteNonQuery();

                    return affectedRows == 1;
                }
            }
        }

        public bool Delete(UtilizadorEndereco UtilizadorEndereco)
        {
            using (SqlConnection conexao = new SqlConnection(_counfiguration.GetConnectionString("DefaultConnection"))) 
            {
                using (SqlCommand cmd = new SqlCommand()) 
                {
                    string query = "DELETE FROM UtilizadoresEnderecos WHERE UtilizadorId=@utilizadorId AND EnderecoId=@enderecoId;";

                    cmd.CommandText= query;
                    cmd.Connection = conexao;

                    cmd.Parameters.Add("@utilizadorId",SqlDbType.Int);
                    cmd.Parameters["@utilizadorId"].Value = UtilizadorEndereco.Utilizador.UtilizadorId;
                    cmd.Parameters.Add("@enderecoId", SqlDbType.Int);
                    cmd.Parameters["@enderecoId"].Value = UtilizadorEndereco.Endereco.EnderecoId;

                    if (conexao.State != ConnectionState.Open) 
                    {
                        conexao.Open(); 
                    }

                    int DelectedRows = cmd.ExecuteNonQuery();

                    return DelectedRows == 1;
                }
            }
        }

        public List<int> GetByUtilizadorId(int id)
        {
            using (SqlConnection conexao = new SqlConnection(_counfiguration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = "SELECT * FROM UtilizadoresEnderecos WHERE UtilizadorId=@id";

                    cmd.CommandText = query;
                    cmd.Connection = conexao;

                    cmd.Parameters.Add("@id", SqlDbType.Int);
                    cmd.Parameters["@id"].Value = id;

                    if(conexao.State != ConnectionState.Open) 
                    { 
                        conexao.Open();
                    }

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<int> enderecos = new List<int>();

                    while (reader.Read()) 
                    {
                        int endereco = Convert.ToInt32(reader["EnderecoId"]);
                        enderecos.Add(endereco);
                    }
                    
                    return enderecos;
                }
            }
        }
    }
}
