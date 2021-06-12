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
    public class ProdutoDAL : IRepository<Produto>
    {
        string connectionString = @"Server=localhost;port=3306;database=RestauranteEtec;uid=root;pwd=''";

        public void Add(Produto model)
        {
            using (MySqlConnection conexao = new MySqlConnection(connectionString))
            {
                var sql = "insert into Produto(Nome, Descricao, Preco, CategoriaId, Foto, ExibirHome, Ativo)" +
                    " values (@Nome, @Descricao, @Preco, @CategoriaId, @Foto, @ExibirHome, @Ativo)";
                MySqlCommand comando = new MySqlCommand(sql, conexao);
                comando.CommandType = CommandType.Text;
                comando.Parameters.AddWithValue("@Nome", model.Nome);
                comando.Parameters.AddWithValue("@Descricao", model.Descricao);
                comando.Parameters.AddWithValue("@Preco", model.Preco);
                comando.Parameters.AddWithValue("@CategoriId", model.CategoriaId);
                comando.Parameters.AddWithValue("@Foto", model.Foto);
                comando.Parameters.AddWithValue("@ExibirHome", model.ExibirHome);
                comando.Parameters.AddWithValue("@Ativo", model.Ativo);

                conexao.Open();
                comando.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public void Delete(int? id)
        {
            using (MySqlConnection conexao = new MySqlConnection(connectionString))
            {
                var sql = "delete from Produto where Id = @Id";
                MySqlCommand comando = new MySqlCommand(sql, conexao);
                comando.CommandType = CommandType.Text;

                comando.Parameters.AddWithValue("@Id", id);

                conexao.Open();
                comando.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public IEnumerable<Produto> GetAll()
        {
            List<Produto> produtos = new List<Produto>();
            CategoriaDAL categoria = new CategoriaDAL();
            using (MySqlConnection conexao = new MySqlConnection(connectionString))
            {
                var sql = "select * from Produto";
                MySqlCommand comando = new MySqlCommand(sql, conexao);
                comando.CommandType = CommandType.Text;

                conexao.Open();
                MySqlDataReader leitor = comando.ExecuteReader();
                while (leitor.Read())
                {
                    Produto produto = new Produto()
                    {
                        Id = Convert.ToInt32(leitor["Id"]),
                        Nome = leitor["Nome"].ToString(),
                        Descricao = leitor["Descricao"].ToString(),
                        Preco = Convert.ToDecimal(leitor["Preco"]),
                        CategoriaId = Convert.ToInt32(leitor["CategoriaId"]),
                        Foto = leitor["Foto"].ToString(),
                        ExibirHome = Convert.ToByte(leitor["ExibirHome"]) == 1,
                        Ativo = Convert.ToByte(leitor["Ativo"]) == 1
                    };
                    produto.Categoria = categoria.GetById(produto.CategoriaId);
                    produtos.Add(produto);
                }
                conexao.Close();
            }
            return produtos;
        }

        public Produto GetById(int? id)
        {
            Produto produto = new Produto();
            CategoriaDAL categoria = new CategoriaDAL();
            using (MySqlConnection conexao = new MySqlConnection(connectionString))
            {
                var sql = "select * from Produto";
                MySqlCommand comando = new MySqlCommand(sql, conexao);
                comando.CommandType = CommandType.Text;

                conexao.Open();
                MySqlDataReader leitor = comando.ExecuteReader();
                if (!leitor.HasRows)
                {
                    conexao.Close();
                    return null;
                }
                produto.Id = Convert.ToInt32(leitor["Id"]);
                produto.Nome = leitor["Nome"].ToString();
                produto.Descricao = leitor["Descricao"].ToString();
                produto.Preco = Convert.ToDecimal(leitor["Preco"]);
                produto.CategoriaId = Convert.ToInt32(leitor["CategoriaId"]);
                produto.Foto = leitor["Foto"].ToString();
                produto.ExibirHome = Convert.ToByte(leitor["ExibirHome"]) == 1;
                produto.Ativo = Convert.ToByte(leitor["Ativo"]) == 1;
                produto.Categoria = categoria.GetById(produto.CategoriaId);

                conexao.Close();
            }
            return produto;
        }

        public void Update(Produto model)
        {
            using (MySqlConnection conexao = new MySqlConnection(connectionString))
            {
                var sql = "update Produto set " +
                    " Nome = @Nome," +
                    " Descricao = @Descricao," +
                    " Preco = @Preco," +
                    " CategoriaId = @CategoriaId," +
                    " Foto = @Foto," +
                    " ExibirHome = @ExibirHome," +
                    " Ativo = @Ativo" +
                    " where Id = @Id";
                MySqlCommand comando = new MySqlCommand(sql, conexao);
                comando.CommandType = CommandType.Text;
                comando.Parameters.AddWithValue("@Id", model.Id);
                comando.Parameters.AddWithValue("@Nome", model.Nome);
                comando.Parameters.AddWithValue("@Descricao", model.Descricao);
                comando.Parameters.AddWithValue("@Preco", model.Preco);
                comando.Parameters.AddWithValue("@CategoriId", model.CategoriaId);
                comando.Parameters.AddWithValue("@Foto", model.Foto);
                comando.Parameters.AddWithValue("@ExibirHome", model.ExibirHome);
                comando.Parameters.AddWithValue("@Ativo", model.Ativo);

                conexao.Open();
                comando.ExecuteNonQuery();
                conexao.Close();
            }
        }
    }
}
