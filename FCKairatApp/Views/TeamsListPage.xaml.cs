namespace FCKairatApp;

public partial class TeamsListPage : ContentPage
{
	public TeamsListPage()
	{
		InitializeComponent();
		AddingGrid.IsVisible = false;
	}

	public void AddBtnClicked (object sender, EventArgs e)
	{
		AddingGrid.IsVisible = true;
	}
}