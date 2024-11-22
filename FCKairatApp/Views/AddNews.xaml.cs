using FCKairatApp.Dtos;
using FCKairatApp.ViewModels;
using Microsoft.Maui.Storage;

namespace FCKairatApp;

public partial class AddNews : ContentPage
{
    public NewsViewModel thisContext;

    public AddNews()
	{
		InitializeComponent();
		thisContext = new NewsViewModel();
		BindingContext = thisContext;
        DeleteBtn.IsVisible = false;		
	}
    public AddNews(NewsDto CurArticle)
    {
        InitializeComponent();
        thisContext = new NewsViewModel();
        DeleteBtn.IsVisible = true;
        if (CurArticle != null)
        {
            thisContext.Title = CurArticle.Title;
            thisContext.Description = CurArticle.Description;
            thisContext.Author = CurArticle.Author;
            thisContext.IsPublished = CurArticle.IsPublished;
            thisContext.NewsImage = CurArticle.NewsImage;
            thisContext.articleToChange = CurArticle;
        }
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
            NewsImage.Source = ImageSource.FromStream(() => memoryStream);



            thisContext.NewsImage = memoryStream.ToArray();
        }
    }

    public async void goBack(object sender, EventArgs e)
	{
		await Navigation.PopAsync();
    }
}