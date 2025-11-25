using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using TimbradoAPI.Timbrado;

namespace TimbradoAPI.Controllers
{
    [RoutePrefix("api/timbrado")]
    public class TimbradoController : ApiController
    {
        [HttpPost]
        [Route("timbrar")]
        public IHttpActionResult Timbrar([FromBody] string xml)
        {
            try
            {
                var pac = new Timbrado.Timbrado(); 

                byte[] result = pac.TimbrarF("FIME", "s9%4ns7q#eGq", xml);

                if (result == null) return BadRequest("Sin respuesta");

                return Ok(Convert.ToBase64String(result));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}