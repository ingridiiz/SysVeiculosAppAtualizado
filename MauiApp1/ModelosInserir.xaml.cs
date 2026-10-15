using Microsoft.Maui.Controls;
using System;

namespace SysVeiculosApp;

public partial class ModelosInserir : ContentPage
{
    public ModelosInserir()
    {
        InitializeComponent();
    }

    private async void BtnSalvar_Clicked(object sender, EventArgs e)
    {
        var modelo = new Modelo
        {
            modnome = entNome.Text ?? "",
            modobservacoes = entObservacoes.Text ?? ""
        };

        ModelosRepositorio.Inserir(modelo);

        await Navigation.PopAsync(); // volta para a lista
    }

    private async void BtnVoltar_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}