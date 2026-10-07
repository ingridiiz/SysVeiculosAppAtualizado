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
          
            int.TryParse(txtMarcaModelo.Text, out int marcaId);

            Modelo novoModelo = new Modelo()
            {
                modnome = txtNomeModelo.Text,
                marid = txtMarcaModelo.Text
            };

            if (string.IsNullOrWhiteSpace(novoModelo.modnome))
            {
                await DisplayAlertAsync("Erro", "Por favor, insere o nome do modelo.", "OK");
                return;
            }

            await App.Database.SalvarModeloAsync(novoModelo);

            await DisplayAlertAsync("Sucesso", "Modelo guardado com sucesso!", "OK");

            await Navigation.PopAsync();
        }

        private async void btnVoltarOnClick(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}