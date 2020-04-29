using System;
using WorkUaApiCore;
using JiraApiCore;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.IO;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace DataProcessing
{
    public class Worker : INotifyPropertyChanged
    {
        public Worker(string AuthCode,string zapLink)
        {
            savedDataFile = Directory.GetCurrentDirectory() + @"/Data/JobsWithResponces.json";
            inputDataFile = Directory.GetCurrentDirectory() + @"/Data/InputData.json";
            workApi = new ApiWork(AuthCode);
            jiraApi = new ApiJira(zapLink);
            SavedJobsWithResponses = GetSavedJobs();
            SavedResponses = new ObservableCollection<Response>();
            foreach( var job in savedJobsWithResponses)
            {
                if (job.Active)
                    foreach (var resp in job.Responses)
                        savedResponses.Add(resp);
            }
            NewResponses = 0;
        }
        public Worker(InputData obj):this(obj.AuthCode,obj.ZapLink)
        {
            //obj.SaveData(inputDataFile);
        }
        public Worker(string filePath) :this(new InputData(filePath))
        {
            inputDataFile = filePath;
        }
        public void Work()
        {
            workApi.GetResponses();
            foreach(var job in workApi.Jobs)
            {
                if (job.Active)
                {
                    foreach (var resp in job.Responses)
                    {
                        bool flag = false;
                        foreach(var savedResp in SavedResponses)
                        {
                            if (savedResp.Equals(resp))
                            {
                                flag = true;
                                break;
                            }
                        }
                        if (!flag)
                        {
                            JiraApi.AddRespToJira(job, resp);
                            NewResponses++;
                        }
                    }
                }
            }
            SavedJobsWithResponses = WorkApi.Jobs;
            SaveJobs();
        }
        private int newResponses;
        public int NewResponses
        {
            get { return newResponses; }
            set { newResponses = value; OnPropertyChanged("NewResponses"); }
        }
        public class InputData
        {
            public InputData()
            {

            }
            public InputData(InputData obj)
            {
                this.AuthCode = obj.AuthCode;
                this.ZapLink = obj.ZapLink;
            }
            public InputData(string filePath):this()
            {
                try
                {
                    string content = File.ReadAllText(filePath);
                    InputData obj = JsonConvert.DeserializeObject<InputData>(content);
                    this.AuthCode = obj.AuthCode;
                    this.ZapLink = obj.ZapLink;
                }
                catch { }
            }
            public void SaveData(string savedDataFile)
            {
                string content = JsonConvert.SerializeObject(this);
                File.WriteAllText(savedDataFile, content);
            }
            public string AuthCode { get; set; }
            public string ZapLink { get; set; }
        }
        public ObservableCollection<Job> GetSavedJobs()
        {
            ObservableCollection<Job> jobs;
            string content = File.ReadAllText(savedDataFile);
            try
            {
                jobs = JsonConvert.DeserializeObject<ObservableCollection<Job>>(content);
            }
            catch
            {
                jobs = new ObservableCollection<Job>();
            }
            return jobs;
        }
        public void SaveJobs()
        {
            string content = JsonConvert.SerializeObject(SavedJobsWithResponses);
            File.WriteAllText(savedDataFile, content);
        }
        private string savedDataFile;
        public static  string inputDataFile;
        private ObservableCollection<Response> savedResponses;
        public ObservableCollection<Response> SavedResponses
        {
            get { return savedResponses; }
            set { savedResponses = value;OnPropertyChanged("SavedResponses"); }
        }
        private ObservableCollection<Job> savedJobsWithResponses;
        public ObservableCollection<Job> SavedJobsWithResponses
        {
            get { return savedJobsWithResponses; }
            set { savedJobsWithResponses = value; OnPropertyChanged("SavedJobsWithResponses"); }
        }
        private ApiWork workApi;
        public ApiWork WorkApi
        {
            get { return workApi; }
            set { workApi = value; OnPropertyChanged("WorkApi"); }
        }
        private ApiJira jiraApi;
        public ApiJira JiraApi
        {
            get { return jiraApi; }
            set { jiraApi = value; OnPropertyChanged("JiraApi"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
