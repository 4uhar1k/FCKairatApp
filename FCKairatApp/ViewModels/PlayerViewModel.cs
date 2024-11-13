using FCKairatApp.Dtos;
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

namespace FCKairatApp.ViewModels
{
    public class PlayerViewModel: ViewModelBase
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public ObservableCollection<PlayerDto> Players { get; set; }
        public ISQLiteAsyncConnection database { get; set; }
        string name, surname, position, startmonth,startyear, expirymonth, expiryyear;
        int number, goalamount, assistamount;
        public PlayerDto PlayerToEdit { get; set; }
        public ICommand AddPlayer { get; set; }
        public PlayerViewModel()
        {
            Players = new ObservableCollection<PlayerDto>();
            database = baseConnection.CreateConnection();
            LoadPlayers();
            

            AddPlayer = new Command(() =>
            {
                PlayerDto newPlayer = new PlayerDto()
                {
                    Name = Name,
                    Surname = Surname,
                    Number = Number,
                    Position = Position,
                    GoalsAmount = GoalsAmount,
                    AssistsAmount = AssistsAmount,
                    StartDate = $"{StartMonth} {StartYear}",
                    ExpiryDate = $"{ExpiryMonth} {ExpiryYear}"
                };
                database.InsertAsync(newPlayer);
                if (PlayerToEdit!=null)
                {
                    database.DeleteAsync(PlayerToEdit);
                }
            }, () => Name!="" & Surname!="" & Number!=0 & Number!=null & GoalsAmount!=null & AssistsAmount!=null & Position!="" & StartMonth!="" & StartYear!="" & ExpiryMonth!="" & ExpiryYear!="" & Name!=null & Surname!=null & Position!=null & StartMonth!=null & StartYear!=null & ExpiryMonth!=null & ExpiryYear!=null);
        }

        public async void LoadPlayers()
        {
            List<PlayerDto> PlayersFromDatabase = await database.Table<PlayerDto>().ToListAsync();
            foreach (PlayerDto player in PlayersFromDatabase)
            {
                Players.Add(player);
            }
        }

        

        public string Name 
        {
            get => name; 
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Surname
        {
            get => surname;
            set
            {
                if (surname != value)
                {
                    surname = value;
                    OnPropertyChanged();
                }
            }
        }
        public int Number
        {
            get => number;
            set
            {
                if (number != value)
                {
                    number = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Position
        {
            get => position;
            set
            {
                if (position != value)
                {
                    position = value;
                    OnPropertyChanged();
                }
            }
        }
        public int GoalsAmount
        {
            get => goalamount;
            set
            {
                if (goalamount != value)
                {
                    goalamount = value;
                    OnPropertyChanged();
                }
            }
        }
        public int AssistsAmount
        {
            get => assistamount;
            set
            {
                if (assistamount != value)
                {
                    assistamount = value;
                    OnPropertyChanged();
                }
            }
        }
        public string StartMonth
        {
            get => startmonth;
            set
            {
                if (startmonth != value)
                {
                    startmonth = value;
                    OnPropertyChanged();
                }
            }
        }
        public string StartYear
        {
            get => startyear;
            set
            {
                if (startyear != value)
                {
                    startyear = value;
                    OnPropertyChanged();
                }
            }
        }
        public string ExpiryMonth
        {
            get => expirymonth;
            set
            {
                if (expirymonth != value)
                {
                    expirymonth = value;
                    OnPropertyChanged();
                }
            }
        }
        public string ExpiryYear
        {
            get => expiryyear;
            set
            {
                if (expiryyear != value)
                {
                    expiryyear = value;
                    OnPropertyChanged();
                }
            }
        }
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
            ((Command)AddPlayer).ChangeCanExecute();
        }

    }
}
