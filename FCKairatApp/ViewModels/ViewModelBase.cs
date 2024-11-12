using Microsoft.Maui.Controls;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
//using Microsoft.UI.Xaml;

namespace FCKairatApp.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public string CurUser = Path.Combine(FileSystem.AppDataDirectory, "curuser.txt");
        public string emailbase, passbase, namebase, surnamebase;
        public SqlConnectionBase baseConnection;
        public ICommand AddArticle { get; set; }

        public ViewModelBase()
        {
            //File.Delete(CurUser);
            baseConnection = new SqlConnectionBase();
            if (File.Exists(CurUser))
            {
                using (StreamReader sr = new StreamReader(CurUser))
                {
                    emailbase = sr.ReadLine();
                    passbase = sr.ReadLine();
                    namebase = sr.ReadLine();
                    surnamebase = sr.ReadLine();
                    sr.Close();
                }
            }          
        }

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
            try
            {
                ((Command)AddArticle).ChangeCanExecute();
            }
            catch { }
        }
        //public async void GoToLogin()
        //{
        //    await Navigation.PushModalAsync(new LoginPage(new SqlConnectionBase()));
        //}
    }
}
