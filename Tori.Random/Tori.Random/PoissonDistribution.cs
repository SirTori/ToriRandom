using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tori.Random
{

    public class PoissonDistribution : BaseDistribution
    {
        private double lambda;
        public double Lambda
        { 
            get { return lambda; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException("Lambda has to be greater than 0!\n" + value + " was given!");
                }
                else
                {
                    lambda = value;
                }
            }
        }

        public PoissonDistribution(double lambda)
        {
            this.Lambda = lambda;
        }

        /// <summary>
        /// For implementation details see http://luc.devroye.org/chapter_ten.pdf p. 505
        /// </summary>
        /// <param name="uniformRandom"></param>
        /// <returns></returns>
        public override int CalcNextInt(System.Random uniformRandom)
        {
            int result = 0;
            double prod = 1;
            double e = Math.Exp(-1 * lambda);
            while (true)
            {
                prod *= uniformRandom.NextDouble();
                if (prod > e)
                {
                    result++;
                }
                else
                {
                    return result;
                }
            }
        }
    }
}
