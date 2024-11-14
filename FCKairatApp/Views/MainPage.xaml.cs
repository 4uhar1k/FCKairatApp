using FCKairatApp.Dtos;
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
        public async void EditText(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.Count != 0)
            {
                var selectedNote = (NewsDto)e.CurrentSelection[0];
                AddNews addArticle = new AddNews(selectedNote);
                await Navigation.PushAsync(addArticle);
                addArticle.NavigatingFrom += Update;
            }                     
            
        }
    }

}
