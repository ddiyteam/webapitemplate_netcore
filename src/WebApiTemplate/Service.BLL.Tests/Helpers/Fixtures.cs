using AutoFixture;
using Service.BLL.Models;

namespace Service.BLL.Tests.Helpers
{
    public static class Fixtures
    {
        public static Car CarFixture(string modelName = null, CarType carType = 0)
        {
            var fixture = new Fixture();

            var car = fixture.Build<Car>();

            if(!string.IsNullOrEmpty(modelName))
            {
                car.With(s => s.ModelName, modelName);
            }

            if (carType > 0)
            {
                car.With(s => s.CarType, carType);
            }           

            return car.Create();
        }
    }
}
