using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using WorkUaApiCore;

namespace JiraApiCore
{
    public class JiraData : INotifyPropertyChanged
    {
        public JiraData(Job job, Response response)
        {
            this.Fio = response.Fio;
            CvLink = "https://www.work.ua/employer/my/archive/" + job.Id + "/" + response.Id + "/download/?from=one";
            PhoneNumber = response.Phone;
            Description = response.Text;
            CandidateID = response.Id;
            VacancyName = job.Name;
            ResponceDate = response.Date;
            Email = response.Email;
            ResponceType = response.Type;
        }
        private string fio;
        public string Fio
        {
            get { return fio; }
            set { fio = value; OnPropertyChanged("Fio"); }
        }
        private string cvLink;
        public string CvLink
        {
            get { return cvLink; }
            set { cvLink = value; OnPropertyChanged("CvLink"); }
        }
        private string phoneNumber;
        public string PhoneNumber
        {
            get { return phoneNumber; }
            set { phoneNumber = value; OnPropertyChanged("Phone"); }
        }
        private string description;
        public string Description
        {
            get { return description; }
            set { description = value; OnPropertyChanged("Description"); }
        }
        private int candidateID;
        public int CandidateID
        {
            get { return candidateID; }
            set { candidateID = value; OnPropertyChanged("CandidateID"); }
        }
        private string vacancyName;
        public string VacancyName
        {
            get { return vacancyName; }
            set { vacancyName = value; OnPropertyChanged("VacancyName"); }
        }
        private DateTime responceDate;
        public DateTime ResponceDate
        {
            get { return responceDate; }
            set { responceDate = value; OnPropertyChanged("ResponceDate"); }
        }
        private string email;
        public string Email
        {
            get { return email;  }
            set { email = value; OnPropertyChanged("Email"); }
        }
        private string responceType;
        public string ResponceType
        {
            get { return responceType; }
            set { responceType = value; OnPropertyChanged("ResponceType"); }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
