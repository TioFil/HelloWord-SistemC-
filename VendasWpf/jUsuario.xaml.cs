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
    /// Lógica interna para jUsuario.xaml
    /// </summary>
    public partial class jUsuario : Window
    {
        public jUsuario()
        {
            InitializeComponent();
        }
        
        private void btnFechar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void btnInserirUsuario_Click(object sender, RoutedEventArgs e)
        {
            Usuario usuario = new Usuario();

            usuario.CodUsuario = Convert.ToInt16(txtCodUsuario.Text);
            usuario.Nome = txtNome.Text;
            usuario.Senha = Convert.ToInt16(txtSenha.Text);

            try
            {
                usuario.InserirUsuario(usuario);
                MessageBox.Show("Pessoa inserida com sucesso.");
                dataGridUsuario.ItemsSource = usuario.RetornarTodosOsUsuarios();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Falha na operação." + ex.Message);
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Usuario usuario = new Usuario();
            dataGridUsuario.ItemsSource = usuario.RetornarTodosOsUsuarios();
        }
        private void GetUsuario_Click(object sender, RoutedEventArgs e)
        {
            Usuario usuario = new Usuario();
            usuario = (Usuario)dataGridUsuario.SelectedItem;
            Usuario rusuario = new Usuario();
            rusuario = usuario.LocalizarUsuarioPorCodigo(usuario);



            //Exibir os dados nas text boxs

            txtCodUsuario.Text = rusuario.CodUsuario.ToString();
            txtNome.Text = rusuario.Nome;
            txtSenha.Text = rusuario.Senha.ToString();
        }
        private void btnAtualizarUsuario_Click(object sender, RoutedEventArgs e)
        {
            Usuario usuario = new Usuario();
            usuario.CodUsuario = Convert.ToInt16(txtCodUsuario.Text);
            usuario.Nome = txtNome.Text;
            usuario.Senha = Convert.ToInt16(txtSenha.Text);
            try
            {
            usuario.AlterarUsuario(usuario);
            MessageBox.Show("Pessoa alterada com sucesso.");
            dataGridUsuario.ItemsSource = usuario.RetornarTodosOsUsuarios();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Falha na operação." + ex.Message);
            }
        }
        private void btnExcluirUsuario_Click(object sender, RoutedEventArgs e)
        {
            Usuario usuario = new Usuario();
            usuario = (Usuario)dataGridUsuario.SelectedItem;
            try
            {
                usuario.ExcluirUsuario(usuario);
                MessageBox.Show("Pessoa excluida com sucesso.");
                dataGridUsuario.ItemsSource = usuario.RetornarTodosOsUsuarios();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Falha na operação." + ex.Message);
            }
        }

        private void GetPessoa_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void TxtCodUsuario_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
    

