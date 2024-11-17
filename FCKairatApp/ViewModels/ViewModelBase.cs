﻿using FCKairatApp.Dtos;
using Microsoft.Maui.Controls;
using SQLite;
using System;
using System.Collections.Generic;
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
        public string CurUser = Path.Combine(FileSystem.AppDataDirectory, "curuser.txt");
        public string Tournaments = Path.Combine(FileSystem.AppDataDirectory, "tournaments.txt");
        
        public string emailbase, passbase, namebase, surnamebase;
        public SqlConnectionBase baseConnection;
        public NewsDto articleToChange { get; set; }
        

        public ViewModelBase()
        {
            //File.Delete(Tournaments);
            baseConnection = new SqlConnectionBase();
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
