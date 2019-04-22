using MotorShow.Repository.Constants;
using MotorShow.Repository.Entities;
using MotorShow.Repository.Extensions;
using MotorShow.Repository.ServiceHelper;
using MotorShow.Logger;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MotorShow.Repository.Repository
{
    public class CarRepository
    {
        private readonly IServiceApiClient _serviceApiClient;

        public CarRepository(IServiceApiClient serviceApiClient)
        {
            _serviceApiClient = serviceApiClient;
        }

        /// <summary>
        /// Calls external API to fetch cars list
        /// </summary>
        /// <returns></returns>
        public async Task<List<CarsShow>> GetMotorShows()
        {
            try
            {
                var client = _serviceApiClient.GetHttpClient(ExternalAPI.AUSENERGY);
                var route = "api/v1/cars";
                var result = await client.SendSimpleAsync<List<CarsShow>>(route);
                return result;
            }
            catch (Exception ex)
            {
                CommonLogger.LogError(ex);
                return null;
            }
        }
    }
}
