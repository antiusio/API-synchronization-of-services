using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace WorkUaApiCore
{
    public class Job : INotifyPropertyChanged
    {
        public Job()
        {
            Responses = new ObservableCollection<Response>();
        }
        private bool active;
        public bool Active
        {
            get { return active; }
            set { active = value; OnPropertyChanged("Active"); }
        }
        private int adress;
        public int Adress
        {
            get { return adress; }
            set { adress = value; OnPropertyChanged("Adress"); }
        }
        private int blocked;
        public int Blocked
        {
            get { return blocked; }
            set { blocked = value; OnPropertyChanged("Blocked"); }
        }
        private DateTime date;
        public DateTime Date
        {
            get { return date; }
            set { date = value; OnPropertyChanged("Date"); }
        }
        private DateTime dateExpire;
        public DateTime DateExpire
        {
            get { return dateExpire; }
            set { dateExpire = value; OnPropertyChanged("DateExpire"); }
        }
        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; OnPropertyChanged("Id"); }
        }
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged("Name"); }
        }
        private string publication;
        public string Publication
        {
            get { return publication; }
            set { publication = value; OnPropertyChanged("Publication"); }
        }
        private int region;
        public int Region
        {
            get { return region; }
            set { region = value; OnPropertyChanged("Region"); }
        }
        private int textResponse;
        public int TextResponse
        {
            get { return textResponse; }
            set { textResponse = value; OnPropertyChanged("TextResponse"); }
        }
        private ObservableCollection<Response> responses;
        public ObservableCollection<Response> Responses
        {
            get { return responses; }
            set { responses = value; OnPropertyChanged("Responses"); }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
