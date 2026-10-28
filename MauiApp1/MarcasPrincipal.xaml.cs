using System.Collections.ObjectModel;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Maui.Controls;

namespace SysVeiculosApp;

public partial class MarcasPrincipal : ContentPage
{
    public MarcasPrincipal()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        AtualizarLista(searchBarMarcas?.Text);
    }

    private void SearchBarMarcas_TextChanged(object sender, TextChangedEventArgs e)
    {
        AtualizarLista(e.NewTextValue);
    }

    private void AtualizarLista(string termo = "")
    {
        var marcas = MarcasRepositorio.Listar();
        termo = (termo ?? "").Trim();

        if (termo != "")
        {
            marcas = marcas
                .Where(m => (m.marnome ?? "").Contains(termo, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

     
        listaMarcas.ItemsSource = marcas;
    }

    private async void BtnInserirOnClick(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MarcasInserir());
    }

    private async void OnAlterarClicked(object sender, EventArgs e)
    {
        var marca = (sender as Button)?.CommandParameter as Marca;
        if (marca == null) return;

        await Navigation.PushAsync(new MarcasAlterar(marca));
    }

    private async void OnExcluirClicked(object sender, EventArgs e)
    {
        var marca = (sender as Button)?.CommandParameter as Marca;
        if (marca == null) return;

        bool confirmar = await DisplayAlertAsync("Confirmação",
            $"Deseja excluir a marca {marca.marnome}?", "Sim", "Não");

        if (confirmar)
        {
            MarcasRepositorio.Excluir(marca.marid);
            AtualizarLista(searchBarMarcas?.Text);
        }
    }
}