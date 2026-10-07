using Microsoft.Maui.Controls;
using SysVeiculosApp;

namespace SysVeiculosApp
{
    public partial class MarcasAlterar : ContentPage
    {
        private Marca marcaSelecionada;

        public MarcasAlterar(Marca marca)
        {
            InitializeComponent();
            marcaSelecionada = marca;

           
            txtMarNome.Text = marcaSelecionada.marnome;
            txtMarObservacoes.Text = marcaSelecionada.marobservacoes;
        }

        private async void btnSalvarOnClick(object sender, EventArgs e)
        {
            
            marcaSelecionada.marnome = txtMarNome.Text;
            marcaSelecionada.marobservacoes = txtMarObservacoes.Text;

            await App.Database.SalvarMarcaAsync(marcaSelecionada);

            await DisplayAlertAsync("Sucesso", "Alteração salva com sucesso!", "OK");
            await Navigation.PopAsync();
        }

        private async void btnVoltarOnClick(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}