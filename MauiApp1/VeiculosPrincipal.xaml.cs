using Microsoft.Maui.Controls;

namespace SysVeiculosApp
{
    public partial class VeiculosPrincipal : ContentPage
    {
        public VeiculosPrincipal()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var veiculos = await App.Database.GetVeiculosAsync() ?? new List<Veiculo>();
            collectionViewVeiculos.ItemsSource = veiculos;
        }

        private void collectionViewVeiculos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.Count > 0)
            {
                collectionViewVeiculos.SelectedItem = e.CurrentSelection[0];
            }
        }

        private async void btnInserirOnClick(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new VeiculosInserir());
        }

        private async void btnAlterar_Clicked(object sender, EventArgs e)
        {
            Veiculo? veiculoSelecionado = collectionViewVeiculos.SelectedItem as Veiculo;
            if (veiculoSelecionado == null)
            {
                await DisplayAlertAsync("Aviso", "Por favor, seleciona um veículo na lista para alterar.", "OK");
                return;
            }

            await Navigation.PushAsync(new VeiculosAlterar(veiculoSelecionado));
        }

        private async void btnExcluir_Clicked(object sender, EventArgs e)
        {
            Veiculo? veiculoSelecionado = collectionViewVeiculos.SelectedItem as Veiculo;
            if (veiculoSelecionado == null)
            {
                await DisplayAlertAsync("Aviso", "Por favor, seleciona um veículo na lista para excluir.", "OK");
                return;
            }

            bool confirmar = await DisplayAlertAsync("Excluir", "Deseja realmente excluir este veículo?", "Sim", "Não");
            if (confirmar)
            {
                await App.Database.DeletarVeiculoAsync(veiculoSelecionado);
                await DisplayAlertAsync("Sucesso", "Veículo excluído com sucesso!", "OK");

                var veiculos = await App.Database.GetVeiculosAsync() ?? new List<Veiculo>();
                collectionViewVeiculos.ItemsSource = veiculos;
            }
        }
    }
}