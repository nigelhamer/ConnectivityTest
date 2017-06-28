using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Client.Controllers
{
    public class HomeController : Controller
    {
        static HttpClient client = new HttpClient();

        public async Task<ActionResult> Index()
        {
            string name = System.Configuration.ConfigurationManager.AppSettings["name"];
            ViewBag.Message = await GetServerResponseAsync(name);

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        static async Task<string> GetServerResponseAsync(string name)
        {
            string baseApiUrl = System.Configuration.ConfigurationManager.AppSettings["baseApiUrl"];

            client.BaseAddress = new Uri(baseApiUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                var serverResponse = "";
                var uri = new Uri(client.BaseAddress, $"api/helloworld?name={name}");
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    serverResponse = await response.Content.ReadAsAsync<string>();
                }
                return serverResponse;
            }
            catch (Exception ex)
            {
                return $"Unable to get message from server. Error was {ex}";
            }

            
        }
    }
}