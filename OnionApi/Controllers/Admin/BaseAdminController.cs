using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OnionApi.Controllers.Admin
{
    [Route("api/admin/[controller]/[action]")]
    [ApiController]
    //[Authorize(Roles = "Admin,SuperAdmin")]
    public abstract class BaseAdminController : ControllerBase
    {
    }
}
