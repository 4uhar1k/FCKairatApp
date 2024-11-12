using FCKairatApp.ViewModels;

namespace FCKairatApp;

public partial class AddNews : ContentPage
{
	public AddNews()
	{
		InitializeComponent();
		BindingContext = new NewsViewModel();
	}

	public async void goBack(object sender, EventArgs e)
	{
		await Navigation.PopAsync();
    }
}