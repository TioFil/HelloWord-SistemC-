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
    /// Lógica interna para jRelacaoClientes.xaml
    /// </summary>
    public partial class jRelacaoClientes : Window
    {
        List<ClsCliente> clientes = new List<ClsCliente>();
        public jRelacaoClientes()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                clientes.Clear();
                ClsCliente cliente = new ClsCliente();
                foreach (ClsCliente c in cliente.GetClientes())
                {
                    clientes.Add(c);
                }
                gridClientes.DataContext = clientes;
                listaClientes.ItemsSource = clientes;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Falha na operação." + ex.Message);
            }
        }
    }

}

