using Azure.Identity;
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
    public class AccountRepository : IAccountRepository
    {
        private static IConfiguration _configuration;

        public AccountRepository(IConfiguration configuration) 
        {
            _configuration = configuration;
        }

        public bool Add(Account account)
        {
            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = "INSERT INTO Accounts (Username,Password,UtilizadorId) " +
                                   " VALUES (@username,@password,@utilizadorId)";

                    cmd.CommandText = query;
                    cmd.Connection = conexao;

                    cmd.Parameters.Add("@username", SqlDbType.NVarChar);
                    cmd.Parameters["@username"].Value = account.Username;
                    cmd.Parameters.Add("@password", SqlDbType.NVarChar);
                    cmd.Parameters["@password"].Value = account.Password;
                    cmd.Parameters.Add("@utilizadorId",SqlDbType.Int);
                    cmd.Parameters["@utilizadorId"].Value = account.Utilizador.UtilizadorId;

                    if (conexao.State != ConnectionState.Open)
                        conexao.Open();

                    int affectedRows = cmd.ExecuteNonQuery();

                    return affectedRows == 1;
                }
            }
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Account account)
        {
            throw new NotImplementedException();
        }

        public List<Account> GetAll()
        {
            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = "SELECT * FROM Accounts; ";

                    cmd.CommandText = query;
                    cmd.Connection = conexao;

                    if (conexao.State != ConnectionState.Open)
                        conexao.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Account> accounts = new List<Account>();

                    while (reader.Read())
                    {
                         Account account = new Account(Convert.ToInt32(reader["AccountId"]), reader["Username"].ToString(),
                         reader["Password"].ToString(), new Utilizador(Convert.ToInt32(reader["UtilizadorId"])));

                        accounts.Add(account);
                    }

                    return accounts;
                }
            }
        }

        public Account GetById(int id)
        {
            throw new NotImplementedException();
        }

        public bool UpdateByUtilizadorId(Account account)
        {
            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")) ) 
            {
                using (SqlCommand cmd = new SqlCommand()) 
                {
                    string query = "UPDATE Accounts SET Username=@username, Password=@password WHERE UtilizadorId=@id;";

                    cmd.CommandText = query;
                    cmd.Connection = conexao;

                    cmd.Parameters.Add("@username", SqlDbType.NVarChar);
                    cmd.Parameters["@username"].Value = account.Username;
                    cmd.Parameters.Add("@password",SqlDbType.NVarChar);
                    cmd.Parameters["@password"].Value = account.Password;
                    cmd.Parameters.Add("@id", SqlDbType.Int);
                    cmd.Parameters["@id"].Value = account.Utilizador.UtilizadorId;

                    if(conexao.State != ConnectionState.Open)
                           conexao.Open();

                    int affectedRows = cmd.ExecuteNonQuery();

                    return affectedRows == 1;

                }
            }
        }

        public Account GetByUtilizadorId(int id)
        {
            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = "SELECT * FROM Accounts WHERE UtilizadorId = @id ";

                    cmd.CommandText = query;
                    cmd.Connection = conexao;

                    cmd.Parameters.Add("@id", SqlDbType.Int);
                    cmd.Parameters["@id"].Value = id;

                    if (conexao.State != ConnectionState.Open)
                        conexao.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read()) 
                    {
                        Account account = new Account(Convert.ToInt32(reader["AccountId"]), reader["Username"].ToString(),
                        reader["Password"].ToString(), new Utilizador(Convert.ToInt32(reader["UtilizadorId"])));

                        return account;
                    }

                    return null;
                }
            }
        }

        public bool Update(Account account)
        {
            throw new NotImplementedException();
        }

        public bool DeleteByUtilizadorId(int id)
        {
            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"))) 
            {
                using (SqlCommand cmd = new SqlCommand()) 
                {
                    string query = "DELETE FROM Accounts WHERE UtilizadorId = @id";

                    cmd.CommandText = query;
                    cmd.Connection = conexao;

                    cmd.Parameters.Add("@id", SqlDbType.NVarChar);
                    cmd.Parameters["@id"].Value = id;

                    if(conexao.State != ConnectionState.Open)
                            conexao.Open(); 

                    int affectedRows=cmd.ExecuteNonQuery();

                    return affectedRows == 1;
                }
            }
        }

        public bool DeleteByUtilizadorId(Account account)
        {
            return DeleteByUtilizadorId(account.Utilizador.UtilizadorId);
        }

        public Account GetByUsername(string username)
        {
            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = "SELECT * FROM Accounts WHERE Username = @username; ";

                    cmd.CommandText = query;
                    cmd.Connection = conexao;

                    cmd.Parameters.Add("@username", SqlDbType.NVarChar);
                    cmd.Parameters["@username"].Value = username;

                    if (conexao.State != ConnectionState.Open)
                        conexao.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                         Account account = new Account(Convert.ToInt32(reader["AccountId"]), reader["Username"].ToString(),
                         reader["Password"].ToString(), new Utilizador(Convert.ToInt32(reader["UtilizadorId"])));

                        return account;
                    }

                    return null;
                }
            }
        }
    }
}
