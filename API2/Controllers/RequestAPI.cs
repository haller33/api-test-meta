using System;
using System.Web;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using Meta.Domain.src;
using Meta.log;

namespace Meta.Controller.src
{
    static class RequestAPI
    {
        private static readonly int LimitTimeout = 10000;
        private static readonly string MethodFor = "GET"; 

        public static bool TryAddCookie(this WebRequest webRequest, Cookie cookie)
        {
            HttpWebRequest httpRequest = webRequest as HttpWebRequest;
            if (httpRequest == null)
            {
                return false;
            }

            if (httpRequest.CookieContainer == null)
            {
                httpRequest.CookieContainer = new CookieContainer();
            }

            httpRequest.CookieContainer.Add(cookie);
            return true;
        }
        public static string GetT (string url, string parametros, string authHeaders)
        {
            string responseFromServer = "";

            try
            {
                string urlPram = HttpUtility.UrlDecode(url + parametros);

                WebRequest request = WebRequest.Create(urlPram);

                request.Method = MethodFor;

                request.Timeout = LimitTimeout;
                
                
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    responseFromServer = reader.ReadToEnd();
                }

            }
            catch (WebException e) {
                
                Dictionary<string, string> ErroResponse = new Dictionary<string, string> () 
                {
                    { "message", e.Message },
                    { "Status", e.Status.ToString (  ) },
                    { "stack", new StreamReader(e.Response.GetResponseStream()).ReadToEnd() },
                    { "erro", e.ToString (  ) }
                };
                
                responseFromServer = JsonParse.genJson(ErroResponse, new List<string>(){});
                CentralLog.LogError(e, ":: ERROR >> CANOT ACCESS URL ::");
            }
            catch (Exception e)
            {
                Dictionary<string, string> ErroResponse = new Dictionary<string, string> () 
                {
                    { "message", e.Message },
                    { "Status", "5001" },
                    { "Erro", e.ToString (  ) }
                };
                
                responseFromServer = JsonParse.genJson(ErroResponse, new List<string>(){});
                CentralLog.LogError(e, ":: ERROR >> CANOT ACCESS URL ::");
            } 
        
            return responseFromServer;
        }
    }
}
