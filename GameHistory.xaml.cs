namespace MathGame.Maui;

public partial class GameHistory : ContentPage
{
	public GameHistory()
	{
		InitializeComponent();
        gamesList.ItemsSource = App.GameRepository.GetAllGames();
    }

    public void OnDelete(object sender, EventArgs e)
    {
        Button button = (Button)sender;

        App.GameRepository.Delete((int)button.BindingContext);

        gamesList.ItemsSource = App.GameRepository.GetAllGames();
    }
}