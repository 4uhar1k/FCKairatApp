using FCKairatApp.ViewModels;

namespace FCKairatApp;

public partial class AddGamePage : ContentPage
{
	public GamesNTeamsViewModel thisContext;
	public AddGamePage()
	{
		InitializeComponent();
        thisContext = new GamesNTeamsViewModel();
		BindingContext = thisContext;
    }
	public async void goBack(object sender, EventArgs e)
	{
		await Navigation.PopAsync();
	}
}