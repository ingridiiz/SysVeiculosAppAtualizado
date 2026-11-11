using Microsoft.Maui.Controls;
using System;
using System.Linq;

namespace SysVeiculosApp;

public partial class VeiculosAlterar : ContentPage
{
    private Veiculo _veiculo;

    public VeiculosAlterar(Veiculo veiculo)
    {
        InitializeComponent();
        _veiculo = veiculo;

        entCodigo.Text = _veiculo.veid.ToString();
        entNome.Text = _veiculo.veinome;
        entAnoFabricacao.Text = _veiculo.veianofabricacao.ToString();
        entAnoModelo.Text = _veiculo.veianomodelos.ToString();
        entObservacoes.Text = _veiculo.velobservacoes;

       
        var modelos = ModelosRepositorio.Listar();
        pickModelo.ItemsSource = modelos;
        pickModelo.SelectedItem = modelos.FirstOrDefault(m => m.modid == _veiculo.modid);
    }

    private async void BtnSalvar_Clicked(object sender, EventArgs e)
    {
        var modelo = pickModelo.SelectedItem as Modelo;
        if (modelo == null)
        {
            await DisplayAlertAsync("Atenção", "Selecione o modelo do veículo.", "OK");
            return;
        }

        _veiculo.veinome = entNome.Text ?? "";
        _veiculo.veianofabricacao = int.TryParse(entAnoFabricacao.Text, out int anoFab) ? anoFab : 0;
        _veiculo.veianomodelos = int.TryParse(entAnoModelo.Text, out int anoMod) ? anoMod : 0;
        _veiculo.velobservacoes = entObservacoes.Text ?? "";
        _veiculo.modid = modelo.modid;

        VeiculosRepositorio.Alterar(_veiculo);

        await Navigation.PopAsync();
    }

    private async void BtnVoltar_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}