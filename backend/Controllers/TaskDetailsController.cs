using backend.Controllers.Base;
using backend.Models.Dtos;
using backend.Models.Dtos.TaskDetail;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace backend.Controllers
{
    [Route("api/details")]
    public class TaskDetailsController : AuthorizeBaseApi
    {
        private readonly ITaskDetailService _detailService;

        public TaskDetailsController(ITaskDetailService detailService)
        {
            _detailService = detailService;
        }

        /// <summary>
        /// Get one task list by taskDetail-ID
        /// </summary>
        /// <param name="Guid"></param>
        /// <returns></returns>
        [HttpGet("byList/{listId:Guid}")]
        [ProducesResponseType(typeof(Response<List<TaskDetailResult>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByList([FromRoute] Guid listId)
        {
            var rs = await _detailService.GetByListAsync(listId);
            if (rs == null || !rs.Any()) return NotFound();

            return Ok(new Response<List<TaskDetailResult>>
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Task Details content:",
                Data = rs
            });
        }

        /// <summary>
        /// Get one task detail by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("{id:Guid}")]
        [ProducesResponseType(typeof(Response<TaskDetailResult>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var rs = await _detailService.GetByIdAsync(id);
            if (rs == null) return NotFound();

            return Ok(new Response<TaskDetailResult>
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Task Detail content:",
                Data = rs
            });
        }

        /// <summary>
        /// Get all task detail data
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("getAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            var rs = await _detailService.GetAllAsync();
            if (rs == null) return StatusCode(StatusCodes.Status500InternalServerError);
            if (!rs.Any()) return Ok(new Response<List<TaskDetailResult>>
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Empty detail",
                Data = rs
            });

            return Ok(new Response<List<TaskDetailResult>>
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Task Details content:",
                Data = rs
            });
        }

        /// <summary>
        /// Create task detail
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] AddTaskDetail model)
        {
            if (!ModelState.IsValid) return BadRequest();
            var rs = await _detailService.CreateAsync(model);
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

        /// <summary>
        /// Update one task detail
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Update([FromBody] EditTaskDetail model)
        {
            if (!ModelState.IsValid) return BadRequest();
            var result = await _detailService.UpdateAsync(model);
            if (!result) return BadRequest();
            return Ok(new Response<string>
            {
                StatusCode = HttpStatusCode.OK,
                Message = "List has changed."

            });
        }

        /// <summary>
        /// Delete one task detail by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            if (id == Guid.Empty) return NotFound();
            var result = await _detailService.DeleteAsync(id);
            if (!result) return NotFound();
            return Ok(new Response<string>
            {
                StatusCode = HttpStatusCode.OK,
                Message = "List has deleted."
            });
        }
    }
}