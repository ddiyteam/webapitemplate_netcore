using Service.DAL.MySql.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.DAL.MySql.Contract
{
    public interface ICarsRepository
    {
        Task<CarEntity> CreateCarAsync(CarEntity car);
        Task<CarEntity> GetCarAsync(Guid id);
        Task<bool> UpdateCarAsync(CarEntity car);
        Task<bool> DeleteCarAsync(Guid id);

        Task<IEnumerable<CarEntity>> GetCarsListAsync(int pageNumber, int pageSize);
    }
}
