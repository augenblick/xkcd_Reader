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
            if (xkcdClient != null)
            {
                xkcdClient = new HttpClient();
                xkcdClient.BaseAddress = new Uri("http://xkcd.com/");
                xkcdClient.DefaultRequestHeaders.Clear();
                xkcdClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            }

        }

        public async Task retrieveXkcd(int comicNumber)
        {
            // retrieve comic object
            if (comicNumber > 0)
            {
                
            }

            else
            {

            }
        }

            
        


    }
}
