using FCKairatApp.Dtos;
using Microsoft.Maui.Controls;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
//using Microsoft.UI.Xaml;

namespace FCKairatApp.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public ISQLiteAsyncConnection database { get; set; }
        public string CurLanguage = Path.Combine(FileSystem.AppDataDirectory, "language.txt");
        public string CurUser = Path.Combine(FileSystem.AppDataDirectory, "curuser.txt");
        public string Tournaments = Path.Combine(FileSystem.AppDataDirectory, "tournaments.txt");
        public ObservableCollection<PlayerDto> Players { get; set; }
        public ObservableCollection<string> PlayerNames { get; set; }
        public string emailbase, passbase, namebase, surnamebase;
        public int languageid;
        public SqlConnectionBase baseConnection;
        public NewsDto articleToChange { get; set; }

        // for localization
        public ObservableCollection<string> SurnameBindings = new ObservableCollection<string>() {"Тегіңіз", "Фамилия", "Surname" };
        public ObservableCollection<string> NameBindings = new ObservableCollection<string>() { "Атыңыз", "Имя", "Name" };
        public ObservableCollection<string> PassBindings = new ObservableCollection<string>() { "Құпиясөз", "Пароль", "Password" };
        public ObservableCollection<string> SignInBindings = new ObservableCollection<string>() { "Кіру", "Войти", "Sign in" };
        public ObservableCollection<string> SignUpBindings = new ObservableCollection<string>() { "Тіркелу", "Зарегистрироваться", "Sign up" };
        public ObservableCollection<string> ForgotPassBindings = new ObservableCollection<string>() { "Құпиясөзді ұмыттыңыз ба?", "Забыли пароль?", "Forgot your password?" };
        public ObservableCollection<string> LanguageBindings { get; set; }
        public ObservableCollection<string> UserNotFoundBindings = new ObservableCollection<string>() { "Қолданушы табылмаған", "Пользователь не найден", "User not found" };
        public ObservableCollection<string> WrongPassBindings = new ObservableCollection<string>() { "Бұрыс құпиясөз", "Неправильный пароль", "Wrong password" };
        public ObservableCollection<string> SignedUpBindings = new ObservableCollection<string>() { "Табысты тіркелдіңіз", "Вы успешно зарегистрировались", "You've been successfully signed up" };

        public string SurnamePHBinding { get; set; }
        public string NamePHBinding { get; set; }
        public string PassPHBinding { get; set; }
        public string SignInBtnBinding { get; set; }
        public string SignUpBtnBinding { get; set; }
        public string ForgotPassBtnBinding { get; set; }
        public string LanguagePickerBinding { get; set; }
        public string UserNotFoundBinding { get; set; }
        public string WrongPassBinding { get; set; }
        public string SignedUpBinding { get; set; }
        //
        public ViewModelBase()
        {
            // for localization
            LanguageBindings = new ObservableCollection<string>() { "KZ", "RU", "EN" };
            //



            //File.Delete(Tournaments);
            baseConnection = new SqlConnectionBase();
            Players = new ObservableCollection<PlayerDto>();
            PlayerNames = new ObservableCollection<string>();
            LanguageId = 0;
            LoadPlayers();
            //File.Delete(CurLanguage);
            if (!File.Exists(CurLanguage))
            {
                File.Create(CurLanguage);               
            }
            else
            {
                string? curlanguage;
                using (StreamReader sr = new StreamReader(CurLanguage))
                {
                    curlanguage = sr.ReadLine();
                    sr.Close();
                }
                
                switch (curlanguage)
                {
                    case "KZ":
                        LanguageId = 0; break;
                    case "RU":
                        LanguageId = 1; break;
                    case "EN":
                        LanguageId = 2; break;
                }
            }
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
            SurnamePHBinding = SurnameBindings[LanguageId];
            NamePHBinding = NameBindings[LanguageId];
            PassPHBinding = PassBindings[LanguageId];
            SignInBtnBinding = SignInBindings[LanguageId];
            SignUpBtnBinding = SignUpBindings[LanguageId];
            ForgotPassBtnBinding = ForgotPassBindings[LanguageId];
            UserNotFoundBinding = UserNotFoundBindings[LanguageId];
            WrongPassBinding = WrongPassBindings[LanguageId];
            LanguagePickerBinding = LanguageBindings[LanguageId];
            SignedUpBinding = SignedUpBindings[LanguageId];
        }

        public async void LoadPlayers()
        {
            ISQLiteAsyncConnection database = baseConnection.CreateConnection();
            List<PlayerDto> PlayersFromDatabase = await database.Table<PlayerDto>().OrderBy(n => n.Number).ToListAsync();
            foreach (PlayerDto player in PlayersFromDatabase)
            {
                Players.Add(player);
                PlayerNames.Add($"{player.Number}. {player.Name} {player.Surname}");
            }


        }

        public int LanguageId
        {
            get => languageid;
            set
            {
                if (languageid != value)
                {
                    languageid = value;
                    OnPropertyChanged();
                }
            }
        }

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
            
        }
        //public async void GoToLogin()
        //{
        //    await Navigation.PushModalAsync(new LoginPage(new SqlConnectionBase()));
        //}
    }
}
