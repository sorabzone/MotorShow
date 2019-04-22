using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MotorShow.Engine.Models;
using MotorShow.Engine.Services;
using MotorShow.ResponseHelpers;
using MotorShow.Logger;

namespace MotorShow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly CarService _carService;

        public CarsController(CarService carService)
        {
            _carService = carService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _carService.GetCarsShowDetail();

                return Ok(new CustomResponse<List<CarMake>>
                {
                    Code = ResponseHelpers.StatusCode.Success,
                    Data = result
                });
            }
            catch (Exception ex)
            {
                CommonLogger.LogError(ex);
            }

            return Ok(CustomExceptions.GenerateExceptionForApp("Error getting cars"));
        }
    }
}
