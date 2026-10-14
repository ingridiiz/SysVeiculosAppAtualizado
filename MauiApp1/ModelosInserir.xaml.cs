using Microsoft.Maui.Controls;
using System;
using SysVeiculosApp;

namespace SysVeiculosApp
{
    public partial class ModelosInserir : ContentPage
    {
        public ModelosInserir()
        {
            InitializeComponent();
        }

        private async void btnSalvarOnClick(object sender, EventArgs e)
        {
            var modelo = new Modelo
            {
                modnome = txtNomeModelo.Text ?? string.Empty
            };

            if (string.IsNullOrWhiteSpace(modelo.modnome))
            {
                await DisplayAlertAsync("Erro", "Por favor, insere o nome do modelo.", "OK");
                return;
            }

            ModelosRepositorio.Inserir(modelo);

            await Navigation.PopAsync(); 
        }

        private async void btnVoltarOnClick(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}