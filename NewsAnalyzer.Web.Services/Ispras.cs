using NewsAnalyzer.Core.DataTransferObjects;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace NewsAnalyzer.Web.Services
{
    public static class Ispras
    {
        public static async Task<List<string>> GetTexterra(string text) {

            var url = "http://api.ispras.ru/texterra/v1/nlp?targetType=lemma&apikey=08bbd9026bbaee3a7859564f6b804f46ebe09d06";
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders
                    .Accept
                    .Add( new MediaTypeWithQualityHeaderValue("application/json"));

                var request = new HttpRequestMessage(HttpMethod.Post, url)
                {
                    Content = new StringContent("[{\"text\":\""+ text +"\"}]",Encoding.UTF8, "application/json")
                };

                var response = await httpClient.SendAsync(request);
                var responseString = await response.Content.ReadAsStringAsync();
                
               return GetWordsList(responseString);
            }
        }

        private static List<string> GetWordsList(string json)
        {
            var isprasDto = JsonConvert.DeserializeObject<List<IsprasDto>>(json)
                .ToList();
            
            return isprasDto.FirstOrDefault()
                .Annotations.Lemma
                .Select(l => l.Value)
                .ToList();
        }
    }
}
