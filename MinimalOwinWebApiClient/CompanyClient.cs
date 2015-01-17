using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Add Usings:
using System.Net.Http;
using System.Net;

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

        void SetClientAuthentication(HttpClient client)
        {
            client.DefaultRequestHeaders.Authorization 
                = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _accessToken); 
        }

        public async Task<IEnumerable<Company>> GetCompaniesAsync()
        {
            HttpResponseMessage response;
            using(var client = new HttpClient())
            {
                client.BaseAddress = _baseRequestUri;
                SetClientAuthentication(client);
                response = await client.GetAsync(client.BaseAddress);
            }
            return await response.Content.ReadAsAsync<IEnumerable<Company>>();
        }


        public async Task<Company> GetCompanyAsync(int id)
        {
            HttpResponseMessage response;
            using (var client = new HttpClient())
            {
                client.BaseAddress = _baseRequestUri;
                SetClientAuthentication(client);
                response = await client.GetAsync(new Uri(_baseRequestUri, id.ToString()));
            }
            var result = await response.Content.ReadAsAsync<Company>();
            return result;
        }


        public async Task<HttpStatusCode> AddCompanyAsync(Company company)
        {
            HttpResponseMessage response;
            using(var client = new HttpClient())
            {
                client.BaseAddress = _baseRequestUri;
                SetClientAuthentication(client);
                response = await client.PostAsJsonAsync(client.BaseAddress, company);
            }
            return response.StatusCode;
        }


        public async Task<HttpStatusCode> UpdateCompanyAsync(Company company)
        {
            HttpResponseMessage response;
            using (var client = new HttpClient())
            {
                client.BaseAddress = _baseRequestUri;
                SetClientAuthentication(client);
                response = await client.PutAsJsonAsync(client.BaseAddress, company);
            }
            return response.StatusCode;
        }


        public async Task<HttpStatusCode> DeleteCompanyAsync(int id)
        {
            HttpResponseMessage response;
            using (var client = new HttpClient())
            {
                client.BaseAddress = _baseRequestUri;
                SetClientAuthentication(client);
                response = await client.DeleteAsync(new Uri(client.BaseAddress, id.ToString()));
            }
            return response.StatusCode;
        }
    }
}
