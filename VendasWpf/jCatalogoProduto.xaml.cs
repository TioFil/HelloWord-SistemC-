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
    /// Lógica interna para jCatalogoProduto.xaml
    /// </summary>
    public partial class jCatalogoProduto : Window
    {
        List<ClsCliente> clientes = new List<ClsCliente>();

        public jCatalogoProduto()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Produto produto = new Produto();
            lstCatalogProduto.ItemsSource = produto.RetornarTodosProdutos();
        }
    }
}
