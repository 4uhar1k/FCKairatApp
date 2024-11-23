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
    public class NewsViewModel: ViewModelBase
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public ISQLiteAsyncConnection database;
        public string title, description, author;
        public bool isPublished;
        public Byte[] newsimage;
        public ICommand AddArticle { get; set; }
        public ICommand DeleteArticle { get; set; }
        public ObservableCollection<NewsDto> AllNews { get; set; }
        
        public NewsViewModel()
        {
            AllNews = new ObservableCollection<NewsDto>();
            database = baseConnection.CreateConnection();
            LoadNews();
            
            AddArticle = new Command(() =>
            {
                
                
                if (articleToChange!=null)
                {
                    articleToChange.Title = Title;
                    articleToChange.Description = Description;
                    articleToChange.Author = $"{namebase} {surnamebase}";
                    articleToChange.IsPublished = true;
                    articleToChange.NewsImage = NewsImage;
                    database.UpdateAsync(articleToChange);
                }
                else
                {
                    NewsDto newArticle = new NewsDto()
                    {
                        Title = Title,
                        Description = Description,
                        Author = $"{namebase} {surnamebase}",
                        IsPublished = true,
                        NewsImage = NewsImage
                    };
                    database.InsertAsync(newArticle);
                }
                             
            }, ()=>Title!="" & Description!="" & Title!=null & Description!=null & NewsImage!=null);

            DeleteArticle = new Command(() =>
            {
                NewsDto articleToDelete = AllNews.Where(n => n.Title == articleToChange.Title & n.Description == articleToChange.Description & n.Author == articleToChange.Author & n.IsPublished == articleToChange.IsPublished).First();
                NewsDto newArticle = new NewsDto()
                {
                    Title = Title,
                    Description = Description,
                    Author = $"{namebase} {surnamebase}",
                    IsPublished = true,
                    Id = articleToChange.Id
                };
                database.DeleteAsync(newArticle);
            }, () => Title != "" & Description != "" & Title != null & Description != null);

            
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
        public Byte[] NewsImage
        {
            get => newsimage;
            set
            {
                if (newsimage!=value)
                {
                    newsimage = value;
                }
            }
        }

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
            
            ((Command)AddArticle).ChangeCanExecute();
            ((Command)DeleteArticle).ChangeCanExecute();
        }

    }
}
