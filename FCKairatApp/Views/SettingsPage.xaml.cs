using FCKairatApp.ViewModels;

namespace FCKairatApp;

public partial class SettingsPage : ContentPage
{
	public ViewModelBase thisContext;
	public SettingsPage()
	{
		InitializeComponent();
		thisContext = new ViewModelBase();
		BindingContext = thisContext;
	}

    public async void LanguageChanged(object sender, EventArgs e)
    {
        try
        {
            if (!File.Exists(thisContext.CurLanguage))
            {
                File.Create(thisContext.CurLanguage);
            }
            using (StreamWriter sw = new StreamWriter(thisContext.CurLanguage, false))
            {
                sw.WriteLine(LanguagePicker.SelectedItem.ToString());
                sw.Close();
            }
            Update();
            await DisplayAlert("", thisContext.ReloadAppBinding, "OK");
            

        }
        catch { }
    }
    public void Update()
    {
        thisContext = new ViewModelBase();
        BindingContext = thisContext;
    }
}