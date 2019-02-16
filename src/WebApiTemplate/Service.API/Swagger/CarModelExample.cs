using Service.API.Models;
using Swashbuckle.AspNetCore.Filters;
using System;

namespace Service.API.Swagger
{
    public class CarModelExample : IExamplesProvider
    {
        public object GetExamples()
        {
            var dnow = DateTime.UtcNow;
            return new Car
            {
                Id = Guid.NewGuid(),
                ModelName = "Toyota",
                CarType = CarType.Hatchback,               
                CreatedOn = dnow,
                ModifiedOn = dnow
            };
        }
    }
}
