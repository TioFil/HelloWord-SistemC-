using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Lógica interna para jReajustarPreco.xaml
    /// </summary>
    public partial class jReajustarPreco : Window
    {

        public jReajustarPreco()
        {
            ComboBox combo = new ComboBox();
            InitializeComponent();
        }

        private void radioButton1_Checked(object sender, RoutedEventArgs e)
        {
            Produto produto = new Produto();
            cboProduto.ItemsSource = produto.RetornarTodosProdutos();
            if (radioButton1.IsChecked == true)
                cboProduto.IsEnabled = true;
            else
                cboProduto.IsEnabled = false;
        }
        private void cboProduto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Produto produto = new Produto();
            produto = (Produto)cboProduto.SelectedItem;
            tbxDescricaoProduto.Text = "Descrição: " + produto.Descricao;
            CultureInfo culture = new CultureInfo("pt-BR");
            decimal preco = produto.PrecoUnitario;
            tbxPrecoAtual.Text = "Preço atual: " + preco.ToString("C", culture);
        }
        private void radioButton2_Checked(object sender, RoutedEventArgs e)
        {
            cboProduto.IsEnabled = false;
        }
        private void btnReajustarPreco_Click(object sender, RoutedEventArgs e)
        {
            Produto produto = new Produto();
            try
            {
                if (radioButton1.IsChecked == true)
                {
                    produto = (Produto)cboProduto.SelectedItem;
                    produto.ReajustarPrecoDeUmProduto(produto.ProdutoID,
                   Convert.ToDecimal(txtPercentual.Text));
                    tbxPrecoReajustado.Text = (produto.PrecoUnitario * (1 +
                   Convert.ToDecimal(txtPercentual.Text) / 100)).ToString("C");
                }
                else if (radioButton2.IsChecked == true)
                {
                    produto.ReajustarPrecoDosProdutos(Convert.ToDecimal(txtPercentual.Text));
                    MessageBox.Show("Todos os preços foram reajustados com sucesso.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Falha na operação." + ex.Message);
            }
        }
    }
}

