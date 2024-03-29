﻿using Newtonsoft.Json;
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
        public static T GetById<T>(string path)
        {
            T result = default(T);
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(APP_PATH);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage httpResponse = client.GetAsync(APP_PATH + path).Result;
            if (httpResponse.IsSuccessStatusCode)
            {
                var json = httpResponse.Content.ReadAsStringAsync().Result;
                result = JsonConvert.DeserializeObject<T>(json);
            }
            return result;
        }

        public static T Update<T>(string path, T t)
        {
            T result = default(T);
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(APP_PATH);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage httpResponse = client.PutAsJsonAsync<T>(APP_PATH + path, t).Result;
            if (httpResponse.IsSuccessStatusCode)
            {
                var json = httpResponse.Content.ReadAsStringAsync().Result;
                result = JsonConvert.DeserializeObject<T>(json);
            }
            return result;
        }

        public static int Add<T>(string path, T t)
        {
            int result = 0;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(APP_PATH);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage httpResponse = client.PostAsJsonAsync<T>(APP_PATH + path, t).Result;
            if (httpResponse.IsSuccessStatusCode)
            {
                var json = httpResponse.Content.ReadAsStringAsync().Result;
                result = JsonConvert.DeserializeObject<int>(json);
            }
            return result;
        }

        public static bool Delete(string path)
        {
            bool result = false;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(APP_PATH);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage httpResponse = client.DeleteAsync(APP_PATH + path).Result;
            if (httpResponse.IsSuccessStatusCode)
            {
                result = true;
            }
            return result;
        }
    }
}
