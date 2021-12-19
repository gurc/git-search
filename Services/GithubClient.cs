using System.Net.Http;
using System.Threading.Tasks;
using System;

namespace GitSearch.Services
{
    public class GithubClient:IClient
    {
        private readonly HttpClient client = new HttpClient();
        private string _token;
        public string Token { set
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Token", value);
                _token = value;
            }
            get {return _token; } }

        public GithubClient()
        {
            client.DefaultRequestHeaders.UserAgent.Add(new System.Net.Http.Headers.ProductInfoHeaderValue("AppName", "1.0"));
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<HttpResponseMessage> GetAsync(string subject)
        {
            if (_token == null)
            {
                throw new Exception("Не задан токен для доступа к Github");
            }
            var response = await client.GetAsync(subject);
            return response;
        }
    }
}
