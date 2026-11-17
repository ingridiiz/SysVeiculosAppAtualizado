using Microsoft.Maui.Controls;
using System;

namespace SysVeiculosApp;

public partial class VeiculosInserir : ContentPage
{
    public VeiculosInserir()
    {
        InitializeComponent();

        try
        {
            var marcas = MarcasRepositorio.Listar();
            if (marcas != null)
            {
                pickMarca.ItemsSource = marcas;
            }

            var modelos = ModelosRepositorio.Listar();
            if (modelos != null)
            {
                pickModelo.ItemsSource = modelos;
            }
        }
        catch (Exception ex)
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await DisplayAlertAsync("Erro de Base de Dados", $"Não foi possível carregar os dados: {ex.Message}", "OK");
            });
        }
    }

    private async void BtnSalvar_Clicked(object sender, EventArgs e)
    {
        try
        {
            var marca = pickMarca.SelectedItem as Marca;
            if (marca == null)
            {
                await DisplayAlertAsync("Atenção", "Selecione a marca do veículo.", "OK");
                return;
            }

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
                marid = marca.marid,
                modid = modelo.modid
            };

            VeiculosRepositorio.Inserir(veiculo);
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Erro ao salvar", ex.ToString(), "OK");
            return;
        }

        try
        {
            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Erro ao voltar", ex.ToString(), "OK");
        }
    }

    private async void BtnVoltar_Clicked(object sender, EventArgs e)
    {
        try
        {
            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Erro ao voltar", ex.ToString(), "OK");
        }
    }
}