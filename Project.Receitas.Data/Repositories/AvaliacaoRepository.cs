using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Project.Receitas.Data.Interfaces;
using Projecto.Receitas.Domain.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Project.Receitas.Data.Repositories
{
    public class AvaliacaoRepository : IAvaliacaoRepository
    {
        private static IConfiguration _configuration;

        public AvaliacaoRepository(IConfiguration configuration) 
        {
            _configuration = configuration;
        }

        public bool Add(Avaliacao avaliacao)
        {
            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = " INSERT INTO Avaliacoes (ReceitaId,UtilizadorId,Valor) " +
                                   " VALUES (@receitaId,@utilizadorId,@valor); ";

                    cmd.CommandText = query;
                    cmd.Connection = conexao;

                    cmd.Parameters.Add("@receitaId", SqlDbType.Int);
                    cmd.Parameters["@receitaId"].Value = avaliacao.Receita.ReceitaId;
                    cmd.Parameters.Add("@utilizadorId", SqlDbType.Int);
                    cmd.Parameters["@utilizadorId"].Value = avaliacao.Utilizador.UtilizadorId;
                    cmd.Parameters.Add("@valor", SqlDbType.Int);
                    cmd.Parameters["@valor"].Value = avaliacao.Valor;

                    if (conexao.State != ConnectionState.Open)
                        conexao.Open();

                    int affectedRows = cmd.ExecuteNonQuery();

                    return affectedRows == 1;
                }
            }
        }

        public bool Delete(Avaliacao avaliacao)
        {
            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = " DELETE FROM Avaliacoes WHERE ReceitaId=@receitaId AND UtilizadorId=@utilizadorId;";

                    cmd.CommandText = query;
                    cmd.Connection = conexao;

                    cmd.Parameters.Add("@receitaId", SqlDbType.Int);
                    cmd.Parameters["@receitaId"].Value = avaliacao.Receita.ReceitaId;
                    cmd.Parameters.Add("@utilizadorId", SqlDbType.Int);
                    cmd.Parameters["@utilizadorId"].Value = avaliacao.Utilizador.UtilizadorId;

                    if (conexao.State != ConnectionState.Open)
                        conexao.Open();

                    int affectedRows = cmd.ExecuteNonQuery();

                    return affectedRows == 1;
                }
            }
        }

        public Avaliacao GetByPrimaryKey(int idReceita,int idUser)
        {
            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = "SELECT * FROM Avaliacoes WHERE ReceitaId=@receitaId AND UtilizadorId=@utilizadorId;";

                    cmd.CommandText = query;
                    cmd.Connection = conexao;

                    cmd.Parameters.Add("@receitaId", SqlDbType.Int);
                    cmd.Parameters["@receitaId"].Value = idReceita; 

                    cmd.Parameters.Add("@utilizadorId", SqlDbType.Int);
                    cmd.Parameters["@utilizadorId"].Value = idUser;

                    if (conexao.State != ConnectionState.Open)
                    {
                        conexao.Open();
                    }

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    { 
                        Avaliacao avaliacaoARetornar = new Avaliacao(Convert.ToInt32(reader["Valor"]),
                                                                      new Receita(Convert.ToInt32(reader["ReceitaId"])),
                                                                      new Utilizador(Convert.ToInt32(reader["UtilizadorId"])));

                        return avaliacaoARetornar;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public List<Avaliacao> GetByReceitaId(int id)
        {
            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = "SELECT * FROM Avaliacoes WHERE ReceitaId=@Id;";

                    cmd.CommandText = query;
                    cmd.Connection = conexao;

                    cmd.Parameters.Add("@Id", SqlDbType.Int);
                    cmd.Parameters["@Id"].Value =id;

                    if (conexao.State != ConnectionState.Open)
                    {
                        conexao.Open();
                    }

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Avaliacao> avaliacoes = new List<Avaliacao>();

                    while (reader.Read())
                    {
                        Avaliacao avaliacao = new Avaliacao(Convert.ToInt32(reader["Valor"]),
                                                                      new Receita(Convert.ToInt32(reader["ReceitaId"])),
                                                                      new Utilizador(Convert.ToInt32(reader["UtilizadorId"])));

                        avaliacoes.Add(avaliacao);
                    }
                    
                    return avaliacoes;
                }
            }
        }

        public List<Avaliacao> GetByUtilizadorId(int id)
        {
            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = "SELECT * FROM Avaliacoes WHERE UtilizadorId=@Id;";

                    cmd.CommandText = query;
                    cmd.Connection = conexao;

                    cmd.Parameters.Add("@Id", SqlDbType.Int);
                    cmd.Parameters["@Id"].Value =id;

                    if (conexao.State != ConnectionState.Open)
                    {
                        conexao.Open();
                    }

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Avaliacao> avaliacoes = new List<Avaliacao>();

                    while (reader.Read())
                    {
                        Avaliacao avaliacao = new Avaliacao(Convert.ToInt32(reader["Valor"]),
                                                                      new Receita(Convert.ToInt32(reader["ReceitaId"])),
                                                                      new Utilizador(Convert.ToInt32(reader["UtilizadorId"])));

                        avaliacoes.Add(avaliacao);
                    }

                    return avaliacoes;
                }
            }
        }

        public List<Receita> GetTop3Raking()
        {
            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = " WITH ValueCounts AS( "+

                                   " SELECT ReceitaId, SUM(CASE WHEN valor = 5 THEN 1 ELSE 0 END) AS count_5,"+
                                   " SUM(CASE WHEN valor = 4 THEN 1 ELSE 0 END) AS count_4, " +
                                   " SUM(CASE WHEN valor = 3 THEN 1 ELSE 0 END) AS count_3, " +
                                   " SUM(CASE WHEN valor = 2 THEN 1 ELSE 0 END) AS count_2, " +
                                   " SUM(CASE WHEN valor = 1 THEN 1 ELSE 0 END) AS count_1 " +
                                   " FROM Avaliacoes GROUP BY ReceitaId),"+
                                   " RankedIDs AS("+
                                   " SELECT ReceitaId, count_5, count_4, count_3, count_2, count_1," +
                                   " ROW_NUMBER() OVER (ORDER BY count_5 DESC, count_4 DESC, count_3 " +
                                   " DESC, count_2 DESC, count_1 DESC) AS ranking FROM ValueCounts) " +
                                   " SELECT RankedIDs.ReceitaId, count_5, count_4, count_3, count_2, count_1,* "+
                                   " FROM RankedIDs INNER JOIN Receitas On RankedIDs.ReceitaId= Receitas.ReceitaId " +
                                   " WHERE ranking <= 3 ORDER BY ranking; ";

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

        public double MediaDoValorByReceita(int id)
        {
            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = " SELECT AVG(Valor) FROM Avaliacoes WHERE ReceitaId=@id";

                    cmd.CommandText = query;
                    cmd.Connection = conexao;

                    cmd.Parameters.Add("@id", SqlDbType.Int);
                    cmd.Parameters["@id"].Value = id;

                    if (conexao.State != ConnectionState.Open)
                        conexao.Open();

                    if (cmd.ExecuteScalar().ToString()!=string.Empty) 
                    {
                        return double.Parse(cmd.ExecuteScalar().ToString());
                    }
                    else 
                    {
                        return 0;
                    }
                }
            }
        }

        public bool Update(Avaliacao avaliacao)
        {
            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = "UPDATE Avaliacoes SET Valor=@valor " +
                                   " WHERE ReceitaId=@receitaId AND UtilizadorId=@utilizadorId;";

                    cmd.CommandText = query;
                    cmd.Connection = conexao;

                    cmd.Parameters.Add("@valor", SqlDbType.Int);
                    cmd.Parameters["@valor"].Value = avaliacao.Valor;
                    cmd.Parameters.Add("@receitaId", SqlDbType.Int);
                    cmd.Parameters["@receitaId"].Value = avaliacao.Receita.ReceitaId;
                    cmd.Parameters.Add("@utilizadorId", SqlDbType.Int);
                    cmd.Parameters["@utilizadorId"].Value = avaliacao.Utilizador.UtilizadorId;
                    
                    if (conexao.State != ConnectionState.Open)
                        conexao.Open();

                    int affectedRows = cmd.ExecuteNonQuery();

                    return affectedRows == 1;
                }
            }
        }
    }
}
