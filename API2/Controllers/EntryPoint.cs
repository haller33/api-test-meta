using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Meta.Domain.Auth;
using Meta.Domain.src;
using Meta.log;

namespace Meta.Controller.src
{
    [ApiController]
    public class EntryPointController : ControllerBase
    {

#region "Test Local"

        [HttpGet("calculaJuros")]
        public async Task<IActionResult> getCalculaJuros ( string valorinicial = "1", string messes = "1" ) 
        {
            return await Task.Run(() => {

                string returnStr = "";

                try {

                    string taxaJurosStr = EndPoints.GetTaxaJuros(AppSettingsProvider.NameAPIOne);

                    double valorInicialLocal;
                    double taxaJurosLocal;
                    double messesLocal;
                    
                    double.TryParse(valorinicial, out valorInicialLocal);
                    double.TryParse(taxaJurosStr, out taxaJurosLocal);
                    double.TryParse(messes, out messesLocal);

                    double resultado = valorInicialLocal * Math.Pow((1 + taxaJurosLocal), messesLocal);

                    resultado = Math.Round(resultado, 2);

                    returnStr += resultado.ToString();
                
                } catch (Exception e) {

                    returnStr = "Erro nos parametros ou na API1 :: " + e.ToString();
                } 
                return Ok(returnStr);
            });
        }

        [HttpGet("showmethecode")]
        public async Task<IActionResult> getShowMeTheCode (  ) 
        {
            return await Task.Run(() => {

                string returnStr = "";

                returnStr = "https://github.com/haller33/api-test-meta";

                return Ok(returnStr);
            });
   
        }

#endregion

    }
}