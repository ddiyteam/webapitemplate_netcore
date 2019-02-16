using Service.BLL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.BLL.Contracts
{
    public interface ICarsService
    {
        /// <summary>
        /// Create a new car
        /// </summary>
        /// <param name="car"></param>
        /// <returns></returns>
        Task<Car> CreateCarAsync(Car car);

        /// <summary>
        /// Get car by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Car> GetCarAsync(Guid id);

        /// <summary>
        /// Update car parameters
        /// </summary>
        /// <param name="car"></param>
        /// <returns></returns>
        Task<bool> UpdateCarAsync(Car car);

        /// <summary>
        /// Delete car by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteCarAsync(Guid id);

        /// <summary>
        /// Get cars list 
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<IEnumerable<Car>> GetCarsListAsync(int pageNumber, int pageSize);
    }
}
