using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Project.Receitas.Data.Interfaces;
using Projecto.Receitas.Domain.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Receitas.Data.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private static IConfiguration _configuration;
        
        public CategoriaRepository(IConfiguration configuration) 
        {
            _configuration = configuration;
        }
        public bool Add(Categoria categoria)
        {
            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = "INSERT INTO Categorias (CategoriaNome) VALUES (@nome)";

                    cmd.CommandText = query;
                    cmd.Connection = conexao;

                    cmd.Parameters.Add("@nome",SqlDbType.NVarChar);
                    cmd.Parameters["@nome"].Value = categoria.CategoriaNome;

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
                    string query = "DELETE FROM Categorias WHERE CategoriaId=@id";

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

        public bool Delete(Categoria categoria)
        {
            return Delete(categoria.CategoriaId);
        }

        public List<Categoria> GetAll()
        {
            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = "SELECT * FROM Categorias;";

                    cmd.CommandText = query;
                    cmd.Connection = conexao;

                    if (conexao.State != ConnectionState.Open)
                        conexao.Open();

                    SqlDataReader reader = cmd.ExecuteReader(); 

                    List<Categoria> categorias = new List<Categoria>();
                    
                    while (reader.Read()) 
                    {
                        Categoria categoria = new Categoria(Convert.ToInt32(reader["CategoriaId"]), reader["CategoriaNome"].ToString());

                        categorias.Add(categoria);
                    }

                    return categorias;
                }
            }
        }

        public List<Categoria> GetAllPesquisa(string texto, int offset, int fetch)
        {
            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = $"SELECT * FROM Categorias WHERE CategoriaNome LIKE '%{texto}' OR CategoriaNome LIKE '%{texto}%'" +
                                   $" OR CategoriaNome LIKE '{texto}%' ORDER BY CategoriaNome " +
                                   $" OffSET {offset} ROWS FETCH FIRST {fetch} ROWS ONLY;";

                    cmd.CommandText = query;
                    cmd.Connection = conexao;

                    if (conexao.State != ConnectionState.Open)
                    {
                        conexao.Open();
                    }

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Categoria> categorias = new List<Categoria>();

                    while (reader.Read())
                    {
                        Categoria categoria = new Categoria(Convert.ToInt32(reader["CategoriaId"]), reader["CategoriaNome"].ToString());

                        categorias.Add(categoria);
                    }

                    return categorias;
                }
            }
        }
        public int NumeroTotalDeCategorias(string texto)
        {
            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = $"SELECT Count(*) FROM Categorias WHERE CategoriaNome LIKE '%{texto}' OR " +
                                   $" CategoriaNome LIKE '%{texto}%' OR CategoriaNome LIKE '{texto}%'";

                    cmd.CommandText = query;
                    cmd.Connection = conexao;

                    if (conexao.State != ConnectionState.Open)
                        conexao.Open();

                    int numeroDeCategorias = (int)cmd.ExecuteScalar();

                    return numeroDeCategorias;
                }
            }
        }

        public Categoria GetById(int id)
        {
            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = "SELECT * FROM Categorias WHERE CategoriaId=@id";

                    cmd.CommandText = query;
                    cmd.Connection = conexao;

                    cmd.Parameters.Add("@id", SqlDbType.Int);
                    cmd.Parameters["@id"].Value = id;

                    if (conexao.State != ConnectionState.Open)
                        conexao.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        Categoria categoria = new Categoria(Convert.ToInt32(reader["CategoriaId"]), reader["CategoriaNome"].ToString());

                        return categoria;
                    }
                    else 
                    {
                        return null; 
                    }
                }
            }
        }

        public bool Update(Categoria categoria)
        {
            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = "UPDATE Categorias SET CategoriaNome=@nome WHERE CategoriaId=@id";

                    cmd.CommandText = query;
                    cmd.Connection = conexao;

                    cmd.Parameters.Add("@nome", SqlDbType.NVarChar);
                    cmd.Parameters["@nome"].Value = categoria.CategoriaNome;
                    cmd.Parameters.Add("@id", SqlDbType.Int);
                    cmd.Parameters["@id"].Value = categoria.CategoriaId;

                    if (conexao.State != ConnectionState.Open)
                        conexao.Open();

                    int affectedRows = cmd.ExecuteNonQuery();

                    return affectedRows == 1;
                }
            }
        }
    }
}
