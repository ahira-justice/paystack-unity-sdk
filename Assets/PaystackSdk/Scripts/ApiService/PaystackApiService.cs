using System.Net.Http;

namespace PaystackSdk.Scripts.ApiService
{
    public class PaystackApiService
    {
        
        private string _baseUrl = "https://api.paystack.co";
        private HttpClient _httpClient;
        
        public PaystackApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
    }
}