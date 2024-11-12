using FCKairatApp;
using FCKairatApp.ViewModels;

namespace FCKairatApp
{
    public partial class MainPage : ContentPage
    {

        public NewsViewModel thisContext;
        public MainPage()
        {
            InitializeComponent();
            thisContext = new NewsViewModel();
            BindingContext = thisContext;
            //ShowArticles();
            if (thisContext.emailbase==null)
            {
                GoToLoginPage();
            }
            
        }

        private async void GoToLoginPage()
        {
           
            await Navigation.PushModalAsync(new LoginPage(thisContext.baseConnection));
        }

        public void Update(object sender, EventArgs e)
        {
            thisContext = new NewsViewModel();
            BindingContext = thisContext;
        }
        
        public async void AddNewText(object sender, EventArgs e)
        {
            AddNews addArticle = new AddNews();
            await Navigation.PushAsync(addArticle);
            addArticle.NavigatingFrom += Update;
        }
    }

}
