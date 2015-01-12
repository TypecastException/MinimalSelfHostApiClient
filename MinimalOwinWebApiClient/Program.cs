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
            Console.WriteLine("Read all the companies...");
            var companyClient = new CompanyClient("http://localhost:8080");
            var companies = companyClient.GetCompanies();
            WriteCompaniesList(companies);

            int nextId  = (from c in companies select c.Id).Max() + 1;

            Console.WriteLine("Add a new company...");
            var result = companyClient.AddCompany(new Company { Name = string.Format("New Company #{0}", nextId) });
            WriteStatusCodeResult(result);

            Console.WriteLine("Updated List after Add:");
            companies = companyClient.GetCompanies();
            WriteCompaniesList(companies);

            Console.WriteLine("Update a company...");
            var updateMe = companyClient.GetCompany(nextId);
            updateMe.Name = string.Format("Updated company #{0}", updateMe.Id);
            result = companyClient.UpdateCompany(updateMe);
            WriteStatusCodeResult(result);

            Console.WriteLine("Updated List after Update:");
            companies = companyClient.GetCompanies();
            WriteCompaniesList(companies);

            Console.WriteLine("Delete a company...");
            result = companyClient.DeleteCompany(nextId -1);
            WriteStatusCodeResult(result);

            Console.WriteLine("Updated List after Delete:");
            companies = companyClient.GetCompanies();
            WriteCompaniesList(companies);

            Console.Read();
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


    //public class CompanyClient
    //{
    //    string _hostUri;

    //    public CompanyClient(string hostUri)
    //    {
    //        _hostUri = hostUri;
    //    }


    //    public HttpClient CreateClient()
    //    {
    //        var client = new HttpClient();
    //        client.BaseAddress = new Uri(new Uri(_hostUri), "api/companies/");
    //        return client;
    //    }


    //    public IEnumerable<Company> GetCompanies()
    //    {
    //        HttpResponseMessage response;
    //        using(var client = CreateClient())
    //        {
    //            response = client.GetAsync(client.BaseAddress).Result;
    //        }

    //        var result = response.Content.ReadAsAsync<IEnumerable<Company>>().Result;
    //        return result;
    //    }


    //    public Company GetCompany(int id)
    //    {
    //        HttpResponseMessage response;
    //        using (var client = CreateClient())
    //        {
    //            response = client.GetAsync(new Uri(client.BaseAddress, id.ToString())).Result;
    //        }

    //        var result = response.Content.ReadAsAsync<Company>().Result;
    //        return result;
    //    }


    //    public System.Net.HttpStatusCode AddCompany(Company company)
    //    {
    //        HttpResponseMessage response;
    //        using (var client = CreateClient())
    //        {
    //            response = client.PostAsJsonAsync(client.BaseAddress, company).Result;
    //        }
    //        return response.StatusCode;
    //    }


    //    public System.Net.HttpStatusCode UpdateCompany(Company company)
    //    {
    //        HttpResponseMessage response;
    //        using (var client = CreateClient())
    //        {
    //            response = client.PutAsJsonAsync(client.BaseAddress, company).Result;
    //        }
    //        return response.StatusCode;
    //    }


    //    public System.Net.HttpStatusCode DeleteCompany(int id)
    //    {
    //        HttpResponseMessage response;
    //        using (var client = CreateClient())
    //        {
    //            response = client.DeleteAsync(new Uri(client.BaseAddress, id.ToString())).Result;
    //        }
    //        return response.StatusCode;
    //    }
    //}
}
