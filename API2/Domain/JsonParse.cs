using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;
using Meta.log;
using System.Security.Cryptography;

namespace Meta.Domain.src
{
    class JsonParse
    {
        //public static List<MensagemTable> parseJSON ( string json )
        //{
        //    return JsonConvert.DeserializeObject <List <MensagemTable>>(json, 
        //        new JsonSerializerSettings() { DateFormatString = "yyyy-MM-ddThh:mm:ssZ" });
        //}

        public static dynamic parseSWAPJSON(string json)
        {
            try
            {
               return JObject.Parse(json); 
                   //JsonConvert.DeserializeObject<List<JObject>>(json); //,
                  //  new JsonSerializerSettings() { DateFormatString = "yyyy-MM-ddThh:mm:ssZ" });
            }
            catch (Exception e)
            {
                CentralLog.LogError(e, "Error in Parse JSON");
                return new List<JObject> { };
            }
        }
        
        public static string dateJSON()
        {
            return DateTime.Now.ToUniversalTime().ToString("s") +"Z";
        }
        public static string genJson(Dictionary<string, string> jsonDic, List<string> exceptionsOf)
        {
            string json = "{";

            string value = "";

            bool exceptional = false;


            foreach (KeyValuePair<string, string> entry in jsonDic)
            {
                json = json + "\"" + entry.Key + "\": ";


                exceptional = false;

                foreach (string e in exceptionsOf)
                {
                    exceptional = exceptional || entry.Key.Equals(e);
                }
                
                // error in add exception
                // if (Regex.IsMatch(entry.Value, @"^\d+$") && !exceptional) 
                if (exceptional) {
                    value = entry.Value + ", ";
                } else {
                    value = "\"" + entry.Value + "\",";
                }

                json = json + value;
            }

            json = json.Substring(0, json.Length - 1);

            json = json + "}";

            return json;
        }
    }
}
