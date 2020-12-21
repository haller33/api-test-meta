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
	
        [HttpGet("taxaJuros")]
        public async Task<IActionResult> getStatus (  ) 
        {
            return await Task.Run(() => {

                string returnStr = "";
                string taxa = AppSettingsProvider.taxaJuros;

                returnStr = taxa;

                return Ok(returnStr);            
            });
   
        }

        [HttpGet("showmethecode")]
        public async Task<IActionResult> getStatus (  ) 
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