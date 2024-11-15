using FCKairatApp.ViewModels;

namespace FCKairatApp;

public partial class GamesPage : ContentPage
{
	public GamesNTeamsViewModel thisContext;
	public GamesPage()
	{
		InitializeComponent();
		thisContext = new GamesNTeamsViewModel();
		BindingContext = thisContext;
	}

	public async void ShowList(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new TeamsListPage());
	}
	public async void AddGame(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new AddGamePage());
	}

    public void Update(object sender, EventArgs e)
    {        
        thisContext = new GamesNTeamsViewModel();
        BindingContext = thisContext;        
    }
}