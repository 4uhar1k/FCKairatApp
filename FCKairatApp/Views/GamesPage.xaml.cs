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
		AddGamePage addGamePage = new AddGamePage();
		await Navigation.PushAsync(addGamePage);
		addGamePage.SaveGame.Clicked += Update;
	}

    public void Update(object sender, EventArgs e)
    {        
        thisContext = new GamesNTeamsViewModel();
        BindingContext = thisContext;        
    }
}