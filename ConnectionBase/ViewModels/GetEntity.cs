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

        public static async Task<ObservableCollection<T>> GetList<T>(string path)
        {
            ObservableCollection<T> list = new();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(APP_PATH);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage httpResponse = await client.GetAsync(APP_PATH + path);
            if (httpResponse.IsSuccessStatusCode)
            {
                var json = httpResponse.Content.ReadAsStringAsync().Result;
                list = JsonConvert.DeserializeObject<ObservableCollection<T>>(json);
            }
            return list;
        }
        public static async Task<T> GetById<T>(string path)
        {
            T result = default(T);
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(APP_PATH);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage httpResponse = await client.GetAsync(APP_PATH + path);
            if (httpResponse.IsSuccessStatusCode)
            {
                var json = httpResponse.Content.ReadAsStringAsync().Result;
                result = JsonConvert.DeserializeObject<T>(json);
            }
            return result;
        }

        public static async Task<T> Update<T>(string path, T t)
        {
            T result = default(T);
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(APP_PATH);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage httpResponse = await client.PutAsync(APP_PATH + path, t);
            if (httpResponse.IsSuccessStatusCode)
            {
                var json = httpResponse.Content.ReadAsStringAsync().Result;
                result = JsonConvert.DeserializeObject<T>(json);
            }
            return result;
        }
    }
}
