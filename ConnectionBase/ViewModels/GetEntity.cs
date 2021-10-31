using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ConnectionBase.ViewModels
{
    public static class GetEntity
    {
        private const string APP_PATH = "http://localhost:16802/";

        public static ObservableCollection<T> GetList<T>(string path)
        {
            ObservableCollection<T> list = new();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(APP_PATH);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage httpResponse = client.GetAsync(APP_PATH + path).Result;
            if (httpResponse.IsSuccessStatusCode)
            {
                var json = httpResponse.Content.ReadAsStringAsync().Result;
                list = JsonConvert.DeserializeObject<ObservableCollection<T>>(json);
            }
            return list;
        }
    }
}
