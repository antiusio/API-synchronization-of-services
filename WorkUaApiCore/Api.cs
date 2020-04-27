using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;

namespace WorkUaApiCore
{
    public class Api
    {
        public Api(string AuthCode= "d29ya3VhQGtsaW9iYS5jb206S1V5QWEkUXU0RkI4VyN0MA==")
        {
            client = new HttpClient();
            this.AuthCode = AuthCode;
            client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", AuthCode);
            
        }
        private ItemsApi<Job> itemsJobs;
        public ItemsApi<Job> ItemsJobs
        {
            get { return itemsJobs; }
            set { itemsJobs = value; OnPropertyChanged("ItemsJobs"); }
        }
        private ObservableCollection<Response> responses;
        public ObservableCollection<Response> Responses
        {
            get { return responses; }
            set { responses = value; OnPropertyChanged("ItemsResponse"); }
        }
        private string jobsUrl = "https://api.work.ua/jobs/my";
        private string responsesUrl = "https://api.work.ua/jobs/{0}/responces";
        private HttpClient client;
        public HttpClient Client
        {
            get { return client; }
            set { client = value; OnPropertyChanged("Client"); }
        }
        private string authCode;
        public string AuthCode
        {
            get { return authCode; }
            set { authCode = value; OnPropertyChanged("AuthCode"); }
        }
        public void GetJobs()
        {
            var v = client.GetAsync(jobsUrl).GetAwaiter().GetResult();
            string json = v.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            ItemsJobs = JsonConvert.DeserializeObject<ItemsApi<Job>>(json);
            
            ;
        }
        private ItemsApi<Response> GetResponsesFromVacation(int id)
        {
            string url = String.Format(responsesUrl, new object[] { id });
            var v = client.GetAsync(url).GetAwaiter().GetResult();
            string json = v.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            ItemsApi<Response> itemsResponses = JsonConvert.DeserializeObject<ItemsApi<Response>>(json);
            return itemsResponses;
        }
        public void GetResponses()
        {
            Responses = new ObservableCollection<Response>();
            foreach (var item in itemsJobs.Items)
                if(item.Active)
                {
                    ItemsApi<Response> items = GetResponsesFromVacation(item.Id);
                    foreach (var resp in items.Items)
                    {

                        item.Responses.Add(resp);
                        Responses.Add(resp);
                    }
                }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
