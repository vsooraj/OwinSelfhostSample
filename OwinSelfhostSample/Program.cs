using Microsoft.Owin.Hosting;
using OwinSelfhostSample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace OwinSelfhostSample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting web Server...");
            string baseUri = "http://localhost:8080/";//9000 Also we can use.

            #region Sample
            // Start OWIN host 
            //using (WebApp.Start<Startup>(url: baseUri))
            //{
            //    // Create HttpCient and make a request to api/values 
            //    HttpClient client = new HttpClient();

            //    var response = client.GetAsync(baseUri + "api/companies").Result;

            //    Console.WriteLine(response);
            //    Console.WriteLine(response.Content.ReadAsStringAsync().Result);
            //} 
            #endregion

            Console.WriteLine("Starting web Server {0}...", baseUri);
            using (WebApp.Start<Startup>(baseUri))
            {
                //Console.WriteLine("Server running at {0} - press Enter to quit. ", baseUri);
                //Console.ReadLine();

                Console.WriteLine("Read all the companies...");
                var companyClient = new CompanyClient(baseUri);

                var companies = companyClient.GetCompanies();
                WriteCompaniesList(companies);


                int nextId = (from c in companies select c.Id).Max() + 1;

                Console.WriteLine("Add a new company...");
                var result = companyClient.AddCompany(
                    new Company
                    {
                        Id = nextId,
                        Name = string.Format("New Company #{0}", nextId)
                    });
                WriteStatusCodeResult(result);

                Console.WriteLine("Updated List after Update:");
                companies = companyClient.GetCompanies();
                WriteCompaniesList(companies);

                Console.WriteLine("Delete a company...");
                result = companyClient.DeleteCompany(nextId - 1);
                WriteStatusCodeResult(result);

                Console.WriteLine("Updated List after Delete:");
                companies = companyClient.GetCompanies();
                WriteCompaniesList(companies);

                //Console.WriteLine("Server running at {0} - press Enter to quit. ", baseUri);
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
            foreach (var company in companies)
            {
                Console.WriteLine("Id: {0} Name: {1}", company.Id, company.Name);
            }
            Console.WriteLine("");
        }
    }
}
