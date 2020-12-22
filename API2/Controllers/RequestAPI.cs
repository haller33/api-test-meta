using System;
using System.Web;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using OdontoSystem.Domain.src;
using OdontoSystem.log;

namespace OdontoSystem.Controller.src
{
    static class RequestAPI
    {
        private static readonly int LimitTimeout = 10000;
        private static readonly string MethodForPOSTMan = "GET"; 

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
        public static string PostT (string url, string parametros, string authHeaders, string typeSend, string postDataSend)
        {
            string responseFromServer = "";

            try
            {
                string urlPram = HttpUtility.UrlDecode(url + parametros);

                WebRequest request = WebRequest.Create(urlPram);

                request.Method = MethodForPOSTMan;

                string postData = postDataSend;
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);

                request.ContentType = typeSend;
                request.ContentLength = byteArray.Length;
                request.Headers["Accept"] = "text/html";
                request.Headers["Authorization"] = authHeaders;
                request.Headers["Connection"] = "Keep-Alive";
                request.Headers["Accept-Language"] = "en";
                request.Headers["Cache-Control"] = "no-store";
                request.Headers["Transfer-Encoding"] = "chunked";
                request.Timeout = LimitTimeout;

                // Get the request stream.
                Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();

                WebResponse response = request.GetResponse();

                string responseString = ((HttpWebResponse)response).StatusDescription;
                
                // CentralLog.LogInfo(((HttpWebResponse)response).StatusDescription);

                if (!(responseString.Equals("OK")))
                {
                    throw new Exception("ERROR in Return From API" + responseString);
                }

                using (dataStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(dataStream);
                    responseFromServer = reader.ReadToEnd();
                }

                response.Close();

            }
            catch (WebException e) {
                
                Dictionary<string, string> ErroResponse = new Dictionary<string, string> () 
                {
                    { "message", e.Message },
                    { "Status", e.Status.ToString() },
                };
                
                responseFromServer = JsonParse.genJson(ErroResponse, new List<string>(){});
                CentralLog.LogError(e, ":: ERROR >> CANOT ACCESS URL ::");
            }
            catch (Exception e)
            {
                Dictionary<string, string> ErroResponse = new Dictionary<string, string> () 
                {
                    { "message", e.Message },
                    { "Status", "500" },
                };
                
                responseFromServer = JsonParse.genJson(ErroResponse, new List<string>(){});
                CentralLog.LogError(e, ":: ERROR >> CANOT ACCESS URL ::");
            } 
        
            return responseFromServer;
        }
    }
}
