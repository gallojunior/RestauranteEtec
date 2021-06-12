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
    public class BlogDAL : IRepository<Blog>
    {
        string connectionString = @"Server=localhost;port=3306;database=RestauranteEtec;uid=root;pwd=''";

        public void Add(Blog model)
        {
            using (MySqlConnection conexao = new MySqlConnection(connectionString))
            {
                var sql = "insert into Blog(Titulo, Texto, DataCadastro, Imagem) values (@Titulo, Texto, DataCadastro, Imagem)";
                MySqlCommand comando = new MySqlCommand(sql, conexao);
                comando.CommandType = CommandType.Text;
                comando.Parameters.AddWithValue("@Titulo", model.Titulo);
                comando.Parameters.AddWithValue("@Texto", model.Texto);
                comando.Parameters.AddWithValue("@DataCadastro", DateTime.Now);
                comando.Parameters.AddWithValue("@Imagem", model.Imagem);

                conexao.Open();
                comando.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public void Delete(int? id)
        {
            using (MySqlConnection conexao = new MySqlConnection(connectionString))
            {
                var sql = "delete from Blog where Id = @Id";
                MySqlCommand comando = new MySqlCommand(sql, conexao);
                comando.CommandType = CommandType.Text;

                comando.Parameters.AddWithValue("@Id", id);

                conexao.Open();
                comando.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public IEnumerable<Blog> GetAll()
        {
            List<Blog> blogs = new List<Blog>();
            using (MySqlConnection conexao = new MySqlConnection(connectionString))
            {
                MySqlCommand comando = new MySqlCommand("select * from Blog", conexao);
                comando.CommandType = CommandType.Text;
                conexao.Open();
                MySqlDataReader leitor = comando.ExecuteReader();
                while (leitor.Read())
                {
                    Blog blog = new Blog()
                    {
                        Id = Convert.ToInt32(leitor["Id"]),
                        Titulo = leitor["Titulo"].ToString(),
                        Texto = leitor["Texto"].ToString(),
                        DataCadastro = Convert.ToDateTime(leitor["DataCadastro"]),
                        Imagem = leitor["Imagem"].ToString()
                    };
                    blogs.Add(blog);
                }
                conexao.Close();
            }
            return blogs;
        }

        public Blog GetById(int? id)
        {
            Blog blog = new Blog();
            using (MySqlConnection conexao = new MySqlConnection(connectionString))
            {
                MySqlCommand comando = new MySqlCommand("select * from Blog", conexao);
                comando.CommandType = CommandType.Text;
                conexao.Open();
                MySqlDataReader leitor = comando.ExecuteReader();
                if (!leitor.HasRows)
                {
                    conexao.Close();
                    return null;
                }
                leitor.Read();
                blog.Id = Convert.ToInt32(leitor["Id"]);
                blog.Titulo = leitor["Titulo"].ToString();
                blog.Texto = leitor["Texto"].ToString();
                blog.DataCadastro = Convert.ToDateTime(leitor["DataCadastro"]);
                blog.Imagem = leitor["Imagem"].ToString();

                conexao.Close();
            }
            return blog;
        }

        public void Update(Blog model)
        {
            using (MySqlConnection conexao = new MySqlConnection(connectionString))
            {
                var sql = "update Blog set" +
                    " Titulo = @Titulo," +
                    " Texto = @Texto," +
                    " Imagem = @Imagem" +
                    " where Id = @Id)";
                MySqlCommand comando = new MySqlCommand(sql, conexao);
                comando.CommandType = CommandType.Text;
                comando.Parameters.AddWithValue("@Id", model.Id);
                comando.Parameters.AddWithValue("@Titulo", model.Titulo);
                comando.Parameters.AddWithValue("@Texto", model.Texto);
                comando.Parameters.AddWithValue("@Imagem", model.Imagem);

                conexao.Open();
                comando.ExecuteNonQuery();
                conexao.Close();
            }
        }
    }
}
