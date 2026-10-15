using Microsoft.Maui.Controls;
using System;

namespace SysVeiculosApp;

public partial class MarcasAlterar : ContentPage
{
    private Modelo _modelo;

    public MarcasAlterar(Modelo modelo)
    {
        InitializeComponent();
        _modelo = modelo;

        entNome.Text = _modelo.modnome;
        entObservacoes.Text = _modelo.modobservacoes;
    }

    private async void BtnSalvar_Clicked(object sender, EventArgs e)
    {
        _modelo.modnome = entNome.Text ?? "";
        _modelo.modobservacoes = entObservacoes.Text ?? "";

        ModelosRepositorio.Alterar(_modelo);

        await Navigation.PopAsync();
    }

    private async void BtnVoltar_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}