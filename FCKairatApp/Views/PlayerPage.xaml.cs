using FCKairatApp.Dtos;
using FCKairatApp.ViewModels;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FCKairatApp;

public partial class PlayerPage : ContentPage
{
    PlayerViewModel thisContext;
    public PlayerDto oldPlayerName, oldPlayerNumber;
     
	public PlayerPage()
	{
		InitializeComponent();
        thisContext = new PlayerViewModel();
        BindingContext = thisContext;
        if (thisContext.GoalsAmount == 0)
        {
            GoalsEntry.Text = "";
        }
        if (thisContext.AssistsAmount == 0)
        {
            AssistsEntry.Text = "";
        }
        if (thisContext.Number == 0)
        {
            NumberEntry.Text = "";
        }
    }
    public PlayerPage(object Player)
    {
        InitializeComponent();
        PlayerDto PlayerToChange = (PlayerDto)Player;
        thisContext = new PlayerViewModel();
        
        thisContext.Name = PlayerToChange.Name;
        thisContext.Surname = PlayerToChange.Surname;
        thisContext.Number = PlayerToChange.Number;
        thisContext.Position = PlayerToChange.Position;
        thisContext.GoalsAmount = PlayerToChange.GoalsAmount;
        thisContext.AssistsAmount = PlayerToChange.AssistsAmount;
        thisContext.StartMonth = PlayerToChange.StartDate.Split(' ')[0];
        thisContext.StartYear = PlayerToChange.StartDate.Split(' ')[1];
        thisContext.ExpiryMonth = PlayerToChange.ExpiryDate.Split(' ')[0];
        thisContext.ExpiryYear = PlayerToChange.ExpiryDate.Split(' ')[1];
        thisContext.PlayerToEdit = PlayerToChange;
        BindingContext = thisContext;
    }

    public async void goBack(object sender, EventArgs e)
    {
        
        if (thisContext.oldPlayerName != null & thisContext.PlayerToEdit == null)
        {
            await DisplayAlert("", "Player with this name already exists", "OK");
        }
        else if (thisContext.oldPlayerNumber != null & thisContext.PlayerToEdit == null)
        {
            await DisplayAlert("", "Player with this number already exists", "OK");
        }
        else
            await Navigation.PopAsync();
    }

    public async void PlayerUniqueCheck()
    {
        
    }

}