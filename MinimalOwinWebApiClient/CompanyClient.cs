using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Add Usings:
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;
using System.Web.Http;

// Add for Identity/Token Deserialization:
using Newtonsoft.Json;


namespace MinimalOwinWebApiClient
{
    public class CompanyClient
    {
        string _accessToken;
        Uri _baseRequestUri;
        public CompanyClient(Uri baseUri, string accessToken)
        {
            _accessToken = accessToken;
            _baseRequestUri = new Uri(baseUri, "api/companies/");
        }


        // Handy helper method to set the access token for each request:
        void SetClientAuthentication(HttpClient client)
        {
            client.DefaultRequestHeaders.Authorization 
                = new AuthenticationHeaderValue("Bearer", _accessToken); 
        }


        public async Task<IEnumerable<Company>> GetCompaniesAsync()
        {
            HttpResponseMessage response;
            using(var client = new HttpClient())
            {
                SetClientAuthentication(client);
                response = await client.GetAsync(_baseRequestUri);
            }
            if(!response.IsSuccessStatusCode)
            {
                throw new Exception(string.Format(
                    "API Error: Status Code: {0}", response.StatusCode));
            }
            return await response.Content.ReadAsAsync<IEnumerable<Company>>();
        }


        public async Task<Company> GetCompanyAsync(int id)
        {
            HttpResponseMessage response;
            using (var client = new HttpClient())
            {
                SetClientAuthentication(client);

                // Combine base address URI and ID to new URI
                // that looks like http://hosturl/api/companies/id
                response = await client.GetAsync(
                    new Uri(_baseRequestUri, id.ToString()));
            }
            var result = await response.Content.ReadAsAsync<Company>();
            return result;
        }


        public async Task<HttpStatusCode> AddCompanyAsync(Company company)
        {
            HttpResponseMessage response;
            using(var client = new HttpClient())
            {
                SetClientAuthentication(client);
                response = await client.PostAsJsonAsync(
                    _baseRequestUri, company);
            }
            return response.StatusCode;
        }


        public async Task<HttpStatusCode> UpdateCompanyAsync(Company company)
        {
            HttpResponseMessage response;
            using (var client = new HttpClient())
            {
                SetClientAuthentication(client);
                response = await client.PutAsJsonAsync(
                    _baseRequestUri, company);
            }
            return response.StatusCode;
        }


        public async Task<HttpStatusCode> DeleteCompanyAsync(int id)
        {
            HttpResponseMessage response;
            using (var client = new HttpClient())
            {
                SetClientAuthentication(client);

                // Combine base address URI and ID to new URI
                // that looks like http://hosturl/api/companies/id
                response = await client.DeleteAsync(
                    new Uri(_baseRequestUri, id.ToString()));
            }
            return response.StatusCode;
        }
    }
}
