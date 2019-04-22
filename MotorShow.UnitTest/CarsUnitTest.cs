using MotorShow.Engine.Models;
using MotorShow.Engine.Services;
using MotorShow.Repository.Entities;
using MotorShow.Repository.Repository;
using MotorShow.Repository.ServiceHelper;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MotorShower.UnitTest
{
    public class CarsUnitTest
    {
        private CarRepository _carRepository;
        private CarService _carService;

        public CarsUnitTest()
        {
            _carRepository = new CarRepository(new ServiceApiClient());
            _carService = new CarService(_carRepository);
        }

        /// <summary>
        /// When external API call fail or failed response
        /// </summary>
        /// <returns></returns>
        [TestCase]
        public void When_Exception_Handled()
        {
            Assert.DoesNotThrowAsync(async () => await _carRepository.GetMotorShows());
        }

        /// <summary>
        /// Testing random data
        /// </summary>
        /// <returns></returns>
        [TestCase]
        public void When_Test_Random()
        {
            var input = new List<CarsShow>()
            {
                new CarsShow() { Name = "Melbourne Motor Show", Cars = new List<Car>() {
                new Car() { Make = "Edison Motors", Model = "Mark 4" } ,
                new Car() { Make = "George Motors", Model = ""}}
                               } ,
                new CarsShow() { Name = "Cartopia", Cars = new List<Car>() {
                new Car() { Make = "Edison Motors", Model = "Delta 4" } ,
                new Car() { Make = "Julio Mechannica", Model = "Delta 5"}}
                               }
            };

            var expectedResult = new List<CarMake>()
            {
                new CarMake()
                {
                     Name = "Edison Motors",
                     Models = new List<CarModels>()
                     {
                          new CarModels()
                          {
                               Name ="Mark 4",
                                Shows = new List<Show>()
                                {
                                     new Show()
                                     {
                                          Name="Melbourne Motor Show"
                                     }
                                }
                          },
                          new CarModels()
                          {
                               Name ="Delta 4",
                                Shows = new List<Show>()
                                {
                                     new Show()
                                     {
                                          Name="Cartopia"
                                     }
                                }
                          }
                     }
                },
                new CarMake()
                {
                     Name = "George Motors",
                     Models = new List<CarModels>()
                     {
                          new CarModels()
                          {
                               Name ="",
                                Shows = new List<Show>()
                                {
                                     new Show()
                                     {
                                          Name="Melbourne Motor Show"
                                     }
                                }
                          }
                     }
                },
                new CarMake()
                {
                     Name = "Julio Mechannica",
                     Models = new List<CarModels>()
                     {
                          new CarModels()
                          {
                               Name ="Delta 5",
                                Shows = new List<Show>()
                                {
                                     new Show()
                                     {
                                          Name="Cartopia"
                                     }
                                }
                          }
                     }
                }
            };

            var actualResult = _carService.GetCarsByMake(input);

            Assert.AreEqual(expectedResult.Count(), actualResult.Count());
            Assert.AreEqual(expectedResult.Select(s => s.Models).Count(), actualResult.Select(s => s.Models).Count());
            Assert.AreEqual(expectedResult.Select(s => s.Models.Select(d => d.Shows)).Count(), actualResult.Select(s => s.Models.Select(d => d.Shows)).Count());
        }

        /// <summary>
        /// Testing Empty Model
        /// </summary>
        /// <returns></returns>
        [TestCase]
        public void When_Test_Model_Empty()
        {
            var input = new List<CarsShow>()
            {
                new CarsShow() { Name = "Melbourne Motor Show", Cars = new List<Car>() {
                new Car() { Make = "Edison Motors", Model = "Mark 4" } ,
                new Car() { Make = "George Motors", Model = ""}}
                               }
            };

            var actualResult = _carService.GetCarsByMake(input);

            Assert.AreEqual(2, actualResult.Count());
            Assert.AreEqual(2, actualResult.Select(s => s.Models).Count());
            Assert.AreEqual(2, actualResult.Select(s => s.Models.Select(d => d.Shows)).Count());
        }

        /// <summary>
        /// Testing Empty Make
        /// </summary>
        /// <returns></returns>
        [TestCase]
        public void When_Test_Make_Empty()
        {
            var input = new List<CarsShow>()
            {
                new CarsShow() { Name = "Melbourne Motor Show", Cars = new List<Car>() {
                new Car() { Make = "Edison Motors", Model = "Mark 4" } ,
                new Car() { Make = "", Model = "Delta 5"}}
                               }
            };

            var actualResult = _carService.GetCarsByMake(input);

            Assert.AreEqual(2, actualResult.Count());
            Assert.AreEqual(2, actualResult.Select(s => s.Models).Count());
            Assert.AreEqual(2, actualResult.Select(s => s.Models.Select(d => d.Shows)).Count());
        }

        /// <summary>
        /// Testing Empty Show
        /// </summary>
        /// <returns></returns>
        [TestCase]
        public void When_Test_Show_Empty()
        {
            var input = new List<CarsShow>()
            {
                new CarsShow() { Name = "", Cars = new List<Car>() {
                new Car() { Make = "Edison Motors", Model = "Mark 4" } ,
                new Car() { Make = "George", Model = "Delta 5"}}
                               }
            };

            var actualResult = _carService.GetCarsByMake(input);

            Assert.AreEqual(2, actualResult.Count());
            Assert.AreEqual(2, actualResult.Select(s => s.Models).Count());
            Assert.AreEqual(2, actualResult.Select(s => s.Models.Select(d => d.Shows)).Count());
        }
    }
}
