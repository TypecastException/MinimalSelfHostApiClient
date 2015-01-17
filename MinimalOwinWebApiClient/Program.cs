using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Add Usings:
using System.Net.Http;

namespace MinimalOwinWebApiClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Run();
            Console.WriteLine("Done! Press the Enter key to Exit...");
            Console.ReadLine();
            return;
        }

        static async void Run()
        {
            // Create an http client provider:
            string hostUriString = "http://localhost:8080";
            var provider = new apiClientProvider(hostUriString);
            string _accessToken;

            try
            {
                Dictionary<string, string> tokenDictionary = await provider.GetTokenDictionary("john@example.com", "password");
                _accessToken = tokenDictionary["access_token"];
            }
            catch (AggregateException ex)
            {
                Console.WriteLine(ex.InnerExceptions[0].Message);
                Console.WriteLine("Press the Enter key to Exit...");
                Console.ReadLine();
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Press the Enter key to Exit...");
                Console.ReadLine();
                return;
            }


            // Create a company client instance:
            var baseUri = new Uri(hostUriString);
            var companyClient = new CompanyClient(baseUri, _accessToken);

            // Read initial companies:
            Console.WriteLine("Read all the companies...");
            var companies = await companyClient.GetCompaniesAsync();
            WriteCompaniesList(companies);

            int nextId = (from c in companies select c.Id).Max() + 1;

            Console.WriteLine("Add a new company...");
            var result = await companyClient.AddCompanyAsync(new Company { Name = string.Format("New Company #{0}", nextId) });
            WriteStatusCodeResult(result);

            Console.WriteLine("Updated List after Add:");
            companies = await companyClient.GetCompaniesAsync();
            WriteCompaniesList(companies);

            Console.WriteLine("Update a company...");
            var updateMe = await companyClient.GetCompanyAsync(nextId);
            updateMe.Name = string.Format("Updated company #{0}", updateMe.Id);
            result = await companyClient.UpdateCompanyAsync(updateMe);
            WriteStatusCodeResult(result);

            Console.WriteLine("Updated List after Update:");
            companies = await companyClient.GetCompaniesAsync();
            WriteCompaniesList(companies);


            Console.WriteLine("Delete a company...");
            result = await companyClient.DeleteCompanyAsync(nextId - 1);
            WriteStatusCodeResult(result);

            Console.WriteLine("Updated List after Delete:");
            companies = await companyClient.GetCompaniesAsync();
            WriteCompaniesList(companies);
        }


        static void WriteCompaniesList(IEnumerable<Company> companies)
        {
            foreach(var company in companies)
            {
                Console.WriteLine("Id: {0} Name: {1}", company.Id, company.Name);
            }
            Console.WriteLine("");
        }

        static void WriteStatusCodeResult(System.Net.HttpStatusCode statusCode)
        {
            if(statusCode == System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine("Opreation Succeeded - status code {0}", statusCode);
            }
            else
            {
                Console.WriteLine("Opreation Failed - status code {0}", statusCode);
            }
            Console.WriteLine("");
        }
    }
}
