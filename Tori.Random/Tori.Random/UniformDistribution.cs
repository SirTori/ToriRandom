using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tori.Random
{
    public class UniformDistribution : BaseDistribution
    {
        private double min;
        public double Min
        {
            get { return min; }
            set 
            {
                if (value >= Max)
                {
                    throw new ArgumentException("Min value has to be lower than Max");
                }
                min = value;
                minI = (int)value;
            }
        }

        private double max;
        public double Max
        {
            get { return max; }
            set
            {
                if (value <= Min)
                {
                    throw new ArgumentException("Min value has to be lower than Max");
                }
                max = value;
                maxI = (int)value;
            }
        }

        private int minI;
        private int maxI;

        public UniformDistribution() : this(0f, 1f) { }

        public UniformDistribution(double min, double max)
        {
            Set(min, max);
        }

        public UniformDistribution(int min, int max)
            : this((double)min, (double)max) { }

        public void Set(double min, double max)
        {
            this.minI = (int)min;
            this.min = min;
            this.Max = max;
        }

        public void Set(int min, int max)
        {
            this.minI = min;
            this.min = min;
            this.Max = max;
        }

        public override double CalcNextDouble(System.Random uniformRandom)
        {
            return uniformRandom.NextDouble() * (max - min) + min;
        }

        public override int CalcNextInt(System.Random uniformRandom)
        {
            return uniformRandom.Next(minI, maxI);
        }

    }
}
