using backend.Controllers.Base;
using backend.Models.Dtos;
using backend.Models.Dtos.TaskList;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace backend.Controllers
{
    [Route("api/lists")]
    public class TaskListsController : AuthorizeBaseApi
    {
        private readonly ITaskListService _listService;

        public TaskListsController(ITaskListService listService)
        {
            _listService = listService;
        }

        [AllowAnonymous]
        [HttpGet("{id:Guid}")]
        [ProducesResponseType(typeof(Response<TaskListResult>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var rs = await _listService.GetByIdAsync(id);
            if (rs == null) return NotFound();

            return Ok(new Response<TaskListResult>
            {
                StatusCode = HttpStatusCode.OK,
                Message = $"Task List content:",
                Data = rs
            });
        }

        [HttpGet("workspace/{id:Guid}")]
        [ProducesResponseType(typeof(Response<List<TaskListResult>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByWorkSpace([FromRoute] Guid workspaceId)
        {

            var rs = await _listService.GetByWorkSpaceAsync(workspaceId);
            if (rs == null || !rs.Any()) return NotFound();

            return Ok(new Response<List<TaskListResult>>
            {
                StatusCode = HttpStatusCode.OK,
                Message = $"Task Lists content:",
                Data = rs
            });
        }

        [AllowAnonymous]
        [HttpGet("getAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            var rs = await _listService.GetAllAsync();
            if (rs == null) return StatusCode(StatusCodes.Status500InternalServerError);
            if (!rs.Any()) return Ok(new Response<List<TaskListResult>>
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Empty list",
                Data = rs
            });

            return Ok(new Response<List<TaskListResult>>
            {
                StatusCode = HttpStatusCode.OK,
                Message = $"Task Lists content:",
                Data = rs
            });
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] AddTaskList model)
        {
            if (!ModelState.IsValid) return BadRequest();
            var rs = await _listService.CreateAsync(model);
            if (rs == null) return StatusCode(StatusCodes.Status500InternalServerError);

            var createdRoute = new { action = nameof(Get), id = rs };
            return CreatedAtRoute(
                createdRoute,
                new Response<string>
                {
                    StatusCode = HttpStatusCode.Created,
                    Message = "List has created.",
                    Data = Url.RouteUrl(createdRoute)
                }
            );
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Update([FromBody] EditTaskList model)
        {
            if (!ModelState.IsValid) return BadRequest();
            var result = await _listService.UpdateAsync(model);
            if (!result) return BadRequest();
            return Ok(new Response<string>
            {
                StatusCode = HttpStatusCode.OK,
                Message = "List has changed."

            });
        }

        [HttpDelete("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            if (id == null) return NotFound();
            var result = await _listService.DeleteAsync(id);
            if (!result) return NotFound();
            return Ok(new Response<string>
            {
                StatusCode = HttpStatusCode.OK,
                Message = "List has deleted."

            });
        }
    }
}