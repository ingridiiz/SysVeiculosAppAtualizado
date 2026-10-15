using Microsoft.Maui.Controls;
using System;

namespace SysVeiculosApp;

public partial class VeiculosAlterar : ContentPage
{
    private Veiculo _veiculo;

    public VeiculosAlterar(Veiculo veiculo)
    {
        InitializeComponent();
        _veiculo = veiculo;

        entNome.Text = _veiculo.veinome;
        entAnoFabricacao.Text = _veiculo.veianofabricacao.ToString();
        entAnoModelo.Text = _veiculo.veianomodelos.ToString();
        entObservacoes.Text = _veiculo.velobservacoes;
    }

    private async void BtnSalvar_Clicked(object sender, EventArgs e)
    {
        _veiculo.veinome = entNome.Text ?? "";
        _veiculo.veianofabricacao = int.TryParse(entAnoFabricacao.Text, out int anoFab) ? anoFab : 0;
        _veiculo.veianomodelos = int.TryParse(entAnoModelo.Text, out int anoMod) ? anoMod : 0;
        _veiculo.velobservacoes = entObservacoes.Text ?? "";

        VeiculosRepositorio.Alterar(_veiculo);

        await Navigation.PopAsync();
    }

    private async void BtnVoltar_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}