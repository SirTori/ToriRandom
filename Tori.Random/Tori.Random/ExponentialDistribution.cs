using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tori.Random
{
    public class ExponentialDistribution : BaseDistribution
    {

        private double lambda;
        public double Lambda {
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

        public ExponentialDistribution(double lambda)
        {
            this.Lambda = lambda;
        }

        public override double CalcNextDouble(System.Random uniformRandom)
        {
            return (-Math.Log(uniformRandom.NextDouble()) / Lambda);
        }
    }
}
