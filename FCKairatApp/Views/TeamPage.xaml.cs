using FCKairatApp.ViewModels;

namespace FCKairatApp;

public partial class TeamPage : ContentPage
{
	public PlayerViewModel playersContext;
	public TeamPage()
	{
		InitializeComponent();
        playersContext = new PlayerViewModel();
		BindingContext = playersContext;
	}

	

	public async void GoToPlayer(object sender, EventArgs e)
	{
		Button btn = sender as Button;
		PlayerPage playerPage;
		if (btn.CommandParameter!=null)
		{
            playerPage = new PlayerPage(btn.CommandParameter);
        }
		else
		{
            playerPage = new PlayerPage();
            
        }
        await Navigation.PushAsync(playerPage);
        playerPage.NavigatingFrom += Update;

    }

	public void Update(object sender, EventArgs e)
	{
		
            playersContext = new PlayerViewModel();
            BindingContext = playersContext;
        
        
    }
}