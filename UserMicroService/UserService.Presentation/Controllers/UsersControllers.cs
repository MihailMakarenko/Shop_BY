using Microsoft.AspNetCore.Mvc;
using Service.Contract;
namespace UserService.Presentation.Controllers
{
    [Route("api/users")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    public class UsersControllers(IServiceManager _serviceManager)
    {
        

    }
}
