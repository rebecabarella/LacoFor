using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LacoFor;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }
    public decimal saldoInicial = 1600.00M;
    

    private async void BotaoSorteio_OnClick(object sender, RoutedEventArgs e)
    {
        
        var quantidadeTexto = tbQuantidade.Text;
        int quantidadeSorteios;
        try
        {
            quantidadeSorteios = Convert.ToInt32(quantidadeTexto);
        }
        catch (FormatException)
        {
            MessageBox.Show("Entrada invalida. Coloque apenas números de um para cima!");
            return;
        }
        catch (OverflowException)
        {
            MessageBox.Show("Erro! O número digitado é maior que o número suportado!");
            return;
        }
        btnSorteio.IsEnabled = false;
        
        if (quantidadeSorteios < 1)
        {
            quantidadeSorteios = 1;
        }
        var sorteador = new Random();
        for (int contador = 0; contador < quantidadeSorteios; contador++)
        {
            if (saldoInicial >= 10)
            {
                tbResultado.Text = sorteador.Next(0, 40000000).ToString();
                saldoInicial -= 10M;
                tbSaldo.Text = "R$"+saldoInicial.ToString();
                await Task.Delay(1000);
            }
            else
            {
                MessageBox.Show("Você não tem dinheiro suficiente para realizar o sorteio!");
                break;
            }
        }

        btnSorteio.IsEnabled = true;
    }
}