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
            public  class Pedido
            {

            SqlConnection con = null;
            SqlCommand cmd = null;
            private int pedidoID;
            public int PedidoID
            {
            get { return pedidoID; }
            set { pedidoID = value; }
            }
            private DateTime data;
            public DateTime Data
            {
            get { return data; }
            set { data = value; }
            }
            private decimal valor;
            public decimal Valor
            {
            get { return valor; }
            set { valor = value; }
            }
            private int clienteID;
            public int ClienteID
            {
            get { return clienteID; }
            set { clienteID = value; }
            }


            private List<ItensPedido> listaDeItens;
            public List<ItensPedido> ListaDeItens
            {
            get { return listaDeItens; }
            set
            {
            listaDeItens = value;
            }
            }


            public Pedido(int pedidoID, List<ItensPedido> listaDeItens)
            {
            this.PedidoID = pedidoID;
            this.ListaDeItens = listaDeItens;
            }


            public Pedido() { }
            public void InserirPedido(Pedido pedido)
            {
            con = new SqlConnection(ConfigurationManager.AppSettings["conString"]);
            try
            {
            con.Open();
            cmd = new SqlCommand("InsertPedido", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pedidoID", pedido.PedidoID);
            cmd.Parameters.AddWithValue("@data", pedido.Data);
            cmd.Parameters.AddWithValue("@valor", pedido.Valor);
            cmd.Parameters.AddWithValue("@clienteID", pedido.ClienteID);
            //Executa o comando para inserir o pedido no banco de dado
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


            public List<Pedido> GetPedidosPorClienteID(int clienteID)
            {
            con = new SqlConnection(ConfigurationManager.AppSettings["conString"]);
            try
            {
            con.Open();
            cmd = new SqlCommand("GetPedidos", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@clienteID", clienteID);
            SqlDataReader reader = cmd.ExecuteReader();
            //Cria uma lista de pedidos para recebe
            List<Pedido> listaPedido = new List<Pedido>();
            while (reader.Read())
            {
            //Cria um objeto Pedido
            Pedido pedido = new Pedido();
            pedido.PedidoID = Convert.ToInt16(reader["pedidoID"]);
            pedido.Data = Convert.ToDateTime(reader["data"]);
            pedido.Valor = Convert.ToDecimal(reader["valor"]);
            pedido.ClienteID = Convert.ToInt16(reader["clienteID"]);
            listaPedido.Add(pedido);
            }
            return listaPedido;
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


            public int GetUtimoPedido()
            {
            con = new SqlConnection(ConfigurationManager.AppSettings["conString"]);
            try
            {
            con.Open();
            cmd = new SqlCommand("UtimoPedido", con);
            cmd.CommandType = CommandType.StoredProcedure;
            //Executa o comando que retorna o valor do útimo pedido
            if (cmd.ExecuteScalar() == null)
            return 0;
            else
            {
            return Convert.ToInt16(cmd.ExecuteScalar());
            }
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


            public List<Pedido> GetPedidosComItens(int clienteID)
            {
            //Realiza uma consulta para pedidos usando a procedure GetPedidos.
            con = new SqlConnection(ConfigurationManager.AppSettings["conString"]);
            SqlCommand cmd = new SqlCommand("GetPedidos", con);
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@clienteID", clienteID);
            //Armazena o resulta em um DataSet temporário
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "Pedidos");
            //Realiza uma consulta para itensPedido usando o procedure Get.
            cmd.CommandText = "GetItensPedido";
            adapter.Fill(ds, "ItensPedido");
            //Configura uma relação entre as duas tabelas.
            //Isto torna mais fácil descobrir os itens em cada pedido.
            DataRelation relPedidoItens = new DataRelation("PedidoItens",
            ds.Tables["Pedidos"].Columns["PedidoID"],
            ds.Tables["ItensPedido"].Columns["PedidoID"]);
            ds.Relations.Add(relPedidoItens);
            //Constroi a coleção de objetos pedidos.


            List<Pedido> listaPedidos = new List<Pedido>();
            foreach (DataRow pedidoRow in ds.Tables["Pedidos"].Rows)
            {
            //Adicionar a coleção de aninhado objetos de itens para esta pedido.
            List<ItensPedido> ListaDeItens = new List<ItensPedido>();
            foreach (DataRow itensRow in pedidoRow.GetChildRows(relPedidoItens))
            {
            ListaDeItens.Add(new ItensPedido(Convert.ToInt16(itensRow["itemNum"]),
            Convert.ToInt16(itensRow["Qtdade"]),
            Convert.ToDecimal(itensRow["precoVenda"]),
            Convert.ToInt16(itensRow["pedidoID"]),
            Convert.ToInt16(itensRow["produtoID"])));
            }
            listaPedidos.Add(new Pedido(Convert.ToInt16(pedidoRow["PedidoID"]),
            ListaDeItens));
            }
            return listaPedidos;
           }
       }
       }
           