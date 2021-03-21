using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendasWpf
{
    public partial class ClsCliente
    {
        private SqlConnection con = null;
        private int clienteID;
        public int ClienteID


        {
            get { return clienteID; }
            set { clienteID = value; }
        }
        private string nome;

        public string Nome
        {
            get { return nome; }
            set { nome = value; }
        }
        private string sexo;

        public string Sexo
        {
            get { return sexo; }
            set { sexo = value; }
        }
        private string email;

        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        public ClsCliente(int clienteID, string nome, string sexo, string email)
        {
            this.ClienteID = clienteID;
            this.Nome = nome;
            this.Sexo = sexo;
            this.Email = email;
        }

        
        public void AlterarCliente(ClsCliente cliente)
        {
            con = new SqlConnection(ConfigurationManager.AppSettings["conString"]);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("AtualizarCliente", con);
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@clienteID", cliente.ClienteID);
                cmd.Parameters.AddWithValue("@nome", cliente.Nome);
                cmd.Parameters.AddWithValue("@sexo", cliente.Sexo);
                cmd.Parameters.AddWithValue("@email", cliente.Email);
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw new Exception("Falha na operação." + ex.Message);
            }
        }
        public void ExcluirCliente(ClsCliente cliente)
        {
            con = new SqlConnection(ConfigurationManager.AppSettings["conString"]);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("DeletarCliente", con);
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@clienteID", cliente.ClienteID);

                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw new Exception("Falha na operação." + ex.Message);
            }
        }
        public ClsCliente RetornarUmCliente(ClsCliente cliente)
        {
            con = new SqlConnection(ConfigurationManager.AppSettings["conString"]);
            SqlDataReader reader = null;
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("LocalizarClientePorID", con);
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@clienteID", cliente.ClienteID);
                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                ClsCliente rcliente = new ClsCliente(0,"","","");
                while (reader.Read())
                {
                    rcliente.ClienteID = Convert.ToInt16(reader["clienteID"]);
                    rcliente.Nome = reader["nome"].ToString();
                    rcliente.Sexo = reader["sexo"].ToString();
                    rcliente.Email = reader["email"].ToString();
                }
                return rcliente;
            }
            catch (SqlException ex)
            {
                throw new Exception("Falha na operação." + ex.Message);
            }
            finally
            {
                reader.Close();
                con.Close();
            }
        }
        public List<ClsCliente> GetClientes()
        {
            con = new SqlConnection(ConfigurationManager.AppSettings["conString"]);
            SqlDataReader reader = null;
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("LocalizarTodosClientes", con);
                cmd.CommandType = CommandType.StoredProcedure;
                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                //Lista de todos os produtos retornados
                List<ClsCliente> listaClientes = new List<ClsCliente>();
                //Lê o registro reornado, se foi localizado
                while (reader.Read())
                {
                    ClsCliente rcliente = new ClsCliente();
                    rcliente.ClienteID = Convert.ToInt16(reader["clienteID"]);
                    rcliente.Nome = reader["nome"].ToString();
                    rcliente.Sexo = reader["sexo"].ToString();
                    rcliente.Email = reader["email"].ToString();
                    listaClientes.Add(rcliente);
                }
                return listaClientes;
            }
            catch (Exception ex)
            {
                throw new Exception("Falha na operação: " + ex.Message);
            }
            finally
            {
                reader.Close();
                con.Close();
            }
        }
    }
}
    
