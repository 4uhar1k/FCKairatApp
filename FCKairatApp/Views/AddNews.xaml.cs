using FCKairatApp.ViewModels;

namespace FCKairatApp;

public partial class AddNews : ContentPage
{
	public AddNews()
	{
		InitializeComponent();
		NewsViewModel thisContext = new NewsViewModel();
		BindingContext = thisContext;
		//if (thisContext.articleToChange!=null)
		//{
		//	thisContext.Title = thisContext.articleToChange.Title;
  //          thisContext.Description = thisContext.articleToChange.Description;
  //          thisContext.Author = thisContext.articleToChange.Author;
  //          thisContext.IsPublished = thisContext.articleToChange.IsPublished;
  //      }
	}
    public AddNews(NewsDto CurArticle)
    {
        InitializeComponent();
        NewsViewModel thisContext = new NewsViewModel();
        
        if (CurArticle != null)
        {
            thisContext.Title = CurArticle.Title;
            thisContext.Description = CurArticle.Description;
            thisContext.Author = CurArticle.Author;
            thisContext.IsPublished = CurArticle.IsPublished;
            thisContext.articleToChange = CurArticle;
        }
        BindingContext = thisContext;
    }

    public async void goBack(object sender, EventArgs e)
	{
		await Navigation.PopAsync();
    }
}