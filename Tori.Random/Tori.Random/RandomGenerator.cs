using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tori.Random
{
    public class RandomGenerator
    {
        System.Random uniformRandom;

        protected int seed;
        public int Seed
        {
            get
            {
                return seed;
            }
            set
            {
                seed = value;
                uniformRandom = new System.Random(seed);
            }
        }

        public BaseDistribution distribution;

        public RandomGenerator()
            : this((int)System.DateTime.Now.Ticks)
        {
        }

        public RandomGenerator(int seed) 
        {
            Seed = seed;
        }

        public int Next()
        {
            return distribution.CalcNextInt(uniformRandom);
        }

        public float NextSingle()
        {
            return distribution.CalcNextSingle(uniformRandom);
        }

        public double NextDouble()
        {
            return distribution.CalcNextDouble(uniformRandom);
        }
    }
}
