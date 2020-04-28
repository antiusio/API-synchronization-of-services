using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;

namespace WorkUaApiCore
{
    public class ApiWork:INotifyPropertyChanged
    {
        public ApiWork(string AuthCode= "d29ya3VhQGtsaW9iYS5jb206S1V5QWEkUXU0RkI4VyN0MA==")
        {
            HttpClientHandler handler = null;
            handler = new HttpClientHandler
            {
                Proxy = new WebProxy("127.0.0.1", 8888)
            };
            httpClient = new HttpClient(handler);
            this.AuthCode = AuthCode;
            httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", AuthCode);
            
        }
        private ObservableCollection<Job> jobs;
        public ObservableCollection<Job> Jobs
        {
            get { return jobs; }
            set { jobs = value; OnPropertyChanged("Jobs"); }
        }
        private ObservableCollection<Response> responses;
        public ObservableCollection<Response> Responses
        {
            get { return responses; }
            set { responses = value; OnPropertyChanged("ItemsResponse"); }
        }
        private string jobsUrl = "https://api.work.ua/jobs/my";
        private string responsesUrl = "https://api.work.ua/jobs/{0}/responces";
        private HttpClient httpClient;
        public HttpClient HttpClient
        {
            get { return httpClient; }
            set { httpClient = value; OnPropertyChanged("Client"); }
        }
        private string authCode;
        public string AuthCode
        {
            get { return authCode; }
            set { authCode = value; OnPropertyChanged("AuthCode"); }
        }
        private void GetJobs()
        {
            var v = httpClient.GetAsync(jobsUrl).GetAwaiter().GetResult();
            string json = v.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            ItemsApi<Job> ItemsJobs = JsonConvert.DeserializeObject<ItemsApi<Job>>(json);
            Jobs = ItemsJobs.Items;
        }
        private ItemsApi<Response> GetResponsesFromVacation(int id)
        {
            string url = String.Format(responsesUrl, new object[] { id });
            var v = httpClient.GetAsync(url).GetAwaiter().GetResult();
            string json = v.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            ItemsApi<Response> itemsResponses = JsonConvert.DeserializeObject<ItemsApi<Response>>(json);
            return itemsResponses;
        }
        public void GetResponses()
        {
            GetJobs();
            Responses = new ObservableCollection<Response>();
            foreach (var item in Jobs)
                if(item.Active)
                {
                    ItemsApi<Response> items = GetResponsesFromVacation(item.Id);
                    foreach (var resp in items.Items)
                    {

                        item.Responses.Add(resp);
                        Responses.Add(resp);
                    }
                }
            ;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
