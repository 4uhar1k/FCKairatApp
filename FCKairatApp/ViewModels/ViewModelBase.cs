using FCKairatApp.Dtos;
using Microsoft.Maui.Controls;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public ISQLiteAsyncConnection database { get; set; }
        public string CurUser = Path.Combine(FileSystem.AppDataDirectory, "curuser.txt");
        public string Tournaments = Path.Combine(FileSystem.AppDataDirectory, "tournaments.txt");
        public ObservableCollection<PlayerDto> Players { get; set; }
        public ObservableCollection<string> PlayerNames { get; set; }
        public string emailbase, passbase, namebase, surnamebase;
        public SqlConnectionBase baseConnection;
        public NewsDto articleToChange { get; set; }
        

        public ViewModelBase()
        {
            
            //File.Delete(Tournaments);
            baseConnection = new SqlConnectionBase();
            Players = new ObservableCollection<PlayerDto>();
            PlayerNames = new ObservableCollection<string>();
            LoadPlayers();
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
