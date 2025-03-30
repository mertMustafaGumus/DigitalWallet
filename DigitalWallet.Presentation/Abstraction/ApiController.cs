using Microsoft.AspNetCore.Mvc;

namespace DigitalWallet.Presentation.Abstraction
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class ApiController : ControllerBase
    {
    }
}
