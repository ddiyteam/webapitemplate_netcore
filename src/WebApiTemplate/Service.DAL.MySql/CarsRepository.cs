using Dapper;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using Service.DAL.MySql.Contract;
using Service.DAL.MySql.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Service.DAL.MySql
{
    public class CarsRepository : ICarsRepository, IHealthCheck
    {

        private readonly IOptionsMonitor<CarsMySqlRepositoryOption> _options;

        public CarsRepository(IOptionsMonitor<CarsMySqlRepositoryOption> options)
        {
            _options = options;            
        }        

        public async Task<CarEntity> CreateCarAsync(CarEntity newCar)
        {
            if(newCar.Id == Guid.Empty.ToString())
            {
                newCar.Id = Guid.NewGuid().ToString();
            }
            var dnow = DateTime.UtcNow;
            newCar.CreatedOn = dnow;
            newCar.ModifiedOn = dnow;

            const string sqlQuery = @"INSERT INTO cars (
                    id,
                    modelname,                   
                    cartype,
                    createdon,
                    modifiedon                   
                )
                VALUES (
                    @id,
                    @modelname,                   
                    @cartype,
                    @createdon,
                    @modifiedon);";

            using (var db = new MySqlConnection(_options.CurrentValue.CarsDbConnectionString))
            {                
                await db.ExecuteAsync(sqlQuery, newCar, commandType: CommandType.Text);
                return newCar;
            }
        }

        public async Task<CarEntity> GetCarAsync(Guid id)
        {
            using (var db = new MySqlConnection(_options.CurrentValue.CarsDbConnectionString))
            {
                const string sqlQuery = @"SELECT 
                    id,
                    modelname,                    
                    cartype,
                    createdon,
                    modifiedon
                FROM cars
                WHERE id=@id;";
                return await db.QueryFirstAsync<CarEntity>(sqlQuery, new { id = id.ToString() }, commandType: CommandType.Text);
            }
        }

        public async Task<bool> UpdateCarAsync(CarEntity car)
        {
            car.ModifiedOn = DateTime.UtcNow;

            const string sqlQuery = @"UPDATE cars SET                
                modelname = @modelname,                
                cartype = @cartype,
                createdon = @createdon,
                modifiedon = @modifiedon
            WHERE id = @id;";

            using (var db = new MySqlConnection(_options.CurrentValue.CarsDbConnectionString))
            {
                await db.ExecuteAsync(sqlQuery, car, commandType: CommandType.Text);
                return true;
            }
        }

        public async Task<bool> DeleteCarAsync(Guid id)
        {
            const string sqlQuery = @"DELETE FROM cars WHERE id = @id;";
            using (var db = new MySqlConnection(_options.CurrentValue.CarsDbConnectionString))
            {  
                await db.ExecuteAsync(sqlQuery, new { id = id.ToString() }, commandType: CommandType.Text);
                return true;
            }            
        }

        public async Task<IEnumerable<CarEntity>> GetCarsListAsync(int pageNumber, int pageSize)
        {
            using (var db = new MySqlConnection(_options.CurrentValue.CarsDbConnectionString))
            {
                var offset = pageNumber <= 1 ? 0 : (pageNumber-1) * pageSize;
                const string sqlQuery = @"SELECT 
                    id,
                    modelname,                    
                    cartype,
                    createdon,
                    modifiedon
                FROM cars
                LIMIT @pageSize OFFSET @offset;";
                return await db.QueryAsync<CarEntity>(sqlQuery, new { pageSize, offset }, commandType: CommandType.Text);
            }
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            using (var db = new MySqlConnection(_options.CurrentValue.CarsDbConnectionString))
            {
                try
                {
                    db.Open();
                    db.Close();
                    return Task.FromResult(HealthCheckResult.Healthy());
                }
                catch
                {
                    return Task.FromResult(HealthCheckResult.Unhealthy());
                }
            }
        }
    }
}
