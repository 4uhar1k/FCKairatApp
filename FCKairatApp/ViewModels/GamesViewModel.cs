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
    public class GamesViewModel: TeamsViewModel
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public ObservableCollection<GameDto> Games {  get; set; }        
        public ObservableCollection<GoalDto> GoalsOfFirstTeam { get; set; }
        public ObservableCollection<GoalDto> GoalsOfSecondTeam { get; set; }        
        public ObservableCollection<string> TournamentNames { get; set; }
        public string ScoredPlayerName { get; set; }        
        public GameDto GameToChange { get; set; }
        public PlayerDto PlayerToUpdate { get; set; }
        string firstteamname, secondteamname, gametime, tournament, score, day, month, year, time, tournamentname, scoredplayersurname, scoredteam, ticketslink;
        int id, firstteamscore, secondteamscore, gameid, scoredminute;
        bool islive;        
        public ICommand AddGame { get; set; }
        public ICommand RemoveGame { get; set; }
        public ICommand EndGame { get; set; }
        public ICommand AddTournament { get; set; }
        public ICommand AddGoal { get; set; }
        public ICommand BuyTickets { get; set; }
        public GamesViewModel()
        {
            Games = new ObservableCollection<GameDto>();         
            GoalsOfFirstTeam = new ObservableCollection<GoalDto>();
            GoalsOfSecondTeam = new ObservableCollection<GoalDto>();
            database = baseConnection.CreateConnection();
            LoadGames();
            
            if (!File.Exists(Tournaments))
            {
                File.Create(Tournaments);
            }
            TournamentNames = new ObservableCollection<string>();
            string? line;
            using (StreamReader reader = new StreamReader(Tournaments))
            {
                
                while ((line = reader.ReadLine()) != null)
                {
                    TournamentNames.Add(line);
                    
                }
                reader.Close();
            }
            if (!TournamentNames.Contains("Add new tournament..."))
            {
                
                using (StreamWriter sw = new StreamWriter(Tournaments, false))
                {
                    sw.WriteLine("Add new tournament...");
                    for (int i = 0; i < TournamentNames.Count; i++)
                    {
                        sw.WriteLine(TournamentNames[i]);
                    }
                    TournamentNames.Add("Add new tournament...");
                    sw.Close();
                }
            }
            
            AddGame = new Command(() =>
            {
                GameDto NewGame = new GameDto()
                {
                    
                    FirstTeamName = FirstTeamName,
                    SecondTeamName = SecondTeamName,
                    FirstTeamScore = FirstTeamScore,
                    SecondTeamScore = SecondTeamScore,
                    GameTime = $"{Day} {Month} {Year} {Time}",
                    Tournament = Tournament,
                    TicketsLink = TicketsLink
                };                
                if (GameToChange != null)
                {                   
                    NewGame.IsLive = GameToChange.IsLive;
                    GameToChange.FirstTeamName = NewGame.FirstTeamName;
                    GameToChange.SecondTeamName = NewGame.SecondTeamName;
                    GameToChange.FirstTeamScore = NewGame.FirstTeamScore;
                    GameToChange.SecondTeamScore = NewGame.SecondTeamScore;
                    GameToChange.GameTime = NewGame.GameTime;
                    GameToChange.Tournament = NewGame.Tournament;
                    GameToChange.TicketsLink = NewGame.TicketsLink;
                    database.UpdateAsync(GameToChange);                    
                }
                else
                {
                    NewGame.IsLive = true;
                    database.InsertAsync(NewGame);
                }              
                }, () => FirstTeamName != null & SecondTeamName != null & FirstTeamName!=SecondTeamName & Tournament!=null & Day!=null & Day!="" & Month!=null 
            & Year!=null & Year!="" & Time!=null & Time!="" & IsDataCorrect());
            RemoveGame = new Command((object SelectedGame) =>
            {
                GameDto GameToDelete = (GameDto)SelectedGame;                
                DeleteGoals(GameToDelete);
                { };                
                database.DeleteAsync(GameToDelete);                
            });
            EndGame = new Command(() =>
            {
                GameDto NewGame = new GameDto()
                {
                    //Id = Games.Count+1,
                    FirstTeamName = FirstTeamName,
                    SecondTeamName = SecondTeamName,
                    FirstTeamScore = FirstTeamScore,
                    SecondTeamScore = SecondTeamScore,
                    GameTime = $"{Day} {Month} {Year} {Time}",
                    Tournament = Tournament,
                    IsLive = false
                };
                
                    GameToChange.FirstTeamName = NewGame.FirstTeamName;
                    GameToChange.SecondTeamName = NewGame.SecondTeamName;
                    GameToChange.FirstTeamScore = NewGame.FirstTeamScore;
                    GameToChange.SecondTeamScore = NewGame.SecondTeamScore;
                    GameToChange.GameTime = NewGame.GameTime;
                    GameToChange.Tournament = NewGame.Tournament;
                    GameToChange.IsLive = NewGame.IsLive;
                    database.UpdateAsync(GameToChange);

                TeamDto FirstTeamToChange = Teams.Where(n => n.TeamName == FirstTeamName).FirstOrDefault();
                TeamDto SecondTeamToChange = Teams.Where(n => n.TeamName == SecondTeamName).FirstOrDefault();
                FirstTeamToChange.GoalsScored += FirstTeamScore;
                FirstTeamToChange.GoalsMissed += SecondTeamScore;
                SecondTeamToChange.GoalsScored += SecondTeamScore;
                SecondTeamToChange.GoalsMissed += FirstTeamScore;
                if (FirstTeamScore > SecondTeamScore)
                {
                    FirstTeamToChange.WinsAmount++;
                    FirstTeamToChange.Points += 3;
                    SecondTeamToChange.LosesAmount++;
                }
                else if (FirstTeamScore < SecondTeamScore)
                {
                    SecondTeamToChange.WinsAmount++;
                    SecondTeamToChange.Points += 3;
                    FirstTeamToChange.LosesAmount++;
                }
                else
                {
                    FirstTeamToChange.DrawsAmount++;
                    FirstTeamToChange.Points++;
                    SecondTeamToChange.DrawsAmount++;
                    SecondTeamToChange.Points++;
                }
                database.UpdateAsync(FirstTeamToChange);
                database.UpdateAsync(SecondTeamToChange);
            });

            AddTournament = new Command(() =>
            {
                using (StreamWriter sw = new StreamWriter(Tournaments,true))
                {
                    sw.WriteLine(TournamentName);
                }
            }, () => TournamentName != null & TournamentName!="");

            AddGoal = new Command((object SelectedTeamName) =>
            {
                string ScoredTeamName = (string)SelectedTeamName;
                GoalDto newGoal = new GoalDto()
                {
                    GameId = GameToChange.Id,
                    ScoredTeam = ScoredTeamName
                };
                
                if (ScoredTeamName == "FC Kairat Almaty")
                {
                    newGoal.ScoredPlayerSurname = ScoredPlayerSurname.Split(' ')[2];
                }
                else
                    newGoal.ScoredPlayerSurname= ScoredPlayerSurname;

                if (ScoredTeamName == FirstTeamName)
                    FirstTeamScore++;
                else
                    SecondTeamScore++;
                database.InsertAsync(newGoal);                
                GameDto NewGame = new GameDto()
                {                    
                    FirstTeamName = FirstTeamName,
                    SecondTeamName = SecondTeamName,
                    FirstTeamScore = FirstTeamScore,
                    SecondTeamScore = SecondTeamScore,
                    GameTime = $"{Day} {Month} {Year} {Time}",
                    Tournament = Tournament,
                    IsLive = true
                };                               
                GameToChange.FirstTeamName = NewGame.FirstTeamName;
                GameToChange.SecondTeamName = NewGame.SecondTeamName;
                GameToChange.FirstTeamScore = NewGame.FirstTeamScore;
                GameToChange.SecondTeamScore = NewGame.SecondTeamScore;
                GameToChange.GameTime = NewGame.GameTime;
                GameToChange.Tournament = NewGame.Tournament;
                GameToChange.IsLive = NewGame.IsLive;
                database.UpdateAsync(GameToChange);
                if (ScoredTeamName == "FC Kairat Almaty")
                {
                    PlayerToUpdate = Players.Where(n => n.Name == ScoredPlayerSurname.Split(' ')[1] & n.Surname == ScoredPlayerSurname.Split(' ')[2]).FirstOrDefault();
                    PlayerToUpdate.GoalsAmount++;
                    database.UpdateAsync(PlayerToUpdate);                    
                }
                
            }, (object s) => ScoredPlayerSurname!=null & ScoredPlayerSurname!="");

            BuyTickets = new Command((object SelectedGame) =>
            {
                GameDto newGame = (GameDto)SelectedGame;
                string LinkToTickets = newGame.TicketsLink;
                BrowserOpen_Clicked(LinkToTickets);
            });
        }
        
        public async void LoadGames()
        {
            List<GameDto> ListOfGames = await database.Table<GameDto>().ToListAsync();
            foreach (GameDto game in ListOfGames)
            {
                Games.Insert(0,game);
            }
            
            if (GameToChange != null)
            {
                
                List<GoalDto> ListOfGoalsOfFirstTeam = await database.Table<GoalDto>().Where(n => n.GameId == GameToChange.Id & n.ScoredTeam == GameToChange.FirstTeamName).ToListAsync();
                
                List<GoalDto> ListOfGoalsOfSecondTeam = await database.Table<GoalDto>().Where(n => n.GameId == GameToChange.Id & n.ScoredTeam == GameToChange.SecondTeamName).ToListAsync();
               
                foreach (GoalDto goal in ListOfGoalsOfFirstTeam)
                {
                    GoalsOfFirstTeam.Add(goal);

                }
                foreach (GoalDto goal in ListOfGoalsOfSecondTeam)
                {
                    GoalsOfSecondTeam.Add(goal);

                }

            }
                 
        }
        
        public async void DeleteGoals(GameDto game)
        {
            List<GoalDto> ListOfGoalsOfFirstTeam = await database.Table<GoalDto>().Where(n => n.GameId == game.Id).ToListAsync();
            { };
            foreach (GoalDto goal in ListOfGoalsOfFirstTeam)
            {
                await database.DeleteAsync(goal);               
            }
        }

        private async void BrowserOpen_Clicked(string LinkToTickets)
        {
            try
            {
                Uri uri = new Uri(LinkToTickets);
                await Browser.Default.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
            }
            catch (Exception ex)
            {
                //some shi
            }
        }
        public bool IsDataCorrect()
        {
            try
            {
                
                if (FirstTeamName != "FC Kairat Almaty" & SecondTeamName != "FC Kairat Almaty")
                    return false;
               
                return true;
            }
            catch
            {
                return false;
            }
        }
        
        
        // for game

        public int Id
        {
            get => id;
            set
            {
                if (id != value)
                {
                    id = value;
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
                    OnPropertyChanged(nameof(FirstTeamName));
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
                    OnPropertyChanged(nameof(SecondTeamName));
                }
            }
        }
        public int FirstTeamScore
        {
            get => firstteamscore;

            set
            {
                if (firstteamscore != value)
                {
                    firstteamscore = value;
                    OnPropertyChanged();
                }
            }
        }
        public int SecondTeamScore
        {
            get => secondteamscore;

            set
            {
                if (secondteamscore != value)
                {
                    secondteamscore = value;
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
        public string Tournament
        {
            get => tournament;

            set
            {
                if (tournament != value)
                {
                    tournament = value;
                    OnPropertyChanged();
                }
            }
        }
        public bool IsLive
        {
            get => islive;
            set
            {
                if (islive != value)
                {
                    islive = value;
                    OnPropertyChanged();
                }
            }
        }

        public string TicketsLink
        {
            get => ticketslink;

            set
            {
                if (ticketslink != value)
                {
                    ticketslink = value;
                    OnPropertyChanged();
                }
            }
        }

        //for goals

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
        public string ScoredPlayerSurname
        {
            get => scoredplayersurname;
            set
            {
                if (scoredplayersurname != value)
                {
                    scoredplayersurname = value;
                    OnPropertyChanged();
                }
            }
        }
        public string ScoredTeam
        {
            get => scoredteam;
            set
            {
                if (scoredteam != value)
                {
                    scoredteam = value;
                    OnPropertyChanged();
                }
            }
        }
        public int ScoredMinute
        {
            get => scoredminute;
            set
            {
                if (scoredminute!= value)
                {
                    scoredminute = value;
                    OnPropertyChanged();
                }
            }
        }
        // helping stuff for game
        public string Score
        {
            get => score;

            set
            {
                if (score != value)
                {
                    score = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Day
        {
            get => day;

            set
            {
                if (day != value)
                {
                    day = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Month
        {
            get => month;

            set
            {
                if (month != value)
                {
                    month = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Year
        {
            get => year;

            set
            {
                if (year != value)
                {
                    year = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Time
        {
            get => time;

            set
            {
                if (time != value)
                {
                    time = value;
                    OnPropertyChanged();
                }
            }
        }
        public string TournamentName
        {
            get => tournamentname;

            set
            {
                if (tournamentname != value)
                {
                    tournamentname = value;
                    OnPropertyChanged();
                }
            }
        }
        

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
            ((Command)AddGame).ChangeCanExecute();
            ((Command)AddTournament).ChangeCanExecute();
            ((Command)AddGoal).ChangeCanExecute();
            ((Command)BuyTickets).ChangeCanExecute();
        }

    }
}
