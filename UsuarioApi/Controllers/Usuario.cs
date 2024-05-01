using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UsuarioApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Usuario : ControllerBase
    {
        [HttpPost]
        [Route("Add")]
        public IActionResult Add(ML.Usuario usuario)
        {
            var result = BL.Usuario.AddUsuario(usuario);

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
