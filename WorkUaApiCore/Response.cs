using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace WorkUaApiCore
{
    public class Response : INotifyPropertyChanged
    {
        private DateTime date;
        public DateTime Date
        {
            get { return date; }
            set { date = value; OnPropertyChanged("Date"); }
        }
        private string email;
        public string Email
        {
            get { return email; }
            set { email = value;OnPropertyChanged("Email"); }
        }
        private string fio;
        public string Fio
        {
            get { return fio; }
            set { fio = value; OnPropertyChanged("Fio"); }
        }
        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; OnPropertyChanged("Id"); }
        }
        private string phone;
        public string Phone
        {
            get { return phone; }
            set { phone = value; OnPropertyChanged("Phone"); }
        }
        private string text;
        public string Text
        {
            get { return text; }
            set { text = value; OnPropertyChanged("Text"); }
        }
        private string type;
        public string Type
        {
            get { return type; }
            set { type = value; OnPropertyChanged("Type"); }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
        public override bool Equals(object obj)
        {
            if(!(obj is Response))
                return base.Equals(obj);
            else
            {
                return this.Id.Equals(((Response)obj).Id);
            }
        }
    }
}
