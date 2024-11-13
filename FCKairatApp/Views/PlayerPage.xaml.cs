using FCKairatApp.ViewModels;

namespace FCKairatApp;

public partial class PlayerPage : ContentPage
{
    PlayerViewModel thisContext;
	public PlayerPage()
	{
		InitializeComponent();
        thisContext = new PlayerViewModel();
        BindingContext = thisContext;
    }

    public async void goBack(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}