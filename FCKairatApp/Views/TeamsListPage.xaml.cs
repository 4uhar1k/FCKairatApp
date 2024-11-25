using FCKairatApp.Dtos;
using FCKairatApp.ViewModels;
using System.Globalization;

namespace FCKairatApp;

public partial class TeamsListPage : ContentPage
{
	public TeamsViewModel thisContext;
    public ByteArrayToImageSourceConverter Converter = new ByteArrayToImageSourceConverter();
    public Type forConverter = typeof(Type);
    CultureInfo CultForConverter = new CultureInfo("es-ES", false);
    public TeamsListPage()
	{
		InitializeComponent();		
		//AddingGrid.IsVisible = false;
		thisContext = new TeamsViewModel();
		BindingContext = thisContext;
       
	}

	public void AddBtnClicked (object sender, EventArgs e)
	{
		AddingGrid.IsVisible = true;
	}

	public void TeamClicked(object sender, SelectionChangedEventArgs e)
	{
		if (e.CurrentSelection.Count!=0)
		{
			TeamDto SelectedGame = (TeamDto)e.CurrentSelection[0];
            AddingGrid.IsVisible = true;
			NameEntry.Text = SelectedGame.TeamName;
			CoachEntry.Text = SelectedGame.CoachName;
            thisContext.TeamLogo = SelectedGame.TeamLogo;
            LogoOfTeam.Source = (ImageSource)Converter.Convert(SelectedGame.TeamLogo, forConverter, sender, CultForConverter);
            //LogoOfTeam.Source = "{Binding TeamLogo, Converter={StaticResource ByteArrayToImageSourceConverter}}";
			//AddBtn.Text = "Edit";
			TeamsCollection.SelectedItem = null;
            //Update(sender, e);
            //thisContext = new GamesNTeamsViewModel();
            //BindingContext = thisContext;

        }
        
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
            LogoOfTeam.Source = ImageSource.FromStream(() => memoryStream);



            thisContext.TeamLogo = memoryStream.ToArray();
        }
    }

    public async void Update(object sender, EventArgs e)
	{
		if (thisContext.SameTeamName!=null)
		{
			await DisplayAlert("", "There is already a team with this name", "OK");
		}
		else
		{
            thisContext = new TeamsViewModel();
            BindingContext = thisContext;
            //AddBtn.Text = "Add";
            LogoOfTeam.Source = "";
            //AddingGrid.IsVisible = false;
        }
        
    }
}