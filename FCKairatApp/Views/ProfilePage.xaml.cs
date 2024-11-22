using FCKairatApp.ViewModels;
using FCKairatApp.Dtos;
using System.ComponentModel.DataAnnotations.Schema;
using SQLite;
namespace FCKairatApp;

public partial class ProfilePage : ContentPage
{
	public ProfileViewModel thisContext;
	//public ISQLiteAsyncConnection database;

    public ProfilePage()
	{
		InitializeComponent();
		thisContext = new ProfileViewModel();
		BindingContext = thisContext;
	}
	public async void LogOut(object sender, EventArgs e)
	{
		File.Delete(thisContext.CurUser);
		LoginPage loginPage = new LoginPage(thisContext.baseConnection);
        await Navigation.PushModalAsync(loginPage);
		loginPage.NavigatingFrom += Update;
		//loginPage.SignInBtn.Clicked += Update;
	}

	public async void GoToGame(object sender, SelectionChangedEventArgs e)
	{
		if (e.CurrentSelection.Count!=0)
		{
			TicketDto TicketOfGame = (TicketDto)e.CurrentSelection[0];
			GameDto SelectedGame = await thisContext.database.Table<GameDto>().Where(n => n.Id == TicketOfGame.GameId).FirstOrDefaultAsync(); 
            AddGamePage addGamePage = new AddGamePage(SelectedGame);
			await Navigation.PushAsync(addGamePage);
        }
		
	}
	public void Update(object sender, EventArgs e)
	{
		thisContext = new ProfileViewModel();
		BindingContext = thisContext;
    }
}