using FCKairatApp.ViewModels;
namespace FCKairatApp;

public partial class ProfilePage : ContentPage
{
	public ProfileViewModel thisContext;

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
	public void Update(object sender, EventArgs e)
	{
		thisContext = new ProfileViewModel();
		BindingContext = thisContext;
    }
}