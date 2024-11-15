using FCKairatApp.Dtos;
using FCKairatApp.ViewModels;

namespace FCKairatApp;

public partial class TeamsListPage : ContentPage
{
	public GamesNTeamsViewModel thisContext;
	public TeamsListPage()
	{
		InitializeComponent();		
		//AddingGrid.IsVisible = false;
		thisContext = new GamesNTeamsViewModel();
		BindingContext = thisContext;
		
	}

	public void AddBtnClicked (object sender, EventArgs e)
	{
		AddingGrid.IsVisible = true;
	}

	public void TeamClicked(object sender, SelectionChangedEventArgs e)
	{
		if (e.CurrentSelection.Count!=0)
		{
			TeamDto SelectedGame = (TeamDto)e.CurrentSelection[0];
            AddingGrid.IsVisible = true;
			NameEntry.Text = SelectedGame.TeamName;
			CoachEntry.Text = SelectedGame.CoachName;
			AddBtn.Text = "Edit";
			TeamsCollection.SelectedItem = null;
            //thisContext = new GamesNTeamsViewModel();
            //BindingContext = thisContext;

        }
        
    }

	public async void Update(object sender, EventArgs e)
	{
		if (thisContext.SameTeamName!=null)
		{
			await DisplayAlert("", "There is already a team with this name", "OK");
		}
		else
		{
            thisContext = new GamesNTeamsViewModel();
            BindingContext = thisContext;
            AddBtn.Text = "Add";
            //AddingGrid.IsVisible = false;
        }
        
    }
}