using Microsoft.Maui.Controls;
using System;
using SysVeiculosApp;

namespace SysVeiculosApp
{
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
            var modelo = new Modelo
            {
                modid = _modelo.modid,
                modnome = txtNomeModelo.Text ?? string.Empty,
                modobservacoes = txtModObservacoes.Text ?? string.Empty
            };

            if (string.IsNullOrWhiteSpace(modelo.modnome))
            {
                await DisplayAlertAsync("Erro", "Por favor, insere o nome do modelo.", "OK");
                return;
            }

            ModelosRepositorio.Alterar(modelo);

            await Navigation.PopAsync();
        }

        private async void btnVoltarOnClick(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}