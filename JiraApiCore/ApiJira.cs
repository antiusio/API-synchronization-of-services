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
        public ApiJira()
        {
            HttpClientHandler handler = null;
            handler = new HttpClientHandler
            {
                Proxy = new WebProxy("127.0.0.1", 8888)
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
            //var requestData = new Dictionary<string, string>();
            //requestData["text"] = response;
            //var v = httpClient.PostAsync("https://hooks.zapier.com/hooks/catch/7359656/o5pxfek/", new FormUrlEncodedContent(requestData)).GetAwaiter().GetResult();
            //var t = 0==0;
            using (var httpClient = new HttpClient())
            {
                var resp = httpClient.PostAsync(
                    "https://hooks.zapier.com/hooks/catch/7359656/o5ji1du/",
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
