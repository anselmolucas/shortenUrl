using B2EGroup.ShortenUrl.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace B2EGroup.ShortenUrl.WebService.Controllers
{
    public class InfosController : Controller
    {
        string baseurl = "http://localhost:60691/";

        public async Task<ActionResult> Index()
        {
            StatusResult statusResult = new StatusResult();

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(baseurl);

                httpClient.DefaultRequestHeaders.Clear();

                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage httpResponseMessage = await httpClient.GetAsync("api/stats");

                if (httpResponseMessage.IsSuccessStatusCode)
                {  
                    var empResponse = httpResponseMessage.Content.ReadAsStringAsync().Result;
 
                    statusResult = JsonConvert.DeserializeObject<StatusResult>(empResponse);
                }

                return View(statusResult);
            }
        }
    }
}