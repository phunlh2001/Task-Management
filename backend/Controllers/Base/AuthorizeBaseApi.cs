using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers.Base
{
    [ApiController]
    [Authorize]
    public abstract class AuthorizeBaseApi : ControllerBase
    {
    }
}
