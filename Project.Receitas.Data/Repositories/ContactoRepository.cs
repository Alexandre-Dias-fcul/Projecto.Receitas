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
    public class ContactoRepository : IContactoRepository
    {
        private static IConfiguration _configuration;

        public ContactoRepository(IConfiguration configuration) 
        {
            _configuration = configuration;
        }
        public bool Add(Contacto contacto)
        {
            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"))) 
            {
                using (SqlCommand cmd = new SqlCommand()) 
                {
                    string query = "INSERT INTO Contactos (Valor,TipoDeContacto,UtilizadorId) " +
                                   " VALUES (@valor,@tipo,@utilizadorId) ";

                    cmd.CommandText = query;
                    cmd.Connection = conexao;

                    cmd.Parameters.Add("@valor", SqlDbType.NVarChar);
                    cmd.Parameters["@valor"].Value = contacto.Valor;
                    cmd.Parameters.Add("@Tipo", SqlDbType.Int);
                    cmd.Parameters["@tipo"].Value = (int)contacto.Tipo;
                    cmd.Parameters.Add("@utilizadorId", SqlDbType.Int);
                    cmd.Parameters["@utilizadorId"].Value = contacto.Utilizador.UtilizadorId;

                    if(conexao.State != ConnectionState.Open)
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
                    string query = "DELETE FROM Contactos WHERE ContactoId=@id";

                    cmd.CommandText = query;
                    cmd.Connection = conexao;

                    cmd.Parameters.Add("@id",SqlDbType.NVarChar);
                    cmd.Parameters["@id"].Value = id;

                    if(conexao.State != ConnectionState.Open) 
                             conexao.Open();

                    int deletedRows = cmd.ExecuteNonQuery();

                    return deletedRows == 1;
                    
                }
            }
        }

        public bool Delete(Contacto contacto)
        {
            return Delete(contacto.ContactoId);
        }

        public List<Contacto> GetAll()
        {
            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"))) 
            {
                using (SqlCommand cmd = new SqlCommand()) 
                {
                    string query = "SELECT * FROM Contactos";

                    cmd.CommandText = query;
                    cmd.Connection = conexao;

                    if(conexao.State != ConnectionState.Open)
                           conexao.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Contacto> contactos = new List<Contacto>();

                    while(reader.Read()) 
                    {
                        Contacto contacto = new Contacto(Convert.ToInt32(reader["contactoId"]), 
                                               (TipoDeContacto)Convert.ToInt32(reader["TipoDeContacto"]), 
                                               reader["Valor"].ToString(),
                                             new Utilizador(Convert.ToInt32(reader["UtilizadorId"])));

                        contactos.Add(contacto);
                    }

                    return contactos;
                }
            }
        }

        public Contacto GetById(int id)
        {
            using(SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"))) 
            {
                using(SqlCommand cmd = new SqlCommand()) 
                {
                    string query = "SELECT * FROM Contactos WHERE ContactoId=@id";

                    cmd.CommandText = query;
                    cmd.Connection = conexao;

                    cmd.Parameters.Add("@id",SqlDbType.NVarChar);
                    cmd.Parameters["@id"].Value = id;

                    if (conexao.State != ConnectionState.Open)
                            conexao.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    if(reader.Read()) 
                    {
                        Contacto contacto = new Contacto(Convert.ToInt32(reader["contactoId"]),
                                               (TipoDeContacto)Convert.ToInt32(reader["TipoDeContacto"]),
                                               reader["Valor"].ToString(),
                                             new Utilizador(Convert.ToInt32(reader["UtilizadorId"])));

                        return contacto;
                    }

                    return null;
                }
            }
        }

        public List<Contacto> GetByUtilizadorId(int id)
        {
            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = "SELECT * FROM Contactos WHERE UtilizadorId=@id";

                    cmd.CommandText = query;
                    cmd.Connection = conexao;

                    cmd.Parameters.Add("@id", SqlDbType.NVarChar);
                    cmd.Parameters["@id"].Value = id;

                    if (conexao.State != ConnectionState.Open)
                        conexao.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Contacto> contactos = new List<Contacto>();

                    while (reader.Read())
                    {
                        Contacto contacto = new Contacto(Convert.ToInt32(reader["contactoId"]),
                                               (TipoDeContacto)Convert.ToInt32(reader["TipoDeContacto"]),
                                               reader["Valor"].ToString(),
                                             new Utilizador(Convert.ToInt32(reader["UtilizadorId"])));

                        contactos.Add(contacto);
                    }

                    return contactos;
                }
            }
        }

        public bool Update(Contacto contacto)
        {
            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"))) 
            {
                using (SqlCommand cmd = new SqlCommand()) 
                {
                    string query = "UPDATE Contactos SET Valor=@valor, TipoDeContacto = @tipo , UtilizadorId = @utilizadorId" +
                                   " WHERE ContactoId=@id;";

                    cmd.CommandText = query;
                    cmd.Connection = conexao;

                    cmd.Parameters.Add("@valor",SqlDbType.NVarChar);
                    cmd.Parameters["@valor"].Value = contacto.Valor;
                    cmd.Parameters.Add("@tipo", SqlDbType.Int);
                    cmd.Parameters["@tipo"].Value = (int)contacto.Tipo;
                    cmd.Parameters.Add("@utilizadorId", SqlDbType.Int);
                    cmd.Parameters["@utilizadorId"].Value = contacto.Utilizador.UtilizadorId;
                    cmd.Parameters.Add("@id", SqlDbType.Int);
                    cmd.Parameters["@id"].Value = contacto.ContactoId;   

                    if(conexao.State != ConnectionState.Open)
                            conexao.Open();

                    int affectedRows = cmd.ExecuteNonQuery();

                    return affectedRows == 1;
                }      
            }
        }
    }
}
