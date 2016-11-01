using Common;
using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Net;

namespace OwinSelfhostSample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting web Server...");
            string baseUri = "http://localhost:8080/";//9000 Also we can use.


            Console.WriteLine("Starting web Server {0}...", baseUri);
            using (WebApp.Start<Startup>(baseUri))
            {

                Console.WriteLine("Read all the companies...");
                var companyClient = new CompanyClient(baseUri);

                var companies = companyClient.GetCompanies();
                WriteCompaniesList(companies);

                Console.WriteLine("Server running at {0} - press Enter to quit. ", baseUri);
                Console.ReadLine();
            }
        }

        private static void WriteStatusCodeResult(HttpStatusCode statusCode)
        {
            if (statusCode == System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine("Opreation Succeeded - status code {0}", statusCode);
            }
            else
            {
                Console.WriteLine("Opreation Failed - status code {0}", statusCode);
            }
            Console.WriteLine("");
        }

        private static void WriteCompaniesList(IEnumerable<Company> companies)
        {
            if (companies != null)
            {

                foreach (var company in companies)
                {
                    Console.WriteLine("Id: {0} Name: {1}", company.Id, company.Name);
                }
                Console.WriteLine("");
            }
        }
    }
}
