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
    public class NewsViewModel: ViewModelBase
    {
        public ISQLiteAsyncConnection database;
        public string title, description, author;
        public bool isPublished;
        public ObservableCollection<NewsDto> AllNews { get; set; }
        
        public NewsViewModel()
        {
            AllNews = new ObservableCollection<NewsDto>();
            database = baseConnection.CreateConnection();
            LoadNews();

            AddArticle = new Command(() =>
            {
                NewsDto newArticle = new NewsDto()
                {
                    Title = Title,
                    Description = Description,
                    Author = $"{namebase} {surnamebase}",
                    IsPublished = true
                };
                database.InsertAsync(newArticle);                
            }, ()=>Title!=null & Description!=null);

            
            
        }

        public async void LoadNews()
        {            
            List<NewsDto> AllNewsList = await database.Table<NewsDto>().ToListAsync();
            foreach (NewsDto newArticle in AllNewsList)
            {
                //database.DeleteAsync(newArticle);
                AllNews.Add(newArticle);
            }
        }

        public string Title 
        {
            get => title;
            set
            {
                if (title!=value)
                {
                    title = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Description
        {
            get => description;
            set
            {
                if (description != value)
                {
                    description = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Author
        {
            get => author;
            set
            {
                if (author != value)
                {
                    author = value;
                    OnPropertyChanged();
                }
            }
        }
        public bool IsPublished
        {
            get => isPublished;
            set
            {
                if (isPublished != value)
                {
                    isPublished = value;
                    OnPropertyChanged();
                }
            }
        }

        

    }
}
