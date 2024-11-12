using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCKairatApp.ViewModels
{
    public class ProfileViewModel: ViewModelBase
    {
        string email, password, name, surname;
        public string CurUser = Path.Combine(FileSystem.AppDataDirectory, "curuser.txt");
        public ProfileViewModel()
        {
            if (emailbase!=null)
            {
                Email = emailbase;
                Password = passbase;
                Name = namebase;
                Surname = surnamebase;
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

    }
}
