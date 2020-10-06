using Newtonsoft.Json;
using System.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using System.Net;
using System;
using System.Net.Http.Headers;
using System.IO;
using Microsoft.AspNet.SignalR.Client.Http;
using Microsoft.AspNetCore.Http;
using System.Collections;

namespace webApp
{
    class Program
    {
        static async Task Main(string[] args)
        {

            await findPerson();
        }

        public static async Task findPerson()
        {/*
            var url = $"http://192.168.15.27:8090/person/find";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.Credentials = CredentialCache.DefaultCredentials;
            request.ContentType = "application/form-data";
            request.Accept = "application/json";
            */
            try
            {

                string baseUrl = "/person/find";
                var parameters = new Dictionary<string, string>();

                parameters.Add("pass", "87654321");
                parameters.Add("id", "3");

                System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
                client.BaseAddress = new Uri("http://192.168.15.27:8090" + baseUrl);
                System.Net.Http.MultipartFormDataContent form = new System.Net.Http.MultipartFormDataContent();

                HttpContent DictionaryItems = new FormUrlEncodedContent((IEnumerable<KeyValuePair<string, string>>)parameters);
                form.Add(DictionaryItems, "personFind");

                var response = await client.PostAsync("/person/find", form);
                var k = await response.Content.ReadAsStringAsync();

                Console.WriteLine(response);

                Console.ReadKey();
            }
            catch (Exception e)
            {
                
            }
        } 
    
    } 
}

