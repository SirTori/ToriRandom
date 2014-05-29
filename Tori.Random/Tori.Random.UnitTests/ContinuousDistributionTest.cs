using System;
using Tori.Random;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tori.Random.UnitTests
{
    [TestClass]
    public class ContinuousDistributionTest
    {
        protected double AcceptedDelta { get; set; }
        protected long TestSize { get; set; }
        public float DoublePrecision { get; set; }

        [TestInitialize]
        public void Init()
        {
            AcceptedDelta = 0.009;
            TestSize = 1000000;
            DoublePrecision = 3;
        }
        
        [TestMethod]
        [TestCategory("Distribution")]
        [TestCategory("Double")]
        [TestCategory("Uniform")]
        public void UniformD()
        {
            //Arrange
            int min = 10;
            int max = 132;
            List<Double> expectedDistribution = UniformF(min, max);

            //Act
            long[] generatedRandoms = GenerateDoubleDistribution(new UniformDistribution(min, max), min, max);

            double[] generatedDistribution = new double[generatedRandoms.Length];
            for (int i = 0; i < generatedDistribution.Length; i++)
            {
                generatedDistribution[i] = (double)(generatedRandoms[i]) / (double)(TestSize);
            }

            //Assert
            for (int i = 0; i < generatedDistribution.Length; i++)
            {
                Console.Write("generated: " + generatedDistribution[i] + "\texpected: " + expectedDistribution[i] + "\n");
                Assert.IsTrue(Math.Abs((expectedDistribution[i] - generatedDistribution[i])) <= AcceptedDelta);
            }
        }

        [TestMethod]
        [TestCategory("Distribution")]
        [TestCategory("Single")]
        [TestCategory("Uniform")]
        public void UniformS()
        {
            //Arrange
            int min = 10;
            int max = 132;
            List<Double> expectedDistribution = UniformF(min, max);

            //Act
            long[] generatedRandoms = GenerateSingleDistribution(new UniformDistribution(min, max), min, max);

            double[] generatedDistribution = new double[generatedRandoms.Length];
            for (int i = 0; i < generatedDistribution.Length; i++)
            {
                generatedDistribution[i] = (double)(generatedRandoms[i]) / (double)(TestSize);
            }

            //Assert
            for (int i = 0; i < generatedDistribution.Length; i++)
            {
                Console.Write("generated: " + generatedDistribution[i] + "\texpected: " + expectedDistribution[i] + "\n");
                Assert.IsTrue(Math.Abs((expectedDistribution[i] - generatedDistribution[i])) <= AcceptedDelta);
            }
        }
        
        [TestMethod]
        [TestCategory("Distribution")]
        [TestCategory("Double")]
        [TestCategory("Simpson")]
        public void SimpsonD()
        {
            //Arrange
            double a = 0.5;
            double b = 9.6;
            double c = 3.1;
            List<Double> expectedDistribution = SimpsonF(a, b, c);

            //Act
            long[] generatedRandoms = GenerateDoubleDistribution(new SimpsonDistribution(a, b, c), a, b);

            double[] generatedDistribution = new double[generatedRandoms.Length];
            for (int i = 0; i < generatedDistribution.Length; i++)
            {
                generatedDistribution[i] = (double)(generatedRandoms[i]) / (double)(TestSize);
            }

            //Assert
            for (int i = 0; i < generatedDistribution.Length; i++)
            {
                Console.Write("generated: " + generatedDistribution[i] + "\texpected: " + expectedDistribution[i] + "\n");
                Assert.IsTrue(Math.Abs((expectedDistribution[i] - generatedDistribution[i])) <= AcceptedDelta);
            }
        }

        [TestMethod]
        [TestCategory("Distribution")]
        [TestCategory("Single")]
        [TestCategory("Simpson")]
        public void SimpsonS()
        {
            //Arrange
            double a = 0.5;
            double b = 9.6;
            double c = 3.1;
            List<Double> expectedDistribution = SimpsonF(a, b, c);

            //Act
            long[] generatedRandoms = GenerateSingleDistribution(new SimpsonDistribution(a, b, c), a, b);

            double[] generatedDistribution = new double[generatedRandoms.Length];
            for (int i = 0; i < generatedDistribution.Length; i++)
            {
                generatedDistribution[i] = (double)(generatedRandoms[i]) / (double)(TestSize);
            }

            //Assert
            for (int i = 0; i < generatedDistribution.Length; i++)
            {
                Console.Write("generated: " + generatedDistribution[i] + "\texpected: " + expectedDistribution[i] + "\n");
                Assert.IsTrue(Math.Abs((expectedDistribution[i] - generatedDistribution[i])) <= AcceptedDelta);
            }
        }

        [TestMethod]
        [TestCategory("Distribution")]
        [TestCategory("Double")]
        [TestCategory("Exponential")]
        public void ExponentialD()
        {
            //Arrange
            double lambda = 1;
            double min = 0;
            double max = 5;

            List<Double> expectedDistribution = ExponentialF(lambda,min,max);

            //Act
            long[] generatedRandoms = GenerateDoubleDistribution(new ExponentialDistribution(lambda), min, max);

            double[] generatedDistribution = new double[generatedRandoms.Length];
            for (int i = 0; i < generatedDistribution.Length; i++)
            {
                generatedDistribution[i] = (double)(generatedRandoms[i]) / (double)(TestSize);
            }

            //Assert
            for (int i = 0; i < generatedDistribution.Length; i++)
            {
                Console.Write("generated: " + generatedDistribution[i] + "\texpected: " + expectedDistribution[i] + "\n");
                Assert.IsTrue(Math.Abs((expectedDistribution[i] - generatedDistribution[i])) <= AcceptedDelta);
            }
        }

        [TestMethod]
        [TestCategory("Distribution")]
        [TestCategory("Single")]
        [TestCategory("Exponential")]
        public void ExponentialS()
        {
            //Arrange
            double lambda = 1;
            double min = 0;
            double max = 5;

            List<Double> expectedDistribution = ExponentialF(lambda, min, max);

            //Act
            long[] generatedRandoms = GenerateSingleDistribution(new ExponentialDistribution(lambda), min, max);

            double[] generatedDistribution = new double[generatedRandoms.Length];
            for (int i = 0; i < generatedDistribution.Length; i++)
            {
                generatedDistribution[i] = (double)(generatedRandoms[i]) / (double)(TestSize);
            }

            //Assert
            for (int i = 0; i < generatedDistribution.Length; i++)
            {
                Console.Write("generated: " + generatedDistribution[i] + "\texpected: " + expectedDistribution[i] + "\n");
                Assert.IsTrue(Math.Abs((expectedDistribution[i] - generatedDistribution[i])) <= AcceptedDelta);
            }
        }

        [TestMethod]
        [TestCategory("Distribution")]
        [TestCategory("Double")]
        [TestCategory("Gaussian")]
        public void GaussianD()
        {
            //Arrange
            double mean = 0;
            double stdDev = 1;

            List<Double> expectedDistribution = GaussianF(mean, stdDev, -1.5, 1.5); 

            //Act
            long[] generatedRandoms = GenerateDoubleDistribution(new GaussianDistribution(mean, stdDev), -1.5, 1.5);

            double[] generatedDistribution = new double[generatedRandoms.Length];
            for (int i = 0; i < generatedDistribution.Length; i++)
            {
                generatedDistribution[i] = (double)(generatedRandoms[i]) / (double)(TestSize);
            }

            //Assert
            for (int i = 0; i < generatedDistribution.Length; i++)
            {
                Console.Write("generated: " + generatedDistribution[i] + "\texpected: " + expectedDistribution[i] + "\n");
                Assert.IsTrue(Math.Abs((expectedDistribution[i] - generatedDistribution[i])) <= AcceptedDelta);
            }
        }

        [TestMethod]
        [TestCategory("Distribution")]
        [TestCategory("Single")]
        [TestCategory("Gaussian")]
        public void GaussianS()
        {
            //Arrange
            double mean = 0;
            double stdDev = 1;

            List<Double> expectedDistribution = GaussianF(mean, stdDev, -1.5, 1.5);

            //Act
            long[] generatedRandoms = GenerateSingleDistribution(new GaussianDistribution(mean, stdDev), -1.5, 1.5);

            double[] generatedDistribution = new double[generatedRandoms.Length];
            for (int i = 0; i < generatedDistribution.Length; i++)
            {
                generatedDistribution[i] = (double)(generatedRandoms[i]) / (double)(TestSize);
            }

            //Assert
            for (int i = 0; i < generatedDistribution.Length; i++)
            {
                Console.Write("generated: " + generatedDistribution[i] + "\texpected: " + expectedDistribution[i] + "\n");
                Assert.IsTrue(Math.Abs((expectedDistribution[i] - generatedDistribution[i])) <= AcceptedDelta);
            }
        }

        List<Double> UniformF(int min, int max)
        {
            List<Double> calculatedDistribution = new List<Double>();
            double add = Math.Pow(10, -DoublePrecision);
            double border = max + add;
            for (double i = min; i < border; i += add)
            {
                calculatedDistribution.Add(1 / (max - min));
            }

            return calculatedDistribution;
        }

        List<Double> SimpsonF(double a, double b, double c)
        {
            List<Double> calculatedDistribution = new List<Double>();
            double nominator = (b - a) * (c - a);
            double add =  Math.Pow(10, -DoublePrecision);
            double border = c + add;
            for (double i = a; i < border; i += add)
            {
                calculatedDistribution.Add(((2 * (i - a)) / nominator) * add);
            }
            nominator = (b - a) * (b - c);
            border = b + add;
            for (double i = c + Math.Pow(10, -DoublePrecision); i < border ; i += add)
            {
                calculatedDistribution.Add(((2*(b-i)) / nominator) * add);
            }

            return calculatedDistribution;
        }

        List<Double> ExponentialF(double lambda, double min, double max)
        {
            List<Double> calculatedDistribution = new List<Double>();
            double add = Math.Pow(10, -DoublePrecision);
            double end = max + add;
            for (double x = min; x < end; x += add)
            {
                calculatedDistribution.Add(lambda * Math.Exp(-lambda * x)*add);
            }

            return calculatedDistribution;
        }

        List<Double> GaussianF(double mean, double stdDeviation, double min, double max)
        {
            List<Double> calculatedDistribution = new List<Double>();
            double add = Math.Pow(10, -DoublePrecision);
            double nominator = 2 * Math.Pow(stdDeviation, 2);
            double multiplicator = 1 / (stdDeviation*Math.Sqrt(2*Math.PI));
            double end = max + add;
            for (double x = min; x <= end; x += add)
            {
                calculatedDistribution.Add(multiplicator * Math.Exp(-Math.Pow(x-mean,2)/nominator) * add);
            }

            return calculatedDistribution;
        }

        Int64[] GenerateSingleDistribution(BaseDistribution distr, double min, double max, float precision)
        {
            Int64[] valueCount = new Int64[(int)((max - min) * (Math.Pow(10f, precision)))+1];
            RandomGenerator generator = new RandomGenerator(4);
            generator.distribution = distr;

            double currentValue;

            for (long i = 0; i < TestSize; i++)
            {
                currentValue = generator.NextSingle();
                if (currentValue <= max && currentValue >= min)
                    valueCount[(int)((currentValue - min) * (Math.Pow(10f, precision)))]++;
            }

            return valueCount;
        }

        Int64[] GenerateSingleDistribution(BaseDistribution distr, double min, double max)
        {
            return GenerateSingleDistribution(distr, min, max, DoublePrecision);
        }

        Int64[] GenerateDoubleDistribution(BaseDistribution distr, double min, double max, float precision)
        {
            Int64[] valueCount = new Int64[(int)((max - min) * (Math.Pow(10f, precision)))+1];
            RandomGenerator generator = new RandomGenerator(4);
            generator.distribution = distr;

            double currentValue;

            for (long i = 0; i < TestSize; i++)
            {
                currentValue = generator.NextDouble();
                if (currentValue <= max && currentValue >= min)
                    valueCount[(int)((currentValue - min) * (Math.Pow(10f, precision)))]++;
            }

            return valueCount;
        }

        Int64[] GenerateDoubleDistribution(BaseDistribution distr, double min, double max)
        {
            return GenerateDoubleDistribution(distr, min, max, DoublePrecision);
        }

        
    }

  
}
