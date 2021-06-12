using MySql.Data.MySqlClient;
using RestauranteEtec.Interfaces;
using RestauranteEtec.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace RestauranteEtec.DAL
{
    public class CategoriaDAL : IRepository<Categoria>
    {
        string connectionString = @"Server=localhost;port=3306;database=RestauranteEtec;uid=root;pwd=''";

        public void Add(Categoria model)
        {
            using (MySqlConnection conexao = new MySqlConnection(connectionString))
            {
                var sql = "insert into Categoria(Nome) values (@Nome)";
                MySqlCommand comando = new MySqlCommand(sql, conexao);
                comando.CommandType = CommandType.Text;
                comando.Parameters.AddWithValue("@Nome", model.Nome);

                conexao.Open();
                comando.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public void Delete(int? id)
        {
            using (MySqlConnection conexao = new MySqlConnection(connectionString))
            {
                var sql = "delete from Categoria where Id = @Id";
                MySqlCommand comando = new MySqlCommand(sql, conexao);
                comando.CommandType = CommandType.Text;

                comando.Parameters.AddWithValue("@Id", id);

                conexao.Open();
                comando.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public IEnumerable<Categoria> GetAll()
        {
            List<Categoria> categorias = new List<Categoria>();
            using (MySqlConnection conexao = new MySqlConnection(connectionString))
            {
                MySqlCommand comando = new MySqlCommand("select * from Categoria", conexao);
                comando.CommandType = CommandType.Text;
                conexao.Open();
                MySqlDataReader leitor = comando.ExecuteReader();
                while (leitor.Read())
                {
                    Categoria categoria = new Categoria()
                    {
                        Id = Convert.ToInt32(leitor["Id"]),
                        Nome = leitor["Nome"].ToString()
                    };
                    categorias.Add(categoria);
                }
                conexao.Close();
            }
            return categorias;
        }

        public Categoria GetById(int? id)
        {
            Categoria categoria = new Categoria();
            using (MySqlConnection conexao = new MySqlConnection(connectionString))
            {
                MySqlCommand comando = new MySqlCommand("select * from Categoria where Id = @Id", conexao);
                comando.CommandType = CommandType.Text;
                comando.Parameters.AddWithValue("@Id", id);

                conexao.Open();
                MySqlDataReader leitor = comando.ExecuteReader();
                leitor.Read();
                if (!leitor.HasRows)
                {
                    conexao.Close();
                    return null;
                }
                categoria.Id = Convert.ToInt32(leitor["Id"]);
                categoria.Nome = leitor["Nome"].ToString();
                conexao.Close();
            }
            return categoria;
        }

        public void Update(Categoria model)
        {
            using (MySqlConnection conexao = new MySqlConnection(connectionString))
            {
                var sql = "update Categoria set" +
                          "  Nome = @Nome" +
                          " where Id = @Id";
                MySqlCommand comando = new MySqlCommand(sql, conexao);
                comando.CommandType = CommandType.Text;

                comando.Parameters.AddWithValue("@Id", model.Id);
                comando.Parameters.AddWithValue("@Nome", model.Nome);

                conexao.Open();
                comando.ExecuteNonQuery();
                conexao.Close();
            }
        }
    }
}
