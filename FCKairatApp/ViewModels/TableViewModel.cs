using CommunityToolkit.Maui.Core.Extensions;
using FCKairatApp.Dtos;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCKairatApp.ViewModels
{
    public class TableViewModel: ViewModelBase
    {
       public ObservableCollection<TeamDto> SortedTeams { get; set; }
        public TableViewModel()
        {
            database = baseConnection.CreateConnection();
            SortedTeams = new ObservableCollection<TeamDto>();
            LoadTeams();
        }

        public async void LoadTeams()
        {
            List<TeamDto> ListOfTeams = await database.Table<TeamDto>().OrderByDescending(n=>n.Points).ToListAsync();
            foreach (TeamDto team in ListOfTeams)
            {
                SortedTeams.Add(team);
            }
            
        }
    }
}
