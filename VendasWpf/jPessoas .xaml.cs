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
    public partial class jPessoas : Window
    {
        public jPessoas()
        {
            InitializeComponent();
        }
        private void btnFechar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void btnInserirPessoa_Click(object sender, RoutedEventArgs e)
        {
            Pessoas pessoas = new Pessoas();

            pessoas.pessoasID = Convert.ToInt16(txtPessoasID.Text);
            pessoas.Descricao = txtDescricao.Text;
            pessoas.Nome = txtNome.Text;
            pessoas.Idade = Convert.ToInt16(txtIdade.Text);
            try
            {
                pessoas.InserirPessoa(pessoas);
                MessageBox.Show("Pessoa inserida com sucesso.");
                dataGridPessoa.ItemsSource = pessoas.RetornarTodosPessoas();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Falha na operação." + ex.Message);
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Pessoas pessoas = new Pessoas();
            dataGridPessoa.ItemsSource = pessoas.RetornarTodosPessoas();
        }
        private void GetPessoa_Click(object sender, RoutedEventArgs e)
        {
            Pessoas pessoas = new Pessoas();
            pessoas = (Pessoas)dataGridPessoa.SelectedItem;
            Pessoas rpessoas = new Pessoas();
            rpessoas = pessoas.LocalizarPessoaPorID(pessoas);

            //Exibir os dados nas text boxs
            txtPessoasID.Text = rpessoas.pessoasID.ToString();
            txtDescricao.Text = rpessoas.Descricao;
            txtNome.Text = rpessoas.Nome;
            txtIdade.Text = rpessoas.Idade.ToString();
        }
        private void btnAtualizarPessoa_Click(object sender, RoutedEventArgs e)
        {
            Pessoas pessoa = new Pessoas();
            pessoa.pessoasID = Convert.ToInt16(txtPessoasID.Text);
            pessoa.Descricao = txtDescricao.Text;
            pessoa.Nome = txtNome.Text;
            pessoa.Idade = Convert.ToInt16(txtIdade.Text);
            try
            {
                pessoa.AlterarPessoa(pessoa);
                MessageBox.Show("Pessoa alterada com sucesso.");
                dataGridPessoa.ItemsSource = pessoa.RetornarTodosPessoas();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Falha na operação." + ex.Message);
            }
        }
        private void btnExcluirPessoa_Click(object sender, RoutedEventArgs e)
        {
            Pessoas pessoas = new Pessoas();
            pessoas = (Pessoas)dataGridPessoa.SelectedItem;
            try
            {
                pessoas.ExcluirPessoa(pessoas);
                MessageBox.Show("Pessoa excluida com sucesso.");
                dataGridPessoa.ItemsSource = pessoas.RetornarTodosPessoas();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Falha na operação." + ex.Message);
            }
        }

        private void GetPessoa_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
    

