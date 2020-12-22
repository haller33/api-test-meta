using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Meta.Domain.Auth;
using Meta.Domain.src;
using Meta.log;

namespace Meta.Controller.src
{
    class EndPoints
    {
        private static readonly bool flagTESTINProduction = AppSettingsProvider.TestInProduction;
        public static readonly bool ConstructionRequestLOG = false;
        public static readonly bool debugMode = AppSettingsProvider.IsDevelopment;
        public static readonly string sourceRequest = "WEB";
        public static readonly string prodCat = "";

        public static string GetTaxaJuros ( string ARGS )
        {
            return genralRequest ( ARGS, "taxaJuros" );
        }
        private static string genralRequest ( string EndpointTO, string ARGS, string transactionIDCancell = "" )
        {
            	
            if (EndPoints.debugMode) 
                CentralLog.LogInfo($"ENDPOINTS :: CALL {EndpointTO} with [{ARGS}]");

            string url = EndpointTO;

            string parametros = "/" + ARGS;

            const string authHeaders = "";
            const string postDataSend = "";

            if (EndPoints.debugMode) {
                
                CentralLog.LogInfo("ENDPOINTS :: JSON");
                CentralLog.LogInfo(postDataSend);
            }

            string returnStrng = "";

            try {

                string returnStr = RequestAPI.GetT(url, parametros, authHeaders);
                
                if (EndPoints.debugMode) {

                    CentralLog.LogInfo("ENDPOINT :: Return from API");
                    CentralLog.LogInfo(returnStr);
                }
                if (returnStr.Length > 0) {

                    var now = JsonParse.parseSWAPJSON(returnStr);


                    returnStrng = returnStr;
                }
            } catch (Exception e) {
                
                Dictionary<string, string> myResp = new Dictionary<string, string> ()
                {
                    { "status", "500" },
                    { "message", e.Message },
                    { "erroMessage", "imposibilitado de acessar API, tente novamente" },
                    { "erro", e.ToString (  ) }
                };

                returnStrng = JsonParse.genJson(myResp, new List<string>());

                CentralLog.LogError(e, ":: ENDPOINTS ERROR :: >> CANOT ACCESS TOKEN ::");
            }

            return returnStrng;
        }

        private static string microSegundos ()
        {
            return DateTime.Now.ToString("ss.ffffff");
        }
    }
}
