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
    public class ReservaDAL : IRepository<Reserva>
    {
        string stringConnection = @"server=localhost;port=3306;database=RestauranteEtec;uid=root;pwd=''";
        public void Add(Reserva model)
        {
            using (MySqlConnection conexao = new MySqlConnection(stringConnection))
            {
                var sql = "insert into Reserva(NomePessoa, EmailPessoa, FonePessoa, DataCadastro, DataReserva, Convidados, Status)" +
                   " values (@NomePessoa, @EmailPessoa, @FonePessoa, @DataCadastro, @DataReserva, @Convidados, @Status)";
                MySqlCommand comando = new MySqlCommand(sql, conexao);
                comando.CommandType = CommandType.Text;
                comando.Parameters.AddWithValue("@NomePessoa", model.NomePessoa);
                comando.Parameters.AddWithValue("@EmailPessoa", model.EmailPessoa);
                comando.Parameters.AddWithValue("@FonePessoa", model.FonePessoa);
                comando.Parameters.AddWithValue("DataCadastro", DateTime.Now);
                comando.Parameters.AddWithValue("DataReserva", model.DataReserva);
                comando.Parameters.AddWithValue("Convidados", model.Convidados);
                comando.Parameters.AddWithValue("Status", model.Status);

                conexao.Open();
                comando.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public void Delete(int? id)
        {
            using (MySqlConnection conexao = new MySqlConnection(stringConnection))
            {
                var sql = "delete from Reserva where Id = @Id";
                MySqlCommand comando = new MySqlCommand(sql, conexao);
                comando.CommandType = CommandType.Text;
                comando.Parameters.AddWithValue("@Id", id);

                conexao.Open();
                comando.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public IEnumerable<Reserva> GetAll()
        {
            List<Reserva> reservas = new List<Reserva>();
            using (MySqlConnection conexao = new MySqlConnection(stringConnection))
            {
                var sql = "select * from Reserva";
                MySqlCommand comando = new MySqlCommand(sql, conexao);
                comando.CommandType = CommandType.Text;

                conexao.Open();
                MySqlDataReader leitor = comando.ExecuteReader();
                while (leitor.Read())
                {
                    Reserva reserva = new Reserva()
                    {
                        Id = Convert.ToInt32(leitor["Id"]),
                        NomePessoa = leitor["NomePessoa"].ToString(),
                        EmailPessoa = leitor["EmailPessoa"].ToString(),
                        FonePessoa = leitor["FonePessoa"].ToString(),
                        DataCadastro = Convert.ToDateTime(leitor["DataCadastro"]),
                        DataReserva = Convert.ToDateTime(leitor["DataReserva"]),
                        Convidados = Convert.ToByte(leitor["Convidados"]),
                        Status = Convert.ToByte(leitor["Status"])
                    };
                    reservas.Add(reserva);
                }
                conexao.Close();
            }
            return reservas;
        }

        public Reserva GetById(int? id)
        {
            Reserva reserva = new Reserva();
            using (MySqlConnection conexao = new MySqlConnection(stringConnection))
            {
                var sql = "select * from Reserva where Id = @Id";
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
                leitor.Read();
                reserva.Id = Convert.ToInt32(leitor["Id"]);
                reserva.NomePessoa = leitor["NomePessoa"].ToString();
                reserva.EmailPessoa = leitor["EmailPessoa"].ToString();
                reserva.FonePessoa = leitor["FonePessoa"].ToString();
                reserva.DataCadastro = Convert.ToDateTime(leitor["DataCadastro"]);
                reserva.DataReserva = Convert.ToDateTime(leitor["DataReserva"]);
                reserva.Convidados = Convert.ToByte(leitor["Convidados"]);
                reserva.Status = Convert.ToByte(leitor["Status"]);
                conexao.Close();
            }
            return reserva;
        }

        public void Update(Reserva model)
        {
            using (MySqlConnection conexao = new MySqlConnection(stringConnection))
            {
                var sql = "update set Reserva" +
                    " NomePessoa = @NomePessoa," +
                    " EmailPessoa = @EmailPessoa," +
                    " FonePessoa = @FonePessoa," +
                    " DataReserva = @DataCadastro," +
                    " Convidados = @Convidados," +
                    " Status = @Status" +
                    " where Id = @Id";
                MySqlCommand comando = new MySqlCommand(sql, conexao);
                comando.CommandType = CommandType.Text;
                comando.Parameters.AddWithValue("@Id", model.Id);
                comando.Parameters.AddWithValue("@NomePessoa", model.NomePessoa);
                comando.Parameters.AddWithValue("@EmailPessoa", model.EmailPessoa);
                comando.Parameters.AddWithValue("@FonePessoa", model.FonePessoa);
                comando.Parameters.AddWithValue("DataReserva", model.DataReserva);
                comando.Parameters.AddWithValue("Convidados", model.Convidados);
                comando.Parameters.AddWithValue("Status", model.Status);

                conexao.Open();
                comando.ExecuteNonQuery();
                conexao.Close();
            }
        }
    }
}
