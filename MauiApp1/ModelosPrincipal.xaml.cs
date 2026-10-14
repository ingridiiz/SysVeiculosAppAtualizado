using System;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Maui.Controls;

namespace SysVeiculosApp
{
    public partial class ModelosPrincipal : ContentPage
    {
        public ModelosPrincipal()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            AtualizarLista();
        }

        private void SearchBarModelos_TextChanged(object sender, TextChangedEventArgs e)
        {
            AtualizarLista();
        }

        private void AtualizarLista()
        {
            var modelos = ModelosRepositorio.Listar();
            string termo = searchBarModelos.Text;

            if (!string.IsNullOrWhiteSpace(termo))
            {
                modelos = modelos
                    .Where(m => $"{m.modid} {m.modnome} {m.modobservacoes}"
                        .Contains(termo, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            collectionViewModelos.ItemsSource = modelos;
        }

        private async void BtnInserirOnClick(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ModelosInserir());
        }

        private async void BtnEditar_Clicked(object sender, EventArgs e)
        {
            var modelo = (sender as Button)?.CommandParameter as Modelo;
            if (modelo == null) return;

            await Navigation.PushAsync(new ModelosAlterar(modelo));
        }

        private async void BtnExcluir_Clicked(object sender, EventArgs e)
        {
            var modelo = (sender as Button)?.CommandParameter as Modelo;
            if (modelo == null) return;

            bool confirmar = await DisplayAlertAsync("Confirmação",
                $"Deseja excluir o modelo {modelo.modnome}?", "Sim", "Não");

            if (confirmar)
            {
                ModelosRepositorio.Excluir(modelo.modid);
                AtualizarLista();
            }
        }
    }
}