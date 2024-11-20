using FCKairatApp.Dtos;
using Microsoft.Maui.Layouts;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
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
        
        
        string name, surname, position, startmonth,startyear, expirymonth, expiryyear;
        int number, goalamount, assistamount;
        public PlayerDto PlayerToEdit { get; set; }
        public PlayerDto oldPlayerName { get; set; }
        public PlayerDto oldPlayerNumber { get; set; }
        public PlayerDto newPlayer { get; set; }
        public ICommand AddPlayer { get; set; }
        public ICommand DeletePlayer { get; set; }
        public PlayerViewModel()
        {
            
            database = baseConnection.CreateConnection();
            //LoadPlayers();


            AddPlayer = new Command(() =>
            {
                newPlayer = new PlayerDto()
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


                //PlayerUniqueCheck();

                //PlayerNumberCheck();
                oldPlayerName = Players.Where(n => n.Name == Name & n.Surname == Surname).FirstOrDefault();
                oldPlayerNumber = Players.Where(n => n.Number == Number).FirstOrDefault();
                if (oldPlayerNumber==null & oldPlayerName==null & PlayerToEdit==null)
                {
                    database.InsertAsync(newPlayer);
                }
                else if (PlayerToEdit != null)
                {
                    database.InsertAsync(newPlayer);
                    database.DeleteAsync(PlayerToEdit);
                }
            }, () => Name!="" & Surname!="" & Number!=0 & Position!="" & StartMonth!="" & StartYear!="" & ExpiryMonth!="" & ExpiryYear!="" & DataAreCorrect() & Name!=null & Surname!=null & Position!=null & StartMonth!=null & StartYear!=null & ExpiryMonth!=null & ExpiryYear!=null);

            DeletePlayer = new Command((object SelectedPlayer) =>
            {
                PlayerDto PlayerToDelete = (PlayerDto)SelectedPlayer;
                database.DeleteAsync(PlayerToDelete);
            });
        }

        

        public bool DataAreCorrect()
        {
            try
            {
                if (Convert.ToInt32(StartYear) > 1900 & Convert.ToInt32(StartYear)<2100 & Convert.ToInt32(ExpiryYear) > 1900 & Convert.ToInt32(ExpiryYear) < 2100 & Convert.ToInt32(StartYear) <= Convert.ToInt32(ExpiryYear))
                {
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
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
