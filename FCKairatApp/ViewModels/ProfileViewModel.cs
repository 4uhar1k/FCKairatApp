using FCKairatApp.Dtos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FCKairatApp.ViewModels
{
    public class ProfileViewModel: ViewModelBase
    {
        string email, password, name, surname, userlogin, firstteamname, secondteamname, gametime;
        int gameid;
        bool hasnotickets;
        public string CurUser = Path.Combine(FileSystem.AppDataDirectory, "curuser.txt");
        public GameDto GameOfTicket { get; set; }
        public ObservableCollection<TicketDto> Tickets { get; set; }
        public ICommand RemoveTicket { get; set; }
        public ProfileViewModel()
        {
            GameOfTicket = new GameDto();
            if (emailbase!=null)
            {
                Email = emailbase;
                Password = passbase;
                Name = namebase;
                Surname = surnamebase;
            }
            database = baseConnection.CreateConnection();
            Tickets = new ObservableCollection<TicketDto>();
            LoadTickets();
            HasNoTickets = true;
            RemoveTicket = new Command((object ticket) =>
            {
                TicketDto TicketToRemove = (TicketDto)ticket;
                database.DeleteAsync(TicketToRemove);
                if (Tickets.Count == 1)
                    HasNoTickets = true;
                //{ };
            });
        }

        public async void LoadTickets()
        {
            List<TicketDto> ListOfTickets = await database.Table<TicketDto>().Where(n => n.UserLogin == Email).ToListAsync();
            foreach(TicketDto ticket in ListOfTickets)
            {
                GameOfTicket = await database.Table<GameDto>().Where(n => n.Id == ticket.GameId).FirstOrDefaultAsync();
                HasNoTickets = false;
                Tickets.Add(ticket);
                
                //FirstTeamName = GameOfTicket.FirstTeamName;
                //SecondTeamName = GameOfTicket.SecondTeamName;
                //GameTime = GameOfTicket.GameTime;
                
            }
        }

        public string Email
        {
            get => email;
            set
            {
                if (email != value)
                {
                    email = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Password
        {
            get => password;
            set
            {
                if (password != value)
                {
                    password = value;
                    OnPropertyChanged();
                }
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
        public string UserLogin
        {
            get => userlogin;
            set
            {
                if (userlogin != value)
                {
                    userlogin = value;
                    OnPropertyChanged();
                }
            }
        }
        public int GameId
        {
            get => gameid;
            set
            {
                if (gameid != value)
                {
                    gameid = value;
                    OnPropertyChanged();
                }
            }
        }
        
        public string FirstTeamName
        {
            get => firstteamname;
            set
            {
                if (firstteamname != value)
                {
                    firstteamname = value;
                    OnPropertyChanged();
                }
            }
        }
        public string SecondTeamName
        {
            get => secondteamname;
            set
            {
                if (secondteamname != value)
                {
                    secondteamname = value;
                    OnPropertyChanged();
                }
            }
        }
        public string GameTime
        {
            get => gametime;
            set
            {
                if (gametime != value)
                {
                    gametime = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool HasNoTickets
        {
            get => hasnotickets;
            set
            {
                if (hasnotickets != value)
                {
                    hasnotickets = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
