using Microsoft.Maui.Controls;
using System;

namespace SysVeiculosApp;

public partial class VeiculosInserir : ContentPage
{
    public VeiculosInserir()
    {
        InitializeComponent();
    }

    private async void BtnSalvar_Clicked(object sender, EventArgs e)
    {
        var veiculo = new Veiculo
        {
            veinome = entNome.Text ?? "",
            veianofabricacao = int.TryParse(entAnoFabricacao.Text, out int anoFab) ? anoFab : 0,
            veianomodelos = int.TryParse(entAnoModelo.Text, out int anoMod) ? anoMod : 0,
            velobservacoes = entObservacoes.Text ?? ""
        };

        VeiculosRepositorio.Inserir(veiculo);

        await Navigation.PopAsync();
    }

    private async void BtnVoltar_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}