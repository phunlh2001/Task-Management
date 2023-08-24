using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Models.Dtos;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]/Seed/[action]")]
    public class DataSeedingController : ControllerBase
    {
        private IDataSeedingService _seedingService;

        public DataSeedingController(IDataSeedingService seedingService)
        {
            _seedingService = seedingService;
        }

        [HttpGet("{number:int:min(0)}")]
        public async Task<ActionResult<Response<string>>>WorkSpace(int number = 1)
        {
            var rs = _seedingService.SeedWorSpace(number);
            if(rs == false) return BadRequest();
            return Ok(new Response<string>{
                Data = null,
                Message = "Data has seeded",
                StatusCode = 200
            });
        }
        [HttpGet("{number:int:min(0)}")]
        public async Task<ActionResult<Response<string>>>TaskList(Guid workSpaceId, int number = 1)
        {
            
            _seedingService.SeedTaskList(workSpaceId,number);
            return Ok(new Response<string>{
                Data = null,
                Message = "Data has seeded",
                StatusCode = 200
            });
        }
        [HttpGet("{number:int:min(0)}")]
        public async Task<ActionResult<Response<string>>>TaskDetail(Guid taskListId, int number = 1)
        {
            
            _seedingService.SeedTaskList(taskListId,number);
            return Ok(new Response<string>{
                Data = null,
                Message = "Data has seeded",
                StatusCode = 200
            });
        }
        
    }
}