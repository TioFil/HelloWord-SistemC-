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
  public  class ItensPedido
    {
        SqlConnection con = null;
        SqlCommand cmd = null;

        private int itemNum;
        public int ItemNum
        {
            get { return itemNum; }
            set { itemNum = value; }
        }
        private int qtdade;
        public int Qtdade
        {
            get { return qtdade; }
            set { qtdade = value; }
        }
        private decimal precoVenda;
        public decimal PrecoVenda
        {
            get { return precoVenda; }
            set { precoVenda = value; }
        }
        private int pedidoID;
        public int PedidoID
        {
            get { return pedidoID; }
            set { pedidoID = value; }
        }
        private int produtoID;
        public int ProdutoID
        {
            get { return produtoID; }
            set { produtoID = value; }
        }
        public ItensPedido(int itemNum, int qtdade, decimal precovenda,
        int pedidoID, int produtoID)
        {
            this.ItemNum = itemNum;
            this.Qtdade = qtdade;
            this.PrecoVenda = precovenda;
            this.PedidoID = pedidoID;
            this.ProdutoID = produtoID;
        }
        public ItensPedido() { }
        public void InserirItensPedido(ItensPedido item)
        {
            con = new SqlConnection(ConfigurationManager.AppSettings["conString"]);
            try
            {
                con.Open();
                cmd = new SqlCommand("InsertItensPedido", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@itemNum", item.ItemNum);
                cmd.Parameters.AddWithValue("@qtade", item.Qtdade);
                cmd.Parameters.AddWithValue("@precoVenda", item.PrecoVenda);
                cmd.Parameters.AddWithValue("@pedidoID", item.PedidoID);
                cmd.Parameters.AddWithValue("@produtoID", item.ProdutoID);
                //Executa o comando para inserir o Item pedido no banco de dado
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
        public List<ItensPedido> GetItensPorPedidoID(int pedidoID)
        {
            con = new SqlConnection(ConfigurationManager.AppSettings["conString"]);
            try
            {
                con.Open();
                cmd = new SqlCommand("GetItensPedidoPorPedidoID", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@pedidoID", pedidoID);
                //Executa o comando para recuperar os Itens o pedido no banco de dado
                SqlDataReader reader = cmd.ExecuteReader();
                //Cria uma lista para receber os registros dos itensPedido
                List<ItensPedido> listaItens = new List<ItensPedido>();
                while (reader.Read())
                {
                    //Cria uma instância do objeto ItensPedido para receber o item retornado
                    ItensPedido item = new ItensPedido();
                    item.ItemNum = Convert.ToInt16(reader["itemNum"]);
                    item.Qtdade = Convert.ToInt16(reader["qtdade"]);
                    item.PrecoVenda = Convert.ToDecimal(reader["precoVenda"]);
                    item.PedidoID = Convert.ToInt16(reader["pedidoID"]);
                    item.ProdutoID = Convert.ToInt16(reader["produtoID"]);
                    listaItens.Add(item);
                }
                return listaItens;
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

                

