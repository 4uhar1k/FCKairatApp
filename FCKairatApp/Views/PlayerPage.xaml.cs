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
        thisContext.PlayerImage = PlayerToChange.PlayerImage;
        thisContext.PlayerToEdit = PlayerToChange;
        BindingContext = thisContext;
    }

    public async void AddImageFunc(object sender, EventArgs e)
    {
        var result = await FilePicker.Default.PickAsync(new PickOptions
        {
            PickerTitle = "Select an image",
            FileTypes = FilePickerFileType.Images // Only allow image files
        });

        if (result != null)
        {
            using var fileStream = await result.OpenReadAsync();

            // Copy the file stream to a memory stream
            var memoryStream = new MemoryStream();
            await fileStream.CopyToAsync(memoryStream);
            memoryStream.Seek(0, SeekOrigin.Begin);

            // Set the image source
            ImageOfPlayer.Source = ImageSource.FromStream(() => memoryStream);



            thisContext.PlayerImage = memoryStream.ToArray();
        }
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