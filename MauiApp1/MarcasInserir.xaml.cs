namespace SysVeiculosApp;

public partial class MarcasInserir : ContentPage
{
    public MarcasInserir()
    {
        InitializeComponent();
    }

    private async void BtnSalvar_Clicked(object sender, EventArgs e)
    {
        var marca = new Marca
        {
            marnome = entNome.Text ?? "",
            marobservacoes = entObservacoes.Text ?? ""
        };

        MarcasRepositorio.Inserir(marca);

        await Navigation.PopAsync();
    }

    private async void BtnVoltar_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}