using MotorShow.Service.Interface;
using System.Collections.Generic;

namespace MotorShow.Engine.Models
{
    public class CarMake: IName
    {
        public string Name { get; set; }
        public List<CarModels> Models { get; set; }
    }

    public class CarModels : IName
    {
        public string Name { get; set; }
        public List<Show> Shows { get; set; }
    }

    public class Show : IName
    {
        public string Name { get; set; }
    }
}
