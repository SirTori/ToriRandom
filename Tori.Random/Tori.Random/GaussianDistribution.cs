using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tori.Random
{
    public class GaussianDistribution : BaseDistribution
    {
        public double Mean
        {
            get;
            set;
        }
        private double standardDeviation;
        public double StandardDeviation
        {
            get { return standardDeviation; }
            set 
            { 
                standardDeviation = value;
                variance = value * value;
            }
        }
        private double variance;
        public double Variance
        {
            get { return variance; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("Variance is the standard deviation to the power of 2 and thus has to be greater 0!\n" + value + " was given!");
                }
                else
                {
                    variance = value;
                    standardDeviation = Math.Sqrt(value);
                }
            }
        }
        private double buffer = double.NaN;

        public GaussianDistribution(double mean, double standardDeviation)
        {
            this.Mean = mean;
            this.StandardDeviation = standardDeviation;
        }
        
        /// <summary>
        /// Generates a gaussian distributed single random number.
        /// Current implementation uses the Polar-Method to transform uniform random numbers into gaussian ones <see cref="https://en.wikipedia.org/wiki/Marsaglia_polar_method"/>
        /// </summary>
        /// <param name="uniformRandom"></param>
        /// <returns></returns>
        public override double CalcNextDouble(System.Random uniformRandom)
        {
            if (double.IsNaN(buffer))
            {
                double u1 = 0f;
                double u2 = 0f;

                double q = 0f;
                do
                {
                    u1 = (uniformRandom.NextDouble() * 2f) - 1f;
                    u2 = (uniformRandom.NextDouble() * 2f) - 1f;
                    q = (u1 * u1) + (u2 * u2);
                } while (q == 0f || q > 1f);

                double p = Math.Sqrt(-2 * Math.Log(q) / q);
                buffer = standardDeviation * (u1 * p) + Mean;

                return standardDeviation * (u2 * p) + Mean;
            }
            else
            {
                double tmp = buffer;
                buffer = double.NaN;
                return tmp;
            }
        }
    }
}
