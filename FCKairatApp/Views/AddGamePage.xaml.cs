using CommunityToolkit.Maui.Views;
using FCKairatApp.Dtos;
using FCKairatApp.ViewModels;
using SQLite;
using System.Globalization;
//using MauiToolKitPopupSample;

namespace FCKairatApp;

public partial class AddGamePage : ContentPage
{
	public GamesViewModel thisContext = new GamesViewModel();
    public SqlConnectionBase Connection { get; set; }
    public ISQLiteAsyncConnection database { get; set; }
    public ByteArrayToImageSourceConverter Converter = new ByteArrayToImageSourceConverter();
    public Type forConverter = typeof(Type);
    CultureInfo CultForConverter = new CultureInfo("es-ES", false);
    public AddGamePage()
	{
		InitializeComponent();
        Connection = new SqlConnectionBase();
        database = Connection.CreateConnection();
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
        LinkEntry.IsVisible = false;
        StartGameBtn.IsVisible = false;
    }
    public AddGamePage(GameDto SelectedGame)
    {
        InitializeComponent();
        Connection = new SqlConnectionBase();
        database = Connection.CreateConnection();
        
        //thisContext = new GamesNTeamsViewModel();
        //thisContext.LoadTeamsNGames();
        thisContext.GameToChange = SelectedGame;
        thisContext.GameToChange.Id = SelectedGame.Id;
        //thisContext.Id = SelectedGame.Id;
        thisContext.FirstTeamName = SelectedGame.FirstTeamName;
        thisContext.SecondTeamName = SelectedGame.SecondTeamName;
        thisContext.FirstTeamScore = SelectedGame.FirstTeamScore;
        thisContext.SecondTeamScore = SelectedGame.SecondTeamScore;
        thisContext.FirstTeamLogo = SelectedGame.FirstTeamLogo;
        thisContext.SecondTeamLogo = SelectedGame.SecondTeamLogo;
        //FirstTeamPicker.SelectedItem = thisContext.TeamNames.Where(n => n == SelectedGame.FirstTeamName).First();
        //thisContext.Score = $"{SelectedGame.FirstTeamScore}:{SelectedGame.SecondTeamScore}";
        thisContext.Day = SelectedGame.GameTime.Split(' ')[0];
        thisContext.Month = SelectedGame.GameTime.Split(' ')[1];
        thisContext.Year = SelectedGame.GameTime.Split(' ')[2];
        thisContext.Time = SelectedGame.GameTime.Split(' ')[3];
        thisContext.Tournament = SelectedGame.Tournament;
        thisContext.TicketsLink = SelectedGame.TicketsLink;
        //GetLogos(SelectedGame);

        // LogoOfFirstTeam.Source = (ImageSource)Converter.Convert(SelectedTeam.TeamLogo, forConverter, sender, CultForConverter);
        BindingContext = thisContext;

        SaveGame.Text = "Save changes";
        EndGame.IsVisible = SelectedGame.IsLive;
        StartGameBtn.IsVisible = (!SelectedGame.IsLive);
        AddForFirstTeam.IsVisible = SelectedGame.IsLive;
        AddForSecondTeam.IsVisible = SelectedGame.IsLive;
        LinkEntry.IsVisible = (SelectedGame.TicketsLink!=null & SelectedGame.TicketsLink!="");
        AddLinkBtn.IsVisible = (SelectedGame.TicketsLink == null | SelectedGame.TicketsLink == "");
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
    
    //public async Task GetLogos(GameDto SelectedGame)
    //{
    //    TeamDto teamone = await database.Table<TeamDto>().Where(n => n.TeamName == SelectedGame.FirstTeamName).FirstOrDefaultAsync();
    //    TeamDto teamtwo = await database.Table<TeamDto>().Where(n => n.TeamName == SelectedGame.SecondTeamName).FirstOrDefaultAsync();
    //    thisContext.FirstTeamLogo = teamone.TeamLogo;
    //    thisContext.SecondTeamLogo = teamtwo.TeamLogo;
    //    BindingContext = thisContext;
    //}
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

    public void AddGoalFirst (object sender, EventArgs e)
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
    public void AddGoalSecond(object sender, EventArgs e)
    {
        
            if (SecondTeamPicker.SelectedItem.ToString() == "FC Kairat Almaty")
                SecondKairatGoal.IsVisible = true;
            else
                SecondTeamGoal.IsVisible = true;

            SecondGoalAddBtn.IsVisible = true;
        

        //await DisplayAlert("", clickedBtn.AutomationId, "OK");
    }

    public void AddTicketsLink(object sender, EventArgs e)
    {
        LinkEntry.IsVisible = true;
        AddLinkBtn.IsVisible = false;
    }

    public async void UpdateFirstTeamLogo(object sender, EventArgs e)
    {        
        TeamDto SelectedTeam = thisContext.Teams.Where(n => n.TeamName == FirstTeamPicker.SelectedItem.ToString()).FirstOrDefault();
        LogoOfFirstTeam.Source = (ImageSource)Converter.Convert(SelectedTeam.TeamLogo, forConverter, sender, CultForConverter);
        thisContext.FirstTeamLogo = SelectedTeam.TeamLogo;
        //try
        //{
        //    TeamDto SelectedTeam = await database.Table<TeamDto>().Where(n => n.TeamName == FirstTeamPicker.SelectedItem.ToString()).FirstOrDefaultAsync();
        //    LogoOfFirstTeam.Source = (ImageSource)Converter.Convert(SelectedTeam.TeamLogo, forConverter, sender, CultForConverter);
        //}
        //catch
        //{

        //}
    }
    public async void UpdateSecondTeamLogo(object sender, EventArgs e)
    {
        TeamDto SelectedTeam = thisContext.Teams.Where(n => n.TeamName == SecondTeamPicker.SelectedItem.ToString()).FirstOrDefault();
        LogoOfSecondTeam.Source = (ImageSource)Converter.Convert(SelectedTeam.TeamLogo, forConverter, sender, CultForConverter);
        thisContext.SecondTeamLogo = SelectedTeam.TeamLogo;
    }
    public void Update(object sender, EventArgs e)
    {

        thisContext = new GamesViewModel();
        BindingContext = thisContext;
    }
}