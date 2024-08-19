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
    public class ItemRepository : IItemRepository
    {
        private static IConfiguration _configuration;

        public ItemRepository(IConfiguration configuration) 
        {
            _configuration = configuration;
        }
        public bool Add(Item item)
        {
            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = "INSERT INTO Itens (Quantidade,MedidaId,ReceitaId,IngredienteId) " +
                                   " VALUES (@quantidade,@medidaId,@receitaId,@ingredienteId)";

                    cmd.CommandText = query;
                    cmd.Connection = conexao;

                    cmd.Parameters.Add("@quantidade",SqlDbType.Float);
                    cmd.Parameters["@quantidade"].Value = item.Quantidade;
                    cmd.Parameters.Add("@medidaId", SqlDbType.Int);
                    cmd.Parameters["@medidaId"].Value = item.Medida.MedidaId;
                    cmd.Parameters.Add("@receitaId", SqlDbType.Float);
                    cmd.Parameters["@receitaId"].Value = item.Receita.ReceitaId;
                    cmd.Parameters.Add("@ingredienteId", SqlDbType.Float);
                    cmd.Parameters["@ingredienteId"].Value = item.Ingrediente.IngredienteId;

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
                    string query = "DELETE FROM Itens WHERE ItemId=@id";

                    cmd.CommandText = query;
                    cmd.Connection = conexao;

                    cmd.Parameters.Add("@id",SqlDbType.Int);
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

        public bool Delete(Item item)
        {
            return Delete(item.ItemId);
        }

        public List<Item> GetAll()
        {
            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = "SELECT * FROM Itens;";

                    cmd.CommandText = query;
                    cmd.Connection = conexao;

                    if (conexao.State != ConnectionState.Open)
                    {
                        conexao.Open();
                    }

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Item> Items = new List<Item>();

                    while (reader.Read()) 
                    {
                        Item item = new Item(Convert.ToInt32(reader["ItemId"]), float.Parse(reader["Quantidade"].ToString()),
                                            new Medida(Convert.ToInt32(reader["MedidaId"])),
                                            new Receita(Convert.ToInt32(reader["ReceitaId"])),
                                            new Ingrediente(Convert.ToInt32(reader["IngredienteId"])));

                        Items.Add(item);    
                    }

                    return Items;
                }
            }
        }

        public Item GetById(int id)
        {
            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = "SELECT * FROM Itens WHERE ItemId=@id";

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
                        Item item = new Item(Convert.ToInt32(reader["ItemId"]), float.Parse(reader["Quantidade"].ToString()),
                                            new Medida(Convert.ToInt32(reader["MedidaId"])),
                                            new Receita(Convert.ToInt32(reader["ReceitaId"])),
                                            new Ingrediente(Convert.ToInt32(reader["IngredienteId"])));

                        return item;
                    }
                    else 
                    {
                        return null;
                    }
                }
            }
        }

        public List<Item> GetByIngredienteId(int id)
        {
            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = "SELECT * FROM Itens WHERE IngredienteId=@id";

                    cmd.CommandText = query;
                    cmd.Connection = conexao;

                    cmd.Parameters.Add("@id", SqlDbType.Int);
                    cmd.Parameters["@id"].Value = id;

                    if (conexao.State != ConnectionState.Open)
                    {
                        conexao.Open();
                    }

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Item> Items = new List<Item>();

                    while (reader.Read())
                    {
                        Item item = new Item(Convert.ToInt32(reader["ItemId"]), float.Parse(reader["Quantidade"].ToString()),
                                            new Medida(Convert.ToInt32(reader["MedidaId"])),
                                            new Receita(Convert.ToInt32(reader["ReceitaId"])),
                                            new Ingrediente(Convert.ToInt32(reader["IngredienteId"])));

                        Items.Add(item);
                    }

                    return Items;
                }
            }
        }

        public List<Item> GetByMedidaId(int id)
        {
            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = "SELECT * FROM Itens WHERE MedidaId=@id";

                    cmd.CommandText = query;
                    cmd.Connection = conexao;

                    cmd.Parameters.Add("@id", SqlDbType.Int);
                    cmd.Parameters["@id"].Value = id;

                    if (conexao.State != ConnectionState.Open)
                    {
                        conexao.Open();
                    }

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Item> Items = new List<Item>();

                    while (reader.Read())
                    {
                        Item item = new Item(Convert.ToInt32(reader["ItemId"]), float.Parse(reader["Quantidade"].ToString()),
                                            new Medida(Convert.ToInt32(reader["MedidaId"])),
                                            new Receita(Convert.ToInt32(reader["ReceitaId"])),
                                            new Ingrediente(Convert.ToInt32(reader["IngredienteId"])));

                        Items.Add(item);
                    }

                    return Items;
                }
            }
        }

        public List<Item> GetByReceitaId(int id)
        {
            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = "SELECT * FROM Itens WHERE ReceitaId=@id";

                    cmd.CommandText = query;
                    cmd.Connection = conexao;

                    cmd.Parameters.Add("@id",SqlDbType.Int);
                    cmd.Parameters["@id"].Value = id;

                    if (conexao.State != ConnectionState.Open)
                    {
                        conexao.Open();
                    }

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Item> Items = new List<Item>();

                    while (reader.Read())
                    {
                        Item item = new Item(Convert.ToInt32(reader["ItemId"]), float.Parse(reader["Quantidade"].ToString()),
                                            new Medida(Convert.ToInt32(reader["MedidaId"])),
                                            new Receita(Convert.ToInt32(reader["ReceitaId"])),
                                            new Ingrediente(Convert.ToInt32(reader["IngredienteId"])));

                        Items.Add(item);
                    }

                    return Items;
                }
            }
        }

        public bool Update(Item item)
        {
            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = " UPDATE Itens SET Quantidade=@quantidade, MedidaId=@medidaId, ReceitaId=@receitaId, " +
                                   " IngredienteId = @ingredienteId WHERE ItemId = @itemId;";

                    cmd.CommandText = query;
                    cmd.Connection = conexao;

                    cmd.Parameters.Add("@quantidade", SqlDbType.Float);
                    cmd.Parameters["@quantidade"].Value = item.Quantidade;
                    cmd.Parameters.Add("@medidaId", SqlDbType.Int);
                    cmd.Parameters["@medidaId"].Value = item.Medida.MedidaId;
                    cmd.Parameters.Add("@receitaId", SqlDbType.Int);
                    cmd.Parameters["@receitaId"].Value = item.Receita.ReceitaId;
                    cmd.Parameters.Add("@ingredienteId", SqlDbType.Int);
                    cmd.Parameters["@ingredienteId"].Value = item.Ingrediente.IngredienteId;
                    cmd.Parameters.Add("@itemId", SqlDbType.Int);
                    cmd.Parameters["@itemId"].Value = item.ItemId;

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
