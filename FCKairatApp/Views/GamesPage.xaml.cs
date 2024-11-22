using FCKairatApp.Dtos;
using FCKairatApp.ViewModels;

namespace FCKairatApp;

public partial class GamesPage : ContentPage
{
	public GamesViewModel thisContext;
	public GamesPage()
	{
		InitializeComponent();
		thisContext = new GamesViewModel();
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

	public async void EditGame(object sender, SelectionChangedEventArgs e)
	{
		if (e.CurrentSelection.Count!=0)
		{
			GameDto SelectedGame = (GameDto)e.CurrentSelection[0];
            AddGamePage addGamePage = new AddGamePage(SelectedGame);
            await Navigation.PushAsync(addGamePage);
            addGamePage.SaveGame.Clicked += Update;
			addGamePage.EndGame.Clicked += Update;
			addGamePage.FirstGoalAddBtn.Clicked += Update;
			addGamePage.SecondGoalAddBtn.Clicked += Update;
			addGamePage.StartGameBtn.Clicked += Update;
			GamesCollection.SelectedItem = null;
        }
	}
    public void Update(object sender, EventArgs e)
    {        
        thisContext = new GamesViewModel();
        BindingContext = thisContext;        
    }
}