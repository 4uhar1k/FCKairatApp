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
        /*MailAddress from = new MailAddress("vovas0712@gmail.com", "Служба поддержки Studychat");
        MailAddress to = new MailAddress("lizametonidze@gmail.com");
        MailMessage message = new MailMessage(from, to);
        message.Subject = "Напоминание о пароле";
        message.Body = "Здравствуйте" + Environment.NewLine +
            "Ваш пароль от аккаунта FC Kairat: blablabla";



        SmtpClient smtp = new SmtpClient();
        smtp.Host = "smtp.gmail.com";
        smtp.Port = 587;
        smtp.EnableSsl = true;
        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
        smtp.UseDefaultCredentials = false;
        smtp.Credentials = new NetworkCredential(from.Address, "Bob07122005");

        smtp.Send(message);*/
        /*var fromAddress = new MailAddress("vovas0712@gmail.com", "FC Kairat Restoring Password");
        var toAddress = new MailAddress("vovas0712@gmail.com");
        const string fromPassword = "Bob07122005"; // Your email account's password

        // Set up the SMTP client
        SmtpClient smtp = new SmtpClient
        {
            Host = "smtp.gmail.com", // e.g., smtp.gmail.com for Gmail
            Port = 465, // SMTP port, typically 587 or 465 for SSL
            EnableSsl = true, // Enable SSL
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
        };

        // Create the email message
        using (MailMessage message = new MailMessage(fromAddress, toAddress)
        {
            Subject = "subject",
            Body = "body"
        })
        {
            smtp.Send(message); // Send the email
        }
        try
        {

            SmtpClient mySmtpClient = new SmtpClient("smtp.tu-dortmund.de");
            mySmtpClient.EnableSsl = true;
            // set smtp-client with basicAuthentication
            mySmtpClient.UseDefaultCredentials = true;
            System.Net.NetworkCredential basicAuthenticationInfo = new
           System.Net.NetworkCredential("vladimir.vassilyev@tu-dortmund.de", "Bob07122005");
        mySmtpClient.Credentials = basicAuthenticationInfo;

        // add from,to mailaddresses
        MailAddress from = new MailAddress("vladimir.vassilyev@tu-dortmund.de");
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

        }*/
    }
}