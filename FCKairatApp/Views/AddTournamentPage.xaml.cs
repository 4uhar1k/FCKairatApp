using CommunityToolkit.Maui.Views;
using FCKairatApp.ViewModels;
namespace FCKairatApp
{
    public partial class AddTournamentPage : Popup
    {
        public AddTournamentPage()
        {
            InitializeComponent();
            BindingContext = new GamesNTeamsViewModel();
        }

        public void goBack(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

