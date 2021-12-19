using System.Net.Http;
using System.Threading.Tasks;

namespace GitSearch.Services
{
    public interface IClient
    {
        string Token { set; }
        public Task<HttpResponseMessage> GetAsync(string subject);


    }
}
