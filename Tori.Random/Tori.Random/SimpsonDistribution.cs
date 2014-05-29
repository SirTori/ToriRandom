using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tori.Random
{
    public class SimpsonDistribution : BaseDistribution
    {
        protected double a;
        public double A {
            get { return a; }
            private set
            { 
                a = value;
                UpdateF();
            }
        }

        protected double b;
        public double B
        {
            get { return b; }
            private set {
                if (value <= a)
                {
                    throw new ArgumentOutOfRangeException("B has to be greater than A (a < b)");
                }
                else
                {
                    b = value;
                    UpdateF();
                }
            }
        }

        protected double c;
        public double C
        {
            get { return c; }
            private set
            {
                if (value < a || b < value)
                {
                    throw new ArgumentOutOfRangeException("C has to be greater or equal to A and smaller or equal to B (a <= c <= b)");
                }
                else
                {
                    c = value;
                    UpdateF();
                }
            }
        }

        protected double f;

        public SimpsonDistribution(double a, double b, double c)
        {
            SetParameters(a, b, c);
        }

        public void SetParameters(double a, double b, double c)
        {
            A = a;
            B = b;
            C = c;
        }

        /// <summary>
        /// For implementation details see: https://en.wikipedia.org/wiki/Triangular_distribution
        /// </summary>
        /// <param name="uniformRandom"></param>
        /// <returns></returns>
        public override double CalcNextDouble(System.Random uniformRandom)
        {
            double rand = uniformRandom.NextDouble();
            double result = 0f;
            if (rand < f)
            {
                result = a + Math.Sqrt(rand * (b - a) * (c - a));
            }
            else if (rand > f)
            {
                result = b - Math.Sqrt((1 - rand) * (b - a) * (b - c));
            }
            else
            {
                result = c;
            }

            return result;
        }

        protected void UpdateF()
        {
            if ((b - a) == 0)
            {
                return;
            }
            else
            {
                f = (c - a) / (b - a);
            }
        }
    }
}
