using Microsoft.Maui.Controls;
using System.Collections.Generic;
using SysVeiculosApp;

namespace SysVeiculosApp
{
    public partial class ModelosPrincipal : ContentPage
    {
        public ModelosPrincipal()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var modelos = await App.Database.GetModelosAsync() ?? new List<Modelo>();
            collectionViewModelos.ItemsSource = modelos;
        }

        private void collectionViewModelos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.Count > 0)
            {
                collectionViewModelos.SelectedItem = e.CurrentSelection[0];
            }
        }

        private async void btnInserirOnClick(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ModelosInserir());
        }

        private async void btnAlterar_Clicked(object sender, EventArgs e)
        {
            Modelo? modeloSelecionado = collectionViewModelos.SelectedItem as Modelo;
            if (modeloSelecionado == null)
            {
                await DisplayAlertAsync("Aviso", "Por favor, seleciona um modelo na lista para alterar.", "OK");
                return;
            }

            
            await Navigation.PushAsync(new ModelosAlterar(modeloSelecionado));
        }

        private async void btnExcluir_Clicked(object sender, EventArgs e)
        {
            Modelo? modeloSelecionado = collectionViewModelos.SelectedItem as Modelo;
            if (modeloSelecionado == null)
            {
                await DisplayAlertAsync("Aviso", "Por favor, seleciona um modelo na lista para excluir.", "OK");
                return;
            }

            bool confirmar = await DisplayAlertAsync("Excluir", "Deseja realmente excluir este modelo?", "Sim", "Não");
            if (confirmar)
            {
                await App.Database.DeletarModeloAsync(modeloSelecionado);
                await DisplayAlertAsync("Sucesso", "Modelo excluído com sucesso!", "OK");

                var modelos = await App.Database.GetModelosAsync() ?? new List<Modelo>();
                collectionViewModelos.ItemsSource = modelos;
            }
        }
    }
}