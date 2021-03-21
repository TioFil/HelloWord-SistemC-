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
    public class Usuario
    {
        private SqlConnection con = null;
        private SqlCommand cmd = null;
        private int codUsuario;
        public int CodUsuario
        {
            get { return codUsuario; }
            set { codUsuario = value; }
        }
        private string nome;
        public string Nome
        {
            get { return nome; }
            set { nome = value; }
        }
        private decimal senha;
        public decimal Senha
        {
            get { return senha; }
            set { senha = value; }
        }

        public Usuario() { } //Construtor padrão
        public Usuario(int codUsuario, string nome, decimal senha)
        {
            this.CodUsuario = codUsuario;
            this.Nome = nome;
            this.senha = senha;

        }
        public void InserirUsuario(Usuario usuario)
        {
            con = new SqlConnection(ConfigurationManager.AppSettings["conString"]);
            try
            {
                con.Open();
                cmd = new SqlCommand("InsertUsuario", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@codUsuario", usuario.CodUsuario);
                cmd.Parameters.AddWithValue("@nome", usuario.Nome);
                cmd.Parameters.AddWithValue("@senha", usuario.senha);

                //Executa o comando para realizar o cadastro no banco de dado
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Falha na operação: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
        public void AlterarUsuario(Usuario usuario)
        {
            con = new SqlConnection(ConfigurationManager.AppSettings["conString"]);
            try
            {
                con.Open();
                cmd = new SqlCommand("UpdateUsuario", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@codUsuario", usuario.CodUsuario);
                cmd.Parameters.AddWithValue("@nome", usuario.Nome);
                cmd.Parameters.AddWithValue("@senha", usuario.senha);
                //Executa o comando para realizar a alteração do produto no banco de dado
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Falha na operação: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
        public void ExcluirUsuario(Usuario usuario)
        {
            con = new SqlConnection(ConfigurationManager.AppSettings["conString"]);
            try
            {
                con.Open();
                cmd = new SqlCommand("DeleteUsuario", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@codUsuario", usuario.CodUsuario);

                //Executa o comando para realizar a exclusão do produto no banco de dado
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Falha na operação: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
        public Usuario LocalizarUsuarioPorCodigo(Usuario usuario)
        {
            con = new SqlConnection(ConfigurationManager.AppSettings["conString"]);
            SqlDataReader reader = null;
            try
            {
                con.Open();
                cmd = new SqlCommand("GetUsuarioCodigo", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@codUsuario", usuario.CodUsuario);
                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                //Produto a ser retornado
                Usuario rusuario = null;
                //Lê o registro reornado, se foi localizado
                while (reader.Read())
                {
                    rusuario = new Usuario();
                    rusuario.CodUsuario = Convert.ToInt16(reader["codUsuario"]);
                    rusuario.Nome = reader["nome"].ToString();
                    rusuario.Senha = Convert.ToDecimal(reader["senha"]);

                }
                return rusuario;
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
        public List<Usuario> RetornarTodosOsUsuarios()
        {
            con = new SqlConnection(ConfigurationManager.AppSettings["conString"]);
            SqlDataReader reader = null;
            try
            {
                con.Open();
                cmd = new SqlCommand("GetUsuario", con);
                cmd.CommandType = CommandType.StoredProcedure;

                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                //Lista de todos os produtos retornados
                List<Usuario> listaUsuario = new List<Usuario>();
                //Lê o registro reornado, se foi localizado
                while (reader.Read())
                {
                    Usuario rusuario = new Usuario();
                    rusuario.CodUsuario = Convert.ToInt16(reader["codUsuario"]);
                    rusuario.Nome = reader["nome"].ToString();
                    rusuario.Senha = Convert.ToDecimal(reader["senha"]);
                    listaUsuario.Add(rusuario);
                }
                return listaUsuario;
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



           
