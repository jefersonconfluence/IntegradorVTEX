﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Configuration;
using System.Net.Http.Headers;

namespace Integrador.Service
{
    
    class BaseService
    {
        static HttpClient client;
        //static string uri = "https://{{accountName}}.{{environment}}.com.br/";
        static string accountName = ConfigurationManager.AppSettings["accountName"];
        static string environment = ConfigurationManager.AppSettings["environment"];
        static string appKey = ConfigurationManager.AppSettings["appKey"];
        static string appToken = ConfigurationManager.AppSettings["appToken"];

        public BaseService() {
            
        }

        public static HttpClient BuildClient() {
            string baseUri = "https://" + accountName + "." + environment + ".com.br/";
            if (client == null)
            {
                client = new HttpClient();
                client.BaseAddress = new Uri(baseUri);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("x-vtex-api-appKey", appKey);
                client.DefaultRequestHeaders.Add("x-vtex-api-appToken", appToken);
                client.DefaultRequestHeaders.Add("Accept", "application/vnd.vtex.ds.v10+json");
                client.DefaultRequestHeaders.Add("REST-Range", "resources=0-10");
            }
            return client;
        }
    }
}
