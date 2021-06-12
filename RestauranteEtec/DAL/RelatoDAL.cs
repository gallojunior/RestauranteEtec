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
    public class RelatoDAL : IRepository<Relato>
    {
        string connectionString = @"Server=localhost;port=3306;database=RestauranteEtec;uid=root;pwd=''";

        public void Add(Relato model)
        {
            using (MySqlConnection conexao = new MySqlConnection(connectionString))
            {
                var sql = "insert into Relato(Texto, NomePessoa, FotoPessoa, TipoPessoa, DataCadastro, ExibirHome, OrdemExibicao, Ativo)" +
                    " values (@Texto, @NomePessoa, @FotoPessoa, @TipoPessoa, @DataCadastro, @ExibirHome, @OrdemExibicao, @Ativo)";
                MySqlCommand comando = new MySqlCommand(sql, conexao);
                comando.CommandType = CommandType.Text;
                comando.Parameters.AddWithValue("@Texto", model.Texto);
                comando.Parameters.AddWithValue("@NomePessoa", model.NomePessoa);
                comando.Parameters.AddWithValue("@FotoPessoa", model.FotoPessoa);
                comando.Parameters.AddWithValue("@TipoPessoa", model.TipoPessoa);
                comando.Parameters.AddWithValue("@DataCadastro", DateTime.Now);
                comando.Parameters.AddWithValue("@ExibirHome", model.ExibirHome);
                comando.Parameters.AddWithValue("@OrdemExibicao", model.OrdemExibicao);
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
                var sql = "delete from Relato where Id = @Id";
                MySqlCommand comando = new MySqlCommand(sql, conexao);
                comando.CommandType = CommandType.Text;

                comando.Parameters.AddWithValue("@Id", id);

                conexao.Open();
                comando.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public IEnumerable<Relato> GetAll()
        {
            List<Relato> relatos = new List<Relato>();
            using (MySqlConnection conexao = new MySqlConnection(connectionString))
            {
                var sql = "select * from Relato";
                MySqlCommand comando = new MySqlCommand(sql, conexao);
                comando.CommandType = CommandType.Text;

                conexao.Open();
                MySqlDataReader leitor = comando.ExecuteReader();
                while (leitor.Read())
                {
                    Relato relato = new Relato()
                    {
                        Id = Convert.ToInt32(leitor["Id"]),
                        Texto = leitor["Texto"].ToString(),
                        NomePessoa = leitor["NomePessoa"].ToString(),
                        FotoPessoa = leitor["FotoPessoa"].ToString(),
                        TipoPessoa = leitor["TipoPessoa"].ToString(),
                        DataCadastro = Convert.ToDateTime(leitor["DataCadastro"]),
                        ExibirHome = Convert.ToByte(leitor["ExibirHome"]) == 1,
                        OrdemExibicao = Convert.ToByte(leitor["OrdemExibicao"]),
                        Ativo = Convert.ToByte(leitor["Ativo"]) == 1
                    };
                    relatos.Add(relato);
                }
                conexao.Close();
            }
            return relatos;
        }

        public Relato GetById(int? id)
        {
            Relato relato = new Relato();
            using (MySqlConnection conexao = new MySqlConnection(connectionString))
            {
                var sql = "select * from Relato";
                MySqlCommand comando = new MySqlCommand(sql, conexao);
                comando.CommandType = CommandType.Text;

                conexao.Open();
                MySqlDataReader leitor = comando.ExecuteReader();
                if (!leitor.HasRows)
                {
                    conexao.Close();
                    return null;
                }
                leitor.Read();
                relato.Id = Convert.ToInt32(leitor["Id"]);
                relato.Texto = leitor["Texto"].ToString();
                relato.NomePessoa = leitor["NomePessoa"].ToString();
                relato.FotoPessoa = leitor["FotoPessoa"].ToString();
                relato.TipoPessoa = leitor["TipoPessoa"].ToString();
                relato.DataCadastro = Convert.ToDateTime(leitor["DataCadastro"]);
                relato.ExibirHome = Convert.ToByte(leitor["ExibirHome"]) == 1;
                relato.OrdemExibicao = Convert.ToByte(leitor["OrdemExibicao"]);
                relato.Ativo = Convert.ToByte(leitor["Ativo"]) == 1;
                conexao.Close();
            }
            return relato;
        }

        public void Update(Relato model)
        {
            using (MySqlConnection conexao = new MySqlConnection(connectionString))
            {
                var sql = "update Relato set" +
                    " Texto = @Texto," +
                    " NomePessoa = @NomePessoa," +
                    " FotoPessoa = @FotoPessoa," +
                    " TipoPessoa = @TipoPessoa," +
                    " ExibirHome = @ExibirHome," +
                    " OrdemExibicao = @OrdemExibicao," +
                    " Ativo = @Ativo" +
                    " where Id = @Id";
                MySqlCommand comando = new MySqlCommand(sql, conexao);
                comando.CommandType = CommandType.Text;
                comando.Parameters.AddWithValue("@Id", model.Id);
                comando.Parameters.AddWithValue("@Texto", model.Texto);
                comando.Parameters.AddWithValue("@NomePessoa", model.NomePessoa);
                comando.Parameters.AddWithValue("@FotoPessoa", model.FotoPessoa);
                comando.Parameters.AddWithValue("@TipoPessoa", model.TipoPessoa);
                comando.Parameters.AddWithValue("@ExibirHome", model.ExibirHome);
                comando.Parameters.AddWithValue("@OrdemExibicao", model.OrdemExibicao);
                comando.Parameters.AddWithValue("@Ativo", model.Ativo);

                conexao.Open();
                comando.ExecuteNonQuery();
                conexao.Close();
            }
        }
    }
}
