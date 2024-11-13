using FCKairatApp.ViewModels;

namespace FCKairatApp;

public partial class TeamPage : ContentPage
{
	public PlayerViewModel thisContext;
	public TeamPage()
	{
		InitializeComponent();
		thisContext = new PlayerViewModel();
		BindingContext = thisContext;
	}

	public async void GoToPlayer(object sender, EventArgs e)
	{
		PlayerPage playerPage = new PlayerPage();
		await Navigation.PushAsync(playerPage);
		playerPage.NavigatingFrom += Update;
	}

	public void Update(object sender, EventArgs e)
	{
        thisContext = new PlayerViewModel();
        BindingContext = thisContext;
    }
}