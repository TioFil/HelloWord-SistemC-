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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VendasWpf
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void mCliente_Click(object sender, RoutedEventArgs e)
        {
            ClsCliente cliente = new ClsCliente();
            cliente.Owner = this;
            cliente.Show();
        }
        private void mSair_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void mProduto_Click(object sender, RoutedEventArgs e)
        {
            jProduto jproduto = new jProduto();
            jproduto.Owner = this;
            jproduto.Show();
        }
        private void mPessoas_Click(object sender, RoutedEventArgs e)
        {
            jPessoas jpessoas = new jPessoas();
            jpessoas.Owner = this;
            jpessoas.Show();
        }

        private void mUsuario_Click(object sender, RoutedEventArgs e)
        {
            jUsuario jusuario = new jUsuario();
            jusuario.Owner = this;
            jusuario.Show();
        }

        private void mRealiazarVenda_Click(object sender, RoutedEventArgs e)
        {
            jRealizarVenda jrealizarVenda = new jRealizarVenda();
            jrealizarVenda.Owner = this;
            jrealizarVenda.Show();
        }
        private void mReajprecoProduto_Click(object sender, RoutedEventArgs e)
        {
            jReajustarPreco jreajustarPreco = new jReajustarPreco();
            jreajustarPreco.Owner = this;
            jreajustarPreco.Show();
        }
        private void mRelClientes_Click(object sender, RoutedEventArgs e)
        {
            jRelacaoClientes jRelClientes = new jRelacaoClientes();
            jRelClientes.Owner = this;
            jRelClientes.Show();
        }
        private void mCatalogoProduto_Click(object sender, RoutedEventArgs e)
        {
            jCatalogoProduto jcatalogo = new jCatalogoProduto();
            jcatalogo.Owner = this;
            jcatalogo.Show();
        }
        private void mRelVendas_Click(object sender, RoutedEventArgs e)
        {
            jRelVendas jRelvendas = new jRelVendas();
            jRelvendas.Owner = this;

            jRelvendas.Show();
        }
    }
}


