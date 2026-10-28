using Microsoft.Maui.Controls;
using System;

namespace SysVeiculosApp;

public partial class ModelosAlterar : ContentPage
{
    private readonly Modelo _modelo;

    public ModelosAlterar(Modelo modelo)
    {
        InitializeComponent();

        _modelo = modelo;

        entModId.Text = _modelo.modid.ToString();
        txtNomeModelo.Text = _modelo.modnome;
        txtModObservacoes.Text = _modelo.modobservacoes;
    }

    private async void btnSalvarOnClick(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtNomeModelo.Text))
        {
            await DisplayAlertAsync("Erro", "Por favor, insere o nome do modelo.", "OK");
            return;
        }

        _modelo.modnome = txtNomeModelo.Text ?? string.Empty;
        _modelo.modobservacoes = txtModObservacoes.Text ?? string.Empty;

        ModelosRepositorio.Alterar(_modelo);

        await Navigation.PopAsync();
    }

    private async void btnVoltarOnClick(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}