using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Project.Receitas.Data.Interfaces;
using Projecto.Receitas.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Project.Receitas.Data.Repositories
{
    public class UtilizadorRepository : IUtilizadorRepository
    {   
        private static IConfiguration _configuration { get; set; }

        public UtilizadorRepository(IConfiguration configuration) 
        { 
            _configuration= configuration;
        }
        public bool Add(Utilizador utilizador)
        {
            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"))) 
            {
                using (SqlCommand cmd = new SqlCommand())  
                {
                    string query = "INSERT INTO Utilizadores " +
                        " (PrimeiroNome,NomesDoMeio,UltimoNome,Genero,FotoUrl,DataDeNascimento,Nacionalidade,Permissoes) " +
                        " VALUES " +
                        " (@primeiro,@meio,@ultimo,@genero,@foto,@data,@nacionalidade,@permissoes); ";

                    cmd.CommandText = query;
                    cmd.Connection = conexao;

                    cmd.Parameters.Add("@primeiro", SqlDbType.NVarChar);
                    cmd.Parameters["@primeiro"].Value = utilizador.PrimeiroNome;

                    if (utilizador.NomesDoMeio==null)
                    {
                        cmd.Parameters.Add("@meio", SqlDbType.NVarChar);
                        cmd.Parameters["@meio"].Value =string.Empty;
                    }
                    else
                    {
                        cmd.Parameters.Add("@meio", SqlDbType.NVarChar);
                        cmd.Parameters["@meio"].Value = utilizador.NomesDoMeio;
                    }

                    cmd.Parameters.Add("@ultimo", SqlDbType.NVarChar);
                    cmd.Parameters["@ultimo"].Value = utilizador.UltimoNome;


                    if (utilizador.Genero==null)
                    {
                        cmd.Parameters.Add("@genero", SqlDbType.NVarChar);
                        cmd.Parameters["@genero"].Value = string.Empty;
                    }
                    else
                    {
                        cmd.Parameters.Add("@genero", SqlDbType.Int);
                        cmd.Parameters["@genero"].Value = (int)utilizador.Genero;
                    }

                    if (utilizador.FotoUrl==null)
                    {
                        cmd.Parameters.Add("@foto", SqlDbType.NVarChar);
                        cmd.Parameters["@foto"].Value = string.Empty;
                    }
                    else
                    {
                        cmd.Parameters.Add("@foto", SqlDbType.NVarChar);
                        cmd.Parameters["@foto"].Value = utilizador.FotoUrl;
                    }


                    if (utilizador.DataDeNascimento==null)
                    {
                        cmd.Parameters.Add("@data", SqlDbType.NVarChar);
                        cmd.Parameters["@data"].Value =  DBNull.Value;
                    }
                    else
                    {
                        cmd.Parameters.Add("@data", SqlDbType.DateTime);
                        cmd.Parameters["@data"].Value = utilizador.DataDeNascimento;
                    }

                    if (utilizador.Nacionalidade==null)
                    {
                        cmd.Parameters.Add("@nacionalidade", SqlDbType.NVarChar);
                        cmd.Parameters["@nacionalidade"].Value = string.Empty;
                    }
                    else
                    {
                        cmd.Parameters.Add("@nacionalidade", SqlDbType.NVarChar);
                        cmd.Parameters["@nacionalidade"].Value = utilizador.Nacionalidade;
                    }

                    cmd.Parameters.Add("@permissoes", SqlDbType.Int);
                    cmd.Parameters["@permissoes"].Value = (int)utilizador.Permissoes;

                    if (conexao.State != ConnectionState.Open)
                    {
                        conexao.Open();
                    }

                    int affectedRows = cmd.ExecuteNonQuery();

                    return affectedRows == 1;
                }
            }
        }

        public int AddId(Utilizador utilizador)
        {
            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = "INSERT INTO Utilizadores " +
                        " (PrimeiroNome,NomesDoMeio,UltimoNome,Genero,FotoUrl,DataDeNascimento,Nacionalidade,Permissoes) " +
                        " VALUES " +
                        " (@primeiro,@meio,@ultimo,@genero,@foto,@data,@nacionalidade,@permissoes); SELECT SCOPE_IDENTITY(); ";

                    cmd.CommandText = query;
                    cmd.Connection = conexao;

                    cmd.Parameters.Add("@primeiro", SqlDbType.NVarChar);
                    cmd.Parameters["@primeiro"].Value = utilizador.PrimeiroNome;
                    
                    if (utilizador.NomesDoMeio==null) 
                    {
                        cmd.Parameters.Add("@meio", SqlDbType.NVarChar);
                        cmd.Parameters["@meio"].Value = string.Empty;
                    }
                    else 
                    {
                        cmd.Parameters.Add("@meio", SqlDbType.NVarChar);
                        cmd.Parameters["@meio"].Value = utilizador.NomesDoMeio;
                    }
                    
                    cmd.Parameters.Add("@ultimo", SqlDbType.NVarChar);
                    cmd.Parameters["@ultimo"].Value = utilizador.UltimoNome;


                    if (utilizador.Genero==null) 
                    {
                        cmd.Parameters.Add("@genero", SqlDbType.NVarChar);
                        cmd.Parameters["@genero"].Value = string.Empty;
                    }
                    else 
                    {
                        cmd.Parameters.Add("@genero", SqlDbType.Int);
                        cmd.Parameters["@genero"].Value = (int)utilizador.Genero;
                    }  

                    if (utilizador.FotoUrl==null) 
                    {
                        cmd.Parameters.Add("@foto", SqlDbType.NVarChar);
                        cmd.Parameters["@foto"].Value = string.Empty;
                    }
                    else 
                    {
                        cmd.Parameters.Add("@foto", SqlDbType.NVarChar);
                        cmd.Parameters["@foto"].Value = utilizador.FotoUrl;
                    }
                    
                    
                    if (utilizador.DataDeNascimento==null)
                    {
                        cmd.Parameters.Add("@data", SqlDbType.NVarChar);
                        cmd.Parameters["@data"].Value = DBNull.Value;
                    }
                    else
                    {
                        cmd.Parameters.Add("@data", SqlDbType.DateTime);
                        cmd.Parameters["@data"].Value = utilizador.DataDeNascimento;
                    }

                    if (utilizador.Nacionalidade==null) 
                    {
                        cmd.Parameters.Add("@nacionalidade", SqlDbType.NVarChar);
                        cmd.Parameters["@nacionalidade"].Value = string.Empty;
                    }
                    else 
                    {
                        cmd.Parameters.Add("@nacionalidade", SqlDbType.NVarChar);
                        cmd.Parameters["@nacionalidade"].Value = utilizador.Nacionalidade;
                    }
                    
                    cmd.Parameters.Add("@permissoes", SqlDbType.Int);
                    cmd.Parameters["@permissoes"].Value = (int)utilizador.Permissoes;

                    if (conexao.State != ConnectionState.Open) 
                    {
                        conexao.Open();
                    }     

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
                    string query = "DELETE FROM Utilizadores WHERE UtilizadorId = @id;";

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

        public bool Delete(Utilizador utilizador)
        {
            return Delete(utilizador.UtilizadorId);
        }

        public List<Utilizador> GetAll()
        {
            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = "SELECT * FROM Utilizadores; ";

                    cmd.CommandText = query;
                    cmd.Connection = conexao;

                    if (conexao.State != ConnectionState.Open)
                    {
                        conexao.Open();
                    }

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Utilizador> utilizadores = new List<Utilizador>();

                    while (reader.Read()) 
                    { 
                        int id = Convert.ToInt32(reader["UtilizadorId"]);
                        string primeiro = reader["PrimeiroNome"].ToString();
                        string meio = reader["NomesDoMeio"].ToString();
                        string ultimo = reader["UltimoNome"].ToString();
                        TipoDeGenero genero = (TipoDeGenero)Convert.ToInt32(reader["Genero"]);
                        string foto = reader["FotoUrl"].ToString();

                        DateTime? data;

                        if (string.IsNullOrEmpty(reader["DataDeNascimento"].ToString()))
                        {
                            data = null;
                        }
                        else
                        {
                            data = DateTime.Parse(reader["DataDeNascimento"].ToString());
                        }

                        string nacionalidade = reader["Nacionalidade"].ToString();
                        Permissao permissoes = (Permissao)Convert.ToInt32(reader["Permissoes"]);

                        Utilizador utilizador = new Utilizador(id,primeiro,meio,ultimo,genero,foto,data,nacionalidade,permissoes);

                        utilizadores.Add(utilizador);
                    }

                    return utilizadores;
                }
            }
        }

        public List<Utilizador> GetAllPesquisa(string texto, int offset, int fetch) 
        {
            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = $"SELECT * FROM Utilizadores WHERE PrimeiroNome LIKE '%{texto}' OR PrimeiroNome LIKE '%{texto}%' " +
                                   $" OR PrimeiroNome LIKE '{texto}%' OR UltimoNome LIKE '%{texto}' OR UltimoNome LIKE '%{texto}%' " +
                                   $" OR UltimoNome LIKE '{texto}%' ORDER BY PrimeiroNome OffSET {offset} ROWS " +
                                   $" FETCH FIRST {fetch} ROWS ONLY;";

                    cmd.CommandText = query;
                    cmd.Connection = conexao;

                    if (conexao.State != ConnectionState.Open)
                    {
                        conexao.Open();
                    }

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Utilizador> utilizadores = new List<Utilizador>();

                    while (reader.Read())
                    {
                        int id = Convert.ToInt32(reader["UtilizadorId"]);
                        string primeiro = reader["PrimeiroNome"].ToString();
                        string meio = reader["NomesDoMeio"].ToString();
                        string ultimo = reader["UltimoNome"].ToString();
                        TipoDeGenero genero = (TipoDeGenero)Convert.ToInt32(reader["Genero"]);
                        string foto = reader["FotoUrl"].ToString();

                        DateTime? data;

                        if (string.IsNullOrEmpty(reader["DataDeNascimento"].ToString()))
                        {
                            data = null;
                        }
                        else
                        {
                            data = DateTime.Parse(reader["DataDeNascimento"].ToString());
                        }

                        string nacionalidade = reader["Nacionalidade"].ToString();
                        Permissao permissoes = (Permissao)Convert.ToInt32(reader["Permissoes"]);

                        Utilizador utilizador = new Utilizador(id, primeiro, meio, ultimo, genero, foto, data, nacionalidade, permissoes);

                        utilizadores.Add(utilizador);
                    }

                    return utilizadores;
                }
            }
        }

        public int NumeroTotalDeUtilizadores(string texto) 
        {
            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = $"SELECT Count(*) FROM Utilizadores WHERE PrimeiroNome LIKE '%{texto}' OR PrimeiroNome " +
                                   $" LIKE '%{texto}%' OR PrimeiroNome LIKE '{texto}%' OR UltimoNome LIKE '%{texto}' OR " +
                                   $" UltimoNome LIKE '%{texto}%' OR UltimoNome LIKE '{texto}%';";

                    cmd.CommandText = query;
                    cmd.Connection = conexao;

                    if (conexao.State != ConnectionState.Open)
                        conexao.Open();

                    int numeroDeUtilizadores = (int)cmd.ExecuteScalar();

                    return numeroDeUtilizadores;
                }
            }
        }

        public Utilizador GetById(int id)
        {
            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = "SELECT * FROM Utilizadores WHERE UtilizadorId = @id ";

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
                        int idUtilizador = Convert.ToInt32(reader["UtilizadorId"]);
                        string primeiro = reader["PrimeiroNome"].ToString();
                        string meio = reader["NomesDoMeio"].ToString();
                        string ultimo = reader["UltimoNome"].ToString();
                        TipoDeGenero genero = (TipoDeGenero)Convert.ToInt32(reader["Genero"]);
                        string foto = reader["FotoUrl"].ToString();

                        DateTime? data;

                        if (string.IsNullOrEmpty(reader["DataDeNascimento"].ToString()))
                        {
                            data = null;   
                        }
                        else
                        {
                            data = DateTime.Parse(reader["DataDeNascimento"].ToString());
                        }

                        string nacionalidade = reader["Nacionalidade"].ToString();
                        Permissao permissoes = (Permissao)Convert.ToInt32(reader["Permissoes"]);

                        Utilizador utilizador = new Utilizador(id, primeiro, meio, ultimo, genero, foto, data, nacionalidade, permissoes);

                        return utilizador;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public bool Update(Utilizador utilizador)
        {
            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = " UPDATE Utilizadores SET PrimeiroNome = @primeiro, NomesDomeio=@meio, UltimoNome=@ultimo, " +
                                   " Genero=@genero, FotoUrl=@foto, DataDeNascimento = @data, Nacionalidade = @nacionalidade, " +
                                   " Permissoes=@permissoes WHERE UtilizadorId=@id";

                    cmd.CommandText = query;
                    cmd.Connection = conexao;

                    cmd.Parameters.Add("@primeiro", SqlDbType.NVarChar);
                    cmd.Parameters["@primeiro"].Value = utilizador.PrimeiroNome;

                    if (utilizador.NomesDoMeio==null)
                    {
                        cmd.Parameters.Add("@meio", SqlDbType.NVarChar);
                        cmd.Parameters["@meio"].Value = string.Empty;
                    }
                    else
                    {
                        cmd.Parameters.Add("@meio", SqlDbType.NVarChar);
                        cmd.Parameters["@meio"].Value = utilizador.NomesDoMeio;
                    }

                    cmd.Parameters.Add("@ultimo", SqlDbType.NVarChar);
                    cmd.Parameters["@ultimo"].Value = utilizador.UltimoNome;


                    if (utilizador.Genero==null)
                    {
                        cmd.Parameters.Add("@genero", SqlDbType.NVarChar);
                        cmd.Parameters["@genero"].Value = string.Empty;
                    }
                    else
                    {
                        cmd.Parameters.Add("@genero", SqlDbType.Int);
                        cmd.Parameters["@genero"].Value = (int)utilizador.Genero;
                    }

                    if (utilizador.FotoUrl==null)
                    {
                        cmd.Parameters.Add("@foto", SqlDbType.NVarChar);
                        cmd.Parameters["@foto"].Value = string.Empty;
                    }
                    else
                    {
                        cmd.Parameters.Add("@foto", SqlDbType.NVarChar);
                        cmd.Parameters["@foto"].Value = utilizador.FotoUrl;
                    }


                    if (utilizador.DataDeNascimento==null)
                    {
                        cmd.Parameters.Add("@data", SqlDbType.NVarChar);
                        cmd.Parameters["@data"].Value = DBNull.Value;
                    }
                    else
                    {
                        cmd.Parameters.Add("@data", SqlDbType.DateTime);
                        cmd.Parameters["@data"].Value = utilizador.DataDeNascimento;
                    }

                    if (utilizador.Nacionalidade==null)
                    {
                        cmd.Parameters.Add("@nacionalidade", SqlDbType.NVarChar);
                        cmd.Parameters["@nacionalidade"].Value = string.Empty;
                    }
                    else
                    {
                        cmd.Parameters.Add("@nacionalidade", SqlDbType.NVarChar);
                        cmd.Parameters["@nacionalidade"].Value = utilizador.Nacionalidade;
                    }

                    cmd.Parameters.Add("@permissoes", SqlDbType.Int);
                    cmd.Parameters["@permissoes"].Value = (int)utilizador.Permissoes;

                    cmd.Parameters.Add("@id", SqlDbType.NVarChar);
                    cmd.Parameters["@id"].Value = utilizador.UtilizadorId;

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
