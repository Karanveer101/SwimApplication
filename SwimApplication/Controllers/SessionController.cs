using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Diagnostics;
using SwimApplication.Models;
using SwimApplication.Models.ViewModels;
using System.Web.Script.Serialization;

namespace SwimApplication.Controllers
{
    public class SessionController : Controller
    {
        private static readonly HttpClient client;

        static SessionController()
        {
            HttpClientHandler handler = new HttpClientHandler()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, errors) => true
            };
            client = new HttpClient(handler);
            client.BaseAddress = new Uri("https://localhost:44371/api/");
        }

        /// <summary>
        /// Grabs the authentication cookie sent to this controller.
        /// For proper WebAPI authentication, you can send a post request with login credentials to the WebAPI and log the access token from the response. The controller already knows this token, so we're just passing it up the chain.
        /// 
        /// Here is a descriptive article which walks through the process of setting up authorization/authentication directly.
        /// https://docs.microsoft.com/en-us/aspnet/web-api/overview/security/individual-accounts-in-web-api
        /// </summary>
        private void GetApplicationCookie()
        {
            string token = "";
            //HTTP client is set up to be reused, otherwise it will exhaust server resources.
            //This is a bit dangerous because a previously authenticated cookie could be cached for
            //a follow-up request from someone else. Reset cookies in HTTP client before grabbing a new one.
            client.DefaultRequestHeaders.Remove("Cookie");
            if (!User.Identity.IsAuthenticated) return;

            HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies.Get(".AspNet.ApplicationCookie");
            if (cookie != null) token = cookie.Value;

            //collect token as it is submitted to the controller
            //use it to pass along to the WebAPI.
            Debug.WriteLine("Token Submitted is : " + token);
            if (token != "") client.DefaultRequestHeaders.Add("Cookie", ".AspNet.ApplicationCookie=" + token);

            return;
        }

        // GET: Session/List
        public ActionResult List()
        {
            // objective: communicate with our session data api to retrieve a list of sessions
        

            string url = "sessiondata/listsessions";
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is ");
            Debug.WriteLine(response.StatusCode);

            IEnumerable<SessionDto> sessions = response.Content.ReadAsAsync<IEnumerable<SessionDto>>().Result;
            Debug.WriteLine("Number of sessions received");
            Debug.WriteLine(sessions.Count());

            return View(sessions);
        }

        // GET: Session/Details/5
        public ActionResult Details(int id)
        {
            DetailsSession ViewModel = new DetailsSession();
            // objective: communicate with our session data api to retrieve one session
         

            string url = "sessiondata/findsession/"+id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is ");
            Debug.WriteLine(response.StatusCode);

        SessionDto SelectedSession = response.Content.ReadAsAsync<SessionDto>().Result;
            Debug.WriteLine("session received");
            Debug.WriteLine(SelectedSession.Date);

            ViewModel.SelectedSession = SelectedSession;

            return View(ViewModel);
        }

        public ActionResult Error()
        {
            return View();
        }
        // GET: Session/New
        public ActionResult New()
        {
            return View();
        }

        // POST: Session/Create
        [HttpPost]
        [Authorize]

        public ActionResult Create(Session session)
        {
            GetApplicationCookie(); //get token credentials
           

            //objective: add new session to our system using API

            string url = "addsession";

            Debug.WriteLine(session.Date);

            JavaScriptSerializer jss = new JavaScriptSerializer();
            string jsonpayload = jss.Serialize(session);

            HttpContent content = new StringContent(jsonpayload);
            Debug.WriteLine(content);
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }

        }

        // GET: Session/Edit/5
        [Authorize]
        public ActionResult Edit(int id)
        {
            UpdateSession ViewModel = new UpdateSession();

            string url = "sessiondata/findsession/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            SessionDto SelectedSession = response.Content.ReadAsAsync<SessionDto>().Result;
            ViewModel.SelectedSession = SelectedSession;
            return View(ViewModel);
        }

        // POST: Session/Update/5
        [HttpPost]
        [Authorize]

        public ActionResult Update(int id, Session session)
        {
            GetApplicationCookie(); //get token credentials

            string url = "sessiondata/updatesession/" + id;
            JavaScriptSerializer jss = new JavaScriptSerializer();
            string jsonpayload = jss.Serialize(session);

            HttpContent content = new StringContent(jsonpayload);
            Debug.WriteLine(content);
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: Session/Delete/5
        [Authorize]

        public ActionResult DeleteConfirm(int id)
        {
            string url = "sessiondata/findsession/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            SessionDto SelectedSession = response.Content.ReadAsAsync<SessionDto>().Result;
            return View(SelectedSession);
          
        }

        // POST: Session/Delete/5
        [HttpPost]
        [Authorize]

        public ActionResult Delete(int id)
        {
            GetApplicationCookie(); //get token credentials
            string url = "sessiondata/deletesession/" + id;
            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }

        }
    }
}
