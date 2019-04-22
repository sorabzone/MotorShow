using System;
using System.Collections.Generic;
using System.Text;

namespace MotorShow.Service.Interface
{
    public interface IName
    {
        string Name { get; set; }
    }

    public class NameEqualityComparer<T> : IEqualityComparer<T> where T : IName
    {
        public bool Equals(T x, T y)
        {
            // Two items are equal if their keys are equal.
            return x.Name == y.Name;
        }

        public int GetHashCode(T obj)
        {
            return obj.Name.GetHashCode();
        }
    }
}
