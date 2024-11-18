using CommunityToolkit.Maui.Views;
using FCKairatApp.Dtos;
using FCKairatApp.ViewModels;
//using MauiToolKitPopupSample;

namespace FCKairatApp;

public partial class AddGamePage : ContentPage
{
	public GamesNTeamsViewModel thisContext = new GamesNTeamsViewModel();
	public AddGamePage()
	{
		InitializeComponent();
        //thisContext = new GamesNTeamsViewModel();
		BindingContext = thisContext;
        FirstTeamGoal.IsVisible = false;
        FirstKairatGoal.IsVisible = false;
        SecondTeamGoal.IsVisible = false;
        SecondKairatGoal.IsVisible = false;
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

        BindingContext = thisContext;

        FirstTeamGoal.IsVisible = false;
        FirstKairatGoal.IsVisible = false;
        SecondTeamGoal.IsVisible = false;
        SecondKairatGoal.IsVisible = false;
        //FirstTeamPicker.SelectedItem = SelectedGame.FirstTeamName;
        //thisContext.SecondTeamName = SelectedGame.SecondTeamName;
    }
    public async void goBack(object sender, EventArgs e)
	{
		await Navigation.PopAsync();
	}

    public void AddTournament (object sender, EventArgs e)
    {
        try
        {
            if (TournamentPicker.SelectedItem.ToString() == "Add new tournament...")
            {
                AddTournamentPage addTournamentPage = new AddTournamentPage();
                this.ShowPopup(addTournamentPage);
                TournamentPicker.SelectedItem = null;
                addTournamentPage.ReadyBtn.Clicked += Update;
            }
        }
        catch
        {

        }
    }

    public async void AddGoal (object sender, EventArgs e)
    {
        Button clickedBtn = (Button)sender;
        if (clickedBtn.AutomationId == "AddForFirstTeam" & FirstTeamPicker.SelectedItem!=null)
        {
            if (FirstTeamPicker.SelectedItem.ToString() == "FC Kairat Almaty")
            {
                FirstKairatGoal.IsVisible = true;
            }
            else
                FirstTeamGoal.IsVisible = true;
        }
        else if (clickedBtn.AutomationId == "AddForSecondTeam" & SecondTeamPicker.SelectedItem != null)
        {
            if (SecondTeamPicker.SelectedItem.ToString() == "FC Kairat Almaty")
                SecondKairatGoal.IsVisible = true;
            else
                SecondTeamGoal.IsVisible = true;
        }

        //await DisplayAlert("", clickedBtn.AutomationId, "OK");
    }
    public void Update(object sender, EventArgs e)
    {
        thisContext = new GamesNTeamsViewModel();
        BindingContext = thisContext;
    }
}