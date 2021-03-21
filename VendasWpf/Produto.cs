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
   public  class Produto
    {
        private SqlConnection con = null;
        private SqlCommand cmd = null;
        private int produtoID;
        public int ProdutoID
        {
            get { return produtoID; }
            set { produtoID = value; }
        }
        private string descricao;
        public string Descricao
        {
            get { return descricao; }
            set { descricao = value; }
        }
        private decimal precoUnitario;
        public decimal PrecoUnitario
        {
            get { return precoUnitario; }
            set { precoUnitario = value; }
        }
        private int estocada;
        public int Estocada
        {
            get { return estocada; }
            set { estocada = value; }
        }
        public Produto() { } //Construtor padrão
        public Produto(int produtoID, string descricao, decimal precoUnitario, int estocada)
        {
            this.ProdutoID = produtoID;
            this.Descricao = descricao;
            this.PrecoUnitario = precoUnitario;
            this.Estocada = estocada;
        }
        public void InserirProduto(Produto produto)
        {
            con = new SqlConnection(ConfigurationManager.AppSettings["conString"]);
            try
            {
                con.Open();
                cmd = new SqlCommand("InsertProduto", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@produtoID", produto.ProdutoID);
                cmd.Parameters.AddWithValue("@descricao", produto.Descricao);
                cmd.Parameters.AddWithValue("@precoUnitario", produto.PrecoUnitario);
                cmd.Parameters.AddWithValue("@estocada", produto.Estocada);
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
        public void AlterarProduto(Produto produto)
        {
            con = new SqlConnection(ConfigurationManager.AppSettings["conString"]);
            try
            {
                con.Open();
                cmd = new SqlCommand("UpdateProduto", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@produtoID", produto.ProdutoID);
                cmd.Parameters.AddWithValue("@descricao", produto.Descricao);
                cmd.Parameters.AddWithValue("@precoUnitario", produto.PrecoUnitario);
                cmd.Parameters.AddWithValue("@estocada", produto.Estocada);
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
        public void ExcluirProduto(Produto produto)
        {
            con = new SqlConnection(ConfigurationManager.AppSettings["conString"]);
            try
            {
                con.Open();
                cmd = new SqlCommand("DeleteProduto", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@produtoID", produto.ProdutoID);

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
        public Produto LocalizarProdutoPorID(Produto produto)
        {
            con = new SqlConnection(ConfigurationManager.AppSettings["conString"]);
            SqlDataReader reader = null;
            try
            {
                con.Open();
                cmd = new SqlCommand("GetProduto", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@produtoID", produto.ProdutoID);
                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                //Produto a ser retornado
                Produto rproduto = null;
                //Lê o registro reornado, se foi localizado
                while (reader.Read())
                {
                    rproduto = new Produto();
                    rproduto.ProdutoID = Convert.ToInt16(reader["produtoID"]);
                    rproduto.Descricao = reader["descricao"].ToString();
                    rproduto.PrecoUnitario = Convert.ToDecimal(reader["precoUnitario"]);
                    rproduto.Estocada = Convert.ToInt16(reader["estocada"]);
                }
                return rproduto;
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
        public List<Produto> RetornarTodosProdutos()
        {
            con = new SqlConnection(ConfigurationManager.AppSettings["conString"]);
            SqlDataReader reader = null;
            try
            {
                con.Open();
                cmd = new SqlCommand("GetProdutos", con);
                cmd.CommandType = CommandType.StoredProcedure;

                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                //Lista de todos os produtos retornados
                List<Produto> listaProdutos = new List<Produto>();
                //Lê o registro reornado, se foi localizado
                while (reader.Read())
                {
                    Produto rproduto = new Produto();
                    rproduto.ProdutoID = Convert.ToInt16(reader["produtoID"]);
                    rproduto.Descricao = reader["descricao"].ToString();
                    rproduto.PrecoUnitario = Convert.ToDecimal(reader["precoUnitario"]);
                    rproduto.Estocada = Convert.ToInt16(reader["estocada"]);
                    listaProdutos.Add(rproduto);
                }
                return listaProdutos;
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
        public void RealizarVendas(int pedidoID, DateTime data, decimal valor, int clienteID, int
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
        }
    }
}
