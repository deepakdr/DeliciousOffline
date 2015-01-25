using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Helpers;
using System.Web.Http;

namespace DecliciousWrapperAPI.Controllers
{
    public class SampleController : ApiController
    {
        const string CLIENTID = "257e09704536e0caa3bf3a99cb3f2494";
        const string CLIENTSECRETKEY = "a4b2f5a5b81050c69feb94f231369533";
        const string ALLPOSTURL = "https://api.del.icio.us/v1/posts/all";
        const string ALLTAGSURL = "https://api.del.icio.us/v1/tags/get";

        // GET api/delicious
        [ActionName("GetAllBookmarks")]
        public string GetAllBookmarks(string code)
        {
            
            HttpClient client = new HttpClient();
            //var accessToken = GetAccessCode(code);
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + code);
            HttpResponseMessage response = client.GetAsync(ALLPOSTURL).Result;  // Blocking call!
            if (response.IsSuccessStatusCode)
            {
                var dataObjects = response.Content.ReadAsStringAsync().Result;
                return dataObjects;

            }
            return "No Data Available";
        }

        [ActionName("GetAllTags")]
        public string GetAllTags(string code)
        {

            HttpClient client = new HttpClient();
            //var accessToken = GetAccessCode(code);
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + code);
            HttpResponseMessage response = client.GetAsync(ALLTAGSURL).Result;  // Blocking call!
            if (response.IsSuccessStatusCode)
            {
                var dataObjects = response.Content.ReadAsStringAsync().Result;
                return dataObjects;

            }
            return "No Data Available";
        }

        public string GetAccessCode(string code)
        {

            string getAccessTokenURL = "https://avosapi.delicious.com/api/v1/oauth/token?client_id=" + CLIENTID + "&client_secret=" + CLIENTSECRETKEY + "&grant_type=code&code=" + code;
            HttpClient client = new HttpClient();
           
            HttpResponseMessage response = client.PostAsync(getAccessTokenURL, Request.Content).Result;  // Blocking call!
            if (response.IsSuccessStatusCode)
            {
                var dataObjects = response.Content.ReadAsStringAsync().Result;

                var test = new { access_token = string.Empty };
                var r = JsonConvert.DeserializeAnonymousType(dataObjects, test);
                return test.access_token;
            }
            return "No Data Available";
        }
    }
}
