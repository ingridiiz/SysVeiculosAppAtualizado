using Microsoft.Maui.Controls;
using SysVeiculosApp;

namespace SysVeiculosApp
{
    public partial class MarcasInserir : ContentPage
    {
        public MarcasInserir()
        {
            InitializeComponent();
        }

        private async void btnSalvarOnClick(object sender, EventArgs e)
        {
            Marca novaMarca = new Marca()
            {
                marnome = txtNomeMarca.Text,
                marobservacoes = txtObsMarca.Text
            };

            if (string.IsNullOrWhiteSpace(novaMarca.marnome))
            {
                await DisplayAlertAsync("Erro", "Por favor, insere o nome da marca.", "OK");
                return;
            }

            await App.Database.SalvarMarcaAsync(novaMarca);

            await DisplayAlertAsync("Sucesso", "Marca guardada com sucesso!", "OK");

            await Navigation.PopAsync();
        }

        private async void btnVoltarOnClick(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
        

    

