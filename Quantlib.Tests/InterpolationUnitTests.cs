using System;
using System.Collections.Generic;
using Xunit;

namespace Quantlib.Tests
{
    public class InterpolationUnitTests
    {

        private readonly LinearInterpolator _interpolator = new LinearInterpolator();
        
        [Fact]
        public void test_Baseline_Interpolation()
        {
            var testData = new SortedList<double, double>
            {
                {0.0d, 9.0d},
                {1.0d, 1.0d},
                {2.0d, 7.0d},
                {4.0d, 1.0d}
            };

            Assert.Equal(4.0d, _interpolator.Calculate(testData, 1.5d), 6);
        }
        
        [Fact]
        public void test_Baseline_ExactMatch()
        {
            var testData = new SortedList<double, double>
            {
                {0.0d, 9.0d},
                {1.0d, 1.0d},
                {2.0d, 7.0d},
                {4.0d, 1.0d}
            };

            Assert.Equal(1.0d, _interpolator.Calculate(testData, 4.0d), 6);
        }
        
        [Fact]
        public void test_Extrapolation_Left()
        {
            var testData = new SortedList<double, double>
            {
                {0.0d, 9.0d},
                {1.0d, 1.0d},
                {2.0d, 7.0d},
                {4.0d, 1.0d}
            };

            Assert.Equal(17.0d, _interpolator.Calculate(testData, -1.0d), 6);
        }        
        
        [Fact]
        public void test_Extrapolation_Right()
        {
            var testData = new SortedList<double, double>
            {
                {0.0d, 9.0d},
                {1.0d, 1.0d},
                {2.0d, 7.0d},
                {4.0d, 1.0d}
            };

            Assert.Equal(-2.0d, _interpolator.Calculate(testData, 5.0d), 6);
        }        
        
        [Fact]
        public void test_Empty()
        {
            var testData = new SortedList<double, double>();

            Assert.Throws<ArgumentException>(() => _interpolator.Calculate(testData, 1.0d));
        }
        
        [Fact]
        public void test_OnePoint_ExactMatch()
        {
            var testData = new SortedList<double, double>()
            {
                {1.0d, 14.0d}
            };
            
            Assert.Equal(14.0d, _interpolator.Calculate(testData, 1.0d), 6);
        }

        [Fact]
        public void test_OnePoint_DontMatch()
        {
            var testData = new SortedList<double, double>()
            {
                {1.0d, 14.0d}
            };
            
            Assert.Throws<ArgumentException>(() => _interpolator.Calculate(testData, 3.0d));
        }
    }
}