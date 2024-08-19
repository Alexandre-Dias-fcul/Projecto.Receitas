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
    public class ReceitaRepository : IReceitaRepository
    {
        private static IConfiguration _configuration;

        public ReceitaRepository(IConfiguration configuration) 
        { 
            _configuration = configuration;
        }
        public bool Add(Receita receita)
        {
            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = "INSERT INTO Receitas (Titulo,ImagemUrl,ModoDePreparacao,TempoDePreparacao,GrauDeDificuldade," +
                                   " Estado,CategoriaId,UtilizadorId) VALUES (@titulo,@url,@modo,@tempo,@dificuldade,@estado," +
                                   " @categoriaId,@utilizadorId)";

                    cmd.CommandText = query;
                    cmd.Connection = conexao;

                    cmd.Parameters.Add("@titulo", SqlDbType.NVarChar);
                    cmd.Parameters["@titulo"].Value = receita.Titulo;

                    if (receita.ImagemUrl == null) 
                    {
                        cmd.Parameters.Add("@url", SqlDbType.NVarChar);
                        cmd.Parameters["@url"].Value = "";
                    }
                    else 
                    {
                        cmd.Parameters.Add("@url", SqlDbType.NVarChar);
                        cmd.Parameters["@url"].Value = receita.ImagemUrl;
                    }
                    
                    cmd.Parameters.Add("@modo", SqlDbType.NVarChar);
                    cmd.Parameters["@modo"].Value = receita.ModoDePreparacao;
                    cmd.Parameters.Add("@tempo", SqlDbType.Int);
                    cmd.Parameters["@tempo"].Value = (int)receita.TempoDePreparacao;
                    cmd.Parameters.Add("@dificuldade", SqlDbType.Int);
                    cmd.Parameters["@dificuldade"].Value = (int)receita.Dificuldade;
                    cmd.Parameters.Add("@estado",SqlDbType.Int);
                    cmd.Parameters["@estado"].Value=(int)receita.Estado;
                    cmd.Parameters.Add("@categoriaId", SqlDbType.Int);
                    cmd.Parameters["@categoriaId"].Value = receita.Categoria.CategoriaId;
                    cmd.Parameters.Add("@utilizadorId",SqlDbType.Int);
                    cmd.Parameters["@utilizadorId"].Value=receita.Utilizador.UtilizadorId;

                    if (conexao.State != ConnectionState.Open)
                        conexao.Open();

                    int affectedRows = cmd.ExecuteNonQuery();

                    return affectedRows == 1;
                }
            }
        }

        public int AddId(Receita receita)
        {
            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = "INSERT INTO Receitas (Titulo,ImagemUrl,ModoDePreparacao,TempoDePreparacao,GrauDeDificuldade," +
                                   " Estado,CategoriaId,UtilizadorId) VALUES (@titulo,@url,@modo,@tempo,@dificuldade,@estado," +
                                   " @categoriaId,@utilizadorId)  SELECT SCOPE_IDENTITY();";

                    cmd.CommandText = query;
                    cmd.Connection = conexao;

                    cmd.Parameters.Add("@titulo", SqlDbType.NVarChar);
                    cmd.Parameters["@titulo"].Value = receita.Titulo;
                    if (receita.ImagemUrl == null)
                    {
                        cmd.Parameters.Add("@url", SqlDbType.NVarChar);
                        cmd.Parameters["@url"].Value = "";
                    }
                    else
                    {
                        cmd.Parameters.Add("@url", SqlDbType.NVarChar);
                        cmd.Parameters["@url"].Value = receita.ImagemUrl;
                    }
                    cmd.Parameters.Add("@modo", SqlDbType.NVarChar);
                    cmd.Parameters["@modo"].Value = receita.ModoDePreparacao;
                    cmd.Parameters.Add("@tempo", SqlDbType.Int);
                    cmd.Parameters["@tempo"].Value = (int)receita.TempoDePreparacao;
                    cmd.Parameters.Add("@dificuldade", SqlDbType.Int);
                    cmd.Parameters["@dificuldade"].Value = (int)receita.Dificuldade;
                    cmd.Parameters.Add("@estado", SqlDbType.Int);
                    cmd.Parameters["@estado"].Value=(int)receita.Estado;
                    cmd.Parameters.Add("@categoriaId", SqlDbType.Int);
                    cmd.Parameters["@categoriaId"].Value = receita.Categoria.CategoriaId;
                    cmd.Parameters.Add("@utilizadorId", SqlDbType.Int);
                    cmd.Parameters["@utilizadorId"].Value = receita.Utilizador.UtilizadorId;

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
                    string query = "DELETE FROM Receitas WHERE ReceitaId=@id";

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

        public bool Delete(Receita receita)
        {
            return Delete(receita.ReceitaId);
        }

        public List<Receita> GetAll()
        {
            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = "SELECT * FROM Receitas";

                    cmd.CommandText = query;
                    cmd.Connection = conexao;

                    if (conexao.State != ConnectionState.Open) 
                    {
                        conexao.Open();
                    }
                        
                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Receita> receitas = new List<Receita>();

                    while (reader.Read())
                    {
                        Receita receita = new Receita(Convert.ToInt32(reader["ReceitaId"]), reader["Titulo"].ToString(),
                            reader["ImagemUrl"].ToString(), reader["ModoDePreparacao"].ToString(),
                            (Tempo)Convert.ToInt32(reader["TempoDePreparacao"]), (TipoDeEstado)Convert.ToInt32(reader["Estado"]),
                            (GrauDeDificuldade)Convert.ToInt32(reader["GrauDeDificuldade"]),
                            new Categoria(Convert.ToInt32(reader["CategoriaId"])),
                            new Utilizador(Convert.ToInt32(reader["UtilizadorId"]))); 

                        receitas.Add(receita);
                    }

                    return receitas;
                }
            }
        }

        public List<Receita> GetAllPesquisa(string texto, int offset, int fetch)
        {
            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = $"SELECT * FROM Receitas WHERE Titulo LIKE '%{texto}' OR Titulo LIKE '%{texto}%' OR " +
                                   $" Titulo LIKE '{texto}%' ORDER BY Titulo OffSET {offset} ROWS FETCH FIRST {fetch} ROWS ONLY;";

                    cmd.CommandText = query;
                    cmd.Connection = conexao;

                    if (conexao.State != ConnectionState.Open)
                    {
                        conexao.Open();
                    }

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Receita> receitas = new List<Receita>();

                    while (reader.Read())
                    {
                        Receita receita = new Receita(Convert.ToInt32(reader["ReceitaId"]), reader["Titulo"].ToString(),
                            reader["ImagemUrl"].ToString(), reader["ModoDePreparacao"].ToString(),
                            (Tempo)Convert.ToInt32(reader["TempoDePreparacao"]), (TipoDeEstado)Convert.ToInt32(reader["Estado"]),
                            (GrauDeDificuldade)Convert.ToInt32(reader["GrauDeDificuldade"]),
                            new Categoria(Convert.ToInt32(reader["CategoriaId"])),
                            new Utilizador(Convert.ToInt32(reader["UtilizadorId"])));

                        receitas.Add(receita);
                    }

                    return receitas;
                }
            }
        }

        public int NumeroTotalDeReceitasPesquisa(string texto)
        {
            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = $"SELECT Count(*) FROM Receitas WHERE Titulo LIKE '%{texto}' OR Titulo LIKE '%{texto}%' OR " +
                                   $" Titulo LIKE '{texto}%'";

                    cmd.CommandText = query;
                    cmd.Connection = conexao;

                    if (conexao.State != ConnectionState.Open)
                        conexao.Open();

                    int numeroDeReceitas = (int)cmd.ExecuteScalar();

                    return numeroDeReceitas;
                }
            }
        }

        public List<Receita> GetByCategoriaId(int id)
        {
            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = "SELECT * FROM Receitas WHERE CategoriaId = @id;";

                    cmd.CommandText = query;
                    cmd.Connection = conexao;

                    cmd.Parameters.Add("@id", SqlDbType.Int);
                    cmd.Parameters["@id"].Value = id;

                    if (conexao.State != ConnectionState.Open)
                        conexao.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Receita> receitas = new List<Receita>();

                    while (reader.Read())
                    {
                        Receita receita = new Receita(Convert.ToInt32(reader["ReceitaId"]), reader["Titulo"].ToString(),
                            reader["ImagemUrl"].ToString(), reader["ModoDePreparacao"].ToString(),
                            (Tempo)Convert.ToInt32(reader["TempoDePreparacao"]), (TipoDeEstado)Convert.ToInt32(reader["Estado"]),
                            (GrauDeDificuldade)Convert.ToInt32(reader["GrauDeDificuldade"]),
                            new Categoria(Convert.ToInt32(reader["CategoriaId"])),
                            new Utilizador(Convert.ToInt32(reader["UtilizadorId"])));

                        receitas.Add(receita);
                    }

                    return receitas;
                }
            }
        }

        public Receita GetById(int id)
        {
            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = "SELECT * FROM Receitas WHERE ReceitaId=@id";

                    cmd.CommandText = query;
                    cmd.Connection = conexao;

                    cmd.Parameters.Add("@id", SqlDbType.Int);
                    cmd.Parameters["@id"].Value=id;

                    if (conexao.State != ConnectionState.Open)
                        conexao.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        Receita receita = new Receita(Convert.ToInt32(reader["ReceitaId"]), reader["Titulo"].ToString(),
                            reader["ImagemUrl"].ToString(), reader["ModoDePreparacao"].ToString(),
                            (Tempo)Convert.ToInt32(reader["TempoDePreparacao"]), (TipoDeEstado)Convert.ToInt32(reader["Estado"]),
                            (GrauDeDificuldade)Convert.ToInt32(reader["GrauDeDificuldade"]),
                            new Categoria(Convert.ToInt32(reader["CategoriaId"])),
                            new Utilizador(Convert.ToInt32(reader["UtilizadorId"])));

                        return receita;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public List<Receita> GetReceitasValidas(int top)
        {
            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = $"SELECT Top { top } * FROM Receitas WHERE Estado = 1;";

                    cmd.CommandText = query;
                    cmd.Connection = conexao;

                    if (conexao.State != ConnectionState.Open)
                    {
                        conexao.Open();
                    }

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Receita> receitas = new List<Receita>();

                    while (reader.Read())
                    {
                        Receita receita = new Receita(Convert.ToInt32(reader["ReceitaId"]), reader["Titulo"].ToString(),
                            reader["ImagemUrl"].ToString(), reader["ModoDePreparacao"].ToString(),
                            (Tempo)Convert.ToInt32(reader["TempoDePreparacao"]), (TipoDeEstado)Convert.ToInt32(reader["Estado"]),
                            (GrauDeDificuldade)Convert.ToInt32(reader["GrauDeDificuldade"]),
                            new Categoria(Convert.ToInt32(reader["CategoriaId"])),
                            new Utilizador(Convert.ToInt32(reader["UtilizadorId"])));

                        receitas.Add(receita);
                    }

                    return receitas;
                }
            }
        }

        public List<Receita> GetReceitasValidasTituloPesquisa(int top , string texto) 
        {
            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = $"SELECT Top {top} * FROM Receitas WHERE (Titulo LIKE '%{texto}' OR " +
                        $" Titulo LIKE '%{texto}%' OR Titulo LIKE '{texto}%') AND Estado = 1;";

                    cmd.CommandText = query;
                    cmd.Connection = conexao;

                    if (conexao.State != ConnectionState.Open)
                    {
                        conexao.Open();
                    }

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Receita> receitas = new List<Receita>();

                    while (reader.Read())
                    {
                        Receita receita = new Receita(Convert.ToInt32(reader["ReceitaId"]), reader["Titulo"].ToString(),
                            reader["ImagemUrl"].ToString(), reader["ModoDePreparacao"].ToString(),
                            (Tempo)Convert.ToInt32(reader["TempoDePreparacao"]), (TipoDeEstado)Convert.ToInt32(reader["Estado"]),
                            (GrauDeDificuldade)Convert.ToInt32(reader["GrauDeDificuldade"]),
                            new Categoria(Convert.ToInt32(reader["CategoriaId"])),
                            new Utilizador(Convert.ToInt32(reader["UtilizadorId"])));

                        receitas.Add(receita);
                    }

                    return receitas;
                }
            }
        }

        public int NumeroDeReceitasValidas(string texto) 
        {
            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = $"SELECT Count(*) FROM Receitas WHERE (Titulo LIKE '%{texto}' OR " +
                        $" Titulo LIKE '%{texto}%' OR Titulo LIKE '{texto}%') AND Estado = 1;";

                    cmd.CommandText = query;
                    cmd.Connection = conexao;

                    if (conexao.State != ConnectionState.Open)
                        conexao.Open();

                    int numeroDeReceitas = (int)cmd.ExecuteScalar();

                    return numeroDeReceitas;
                }
            }
        }

        public List<Receita> GetReceitasByUtilizadorId(int top,int id)
        {
            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = $"SELECT Top {top} * FROM Receitas WHERE UtilizadorId=@id;";

                    cmd.CommandText = query;
                    cmd.Connection = conexao;

                    cmd.Parameters.Add("@id", SqlDbType.Int);
                    cmd.Parameters["@id"].Value = id;

                    if (conexao.State != ConnectionState.Open)
                    {
                        conexao.Open();
                    }

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Receita> receitas = new List<Receita>();

                    while (reader.Read())
                    {
                        Receita receita = new Receita(Convert.ToInt32(reader["ReceitaId"]), reader["Titulo"].ToString(),
                            reader["ImagemUrl"].ToString(), reader["ModoDePreparacao"].ToString(),
                            (Tempo)Convert.ToInt32(reader["TempoDePreparacao"]), (TipoDeEstado)Convert.ToInt32(reader["Estado"]),
                            (GrauDeDificuldade)Convert.ToInt32(reader["GrauDeDificuldade"]),
                            new Categoria(Convert.ToInt32(reader["CategoriaId"])),
                            new Utilizador(Convert.ToInt32(reader["UtilizadorId"])));

                        receitas.Add(receita);
                    }

                    return receitas;
                }
            }
        }

        public int TotalDeReceitasByUtilizadorId(int id)
        {
            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = $"SELECT Count(*) FROM Receitas WHERE UtilizadorId=@id";
                    
                    cmd.CommandText = query;
                    cmd.Connection = conexao;

                    cmd.Parameters.Add("@id", SqlDbType.Int);
                    cmd.Parameters["@id"].Value = id;

                    if (conexao.State != ConnectionState.Open)
                        conexao.Open();

                    int numeroDeReceitas = (int)cmd.ExecuteScalar();

                    return numeroDeReceitas;
                }
            }
        }

        public bool Update(Receita receita)
        {
            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = "UPDATE Receitas SET Titulo=@titulo, ImagemUrl=@url, ModoDePreparacao=@modo," +
                                   " TempoDePreparacao=@tempo, GrauDeDificuldade=@dificuldade, Estado=@estado, CategoriaId=@categoriaId " +
                                   " ,UtilizadorId=@utilizadorId WHERE ReceitaId=@id";

                    cmd.CommandText = query;
                    cmd.Connection = conexao;

                    cmd.Parameters.Add("@titulo", SqlDbType.NVarChar);
                    cmd.Parameters["@titulo"].Value = receita.Titulo;
                    if (receita.ImagemUrl == null)
                    {
                        cmd.Parameters.Add("@url", SqlDbType.NVarChar);
                        cmd.Parameters["@url"].Value = "";
                    }
                    else
                    {
                        cmd.Parameters.Add("@url", SqlDbType.NVarChar);
                        cmd.Parameters["@url"].Value = receita.ImagemUrl;
                    }
                    cmd.Parameters.Add("@modo", SqlDbType.NVarChar);
                    cmd.Parameters["@modo"].Value = receita.ModoDePreparacao;
                    cmd.Parameters.Add("@tempo", SqlDbType.Int);
                    cmd.Parameters["@tempo"].Value = (int)receita.TempoDePreparacao;
                    cmd.Parameters.Add("@dificuldade", SqlDbType.Int);
                    cmd.Parameters["@dificuldade"].Value = (int)receita.Dificuldade;
                    cmd.Parameters.Add("@estado",SqlDbType.Int);
                    cmd.Parameters["@estado"].Value=(int)receita.Estado;
                    cmd.Parameters.Add("@categoriaId", SqlDbType.Int);
                    cmd.Parameters["@categoriaId"].Value = receita.Categoria.CategoriaId;
                    cmd.Parameters.Add("@utilizadorId", SqlDbType.Int);
                    cmd.Parameters["@utilizadorId"].Value = receita.Utilizador.UtilizadorId;
                    cmd.Parameters.Add("@id", SqlDbType.Int);
                    cmd.Parameters["@id"].Value = receita.ReceitaId;

                    if (conexao.State != ConnectionState.Open)
                        conexao.Open();

                    int affectedRows = cmd.ExecuteNonQuery();

                    return affectedRows == 1;
                }
            }
        }
    }
}
