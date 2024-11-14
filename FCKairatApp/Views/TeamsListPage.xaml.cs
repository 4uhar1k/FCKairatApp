using FCKairatApp.ViewModels;

namespace FCKairatApp;

public partial class TeamsListPage : ContentPage
{
	public GamesNTeamsViewModel thisContext;
	public TeamsListPage()
	{
		InitializeComponent();		
		AddingGrid.IsVisible = false;
		thisContext = new GamesNTeamsViewModel();
		BindingContext = thisContext;
	}

	public void AddBtnClicked (object sender, EventArgs e)
	{
		AddingGrid.IsVisible = true;
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
            AddingGrid.IsVisible = false;
        }
        
    }
}