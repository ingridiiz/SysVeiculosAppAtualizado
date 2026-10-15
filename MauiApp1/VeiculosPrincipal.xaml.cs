
using System.Collections.ObjectModel;

namespace SysVeiculosApp;

public partial class VeiculosPrincipal : ContentPage
{
    public VeiculosPrincipal()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        AtualizarLista();
    }

    private void SearchBarVeiculos_TextChanged(object sender, TextChangedEventArgs e)
    {
        AtualizarLista();
    }

    private void AtualizarLista()
    {
        var veiculos = VeiculosRepositorio.Listar();
        string termo = searchBarVeiculos.Text;

        if (!string.IsNullOrWhiteSpace(termo))
        {
            veiculos = veiculos
                .Where(v => $"{v.veid} {v.veinome} {v.veianofabricacao} {v.velobservacoes}"
                    .Contains(termo, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        listaVeiculos.ItemsSource = veiculos;
    }

    private async void BtnInserirOnClick(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new VeiculosInserir());
    }

    private async void OnAlterarClicked(object sender, EventArgs e)
    {
        var veiculo = (sender as Button)?.CommandParameter as Veiculo;
        if (veiculo == null) return;

        await Navigation.PushAsync(new VeiculosAlterar(veiculo));
    }

    private async void OnExcluirClicked(object sender, EventArgs e)
    {
        var veiculo = (sender as Button)?.CommandParameter as Veiculo;
        if (veiculo == null) return;

        bool confirmar = await DisplayAlertAsync("Confirmação",
            $"Deseja excluir o veículo {veiculo.veinome}?", "Sim", "Não");

        if (confirmar)
        {
            VeiculosRepositorio.Excluir(veiculo.veid);
            AtualizarLista();
        }
    }
}
