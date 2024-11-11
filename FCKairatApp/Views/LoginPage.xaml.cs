namespace FCKairatApp;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
	}

	public void SignIn(object sender, EventArgs e)
	{
		Navigation.PopModalAsync();
	}

	public void SignUp(object sender, EventArgs e)
	{
		SignUpBtn.IsVisible = false;
		RestorePassword.IsVisible = false;
		SignInBtn.Text = "Sign up";
		SignInBtn.Clicked -= SignIn;
        SignInBtn.Clicked += SignedUp;
    }

	public async void SignedUp(object sender, EventArgs e)
	{
		await DisplayAlert("", "You've successfully signed up!", "OK");
        RestorePassword.IsVisible = true;
        SignUpBtn.IsVisible = true;
        SignInBtn.Text = "Sign in";
        SignInBtn.Clicked -= SignedUp;
        SignInBtn.Clicked += SignIn;
    }
}