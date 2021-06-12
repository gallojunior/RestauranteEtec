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
    public class ContatoDAL : IRepository<Contato>
    {
        string connectionString = @"Server=localhost;port=3306;database=RestauranteEtec;uid=root;pwd=''";

        public void Add(Contato model)
        {
            using (MySqlConnection conexao = new MySqlConnection(connectionString))
            {
                var sql = "insert into Contato(DataCadastro, NomePessoa, EmailPessoa, Assunto, Mensagem, Status, Retorno)" +
                    " values (@DataCadastro, @NomePessoa, @EmailPessoa, @Assunto, @Mensagem, @Status, @Retorno)";
                MySqlCommand comando = new MySqlCommand(sql, conexao);
                comando.CommandType = CommandType.Text;
                comando.Parameters.AddWithValue("@DataCadastro", DateTime.Now);
                comando.Parameters.AddWithValue("@NomePessoa", model.NomePessoa);
                comando.Parameters.AddWithValue("@EmailPessoa", model.EmailPessoa);
                comando.Parameters.AddWithValue("@Assunto", model.Mensagem);
                comando.Parameters.AddWithValue("@Mensagem", model.Mensagem);
                comando.Parameters.AddWithValue("@Status", model.Status);
                comando.Parameters.AddWithValue("@Retorno", model.Retorno);

                conexao.Open();
                comando.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public void Delete(int? id)
        {
            using (MySqlConnection conexao = new MySqlConnection(connectionString))
            {
                var sql = "delete from Contato where Id = @Id";
                MySqlCommand comando = new MySqlCommand(sql, conexao);
                comando.CommandType = CommandType.Text;

                comando.Parameters.AddWithValue("@Id", id);

                conexao.Open();
                comando.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public IEnumerable<Contato> GetAll()
        {
            List<Contato> contatos = new List<Contato>();
            using (MySqlConnection conexao = new MySqlConnection(connectionString))
            {
                var sql = "select * from Contato";
                MySqlCommand comando = new MySqlCommand(sql, conexao);
                comando.CommandType = CommandType.Text;

                conexao.Open();
                MySqlDataReader leitor = comando.ExecuteReader();
                while (leitor.Read())
                {
                    Contato contato = new Contato()
                    {
                        Id = Convert.ToInt32(leitor["Id"]),
                        DataCadastro = Convert.ToDateTime(leitor["DataCadastro"]),
                        NomePessoa = leitor["NomePessoa"].ToString(),
                        EmailPessoa = leitor["EmailPessoa"].ToString(),
                        Assunto = leitor["Assunto"].ToString(),
                        Mensagem = leitor["Mensagem"].ToString(),
                        Status = Convert.ToByte(leitor["Status"]),
                        Retorno = leitor["Retorno"].ToString()
                    };
                    contatos.Add(contato);
                }
                conexao.Close();
            }
            return contatos;
        }

        public Contato GetById(int? id)
        {
            Contato contato = new Contato();
            using (MySqlConnection conexao = new MySqlConnection(connectionString))
            {
                var sql = "select * from Contato where Id = @Id";
                MySqlCommand comando = new MySqlCommand(sql, conexao);
                comando.CommandType = CommandType.Text;
                comando.Parameters.AddWithValue("@Id", id);

                conexao.Open();
                MySqlDataReader leitor = comando.ExecuteReader();
                if (!leitor.HasRows)
                {
                    conexao.Close();
                    return null;
                }
                contato.Id = Convert.ToInt32(leitor["Id"]);
                contato.DataCadastro = Convert.ToDateTime(leitor["DataCadastro"]);
                contato.NomePessoa = leitor["NomePessoa"].ToString();
                contato.EmailPessoa = leitor["EmailPessoa"].ToString();
                contato.Assunto = leitor["Assunto"].ToString();
                contato.Mensagem = leitor["Mensagem"].ToString();
                contato.Status = Convert.ToByte(leitor["Status"]);
                contato.Retorno = leitor["Retorno"].ToString();
                conexao.Close();
            }
            return contato;
        }

        public void Update(Contato model)
        {
            using (MySqlConnection conexao = new MySqlConnection(connectionString))
            {
                var sql = "update Contato set" +
                    " NomePessoa = @NomePessoa," +
                    " EmailPessoa = @EmailPessoa," +
                    " Assunto = @Assunto," +
                    " Mensagem = @Mensagem," +
                    " Status = @Status," +
                    " Retorno = @Retorno" +
                    " where Id = @Id";
                MySqlCommand comando = new MySqlCommand(sql, conexao);
                comando.CommandType = CommandType.Text;
                comando.Parameters.AddWithValue("@Id", model.Id);
                comando.Parameters.AddWithValue("@NomePessoa", model.NomePessoa);
                comando.Parameters.AddWithValue("@EmailPessoa", model.EmailPessoa);
                comando.Parameters.AddWithValue("@Assunto", model.Mensagem);
                comando.Parameters.AddWithValue("@Mensagem", model.Mensagem);
                comando.Parameters.AddWithValue("@Status", model.Status);
                comando.Parameters.AddWithValue("@Retorno", model.Retorno);

                conexao.Open();
                comando.ExecuteNonQuery();
                conexao.Close();
            }
        }
    }
}
