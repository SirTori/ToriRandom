using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tori.Random;
using System.Collections.Generic;

namespace Tori.Random.UnitTests
{
    [TestClass]
    public class DiscreteDistributionTest
    {
        protected double AcceptedDelta { get; set; }
        protected long TestSize { get; set; }

        [TestInitialize]
        public void Init()
        {
            AcceptedDelta = 0.009;
            TestSize = 1000000;
        }

        [TestMethod]
        [TestCategory("Distribution")]
        [TestCategory("Integer")]
        [TestCategory("Uniform")]
        public void UniformI()
        {
            //Arrange
            int min = 10;
            int max = 132;
            List<Double> expectedDistribution = UniformF(min, max);

            //Act
            long[] generatedRandoms = GenerateDistribution(new UniformDistribution(min, max), min, max);

            double[] generatedDistribution = new double[generatedRandoms.Length];
            for (int i = 0; i < generatedDistribution.Length; i++)
            {
                generatedDistribution[i] = (double)(generatedRandoms[i]) / (double)(TestSize);
            }

            //Assert
            for (int i = 0; i < generatedDistribution.Length; i++)
            {
                Console.Write("generated: " + generatedDistribution[i] + "\texpected: " + expectedDistribution[i]);
                Assert.IsTrue(Math.Abs((expectedDistribution[i] - generatedDistribution[i])) <= AcceptedDelta);
            }
        }

        [TestMethod]
        [TestCategory("Distribution")]
        [TestCategory("Integer")]
        [TestCategory("Poisson")]
        public void PoissonI()
        {
            //Arrange
            int lambda = 10;
            int min = 0;
            int max = 20;
            List<Double> expectedDistribution = PoissonF(lambda, min, max);

            //Act
            long[] generatedRandoms = GenerateDistribution(new PoissonDistribution(lambda), min, max);

            double[] generatedDistribution = new double[generatedRandoms.Length];
            for (int i = 0; i < generatedDistribution.Length; i++)
            {
                generatedDistribution[i] = (double)(generatedRandoms[i]) / (double)(TestSize);
            }

            //Assert
            for (int i = 0; i < generatedDistribution.Length; i++)
            {
                Console.Write("generated: " + generatedDistribution[i] + "\texpected: " + expectedDistribution[i]);
                Assert.IsTrue(Math.Abs((expectedDistribution[i] - generatedDistribution[i])) <= AcceptedDelta);
            }
        }

        [TestMethod]
        [TestCategory("Distribution")]
        [TestCategory("Integer")]
        [TestCategory("Binomial")]
        public void BinomialI()
        {
            //Arrange
            int n = 30;
            double p = 0.1;
            List<Double> expectedDistribution = BinomialF(n, p);

            //Act
            long[] generatedRandoms = GenerateDistribution(new BinomialDistribution(n, p), 0, 30);

            double[] generatedDistribution = new double[generatedRandoms.Length];
            for (int i = 0; i < generatedDistribution.Length; i++)
            {
                generatedDistribution[i] = (double)(generatedRandoms[i]) / (double)(TestSize);
            }

            //Assert
            for (int i = 0; i < generatedDistribution.Length; i++)
            {
                Console.Write("generated: " + generatedDistribution[i] + "\texpected: " + expectedDistribution[i]);
                Assert.IsTrue(Math.Abs((expectedDistribution[i] - generatedDistribution[i])) <= AcceptedDelta);
            }
        }

        [TestMethod]
        [TestCategory("Distribution")]
        [TestCategory("Integer")]
        [TestCategory("Geometric")]
        public void GeometricI()
        {
            //Arrange
            double p = 0.8;
            int min = 0;
            int max = 5;
            List<Double> expectedDistribution = GeometricF(p, min, max);

            //Act
            long[] generatedRandoms = GenerateDistribution(new GeometricDistribution(p), min, max);

            double[] generatedDistribution = new double[generatedRandoms.Length];
            for (int i = 0; i < generatedDistribution.Length; i++)
            {
                generatedDistribution[i] = (double)(generatedRandoms[i]) / (double)(TestSize);
            }

            //Assert
            for (int i = 0; i < generatedDistribution.Length; i++)
            {
                Console.Write("generated: " + generatedDistribution[i] + "\texpected: " + expectedDistribution[i]);
                Assert.IsTrue(Math.Abs((expectedDistribution[i] - generatedDistribution[i])) <= AcceptedDelta);
            }
        }

        List<Double> UniformF(int min, int max)
        {
            List<Double> calculatedDistribution = new List<Double>();
            for (double i = min; i <= max; i ++)
            {
                calculatedDistribution.Add(1 / (max - min));
            }

            return calculatedDistribution;
        }

        List<Double> PoissonF(long lambda, int min, int max)
        {
            List<Double> p = new List<Double>();
            p.Add(Math.Exp(-lambda));
            for (int k = min+1; k <= max; k++)
            {
                p.Add(((double)lambda / (double)k) * p[k - 1]);
            }
            return p;
        }

        List<Double> BinomialF(long n, double p)
        {
            List<Double> calculatedDistribution = new List<Double>();
            for (long k = 0; k <= n; k++)
            {
                calculatedDistribution.Add(BinomialCoefficient(n, k) * Math.Pow(p, k) * Math.Pow(1 - p, n - k));
            }
            return calculatedDistribution;
        }

        List<Double> GeometricF(double p, int min, int max)
        {
            List<Double> calculatedDistribution = new List<Double>();
            for (long k = min; k <= max; k++)
            {
                calculatedDistribution.Add(Math.Pow(1-p,k)*p);
            }
            return calculatedDistribution;
        }

        long BinomialCoefficient(long n, long k)
        {
            if ((k < 0) || (k > n)) return 0;
            k = (k > n / 2) ? n - k : k;
            return FallingPower(n, k) / Factorial(k);
        }

        long Factorial(long n)
        {
            if (n == 0) return 1;
            long m = n;
            while (n-- > 2) m *= n;
            return m;
        }

        long FallingPower(long n, long p)
        {
            long t = 1;
            for (long i = 0; i < p; i++) t *= n--;
            return t;
        }

        Int64[] GenerateDistribution(BaseDistribution distr, int min, int max)
        {
            Int64[] valueCount = new Int64[(max - min) + 1];
            RandomGenerator generator = new RandomGenerator(4);
            generator.distribution = distr;

            int currentValue;

            for (long i = 0; i < TestSize; i++)
            {
                currentValue = generator.Next();
                if (currentValue <= max && currentValue >= min)
                    valueCount[currentValue - min]++;
            }

            return valueCount;
        }

    }
}
