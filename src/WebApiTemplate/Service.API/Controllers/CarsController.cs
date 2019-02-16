using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.API.Models;
using Service.API.Swagger;
using Service.BLL.Contracts;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Service.API.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/cars")]
    [ApiVersion("1.0")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICarsService _carsService;

        /// <summary>
        /// Cars db api
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="carsService"></param>
        public CarsController(IMapper mapper, ICarsService carsService)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _carsService = carsService ?? throw new ArgumentNullException(nameof(carsService));
        }

        /// <summary>
        /// Create a new car
        /// </summary>
        /// <param name="car"></param>
        /// <returns>Returns created car</returns>           
        [HttpPost("")]
        [SwaggerResponseExample((int)HttpStatusCode.Created, typeof(CarModelExample))]
        [SwaggerResponse((int)HttpStatusCode.Created, Type = typeof(Car), Description = "Returns created car")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Description = "Unexpected error")]
        public async Task<IActionResult> CreateCarAsync([FromBody] Car car)
        { 
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _carsService.CreateCarAsync(_mapper.Map<BLL.Models.Car>(car));
            return Created($"{result.Id}", _mapper.Map<Car>(result));
        }

        /// <summary>
        /// Get car by id
        /// </summary>
        /// <param name="id">Car Id</param>
        /// <returns>Returns finded car</returns>
        [HttpGet("{id}")]        
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(Car), Description = "Returns finded car")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(CarModelExample))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Description = "Missing or invalid car id")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Description = "Unexpected error")]
        public async Task<IActionResult> GetCarAsync([FromRoute] Guid id) 
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            var result = await _carsService.GetCarAsync(id);
            return Ok(_mapper.Map<Car>(result));
        }

        /// <summary>
        /// Update existing car
        /// </summary>
        /// <param name="id">Car id</param>
        /// <param name="car">Car parameters</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [SwaggerRequestExample(typeof(Car), typeof(CarModelExample))]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "Returns 200")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Description = "Missing or invalid car object")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Description = "Unexpected error")]
        public async Task<IActionResult> UpdateCarAsync([FromRoute] Guid id, [FromBody] Car car)
        {
            if (id == Guid.Empty || !ModelState.IsValid)
            {
                return BadRequest();
            }

            car.Id = id;
            var result = await _carsService.UpdateCarAsync(_mapper.Map<BLL.Models.Car>(car));
            return Ok();
        }

        /// <summary>
        /// Delete car
        /// </summary>
        /// <param name="id">Car id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "Returns 200")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Description = "Missing or invalid car id")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Description = "Unexpected error")]
        public async Task<IActionResult> DeleteCarAsync([FromRoute] Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            var result = await _carsService.DeleteCarAsync(id);
            return Ok();
        }

        /// <summary>
        /// Get cars list
        /// </summary>
        /// <param name="pageNumber">Page number</param>
        /// <param name="pageSize">Page size</param>
        /// <returns></returns>
        [HttpGet("")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(IEnumerable<Car>), Description = "Returns finded cars array")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Description = "Missing or invalid pageNumber or pageSize")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Description = "Unexpected error")]
        public async Task<IActionResult> GetCarsListAsync([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 50)
        {
            if(pageNumber==0 || pageSize == 0)
            {
                return BadRequest();
            }

            var result = await _carsService.GetCarsListAsync(pageNumber, pageSize);
            return Ok(_mapper.Map<IEnumerable<Car>>(result));
        }
    }
}