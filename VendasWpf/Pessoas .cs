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
    public class Pessoas
    { 
       //torna a conexao privada.
        private SqlConnection con = null;
        private SqlCommand cmd = null;

        private int PessoasID;
        public int pessoasID
        {
            get { return pessoasID; }
            set { PessoasID = value; }
        }
        private string descricao;
        public string Descricao
        {
            get { return descricao; }
            set { descricao = value; }
        }
        private string nome;
        public string Nome
        {
            get { return nome; }
            set { nome = value; }
        }
        private int idade;
        public int Idade
        {
            get { return idade; }
            set { idade = value; }
        }
        public Pessoas() { } //Construtor padrão
        public Pessoas(int pessoasID, string descricao, string nome, int idade)
        {
            this.PessoasID = pessoasID;
            this.Descricao = descricao;
            this.Nome = nome;
            this.Idade = idade;
        }
        public void InserirPessoa(Pessoas pessoas)
        {
            con = new SqlConnection(ConfigurationManager.AppSettings["conString"]);
            try
            {
                con.Open();
                cmd = new SqlCommand("InsertPessoass", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@pessoasID", pessoas.PessoasID);
                cmd.Parameters.AddWithValue("@descricao", pessoas.Descricao);
                cmd.Parameters.AddWithValue("@nome", pessoas.Nome);
                cmd.Parameters.AddWithValue("@idade", pessoas.Idade);
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
        public void AlterarPessoa(Pessoas pessoas)
        {
            con = new SqlConnection(ConfigurationManager.AppSettings["conString"]);
            try
            {
                con.Open();
                cmd = new SqlCommand("UpdatePessoass", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@pessoasID", pessoas.PessoasID);
                cmd.Parameters.AddWithValue("@descricao", pessoas.Descricao);
                cmd.Parameters.AddWithValue("@nome", pessoas.Nome);
                cmd.Parameters.AddWithValue("@idade", pessoas.Idade);
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
        public void ExcluirPessoa(Pessoas pessoas)
        {
            con = new SqlConnection(ConfigurationManager.AppSettings["conString"]);
            try
            {
                con.Open();
                cmd = new SqlCommand("DeletePessoass", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@pessoasID", pessoas.PessoasID);

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
        public Pessoas LocalizarPessoaPorID(Pessoas pessoas)
        {
            con = new SqlConnection(ConfigurationManager.AppSettings["conString"]);
            SqlDataReader reader = null;
            try
            {
                con.Open();
                cmd = new SqlCommand("GetPessoass", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@pessoasID", pessoas.PessoasID);
                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                //Produto a ser retornado
                Pessoas rpessoas = null;
                //Lê o registro reornado, se foi localizado
                while (reader.Read())
                {
                    rpessoas = new Pessoas();
                    rpessoas.PessoasID = Convert.ToInt16(reader["pessoasID"]);
                    rpessoas.Descricao = reader["descricao"].ToString();
                    rpessoas.Nome = reader["nome"].ToString();
                    rpessoas.Idade = Convert.ToInt16(reader["idade"]);
                }
                return rpessoas;
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
        public List<Pessoas> RetornarTodosPessoas()
        {
            con = new SqlConnection(ConfigurationManager.AppSettings["conString"]);
            SqlDataReader reader = null;
            try
            {
                con.Open();
                cmd = new SqlCommand("GetPessoass", con);
                cmd.CommandType = CommandType.StoredProcedure;

                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                //Lista de todos os produtos retornados
                List<Pessoas> listaPessoas = new List<Pessoas>();
                //Lê o registro reornado, se foi localizado
                while (reader.Read())
                {
                    Pessoas rpessoas = new Pessoas();
                    rpessoas.PessoasID = Convert.ToInt16(reader["pessoasID"]);
                    rpessoas.Descricao = reader["descricao"].ToString();
                    rpessoas.Nome = reader["nome"].ToString();
                    rpessoas.Idade = Convert.ToInt16(reader["idade"]);
                    listaPessoas.Add(rpessoas);
                }
                return listaPessoas;
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
     /*   public void RealizarVendas(int pedidoID, DateTime data, decimal valor, int clienteID, int
       itemNum,
        int qtdade, int produtoID, decimal precoVenda)
        {
            con = new SqlConnection(ConfigurationManager.AppSettings["conString"]);
            try
            {
                con.Open();
                cmd = new SqlCommand("RealizarVendas", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@pedidoID", pedidoID);
                cmd.Parameters.AddWithValue("@data", data);
                cmd.Parameters.AddWithValue("@valor", valor);
                cmd.Parameters.AddWithValue("@clienteID", clienteID);
                cmd.Parameters.AddWithValue("@itemNum", itemNum);
                cmd.Parameters.AddWithValue("@qtdade", qtdade);
                cmd.Parameters.AddWithValue("produtoID", produtoID);
                cmd.Parameters.AddWithValue("precoVenda", precoVenda);
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
        public void DarBaixaNoProduto(int produtoID, int qtdade)
        {
            con = new SqlConnection(ConfigurationManager.AppSettings["conString"]);
            try
            {
                con.Open();
                cmd = new SqlCommand("AtualizarEstoque", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("produtoID", produtoID);
                cmd.Parameters.AddWithValue("@qtdade", qtdade);

                //Executa o comando para realizar a baixa na qtdade estocada do produto no banco de

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
        public void ReajustarPrecoDeUmProduto(int produtoID, decimal percentual)
        {
            con = new SqlConnection(ConfigurationManager.AppSettings["conString"]);
            try
            {
                con.Open();
                cmd = new SqlCommand("ReajustarPrecoDeUmProduto", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("produtoID", produtoID);
                cmd.Parameters.AddWithValue("@percentual", percentual);
                //Executa o comando para realizar o reajuste de preço do produto no banco de dado
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
        public void ReajustarPrecoDosProdutos(decimal percentual)
        {
            con = new SqlConnection(ConfigurationManager.AppSettings["conString"]);
            try
            {
                con.Open();
                cmd = new SqlCommand("ReajustarPrecoDeProdutos", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@percentual", percentual);
                //Executa o comando para realizar o reajuste de preço de todos os produto.
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
            /*/
        }
    }



