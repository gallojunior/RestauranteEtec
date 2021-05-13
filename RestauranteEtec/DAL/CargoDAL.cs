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
    public class CargoDAL : IRepository<Cargo>
    {

        string connectionString = @"Server=localhost;port=3306;database=RestauranteEtec;uid=root;pwd=''";

        public void Add(Cargo model)
        {
            using (MySqlConnection conexao = new MySqlConnection(connectionString))
            {
                var sql = "insert into Cargo(Nome) values (@Nome)";
                MySqlCommand comando = new MySqlCommand(sql);
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
                var sql = "delete from Cargo where Id = @Id";
                MySqlCommand comando = new MySqlCommand(sql);
                comando.CommandType = CommandType.Text;
                
                comando.Parameters.AddWithValue("@Id", id);

                conexao.Open();
                comando.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public IEnumerable<Cargo> GetAll()
        {
            List<Cargo> cargos = new List<Cargo>();
            using (MySqlConnection conexao = new MySqlConnection(connectionString))
            {
                MySqlCommand comando = new MySqlCommand("select * from Cargo", conexao);
                comando.CommandType = CommandType.Text;

                conexao.Open();
                MySqlDataReader leitor = comando.ExecuteReader();
                while (leitor.Read())
                {
                    Cargo cargo = new Cargo()
                    {
                        Id = Convert.ToInt32(leitor["Id"]),
                        Nome = leitor["Nome"].ToString()
                    };
                    cargos.Add(cargo);
                }
                conexao.Close();
            }
            return cargos;
        }

        public Cargo GetById(int? id)
        {
            Cargo cargo = new Cargo();
            using (MySqlConnection conexao = new MySqlConnection(connectionString))
            {
                MySqlCommand comando = new MySqlCommand("select * from Cargo where Id = @Id", conexao);
                comando.CommandType = CommandType.Text;

                conexao.Open();
                MySqlDataReader leitor = comando.ExecuteReader();
                leitor.Read();
                if (!leitor.HasRows)
                {
                    conexao.Close();
                    return null;
                }
                cargo.Id = Convert.ToInt32(leitor["Id"]);
                cargo.Nome = leitor["Nome"].ToString();
                conexao.Close();
            }
            return cargo;
        }

        public void Update(Cargo model)
        {
            using (MySqlConnection conexao = new MySqlConnection(connectionString))
            {
                var sql = "update Cargo set" +
                          "  Nome = @Nome" +
                          "where Id = @Id";
                MySqlCommand comando = new MySqlCommand(sql);
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
