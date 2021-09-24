using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Quantlib
{
    public interface IInterpolator
    {
        double Calculate(SortedList<double, double> data, double targetX);
    }

    public class LinearInterpolator : IInterpolator
    {
        [Pure]
        public double Calculate(SortedList<double, double> data, double targetX)
        {
            if (!data.Any() || data.Keys.First() > targetX || data.Keys.Last() < targetX)
            {
                throw new ArgumentException("Bad arguments");
            }
            
            var index = data.Keys.ToList().BinarySearch(targetX);

            if (index >= 0)
            {
                return data.ElementAt(index).Value;
            }

            index = ~index;

            var (x0, y0) = data.ElementAt(index - 1);
            var (x1, y1) = data.ElementAt(index);
            
            return y0 + (targetX - x0) * (y1 - y0) / (x1 - x0);
        }
    }
}