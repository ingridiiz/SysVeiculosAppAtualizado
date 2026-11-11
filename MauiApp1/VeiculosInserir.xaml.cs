using Microsoft.Maui.Controls;
using System;

namespace SysVeiculosApp;

public partial class VeiculosInserir : ContentPage
{
    public VeiculosInserir()
    {
        InitializeComponent();

    
        var modelos = ModelosRepositorio.Listar();
        pickModelo.ItemsSource = modelos;
    }

    private async void BtnSalvar_Clicked(object sender, EventArgs e)
    {
        var modelo = pickModelo.SelectedItem as Modelo;
        if (modelo == null)
        {
            await DisplayAlertAsync("Atenção", "Selecione o modelo do veículo.", "OK");
            return;
        }

        var veiculo = new Veiculo
        {
            veinome = entNome.Text ?? "",
            veianofabricacao = int.TryParse(entAnoFabricacao.Text, out int anoFab) ? anoFab : 0,
            veianomodelos = int.TryParse(entAnoModelo.Text, out int anoMod) ? anoMod : 0,
            velobservacoes = entObservacoes.Text ?? "",
            modid = modelo.modid
        };

        VeiculosRepositorio.Inserir(veiculo);

        await Navigation.PopAsync();
    }

    private async void BtnVoltar_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}