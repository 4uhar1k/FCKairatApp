using SQLite;
using System.Net.Mail;

namespace FCKairatApp;

public partial class LoginPage : ContentPage
{
	private readonly SqlConnectionBase _connection;
    public string CurUser = Path.Combine(FileSystem.AppDataDirectory, "curuser.txt");
    public LoginPage(SqlConnectionBase connection)
	{
		InitializeComponent();
        _connection = connection;
		SurnamePlaceholder.IsVisible = false;
        NamePlaceholder.IsVisible = false;
    }

	public async void SignIn(object sender, EventArgs e)
	{
        ISQLiteAsyncConnection database = _connection.CreateConnection();
        UserDto userDto = null;
        try
        {
            userDto = await database.Table<UserDto>().Where(n => n.Email == LoginPlaceholder.Text & n.Password == PassPlaceholder.Text).FirstAsync();
        }
        catch
        {

        }
        if (userDto != null)
        {
            if (!File.Exists(CurUser))
            {
                File.Create(CurUser);
            }
            using (StreamWriter sw = new StreamWriter(CurUser))
            {
                sw.WriteLine(userDto.Email);
                sw.WriteLine(userDto.Password);
                sw.WriteLine(userDto.Name);
                sw.WriteLine(userDto.Surname);
                sw.Close();
            }
            Navigation.PopModalAsync();
        }
        else
        {
            await DisplayAlert("", "No such user", "OK");
        }
        
        
	}

	public void SignUp(object sender, EventArgs e)
	{
        
        SignUpBtn.IsVisible = false;
		RestorePassword.IsVisible = false;
        SurnamePlaceholder.IsVisible = true;
        NamePlaceholder.IsVisible = true;
        LoginPlaceholder.Text = "";
        PassPlaceholder.Text = "";
        SignInBtn.Text = "Sign up";
		SignInBtn.Clicked -= SignIn;
        SignInBtn.Clicked += SignedUp;
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
        await DisplayAlert("", "You've successfully signed up!", "OK");
        RestorePassword.IsVisible = true;
        SignUpBtn.IsVisible = true;
        SurnamePlaceholder.IsVisible = false;
        NamePlaceholder.IsVisible = false;
		LoginPlaceholder.Text = "";
		PassPlaceholder.Text = "";
        SignInBtn.Text = "Sign in";
        SignInBtn.Clicked -= SignedUp;
        SignInBtn.Clicked += SignIn;
    }

    public async void RestorePass(object sender, EventArgs e)
    {
        try
        {
            
            SmtpClient mySmtpClient = new SmtpClient("smtp.gmail.com");
            mySmtpClient.EnableSsl = true;
            // set smtp-client with basicAuthentication
            mySmtpClient.UseDefaultCredentials = true;
            System.Net.NetworkCredential basicAuthenticationInfo = new
               System.Net.NetworkCredential("vovas0712@gmail.com", "Bob07122005");
            mySmtpClient.Credentials = basicAuthenticationInfo;

            // add from,to mailaddresses
            MailAddress from = new MailAddress("vovas0712@gmail.com");
            MailAddress to = new MailAddress("vovas0712@gmail.com");
            MailMessage myMail = new MailMessage(from, to);



            // set subject and encoding
            myMail.Subject = "Test message";
            myMail.SubjectEncoding = System.Text.Encoding.UTF8;

            // set body-message and encoding
            myMail.Body = "<b>Test Mail</b><br>using <b>HTML</b>.";
            myMail.BodyEncoding = System.Text.Encoding.UTF8;
            // text or html
            myMail.IsBodyHtml = true;

            mySmtpClient.Send(myMail);
        }

        catch (SmtpException ex)
        {
            await DisplayAlert("", ex.Message, "OK");
        }
        catch (Exception ex)
        {

        }
    }
}