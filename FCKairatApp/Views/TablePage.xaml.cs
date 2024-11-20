using FCKairatApp.ViewModels;
namespace FCKairatApp;

public partial class TablePage : ContentPage
{
	public TablePage()
	{
		InitializeComponent();
		BindingContext = new GamesNTeamsViewModel();
	}
}