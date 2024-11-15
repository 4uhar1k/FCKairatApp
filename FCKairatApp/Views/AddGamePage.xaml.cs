using FCKairatApp.Dtos;
using FCKairatApp.ViewModels;

namespace FCKairatApp;

public partial class AddGamePage : ContentPage
{
	public GamesNTeamsViewModel thisContext = new GamesNTeamsViewModel();
	public AddGamePage()
	{
		InitializeComponent();
        //thisContext = new GamesNTeamsViewModel();
		BindingContext = thisContext;
    }
    public AddGamePage(GameDto SelectedGame)
    {
        InitializeComponent();
        //thisContext = new GamesNTeamsViewModel();
        //thisContext.LoadTeamsNGames();
        thisContext.GameToChange = SelectedGame;
        thisContext.FirstTeamName = SelectedGame.FirstTeamName;
        thisContext.SecondTeamName = SelectedGame.SecondTeamName;
        //FirstTeamPicker.SelectedItem = thisContext.TeamNames.Where(n => n == SelectedGame.FirstTeamName).First();
        thisContext.Score = $"{SelectedGame.FirstTeamScore}:{SelectedGame.SecondTeamScore}";
        thisContext.Day = SelectedGame.GameTime.Split(' ')[0];
        thisContext.Month = SelectedGame.GameTime.Split(' ')[1];
        thisContext.Year = SelectedGame.GameTime.Split(' ')[2];
        thisContext.Time = SelectedGame.GameTime.Split(' ')[3];
        thisContext.Tournament = SelectedGame.Tournament;
        this.BindingContext = thisContext;
        //FirstTeamPicker.SelectedItem = SelectedGame.FirstTeamName;
        //thisContext.SecondTeamName = SelectedGame.SecondTeamName;
    }
    public async void goBack(object sender, EventArgs e)
	{
		await Navigation.PopAsync();
	}
}