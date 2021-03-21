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
    /// Lógica interna para jProduto.xaml
    /// </summary>
    public partial class jProduto : Window
    {
        public jProduto()
        {
            InitializeComponent();
        }
        private void btnFechar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void btnInserirProduto_Click(object sender, RoutedEventArgs e)
        {
            Produto produto = new Produto();
            produto.ProdutoID = Convert.ToInt16(txtProdutoID.Text);
            produto.Descricao = txtDescricao.Text;
            produto.PrecoUnitario = Convert.ToDecimal(txtPreco.Text);
            produto.Estocada = Convert.ToInt16(txtEstocada.Text);
            try
            {
                produto.InserirProduto(produto);
                MessageBox.Show("Produto inserido com sucesso.");
                dataGridProduto.ItemsSource = produto.RetornarTodosProdutos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Falha na operação." + ex.Message);
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Produto produto = new Produto();
            dataGridProduto.ItemsSource = produto.RetornarTodosProdutos();
        }
        private void GetProduto_Click(object sender, RoutedEventArgs e)
        {
            Produto produto = new Produto();
            produto = (Produto)dataGridProduto.SelectedItem;
            Produto rproduto = new Produto();
            rproduto = produto.LocalizarProdutoPorID(produto);

            //Exibir os dados nas text boxs
            txtProdutoID.Text = rproduto.ProdutoID.ToString();
            txtDescricao.Text = rproduto.Descricao;
            txtPreco.Text = rproduto.PrecoUnitario.ToString();
            txtEstocada.Text = rproduto.Estocada.ToString();
        }
        private void btnAtualizarProduto_Click(object sender, RoutedEventArgs e)
        {
            Produto produto = new Produto();
            produto.ProdutoID = Convert.ToInt16(txtProdutoID.Text);
            produto.Descricao = txtDescricao.Text;
            produto.PrecoUnitario = Convert.ToDecimal(txtPreco.Text);
            produto.Estocada = Convert.ToInt16(txtEstocada.Text);
            try
            {
                produto.AlterarProduto(produto);
                MessageBox.Show("Produto alterado com sucesso.");
                dataGridProduto.ItemsSource = produto.RetornarTodosProdutos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Falha na operação." + ex.Message);
            }
        }
        private void btnExcluirProduto_Click(object sender, RoutedEventArgs e)
        {
            Produto produto = new Produto();
            produto = (Produto)dataGridProduto.SelectedItem;
            try
            {
                produto.ExcluirProduto(produto);
                MessageBox.Show("Produto excluído com sucesso.");
                dataGridProduto.ItemsSource = produto.RetornarTodosProdutos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Falha na operação." + ex.Message);
            }
        }
    }
}
    

