using Microsoft.Maui.Controls;
using SysVeiculosApp;

namespace SysVeiculosApp
{
    public partial class VeiculosInserir : ContentPage
    {
        public VeiculosInserir()
        {
            InitializeComponent();
        }

        private async void btnSalvarOnClick(object sender, EventArgs e)
        {
            Veiculo novoVeiculo = new Veiculo()
            {
                veinome = txtNome.Text,
                veianofabricacao = int.TryParse(txtAnoFabricacao.Text, out int fab) ? fab : 0,
                veianomodelo = int.TryParse(txtAnoModelo.Text, out int mod) ? mod : 0,
                veiobservacoes = txtObservacoes.Text
            };

            if (string.IsNullOrWhiteSpace(novoVeiculo.veinome))
            {
                await DisplayAlertAsync("Erro", "Por favor, insere o nome do veículo.", "OK");
                return;
            }

            await App.Database.SalvarVeiculoAsync(novoVeiculo);

            await DisplayAlertAsync("Sucesso", "Veículo guardado com sucesso!", "OK");

            await Navigation.PopAsync();
        }

        private async void btnVoltarOnClick(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}