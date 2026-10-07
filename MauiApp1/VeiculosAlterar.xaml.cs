namespace SysVeiculosApp;

public partial class VeiculosAlterar : ContentPage
{
    private Veiculo _veiculoSelecionado;

    public VeiculosAlterar(Veiculo veiculo)
    {
        InitializeComponent();
        _veiculoSelecionado = veiculo;

        
        txtNomeVeiculo.Text = _veiculoSelecionado.veinome;
        txtAnoFabricacao.Text = _veiculoSelecionado.veianofabricacao.ToString();
        txtAnoModelo.Text = _veiculoSelecionado.veianomodelo.ToString();
        txtObservacoes.Text = _veiculoSelecionado.veiobservacoes;
    }

    private async void btnSalvarOnClick(object sender, EventArgs e)
    {
      
        _veiculoSelecionado.veinome = txtNomeVeiculo.Text;
        _veiculoSelecionado.veianofabricacao = int.Parse(txtAnoFabricacao.Text);
        _veiculoSelecionado.veianomodelo = int.Parse(txtAnoModelo.Text);
        _veiculoSelecionado.veiobservacoes = txtObservacoes.Text;

      
        await App.Database.SalvarVeiculoAsync(_veiculoSelecionado);

        await DisplayAlertAsync("Sucesso", "Alteração salva com sucesso!", "OK");
        await Navigation.PopAsync();
    }

    private async void btnVoltarOnClick(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}