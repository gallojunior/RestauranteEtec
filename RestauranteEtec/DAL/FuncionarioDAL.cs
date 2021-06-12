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
    public class FuncionarioDAL : IRepository<Funcionario>
    {
        string connectionString = @"Server=localhost;port=3306;database=RestauranteEtec;uid=root;pwd=''";

        public void Add(Funcionario model)
        {
            using (MySqlConnection conexao = new MySqlConnection(connectionString))
            {
                var sql = "insert into Funcionario(Nome, Descricao, Foto, CargoId, ExibirHome, OrdemExibicao, Ativo)" +
                    " values (@Nome, @Descricao, @Foto, @CargoId, @ExibirHome, @OrdemExibicao, @Ativo)";
                MySqlCommand comando = new MySqlCommand(sql, conexao);
                comando.CommandType = CommandType.Text;
                comando.Parameters.AddWithValue("@Nome", model.Nome);
                comando.Parameters.AddWithValue("@Descricao", model.Descricao);
                comando.Parameters.AddWithValue("@Foto", model.Foto);
                comando.Parameters.AddWithValue("@CargoId", model.CargoId);
                comando.Parameters.AddWithValue("@ExibirHome", model.ExibirHome);
                comando.Parameters.AddWithValue("@OrdemExibicao", model.OrdemExibicao);
                comando.Parameters.AddWithValue("@Ativo", true);

                conexao.Open();
                comando.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public void Delete(int? id)
        {
            using (MySqlConnection conexao = new MySqlConnection(connectionString))
            {
                var sql = "delete from Funcionario where Id = @Id";
                MySqlCommand comando = new MySqlCommand(sql, conexao);
                comando.CommandType = CommandType.Text;
                comando.Parameters.AddWithValue("@Id", id);

                conexao.Open();
                comando.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public IEnumerable<Funcionario> GetAll()
        {
            List<Funcionario> funcionarios = new List<Funcionario>();
            CargoDAL cargo = new CargoDAL();
            using (MySqlConnection conexao = new MySqlConnection(connectionString))
            {
                MySqlCommand comando = new MySqlCommand("select * from funcionario", conexao);
                comando.CommandType = CommandType.Text;

                conexao.Open();
                MySqlDataReader leitor = comando.ExecuteReader();
                while (leitor.Read())
                {
                    Funcionario funcionario = new Funcionario()
                    {
                        Id = Convert.ToInt32(leitor["Id"]),
                        Nome = leitor["Nome"].ToString(),
                        Descricao = leitor["Descricao"].ToString(),
                        Foto = leitor["Foto"].ToString(),
                        CargoId = Convert.ToInt32(leitor["CargoId"].ToString()),
                        ExibirHome = Convert.ToByte(leitor["ExibirHome"]) == 1,
                        OrdemExibicao = Convert.ToByte(leitor["OrdemExibicao"]),
                        Ativo = Convert.ToInt32(leitor["Ativo"]) == 1
                    };
                    funcionario.Cargo = cargo.GetById(funcionario.CargoId);

                    funcionarios.Add(funcionario);
                }
                conexao.Close();
            }
            return funcionarios;
        }

        public Funcionario GetById(int? id)
        {
            CargoDAL cargo = new CargoDAL();
            Funcionario funcionario = new Funcionario();
            using (MySqlConnection conexao = new MySqlConnection(connectionString))
            {
                MySqlCommand comando = new MySqlCommand("select * from Funcionario where Id = @Id", conexao);
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
                funcionario.Id = Convert.ToInt32(leitor["Id"]);
                funcionario.Nome = leitor["Nome"].ToString();
                funcionario.Descricao = leitor["Descricao"].ToString();
                funcionario.Foto = leitor["Foto"].ToString();
                funcionario.CargoId = Convert.ToInt32(leitor["CargoId"].ToString());
                funcionario.ExibirHome = Convert.ToInt32(leitor["ExibirHome"]) == 1;
                funcionario.OrdemExibicao = Convert.ToByte(leitor["OrdemExibicao"]);
                funcionario.Ativo = Convert.ToInt32(leitor["Ativo"]) == 1;
                funcionario.Cargo = cargo.GetById(funcionario.CargoId);
                conexao.Close();
            }
            return funcionario;
        }

        public void Update(Funcionario model)
        {
            using (MySqlConnection conexao = new MySqlConnection(connectionString))
            {
                var sql = "update Funcionario set" +
                          "  Nome = @Nome," +
                          "  Descricao = @Descricao," +
                          "  Foto = @Foto," +
                          "  CargoId = @CargoId," +
                          "  ExibirHome = @ExibirHome," +
                          "  OrdemExibicao = @OrdemExibicao," +
                          "  Ativo = @Ativo" +
                          " where Id = @Id";
                MySqlCommand comando = new MySqlCommand(sql, conexao);
                comando.CommandType = CommandType.Text;

                comando.Parameters.AddWithValue("@Id", model.Id);
                comando.Parameters.AddWithValue("@Nome", model.Nome);
                comando.Parameters.AddWithValue("@Descricao", model.Descricao);
                comando.Parameters.AddWithValue("@Foto", model.Foto);
                comando.Parameters.AddWithValue("@CargoId", model.CargoId);
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
