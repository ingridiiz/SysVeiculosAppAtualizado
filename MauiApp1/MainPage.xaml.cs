using Microsoft.Maui.Controls;
using System;

namespace SysVeiculosApp;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }


    protected override void OnAppearing()
    {
        base.OnAppearing();
        lblQtdMarcas.Text = $"{MarcasRepositorio.Listar().Count} cadastradas";
        lblQtdModelos.Text = $"{ModelosRepositorio.Listar().Count} cadastrados";
        lblQtdVeiculos.Text = $"{VeiculosRepositorio.Listar().Count} cadastrados";
    }

    private async void Cartao_Tapped(object sender, TappedEventArgs e)
    {
        string rota = e.Parameter as string;
        if (!string.IsNullOrEmpty(rota))
        {
            await Shell.Current.GoToAsync($"//{rota}");
        }
    }

    private async void BtnSobre_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//Desenvolvedor");
    }
}