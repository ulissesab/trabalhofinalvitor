using Dominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio
{
    public class PessoaRepository : IRepositorioPessoa

    {
        SqlDataAdapter adapter = new SqlDataAdapter();
        public string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Ulisses\source\repos\AtUlisses\AtUlisses\App_Data\aniversario.mdf;Integrated Security=True";

        public IEnumerable<Pessoa> GetPessoas()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var comandText = "SELECT * FROM Pessoa";
                var selectComand = new SqlCommand(comandText, connection);
                Pessoa pessoa = null;
                var pessoas = new List<Pessoa>();
                try
                {
                    connection.Open();
                    using (var reader = selectComand.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            pessoa = new Pessoa();
                            pessoa.Id = (int)reader["Id"];
                            pessoa.Nome = reader["Nome"].ToString();
                            pessoa.Sobrenome = reader["Sobrenome"].ToString();
                            pessoa.Aniversario = (DateTime)reader["Aniversario"];
                            pessoas.Add(pessoa);
                        }
                    }
                }
                finally
                {
                    connection.Close();
                }
                return pessoas;
            }
        }
        public void Create(string nome, string sobrenome, DateTime niver)
        {
            var comandText = "INSERT INTO Pessoa (Nome, Sobrenome, Aniversario) values(@0, @1, @2)";

            using (var connection = new SqlConnection(connectionString))
            {
                using (SqlCommand comm = new SqlCommand())
                {
                    comm.Connection = connection;
                    comm.CommandType = CommandType.Text;
                    comm.CommandText = comandText;
                    comm.Parameters.AddWithValue("@0", nome);
                    comm.Parameters.AddWithValue("@1", sobrenome);
                    comm.Parameters.AddWithValue("@2", niver);

                    try
                    {
                        connection.Open();
                        comm.ExecuteNonQuery();
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }

        }
        public void update(int id, Pessoa pessoa)
        {
            var comandText = "UPDATE Pessoa SET Nome=@0, Sobrenome=@1, Aniversario=@2 where Id=@3";

            using (var connection = new SqlConnection(connectionString))
            {
                using (SqlCommand comm = new SqlCommand())
                {
                    comm.Connection = connection;
                    comm.CommandType = CommandType.Text;
                    comm.CommandText = comandText;
                    comm.Parameters.AddWithValue("@0", pessoa.Nome);
                    comm.Parameters.AddWithValue("@1", pessoa.Sobrenome);
                    comm.Parameters.AddWithValue("@2", pessoa.Aniversario);
                    comm.Parameters.AddWithValue("@3", id);

                    try
                    {
                        connection.Open();
                        comm.ExecuteNonQuery();
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }

        internal static object GetAllPessoa()
        {
            throw new NotImplementedException();
        }
        static public PessoaViewModel GetPessoa(int id)
        {
            var connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Ulisses\source\repos\AtUlisses\AtUlisses\App_Data\aniversario.mdf;Integrated Security=True";

            using (var connection = new SqlConnection(connectionString))
            {
                var commandText = $"SELECT * FROM PESSOA WHERE ID = {id};";
                var selectCommand = new SqlCommand(commandText, connection);

                PessoaViewModel pessoa = null;

                try
                {
                    connection.Open();

                    using (var reader = selectCommand.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            pessoa = new PessoaViewModel();
                            pessoa.Id = (int)reader["Id"];
                            pessoa.Nome = reader["Nome"].ToString();
                            pessoa.Sobrenome = reader["Sobrenome"].ToString();
                            pessoa.Aniversario = (DateTime)reader["Aniversario"];

                        }

                    }
                }
                finally
                {
                    connection.Close();
                }

                return pessoa;

            }
        }
        public void delete(int id, Pessoa pessoa)
        {
            var comandText = "DELETE Pessoa where Id=@1";

            using (var connection = new SqlConnection(connectionString))
            {
                using (SqlCommand comm = new SqlCommand())
                {
                    comm.Connection = connection;
                    comm.CommandType = CommandType.Text;
                    comm.CommandText = comandText;
                    comm.Parameters.AddWithValue("@1", id);

                    try
                    {
                        connection.Open();
                        comm.ExecuteNonQuery();
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }
    }
}

        