using backend.Defaults;
using backend.Models.Dtos;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace backend.Controllers
{
    [ApiController]
    [Authorize(Roles = RoleName.Admin)]
    [Route("api/[controller]/Seed/[action]")]
    public class DataSeedingController : ControllerBase
    {
        private IDataSeedingService _seedingService;

        public DataSeedingController(IDataSeedingService seedingService)
        {
            _seedingService = seedingService;
        }

        [HttpPost("{number:int:min(0)}")]
        public async Task<ActionResult<Response<string>>> WorkSpace([FromRoute] int number = 1)
        {
            var rs = _seedingService.SeedWorSpace(number);
            if (rs == false) return BadRequest();
            return Ok(new Response<string>
            {
                Data = null,
                Message = "Data has seeded",
                StatusCode = HttpStatusCode.OK
            });
        }
        [HttpPost("{number:int:min(0)}")]
        public async Task<ActionResult<Response<string>>> TaskList([FromQuery] Guid workSpaceId, [FromRoute] int number = 1)
        {

            _seedingService.SeedTaskList(workSpaceId, number);
            return Ok(new Response<string>
            {
                Data = null,
                Message = "Data has seeded",
                StatusCode = HttpStatusCode.OK
            });
        }
        [HttpPost("{number:int:min(0)}")]
        public async Task<ActionResult<Response<string>>> TaskDetail([FromQuery] Guid taskListId, [FromRoute] int number = 1)
        {

            _seedingService.SeedTaskList(taskListId, number);
            return Ok(new Response<string>
            {
                Data = null,
                Message = "Data has seeded",
                StatusCode = HttpStatusCode.OK
            });
        }

    }
}