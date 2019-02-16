using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.API.Models;
using Service.BLL;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Service.API.Controllers
{    
    [Authorize]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/todos")]
    [ApiVersion("1.0")]
    [ApiController]      
    public class TodoController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly TodosMockProxyService _todosMockService;

        /// <summary>
        /// Todos mock web proxy 
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="carsService"></param>
        public TodoController(IMapper mapper, TodosMockProxyService todosMockService)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _todosMockService = todosMockService ?? throw new ArgumentNullException(nameof(todosMockService));
        }      

        /// <summary>
        /// Get todo by id
        /// </summary>
        /// <param name="id">Todo id</param>     
        /// <returns></returns>
        [HttpGet("{id}")]       
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "Returns 200")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Description = "Missing todo object")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Description = "Unexpected error")]
        public async Task<IActionResult> GetTodoById([FromRoute] int id)
        {     
            var result = await _todosMockService.GetTodoById(id);
            if(result == null)
            {
                return NotFound(null);
            }
            return Ok(_mapper.Map<Todo>(result));
        }       

        /// <summary>
        /// Get todos list from remote mock api
        /// </summary>      
        /// <returns></returns>
        [HttpGet("")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(IEnumerable<Todo>), Description = "Returns todos array")]
        [SwaggerResponse((int)HttpStatusCode.NoContent, Description = "Missing todos array")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Description = "Unexpected error")]
        public async Task<IActionResult> GetTodos()
        {   
            var result = await _todosMockService.GetTodos();
            if (result == null)
            {
                return NoContent();
            }
            return Ok(_mapper.Map<IEnumerable<Todo>>(result));
        }
    }
}