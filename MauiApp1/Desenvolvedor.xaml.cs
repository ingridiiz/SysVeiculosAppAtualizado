namespace SysVeiculosApp;

public partial class Desenvolvedor : ContentPage
{
    public Desenvolvedor()
    {
        InitializeComponent();
    }

    private async void BtnHome_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//MainPage");
    }
}