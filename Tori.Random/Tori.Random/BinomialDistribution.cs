using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tori.Random
{
    public class BinomialDistribution : BaseDistribution
    {
       
        protected long n;
        /// <summary>
        /// Number of trials
        /// </summary>
        public long N { 
            get { return n; }
            set
            {
                if (value < 1)
                {
                    throw new ArgumentOutOfRangeException("N has to be greater than 0!\n" + value + " was given!");
                }
                else
                {
                    n = value;
                }
            }
        }

        protected double p;
        /// <summary>
        /// Propability of success in each trial
        /// </summary>
        public double P
        {
            get { return p; }
            set
            {
                if (value < 0 || value > 1)
                {
                    throw new ArgumentOutOfRangeException("P has to be between 0 and 1 (inclusive)");
                }
                else
                {
                    p = value;
                }
            }
        }

        public BinomialDistribution(double p)
        {
            N = 1;
            P = p;
        }

        public BinomialDistribution(int n, double p)
        {
            N = n;
            P = p;
        }

        
        /// <summary>
        /// For implementation details see https://de.wikipedia.org/wiki/Binomialverteilung
        /// </summary>
        /// <param name="uniformRandom"></param>
        /// <returns></returns>
        public override int CalcNextInt(System.Random uniformRandom)
        {
            int sum = 0;
            for (int i = 0; i < n; i++)
            {
                sum += Bernoulli(uniformRandom);
            }
            return sum;
        }

        protected int Bernoulli(System.Random uniformRandom)
        {
            return (int)(1f + (p - uniformRandom.NextDouble()));
        }
    }
}
