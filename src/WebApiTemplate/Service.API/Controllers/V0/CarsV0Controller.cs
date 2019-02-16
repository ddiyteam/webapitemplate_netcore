using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.API.Models;
using Service.BLL.Contracts;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Service.API.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/cars")]
    [ApiVersion("0.9", Deprecated = true)]
    [ApiController]
    public class CarsV0Controller : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICarsService _carsService;

        public CarsV0Controller(IMapper mapper, ICarsService carsService)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _carsService = carsService ?? throw new ArgumentNullException(nameof(carsService));
        }

        /// <summary>
        /// Get cars list
        /// </summary>       
        /// <param name="limit">Items count</param>
        /// <returns></returns>
        [HttpGet("")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(IEnumerable<Car>), Description = "Returns finded cars array")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Description = "Missing or invalid pageNumber or pageSize")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Description = "Unexpected error")]
        public async Task<IActionResult> GetCarsV0ListAsync([FromQuery] int limit = 50)
        {
            if(limit == 0)
            {
                return BadRequest();
            }

            var result = await _carsService.GetCarsListAsync(1, limit);
            return Ok(_mapper.Map<IEnumerable<Car>>(result));
        }
    }
}