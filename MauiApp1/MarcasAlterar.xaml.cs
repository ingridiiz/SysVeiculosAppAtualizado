using Microsoft.Maui.Controls;
using System;

namespace SysVeiculosApp;

public partial class MarcasAlterar : ContentPage
{
    private Marca _marca;

    public MarcasAlterar(Marca marca)
    {
        InitializeComponent();
        _marca = marca;

        entNome.Text = _marca.marnome;
        entObservacoes.Text = _marca.marobservacoes;
    }

    private async void BtnSalvar_Clicked(object sender, EventArgs e)
    {
        _marca.marnome = entNome.Text ?? "";
        _marca.marobservacoes = entObservacoes.Text ?? "";

        MarcasRepositorio.Alterar(_marca);

        await Navigation.PopAsync();
    }

    private async void BtnVoltar_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}