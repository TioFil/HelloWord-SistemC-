using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace VendasWpf
{
    /// <summary>
    /// Lógica interna para jRelVendas.xaml
    /// </summary>
    public partial class jRelVendas : Window
    {
        private Pedido objpedido;
        public jRelVendas()
        {
            InitializeComponent();
        }

        private void cboClientes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboClientes.SelectedItem != null)
            {
                listaItensPedido.ItemsSource = null;
            }
            ClsCliente cliente = new ClsCliente();
            cliente = (ClsCliente)cboClientes.SelectedItem;
            cliente.ClienteID = cliente.ClienteID;
            Pedido pedido = new Pedido();
            listaPedidos.ItemsSource = pedido.GetPedidosPorClienteID(cliente.ClienteID);
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ClsCliente cliente = new ClsCliente();
            cboClientes.ItemsSource = cliente.GetClientes();
        }

        private void listaPedidos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            objpedido = new Pedido();
            ItensPedido item = new ItensPedido();
            if (listaPedidos.SelectedItem != null)
            {
                objpedido = (Pedido)listaPedidos.SelectedItem;
                listaItensPedido.ItemsSource = item.GetItensPorPedidoID(objpedido.PedidoID);
            }
        }
    }

    }

