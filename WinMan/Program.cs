using Microsoft.Owin.Hosting;
using System;
using System.Net.Http;

namespace WinMan
{
    class Program
    {
        static void Main(string[] args)
        {
            string baseAddress = "http://*:8000/";
            // Start OWIN host 
            using (WebApp.Start<Startup>(url: baseAddress))
            {
                // Create HttpCient and make a request to api/values 
                HttpClient client = new HttpClient();
                Console.ReadLine();
            }
        }
    }
}
