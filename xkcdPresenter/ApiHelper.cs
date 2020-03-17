using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace xkcdPresenter
{



    // A class that handles retrieval of the xkcd comic info
    class ApiHelper
    {

        private static HttpClient xkcdClient;

        public ApiHelper()
        {
            if (xkcdClient == null)
            {
                xkcdClient = new HttpClient();
                xkcdClient.DefaultRequestHeaders.Clear();
                xkcdClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            }

        }

        public async Task<Comic> retrieveXkcd(int comicNumber)
        {
            string currUri = String.Empty;

            // retrieve comic object
            if (comicNumber > 0)
            {
                currUri = $"http://xkcd.com/{comicNumber}/info.0.json";
            }

            else
            {
                currUri = $"http://xkcd.com/info.0.json";
            }

            using (HttpResponseMessage response = await xkcdClient.GetAsync(currUri))
            {
                System.Diagnostics.Debug.WriteLine(response.Content);
                if (response.IsSuccessStatusCode)
                {
                    System.Diagnostics.Debug.WriteLine("retrieved comic");
                    Comic currentComic = await response.Content.ReadAsAsync<Comic>();
                    return currentComic;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
            
        }

            
        


    }
}
