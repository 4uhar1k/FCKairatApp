using FCKairatApp.ViewModels;
namespace FCKairatApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
        }

        public void Update(object sender, EventArgs e)
        {
            if (Shell.Current.CurrentPage.Title == "Timetable" | Shell.Current.CurrentPage.Title == "Our Team" | Shell.Current.CurrentPage.Title == "TablePage")
            {
                Shell.Current.CurrentPage.BindingContext = new GamesNTeamsViewModel();
            }
            else if (Shell.Current.CurrentPage.Title == "News")
            {
                Shell.Current.CurrentPage.BindingContext = new NewsViewModel();
            }
            else if(Shell.Current.CurrentPage.Title == "Your Profile")
            {
                Shell.Current.CurrentPage.BindingContext = new ProfileViewModel();
            }
        }
    }
}
