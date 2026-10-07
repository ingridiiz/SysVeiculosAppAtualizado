using Microsoft.Maui.Controls;
using SysVeiculosApp;

namespace SysVeiculosApp
{
    public partial class MarcasPrincipal : ContentPage
    {
        public MarcasPrincipal()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var marcas = await App.Database.GetMarcasAsync() ?? new List<Marca>();
            collectionViewMarcas.ItemsSource = marcas;
        }

        private void collectionViewMarcas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.Count > 0)
            {
                collectionViewMarcas.SelectedItem = e.CurrentSelection[0];
            }
        }

        private async void btnInserir_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MarcasInserir());
        }

        private async void btnAlterar_Clicked(object sender, EventArgs e)
        {
            Marca? marcaSelecionada = collectionViewMarcas.SelectedItem as Marca;
            if (marcaSelecionada == null)
            {
                await DisplayAlertAsync("Aviso", "Por favor, seleciona uma marca na lista para alterar.", "OK");
                return;
            }

            await Navigation.PushAsync(new MarcasAlterar(marcaSelecionada));
        }

        private async void btnExcluir_Clicked(object sender, EventArgs e)
        {
            Marca? marcaSelecionada = collectionViewMarcas.SelectedItem as Marca;
            if (marcaSelecionada == null)
            {
                await DisplayAlertAsync("Aviso", "Por favor, seleciona uma marca na lista para excluir.", "OK");
                return;
            }

            bool confirmar = await DisplayAlertAsync("Excluir", "Deseja realmente excluir esta marca?", "Sim", "Não");
            if (confirmar)
            {
                await App.Database.DeletarMarcaAsync(marcaSelecionada);
                await DisplayAlertAsync("Sucesso", "Marca excluída com sucesso!", "OK");

                var marcas = await App.Database.GetMarcasAsync() ?? new List<Marca>();
                collectionViewMarcas.ItemsSource = marcas;
            }
        }
    }
}