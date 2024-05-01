using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PaqueteApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaqueteCrud : ControllerBase
    {
        [HttpGet]
        [Route("GetAll")]

        public IActionResult GetAll()
        {
            var result = BL.Paquete.GetAll();

            if (result.Item1)
            {
                return Ok(result.Item3.Paquetes);
            }
            else
            {
                return BadRequest(result.Item2);
            }
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult Add(ML.Paquete paquete)
        {
            var result = BL.Paquete.Add(paquete);

            if (result.Item1)
            {
                return Ok(result.Item2);
            }
            else
            {
                return BadRequest(result.Item2);
            }
        }
    }
}
