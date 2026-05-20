using System.Windows;
using System.Windows.Input;

namespace LacoFor;

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private const decimal custoSorteio = 10M;
    private readonly string[] emoticons = { "🐯", "🍊", "💎", "💰", "🍒", "🔔" };

    public decimal saldoInicial = 1600.00M;

    public MainWindow()
    {
        InitializeComponent();
        tbSaldo.Text = $"R$ {saldoInicial}";
        tbcusto.Text = $"R$ {custoSorteio}";
    }

    private async void BotaoSorteio_OnClick(object sender, RoutedEventArgs e)
    {
        if (!int.TryParse(tbQuantidade.Text, out var quantidadedeSorteio))
        {
            MessageBox.Show("Coloque apenas valores númericos!");
            return;
        }


        btnSorteio.IsEnabled = false;

        if (quantidadedeSorteio < 1) quantidadedeSorteio = 1;
        var sorteador = new Random();
        for (var contador = 0; contador < quantidadedeSorteio; contador++)
            if (saldoInicial >= custoSorteio)
            {
                saldoInicial -= custoSorteio;
                tbSaldo.Text = "R$ {saldoInicial}";

                var numeroSorteado = sorteador.Next(40000001);

                if (numeroSorteado == 28)
                {
                    tbSlot1.Text = emoticons[0];
                    tbSlot2.Text = emoticons[0];
                    tbSlot3.Text = emoticons[0];
                    saldoInicial += custoSorteio * 2;
                }
                else
                {
                    int slot1, slot2, slot3;
                    do
                    {
                        slot1 = sorteador.Next(emoticons.Length);
                        slot2 = sorteador.Next(emoticons.Length);
                        slot3 = sorteador.Next(emoticons.Length);
                    } while (slot1 == slot2 && slot2 == slot3);

                    tbSlot1.Text = emoticons[sorteador.Next(emoticons.Length)];
                    tbSlot2.Text = emoticons[sorteador.Next(emoticons.Length)];
                    tbSlot3.Text = emoticons[sorteador.Next(emoticons.Length)];
                }

                await Task.Delay(1000);
            }
            else
            {
                MessageBox.Show("Você não tem dinheiro suficiente para realizar o sorteio!");
                break;
            }


        btnSorteio.IsEnabled = true;
    }

    private void TbQuantidade_OnKeyUp(object sender, KeyEventArgs e)
    {
        if (!int.TryParse(tbQuantidade.Text, out var quantidadedeSorteio) || quantidadedeSorteio < 1)
            quantidadedeSorteio = 1;
        var custoTotal = quantidadedeSorteio * custoSorteio;
        tbcusto.Text = $"R$ {custoTotal}";
    }
}