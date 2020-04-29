using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using WorkUaApiCore;

namespace JiraApiCore
{
    public class ApiJira : INotifyPropertyChanged
    {
        private string zapLink;
        public string ZapLink
        {
            get { return zapLink; }
            set { zapLink = value; OnPropertyChanged("ZapLink"); }
        }
        public ApiJira(string zapLink)
        {
            this.ZapLink = zapLink;
            HttpClientHandler handler = null;
            handler = new HttpClientHandler
            {
                //Proxy = new WebProxy("127.0.0.1", 8888)
            };
            httpClient = new HttpClient(handler);
        }
        private HttpClient httpClient;
        public HttpClient HttpClient
        {
            get { return httpClient; }
            set { httpClient = value; OnPropertyChanged("HttpClient"); }
        }
        private void AddTextRespToJira(string response)
        {
            using (var httpClient = new HttpClient())
            {
                var resp = httpClient.PostAsync(
                    ZapLink,
                     new StringContent(response, Encoding.UTF8, "application/json")).GetAwaiter().GetResult();
            }
        }
        public void AddRespToJira(Job job, Response response)
        {
            JiraData jiraData = new JiraData(job, response);
            string json = JsonConvert.SerializeObject(jiraData);
            AddTextRespToJira(json);
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
