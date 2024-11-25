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
        // for LoginPage
        public ObservableCollection<string> SurnameBindings = new ObservableCollection<string>() {"Тегіңіз", "Фамилия", "Surname" };
        public ObservableCollection<string> NameBindings = new ObservableCollection<string>() { "Атыңыз", "Имя", "Name" };
        public ObservableCollection<string> PassBindings = new ObservableCollection<string>() { "Құпиясөз", "Пароль", "Password" };
        public ObservableCollection<string> SignInBindings = new ObservableCollection<string>() { "Кіру", "Войти", "Sign in" };
        public ObservableCollection<string> SignUpBindings = new ObservableCollection<string>() { "Тіркелу", "Зарегистрироваться", "Sign up" };
        public ObservableCollection<string> ForgotPassBindings = new ObservableCollection<string>() { "Құпиясөзді ұмыттыңыз ба?", "Забыли пароль?", "Forgot your password?" };
        public ObservableCollection<string> LanguageTextBindings = new ObservableCollection<string>() { "Тіл", "Язык", "Language" };
        public ObservableCollection<string> LanguageBindings { get; set; }
        public ObservableCollection<string> UserNotFoundBindings = new ObservableCollection<string>() { "Қолданушы табылмаған", "Пользователь не найден", "User not found" };
        public ObservableCollection<string> WrongPassBindings = new ObservableCollection<string>() { "Бұрыс құпиясөз", "Неправильный пароль", "Wrong password" };
        public ObservableCollection<string> SignedUpBindings = new ObservableCollection<string>() { "Табысты тіркелдіңіз", "Вы успешно зарегистрировались", "You've been successfully signed up" };
        // for SettingsPage
        public ObservableCollection<string> ReloadAppBtnBindings = new ObservableCollection<string>() { "Өзгерістерді қолдану", "Cохранить изменения", "Apply changes" };        
        public ObservableCollection<string> ReloadAppBindings = new ObservableCollection<string>() { "Өзгерістерді қолдану үшін қолданбаны қайта іске қосыңыз", "Перезапустите приложения, чтобы сохранить изменения", "Restart app to apply changes"};
        // for ProfilePage
        public ObservableCollection<string> YourTicketsBindings = new ObservableCollection<string>() { "Билеттеріңіз", "Ваши билеты", "Your tickets" };
        public ObservableCollection<string> NoTicketsBindings = new ObservableCollection<string>() { "Билеттеріңіз жоқ", "Нет билетов", "You've got no tickets yet" };
        public ObservableCollection<string> ChangePassBindings = new ObservableCollection<string>() { "Құпиясөзді өзгерту", "Изменить пароль", "Change password" };
        public ObservableCollection<string> LogOutBindings = new ObservableCollection<string>() { "Аккаунттан шығу", "Выйти из аккаунта", "Log out" };
        // for AddNews
        public ObservableCollection<string> AddArticleBindings = new ObservableCollection<string>() { "Жаңа мақала", "Новая статья", "Add new article" };
        public ObservableCollection<string> AddTitleBindings = new ObservableCollection<string>() { "Тақырып", "Заголовок", "Add title..." };
        public ObservableCollection<string> AddDescBindings = new ObservableCollection<string>() { "Мәтін", "Текст", "Add text..." };
        public ObservableCollection<string> AddImageBindings = new ObservableCollection<string>() { "Суретті қосу", "Добавить изображение", "Add image" };

        public string SurnamePHBinding { get; set; }
        public string NamePHBinding { get; set; }
        public string PassPHBinding { get; set; }
        public string SignInBtnBinding { get; set; }
        public string SignUpBtnBinding { get; set; }
        public string ForgotPassBtnBinding { get; set; }
        public string LanguagePickerBinding { get; set; }
        public string LanguageTextBinding { get; set; }
        public string UserNotFoundBinding { get; set; }
        public string WrongPassBinding { get; set; }
        public string SignedUpBinding { get; set; }
        public string ReloadAppBtnBinding { get; set; }
        public string ReloadAppBinding { get; set; }

        public string YourTicketBinding { get; set; }
        public string NoTicketBinding { get; set; }
        public string ChangePassBtnBinding { get; set; }
        public string LogOutBtnBinding { get; set; }

        public string AddArticleBinding { get; set; }
        public string AddTitleBinding { get; set; }
        public string AddDescBinding { get; set; }
        public string AddImageBtnBinding { get; set; }
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
            LanguageTextBinding = LanguageTextBindings[LanguageId];
            SignedUpBinding = SignedUpBindings[LanguageId];

            ReloadAppBtnBinding = ReloadAppBtnBindings[LanguageId];
            ReloadAppBinding = ReloadAppBindings[LanguageId];

            YourTicketBinding = YourTicketsBindings[LanguageId];
            NoTicketBinding = NoTicketsBindings[LanguageId];
            ChangePassBtnBinding = ChangePassBindings[LanguageId];
            LogOutBtnBinding = LogOutBindings[LanguageId];

            AddArticleBinding = AddArticleBindings[LanguageId];
            AddTitleBinding = AddTitleBindings[LanguageId];
            AddDescBinding = AddDescBindings[LanguageId];
            AddImageBtnBinding = AddImageBindings[LanguageId];
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
