using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace vlab_docker_dotnet_core_simple_web_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        public MessageController() { }

        // POST: api/message
        [HttpPost]
        public void Post([FromBody] string value)
        {
            // Do nothing.
        }
    }
}