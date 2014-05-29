using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tori.Random
{
    public class GeometricDistribution : BaseDistribution
    {
        private double p;
        public double P 
        {
            get { return p; }
            set
            {
                if (value <= 0 || value > 1)
                {
                    throw new ArgumentOutOfRangeException("P is a probability greater 0 and thus has to be in the range 0 < p <= 1!\n" + value + " was given!");
                }
                p = value;
            }
        }

        public GeometricDistribution(double p)
        {
            this.P = p;
        }

        /// <summary>
        /// The discrete version of an exponential distribution is the geometric distribution
        /// </summary>
        /// <param name="uniformRandom"></param>
        /// <returns></returns>
        public override int CalcNextInt(System.Random uniformRandom)
        {
            return (int)(Math.Log(uniformRandom.NextDouble())/Math.Log(1-p));
        }
    }
}
