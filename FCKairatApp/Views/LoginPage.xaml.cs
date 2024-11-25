using FCKairatApp.Dtos;
using FCKairatApp.ViewModels;
using SQLite;
using System.Net;
using System.Net.Mail;

namespace FCKairatApp;

public partial class LoginPage : ContentPage
{
	private readonly SqlConnectionBase _connection;
    public string CurUser = Path.Combine(FileSystem.AppDataDirectory, "curuser.txt");
    public string CurLanguage = Path.Combine(FileSystem.AppDataDirectory, "language.txt");
    public ViewModelBase thisContext { get; set; }

    public LoginPage(SqlConnectionBase connection)
	{
		InitializeComponent();        
        _connection = connection;
		SurnamePlaceholder.IsVisible = false;
        NamePlaceholder.IsVisible = false;
        thisContext = new ViewModelBase();
        BindingContext = thisContext;
    }

	public async void SignIn(object sender, EventArgs e)
	{
        ISQLiteAsyncConnection database = _connection.CreateConnection();
        UserDto userDto, loginDto;
        //if (LanguagePicker.SelectedItem != null)
        //{
            //try
            //{
                //if (!File.Exists(CurLanguage))
                //{
                //    File.Create(CurLanguage);
                //}
                //else
                //{
                //    using (StreamWriter sw = new StreamWriter(CurLanguage, false))
                //    {
                //        sw.WriteLine(LanguagePicker.SelectedItem.ToString());
                //        sw.Close();
                //    }
                //}
                loginDto = await database.Table<UserDto>().Where(n=>n.Email == LoginPlaceholder.Text).FirstOrDefaultAsync();
                userDto = await database.Table<UserDto>().Where(n => n.Email == LoginPlaceholder.Text & n.Password == PassPlaceholder.Text).FirstOrDefaultAsync();
            if (loginDto == null)
            {
                await DisplayAlert("", thisContext.UserNotFoundBinding, "OK");
            }
            else if (userDto == null &  loginDto != null)
            {
                await DisplayAlert("", thisContext.WrongPassBinding, "OK");
            }
            else
            {
                if (!File.Exists(CurUser))
                {
                    using (File.Create(CurUser)) { }
                }
                using (StreamWriter sw = new StreamWriter(CurUser))
                {
                    sw.WriteLine(userDto.Email);
                    sw.WriteLine(userDto.Password);
                    sw.WriteLine(userDto.Name);
                    sw.WriteLine(userDto.Surname);
                    sw.Close();
                }

                await Navigation.PopModalAsync();
            }
                
            //}
            //catch
            //{
                
            //}
        //}
        //else
        //{
        //    await DisplayAlert("", "Select language", "OK");
        //}
        
        
        
        
	}
    public void LanguageChanged(object sender, EventArgs e)
    {
        try
        {
            if (!File.Exists(CurLanguage))
            {
                File.Create(CurLanguage);
            }
            using (StreamWriter sw = new StreamWriter(CurLanguage, false))
            {
                sw.WriteLine(LanguagePicker.SelectedItem.ToString());
                sw.Close();
            }

            Update();

        }
        catch { }
    }
	public void SignUp(object sender, EventArgs e)
	{
        
        SignInBtn.IsVisible = false;
		RestorePassword.IsVisible = false;
        SurnamePlaceholder.IsVisible = true;
        NamePlaceholder.IsVisible = true;
        LoginPlaceholder.Text = "";
        PassPlaceholder.Text = "";
        SignUpBtn.Clicked -= SignUp;
        SignUpBtn.Clicked += SignedUp;
        //SignInBtn.Text = "Sign up";
        //SignInBtn.Clicked -= SignIn;
        //SignInBtn.Clicked += SignedUp;
    }

    public async void SignedUp(object sender, EventArgs e)
	{
        ISQLiteAsyncConnection database = _connection.CreateConnection();
        UserDto userDto = new UserDto()
        {
            Name = NamePlaceholder.Text,
            Surname = SurnamePlaceholder.Text,
            Email = LoginPlaceholder.Text,
            Password = PassPlaceholder.Text
        };
        await database.InsertAsync(userDto);
        await DisplayAlert("", thisContext.SignedUpBinding, "OK");
        RestorePassword.IsVisible = true;
        SignInBtn.IsVisible = true;
        SurnamePlaceholder.IsVisible = false;
        NamePlaceholder.IsVisible = false;
		LoginPlaceholder.Text = "";
		PassPlaceholder.Text = "";
        //SignInBtn.Text = "Sign in";
        SignUpBtn.Clicked -= SignedUp;
        SignUpBtn.Clicked += SignIn;
    }

    public void Update()
    {
        thisContext = new ViewModelBase();
        BindingContext = thisContext;
    }
    public async void RestorePass(object sender, EventArgs e)
    {
        await DisplayAlert("", "Not available yet", "OK");
            
    }
}