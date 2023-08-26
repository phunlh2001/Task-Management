using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using backend.Data.Repositories;
using backend.Models.Dtos;
using backend.Models.Dtos.UserWorkSpace;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace backend.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]/[action]")]
    public class WorkSpaceController : ControllerBase
    {
        private readonly IWorkSpaceService _workSpaceService;

        public WorkSpaceController(IWorkSpaceService workSpaceService
        )
        {
            _workSpaceService = workSpaceService;
            
        }

        [AllowAnonymous]
        [HttpGet("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromRoute]Guid id)
        {
            if (id == null) return BadRequest();

            var rs = await _workSpaceService.GetByIdAsync(id);
            if (rs == null) return NotFound();

            return Ok(new Response<WorkSpaceResult>
            {
                StatusCode = HttpStatusCode.OK,
                Message = $"WorkSpace {id} content:",
                Data = rs
            });
        }

        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var rs = await _workSpaceService.GetAllAsync();
            if(rs == null) return StatusCode(StatusCodes.Status500InternalServerError);
            if(!rs.Any()) return Ok(new Response<List<WorkSpaceResult>>{
                StatusCode =HttpStatusCode.OK,
                Message = "Empty list",
                Data = rs
            });

            return Ok(new Response<List<WorkSpaceResult>>
            {
                StatusCode = HttpStatusCode.OK,
                Message = $"WorkSpaces content:",
                Data = rs
            });
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(AddWorkSpace model)
        {
            if (!ModelState.IsValid) return BadRequest();
            var rs = await _workSpaceService.CreateAsync(model, User.Identity.Name);
            if(rs == null || rs == Guid.Empty) return StatusCode(StatusCodes.Status500InternalServerError);

            var createdRoute = new{action = nameof(Get) ,id = rs };
            return CreatedAtRoute(
                createdRoute,   
                new Response<string>
                {
                    StatusCode = HttpStatusCode.Created,
                    Message = "Workspace has created.",
                    Data = Url.RouteUrl(createdRoute)
                }
            );
        }

        [HttpPut("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Update(EditWorkSpace model)
        {
            if (!ModelState.IsValid) return BadRequest();
            var result = await _workSpaceService.UpdateAsync(model);
            if (!result) return BadRequest();
            return Ok(new Response<string>
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Workspace has changed."

            });
        }

        [HttpDelete("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == null) return NotFound();
            var result = await _workSpaceService.DeleteAsync(id);
            if (!result) return NotFound();
            return Ok(new Response<string>
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Workspace has deleted."

            });
        }

    }
}