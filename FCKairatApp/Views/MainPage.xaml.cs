using FCKairatApp;

namespace FCKairatApp
{
    public partial class MainPage : ContentPage
    {
        

        public MainPage()
        {
            InitializeComponent();
            GoToLoginPage();
        }

        private async void GoToLoginPage()
        {
            await Navigation.PushModalAsync(new LoginPage());
        }
    }

}
