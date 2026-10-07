using Microsoft.Maui.Controls;
using System;
using SysVeiculosApp;

namespace SysVeiculosApp
{
    public partial class ModelosAlterar : ContentPage
    {
        private Modelo modeloSelecionado;

        public ModelosAlterar(Modelo modelo)
        {
            InitializeComponent();
            modeloSelecionado = modelo;

            txtNomeModelo.Text = modeloSelecionado.modnome;
            txtMarcaModelo.Text = modeloSelecionado.marid.ToString();

            
        }

        private async void btnSalvarOnClick(object sender, EventArgs e)
        {
            modeloSelecionado.modnome = txtNomeModelo.Text;

            modeloSelecionado.marid = txtMarcaModelo.Text;


            await App.Database.SalvarModeloAsync(modeloSelecionado);

            await DisplayAlertAsync("Sucesso", "Alteração salva com sucesso!", "OK");
            await Navigation.PopAsync();
        }

        private async void btnVoltarOnClick(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}