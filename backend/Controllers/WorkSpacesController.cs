using backend.Controllers.Base;
using backend.Models.Dtos;
using backend.Models.Dtos.UserWorkSpace;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace backend.Controllers
{
    [Route("api/workspace")]
    public class WorkSpacesController : AuthorizeBaseApi
    {
        private readonly IWorkSpaceService _workSpaceService;
        private readonly IAuthenticationService _authenService;

        public WorkSpacesController(IWorkSpaceService workSpaceService
            , IAuthenticationService authenService)
        {
            _workSpaceService = workSpaceService;
            _authenService = authenService;
        }

        /// <summary>
        /// Get workspace by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("{id:Guid}")]
        [ProducesResponseType(typeof(Response<WorkSpaceResult>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            if (id == Guid.Empty) return BadRequest();

            var rs = await _workSpaceService.GetByIdAsync(id);
            if (rs == null) return NotFound();

            return Ok(new Response<WorkSpaceResult>
            {
                StatusCode = HttpStatusCode.OK,
                Message = $"WorkSpace {id} content:",
                Data = rs
            });
        }

        /// <summary>
        /// Get workspace by username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [HttpGet("{username}")]
        [ProducesResponseType(typeof(Response<List<WorkSpaceResult>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByUser([FromRoute] string username)
        {
            var user = await _authenService.GetUserAsync(username);
            if (user == null) return NotFound();

            var rs = await _workSpaceService.GetByUserAsync(user.Id);
            if (rs == null || !rs.Any()) return NotFound();

            return Ok(new Response<List<WorkSpaceResult>>
            {
                StatusCode = HttpStatusCode.OK,
                Message = $"WorkSpace of {username} content:",
                Data = rs
            });
        }

        /// <summary>
        /// Get all workspace
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("getAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            var rs = await _workSpaceService.GetAllAsync();
            if (rs == null) return StatusCode(StatusCodes.Status500InternalServerError);
            if (!rs.Any()) return Ok(new Response<List<WorkSpaceResult>>
            {
                StatusCode = HttpStatusCode.OK,
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

        /// <summary>
        /// Create a workspacce
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] AddWorkSpace model)
        {
            if (!ModelState.IsValid) return BadRequest();
            var rs = await _workSpaceService.CreateAsync(model, User.Identity.Name);
            if (rs == null || rs == Guid.Empty) return StatusCode(StatusCodes.Status500InternalServerError);

            var createdRoute = new { action = nameof(Get), id = rs };
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

        /// <summary>
        /// Update workspace
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Update([FromBody] EditWorkSpace model)
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

        /// <summary>
        /// Delete workspace by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            if (id == Guid.Empty) return NotFound();
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