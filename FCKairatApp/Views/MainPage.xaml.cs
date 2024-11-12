using FCKairatApp;
using FCKairatApp.ViewModels;

namespace FCKairatApp
{
    public partial class MainPage : ContentPage
    {

        public ViewModelBase thisContext;
        public MainPage()
        {
            InitializeComponent();
            thisContext = new ViewModelBase();
            BindingContext = thisContext;
            if (thisContext.emailbase==null)
            {
                GoToLoginPage();
            }
            
        }

        private async void GoToLoginPage()
        {
            await Navigation.PushModalAsync(new LoginPage(thisContext.baseConnection));
        }
    }

}
