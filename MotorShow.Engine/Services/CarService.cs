using MotorShow.Engine.Models;
using MotorShow.Logger;
using MotorShow.Repository.Entities;
using MotorShow.Repository.Repository;
using MotorShow.Service.Interface;
using MotorShow.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MotorShow.Engine.Services
{
    public class CarService
    {
        private readonly CarRepository _carRepository;

        public CarService(CarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        /// <summary>
        /// Gets cars details
        /// </summary>
        /// <returns></returns>
        public async Task<List<CarMake>> GetCarsShowDetail()
        {
            var result = await _carRepository.GetMotorShows();

            if (result == null) return null;

            return GetCarsByMake(result);
        }

        /// <summary>
        /// Generates lst of cars by make and model
        /// </summary>
        /// <param name="carsList"></param>
        /// <returns></returns>
        public List<CarMake> GetCarsByMake(List<CarsShow> carsList)
        {
            var flattenResult = carsList.SelectMany(s => s.Cars.Select(c => new FlattenList { Name = s.Name, Make = c.Make, Model = c.Model })).ToList();

            var makeDictionary = flattenResult.GroupBy(mk => mk.Make ?? "")
                                            .Select(mkd => new CarMake
                                            {
                                                Name = mkd.Key ?? "",
                                                Models =
                                                       mkd.GroupBy(md => md.Model ?? "")
                                                       .Select(p => new CarModels
                                                       {
                                                           Name = p.Key ?? "",
                                                           Shows = p.Select(ps => new Show { Name = ps.Name ?? "" }).Distinct(new NameEqualityComparer<Show>()).ToList()
                                                       })
                                                       .Distinct(new NameEqualityComparer<CarModels>()).ToList()
                                            }
                                            ).Distinct(new NameEqualityComparer<CarMake>()).ToList();

            return makeDictionary;
        }
    }
}
