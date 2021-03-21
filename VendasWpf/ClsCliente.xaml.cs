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
    /// Lógica interna para ClsCliente.xaml
    /// </summary>
    public partial class ClsCliente : Window
    {
        public ClsCliente()
        {
            InitializeComponent();
        }
        private void btnInserirCliente_Click(object sender, RoutedEventArgs e)
        {
            ClsCliente cli = new ClsCliente();
            cli.ClienteID = Convert.ToInt16(txtClienteID.Text);
            cli.Nome = txtNome.Text;
            cli.Sexo = txtSexo.Text;
            cli.Email = txtEmail.Text;
            //Chama o método para inserir um cliente no banco de dados.
            cli.InserirCliente(cli);
            MessageBox.Show("Registro inserido com sucesso.");
        }
        private void btnFechar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void GetCliente_Click(object sender, RoutedEventArgs e)
        {
            ClsCliente cli = new ClsCliente(); //Este cliente é para chamar o método LocalizarClientePorID
             ClsCliente clienteRerornado = new ClsCliente(); //Este cliente recebe o cliente retornado
            cli.ClienteID = Convert.ToInt16(txtClienteID.Text);
            clienteRerornado = cli.RetornarUmCliente(cli);
            txtNome.Text = clienteRerornado.Nome;
            txtSexo.Text = clienteRerornado.Sexo;
            txtEmail.Text = clienteRerornado.Email;
        }
        private void btnAtualizarCliente_Click(object sender, RoutedEventArgs e)
        {
            ClsCliente cli = new ClsCliente();
            cli.ClienteID = Convert.ToInt16(txtClienteID.Text);
            cli.Nome = txtNome.Text;
            cli.Sexo = txtSexo.Text;
            cli.Email = txtEmail.Text;
            //Chama o método para alterar um cliente no banco de dados.
            cli.AlterarCliente(cli);
            MessageBox.Show("Registro alterado com sucesso.");
        }
        private void btnExcluirCliente_Click(object sender, RoutedEventArgs e)
        {
            ClsCliente cli = new ClsCliente();
            cli.ClienteID = Convert.ToInt16(txtClienteID.Text);

            //Chama o método para excluir um cliente no banco de dados.
            cli.ExcluirCliente(cli);

             MessageBox.Show("Registro excluído com sucesso.");


        }
    }
}

