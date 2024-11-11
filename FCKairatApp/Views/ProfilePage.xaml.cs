using FCKairatApp.ViewModels;
namespace FCKairatApp;

public partial class ProfilePage : ContentPage
{
	public ProfilePage()
	{
		InitializeComponent();
		BindingContext = new ProfileViewModel();
	}
	public void Update(object sender, EventArgs e)
	{
		this.BindingContext = new ProfileViewModel();
	}
}