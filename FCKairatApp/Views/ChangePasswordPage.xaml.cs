using FCKairatApp.Dtos;
using FCKairatApp.ViewModels;
using SQLite;

namespace FCKairatApp;

public partial class ChangePasswordPage : ContentPage
{
	public string curpassword, newpassword, newpasswordrepeat;
    public SqlConnectionBase baseConnection;
    public ISQLiteAsyncConnection database;
    public string CurUser = Path.Combine(FileSystem.AppDataDirectory, "curuser.txt");
    public ChangePasswordPage()
	{
		InitializeComponent();
		BindingContext = this;
	}
	public async void UpdatePassword(object sender, EventArgs e)
	{
        baseConnection = new SqlConnectionBase();
        database = baseConnection.CreateConnection();
        string CurrenUserLogin;
        using (StreamReader sr = new StreamReader(CurUser))
        {
            CurrenUserLogin = sr.ReadLine();
            sr.Close();
        }
        UserDto CurrentUser = await database.Table<UserDto>().Where(n => n.Email == CurrenUserLogin).FirstOrDefaultAsync();
        if (CurPassword!=null & CurPassword!="" & NewPassword != null & NewPassword != "" & NewPasswordRepeat != null & NewPasswordRepeat != "")
        {
            if (CurrentUser.Password != CurPassword)
            {
                await DisplayAlert("", "Old password you've written is wrong.", "OK");
            }
            else if (NewPassword != NewPasswordRepeat)
            {
                await DisplayAlert("", "You couldn't repeat your new password.", "OK");
            }
            else if (CurPassword == NewPassword)
            {
                await DisplayAlert("", "Your new password can't be same as old password.", "OK");
            }
            else
            {                
                CurrentUser.Password = NewPassword;
                await database.UpdateAsync(CurrentUser);
                await DisplayAlert("", "Your password was reset.", "OK");
                await Navigation.PopAsync();
            }
        }
        else
        {            
            await DisplayAlert("", "Some data missing.", "OK");
        }
		
	}
    public string CurPassword
    { 
		get => curpassword; 
		set
		{
			if (curpassword!=value)
			{
				curpassword = value;
				OnPropertyChanged();
			}
		}
	}
    public string NewPassword
    {
        get => newpassword;
        set
        {
            if (newpassword != value)
            {
                newpassword = value;
                OnPropertyChanged();
            }
        }
    }
    public string NewPasswordRepeat
    {
        get => newpasswordrepeat;
        set
        {
            if (newpasswordrepeat != value)
            {
                newpasswordrepeat = value;
                OnPropertyChanged();
            }
        }
    }
}