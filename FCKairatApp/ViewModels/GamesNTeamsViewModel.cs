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
    public class GamesNTeamsViewModel: ViewModelBase
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public ObservableCollection<TeamDto> Teams { get; set; }
        public TeamDto SameTeamName {  get; set; }
        public ISQLiteAsyncConnection database { get; set; }
        string teamname, coachname;
        int winsamount, drawsamount, losesamount, goalsscored, goalsmissed, points;
        public ICommand AddTeam { get; set; }
        public ICommand RemoveTeam { get; set; }
        public GamesNTeamsViewModel()
        {
            Teams = new ObservableCollection<TeamDto>();
            database = baseConnection.CreateConnection();
            LoadTeams();
            AddTeam = new Command(() =>
            {
                TeamDto NewTeam = new TeamDto()
                {
                    TeamName = TeamName,
                    CoachName = CoachName
                };
                SameTeamName = Teams.Where(n=>n.TeamName==TeamName).FirstOrDefault();
                if (SameTeamName==null)
                {
                    database.InsertAsync(NewTeam);
                }
                
                                
            }, ()=> TeamName!=null & CoachName!=null & TeamName!="" & CoachName!="");
            RemoveTeam = new Command((object SelectedTeam) =>
            {
                TeamDto TeamToDelete = (TeamDto)SelectedTeam;
                database.DeleteAsync(TeamToDelete);
            });
        }
        public async void LoadTeams()
        {
            List<TeamDto> ListOfTeams = await database.Table<TeamDto>().ToListAsync();
            foreach (TeamDto team in ListOfTeams)
            {
                Teams.Add(team);
            }
        }
        public string TeamName 
        {
            get => teamname;

            set
            {
                if (teamname != value)
                {
                    teamname = value;
                    OnPropertyChanged();
                }
            }
        }
        public string CoachName
        {
            get => coachname;

            set
            {
                if (coachname != value)
                {
                    coachname = value;
                    OnPropertyChanged();
                }
            }
        }

        public int WinsAmount
        {
            get => winsamount;

            set
            {
                if (winsamount != value)
                {
                    winsamount = value;
                    OnPropertyChanged();
                }
            }
        }
        public int DrawsAmount
        {
            get => drawsamount;

            set
            {
                if (drawsamount != value)
                {
                    drawsamount = value;
                    OnPropertyChanged();
                }
            }
        }
        public int LosesAmount
        {
            get => losesamount;

            set
            {
                if (losesamount != value)
                {
                    losesamount = value;
                    OnPropertyChanged();
                }
            }
        }
        public int GoalsScored
        {
            get => goalsscored;

            set
            {
                if (goalsscored != value)
                {
                    goalsscored = value;
                    OnPropertyChanged();
                }
            }
        }
        public int GoalsMissed
        {
            get => goalsmissed;

            set
            {
                if (goalsmissed != value)
                {
                    goalsmissed = value;
                    OnPropertyChanged();
                }
            }
        }
        public int Points
        {
            get => points;

            set
            {
                if (points != value)
                {
                    points = value;
                    OnPropertyChanged();
                }
            }
        }

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
            ((Command)AddTeam).ChangeCanExecute();
        }

    }
}
