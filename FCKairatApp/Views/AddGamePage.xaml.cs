using CommunityToolkit.Maui.Views;
using FCKairatApp.Dtos;
using FCKairatApp.ViewModels;
//using MauiToolKitPopupSample;

namespace FCKairatApp;

public partial class AddGamePage : ContentPage
{
	public GamesViewModel thisContext = new GamesViewModel();
	public AddGamePage()
	{
		InitializeComponent();
        //thisContext = new GamesNTeamsViewModel();
		BindingContext = thisContext;
        FirstTeamGoal.IsVisible = false;
        FirstKairatGoal.IsVisible = false;
        SecondTeamGoal.IsVisible = false;
        SecondKairatGoal.IsVisible = false;
        EndGame.IsVisible = false;
        FirstGoalAddBtn.IsVisible = false;
        SecondGoalAddBtn.IsVisible = false;
        AddForFirstTeam.IsVisible = false;
        AddForSecondTeam.IsVisible = false;
    }
    public AddGamePage(GameDto SelectedGame)
    {
        InitializeComponent();
        //thisContext = new GamesNTeamsViewModel();
        //thisContext.LoadTeamsNGames();
        thisContext.GameToChange = SelectedGame;
        thisContext.GameToChange.Id = SelectedGame.Id;
        //thisContext.Id = SelectedGame.Id;
        thisContext.FirstTeamName = SelectedGame.FirstTeamName;
        thisContext.SecondTeamName = SelectedGame.SecondTeamName;
        thisContext.FirstTeamScore = SelectedGame.FirstTeamScore;
        thisContext.SecondTeamScore = SelectedGame.SecondTeamScore;
        //FirstTeamPicker.SelectedItem = thisContext.TeamNames.Where(n => n == SelectedGame.FirstTeamName).First();
        //thisContext.Score = $"{SelectedGame.FirstTeamScore}:{SelectedGame.SecondTeamScore}";
        thisContext.Day = SelectedGame.GameTime.Split(' ')[0];
        thisContext.Month = SelectedGame.GameTime.Split(' ')[1];
        thisContext.Year = SelectedGame.GameTime.Split(' ')[2];
        thisContext.Time = SelectedGame.GameTime.Split(' ')[3];
        thisContext.Tournament = SelectedGame.Tournament;

        BindingContext = thisContext;


        SaveGame.Text = "Save changes";
        EndGame.IsVisible = SelectedGame.IsLive;

        AddForFirstTeam.IsVisible = SelectedGame.IsLive;
        AddForSecondTeam.IsVisible = SelectedGame.IsLive;

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

    public async void AddGoalFirst (object sender, EventArgs e)
    {
        
            if (FirstTeamPicker.SelectedItem.ToString() == "FC Kairat Almaty")
            {
                FirstKairatGoal.IsVisible = true;
            }
            else
                FirstTeamGoal.IsVisible = true;
            FirstGoalAddBtn.IsVisible = true;
            
        

        //await DisplayAlert("", clickedBtn.AutomationId, "OK");
    }
    public async void AddGoalSecond(object sender, EventArgs e)
    {
        
            if (SecondTeamPicker.SelectedItem.ToString() == "FC Kairat Almaty")
                SecondKairatGoal.IsVisible = true;
            else
                SecondTeamGoal.IsVisible = true;

            SecondGoalAddBtn.IsVisible = true;
        

        //await DisplayAlert("", clickedBtn.AutomationId, "OK");
    }
    public void Update(object sender, EventArgs e)
    {

        thisContext = new GamesViewModel();
        BindingContext = thisContext;
    }
}