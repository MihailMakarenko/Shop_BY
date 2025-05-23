using Microsoft.AspNetCore.Mvc;
using Service.Contract;
using Shared.DataTransferObjects.UserDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Presentation.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AutenticationController(IServiceManager _serviceManager) : ControllerBase
    {
        //[HttpPost]
        //public async Task<IActionResult> RegisterUser([FromBody] UserForCreationDto userForRegistration)
        //{
        //    var result = await _serviceManager.
        //}
    }
}
