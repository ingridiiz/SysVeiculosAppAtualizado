
namespace SysVeiculosApp;

public partial class MarcasInserir : ContentPage
{
    public MarcasInserir()
    {
        InitializeComponent();
    }

    private async void BtnSalvar_Clicked(object sender, EventArgs e)
    {
        var modelo = new Modelo
        {
            modnome = entNome.Text ?? "",
            modobservacoes = entObservacoes.Text ?? ""
        };

        ModelosRepositorio.Inserir(modelo);

        await Navigation.PopAsync(); 
    }

    private async void BtnVoltar_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}