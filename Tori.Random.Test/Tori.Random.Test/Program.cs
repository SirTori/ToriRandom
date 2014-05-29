#define MICRO_LOGGING_
#define DISTRIBUTION
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tori.Random;
using System.IO;

namespace Tori.Random.Test
{
    class Program
    {
        static readonly int SIZE = 10000000;
        static readonly int LOG_PRECISION = 6;
        static readonly char DELIMITER = ';';

        static List<double> means = new List<double>();
        static List<double> standardDeviations = new List<double>();
        static List<string> titles = new List<string>();
        static List<double> times = new List<double>();
        static double tmp;
        static double tmp2;

        static int[] counts;

        static void Main(string[] args)
        {

            TextWriter writer;

            Test(new UniformDistribution(), 0f, 1f, "my_uniform_single", "my_uniform_single_0_1");
            Test(new UniformDistribution(5,500), 5, 500, "my_uniform_int", "my_uniform_int_5_500");
            Test(new GaussianDistribution(0f, 1f), -4f, 4f, "gaussian_single", "gaussian_single_m0_std1");
            Test(new GaussianDistribution(500f, 20f), 400, 600, "gaussian_int", "gaussian_int_m500_std20");
            Test(new BinomialDistribution(1, 0.5f), 0, 1, "bernoulli_int", "bernoulli_int_n1_p0-5");
            Test(new BinomialDistribution(14, 0.1f), 0, 14, "binomial_int", "binomial_int_n20_p0-5");
            Test(new SimpsonDistribution(2f, 15f, 4f), 0f, 16f, "simpson_single", "simpson_single_a2_b10_c3");
            Test(new SimpsonDistribution(30f, 100f, 70f), 20, 120, "simpson_int", "simpson_int_a30_b100_c70");
            Test(new ExponentialDistribution(0.5f), 0f, 8f, "exponential_single", "exp rate = 2f");
            Test(new ExponentialDistribution(0.5f), 0, 100, "exponential_int", "exp rate = 0.5f");
            Test(new PoissonDistribution(3f), 0f, 8f, "poisson_single", "poisson_single_e3");
            Test(new PoissonDistribution(6f), 0, 20, "poisson_int", "poisson_int_e6");

            TestSystemRandom();


            int[] pows = new int[times.Count];
            for (int i = 0; i < times.Count; i++)
            {
                times[i] = times[i] / SIZE;
                pows[i] = -3;
                while (times[i] != 0f && times[i] < 1f)
                {
                    pows[i]--;
                    times[i] *= 10f;
                }
            }

            writer = new StreamWriter("overview.txt");

            writer.WriteLine("size: " + SIZE);
            writer.WriteLine(String.Format("{0,-20}{4}{1,20}{4}{2,20}{4}{3,20}","Title", "Mean", "Sdt Dev", "time per draw (s)", DELIMITER));
            for (int i = 0; i < means.Count; i++)
            {
                writer.WriteLine(String.Format("{0,-20}{5}{1,20}{5}{2,20}{5}{3,13:.10} * 10^{4}", titles[i], means[i].ToString(), standardDeviations[i].ToString(), times[i].ToString(), pows[i].ToString(), DELIMITER));
            }
            writer.Close();

            Console.ReadKey();
        }

        private static void Count(float currentF, float min, float max)
        {
            if (currentF <= max && currentF >= min)
                counts[(int)((currentF - min) * (10f * LOG_PRECISION))]++;
        }

        private static void Count(int currentI, int min, int max)
        {
            if (currentI <= max && currentI >= min)
                counts[currentI-min]++;
        }

        public static void updateTmpStdDev(float rnd)
        {
            tmp += (rnd * rnd);
            tmp2 += rnd;
        }

        public static double StdDev(int n)
        {
            return Math.Sqrt((1f/(n - 1f)) * (tmp - ((1f/n) *(tmp2 * tmp2))));
        }

        public static void Test(Distribution distr, float min, float max, string testName, string testDescription) 
        {
            Console.Write("\nStarted " + testName + ": ");
            RandomGenerator generator = new RandomGenerator(4);
            generator.distribution = distr;
            float currentValue;
            TextWriter writer;
            float percent = 0;
            DateTime start = DateTime.Now;
#if MICRO_LOGGING
            writer = new StreamWriter(testName + "_micro.txt");
#endif
            tmp = 0f;
            tmp2 = 0f;
            
#if DISTRIBUTION
            counts = new int[(int)((max - min) * (10f * LOG_PRECISION) + 1)];
#endif
            for (int i = 0; i < SIZE; i++)
            {
                if ((float)i / (float)SIZE > percent)
                {
                    Console.Write('.');
                    percent += 0.1f;
                }
                currentValue = generator.NextSingle();
                updateTmpStdDev(currentValue);
#if MICRO_LOGGING
                writer.WriteLine(currentF);      
#endif
#if DISTRIBUTION
                Count(currentValue, min, max);
#endif
            }
            times.Add(DateTime.Now.Subtract(start).TotalMilliseconds);
            titles.Add(testDescription);
            means.Add(tmp2 / SIZE);
            standardDeviations.Add(StdDev(SIZE));
#if MICRO_LOGGING
            writer.Close();     
#endif
#if DISTRIBUTION
            writer = new StreamWriter(testName + "_counts.txt");
            double current = min;
            double incr = 1f/(10*LOG_PRECISION);
            for (int i = 0; i < counts.Length; i++)
            {
                writer.Write(String.Format("{0}{1}", Math.Round(current, LOG_PRECISION), DELIMITER));
                current += incr;
            }
            writer.Write("\n");
            for (int i = 0; i < counts.Length; i++)
            {
                writer.Write(String.Format("{0,5:}{1}", counts[i].ToString(), DELIMITER));
            }
            writer.Close();
#endif
            Console.Write("finished "+testName+"\n");
        }

        public static void Test(Distribution distr, int min, int max, string testName, string testDescription)
        {
            Console.Write("\nStarted " + testName + ": ");
            RandomGenerator generator = new RandomGenerator(4);
            generator.distribution = distr;
            int currentValue;
            TextWriter writer;
            float percent = 0;
            DateTime start = DateTime.Now;
#if MICRO_LOGGING
            writer = new StreamWriter(testName + "_micro.txt");
#endif
            tmp = 0f;
            tmp2 = 0f;

#if DISTRIBUTION
            counts = new int[(max - min) + 1];
#endif
            for (int i = 0; i < SIZE; i++)
            {
                if ((float)i / (float)SIZE > percent)
                {
                    Console.Write('.');
                    percent += 0.1f;
                }
                currentValue = generator.Next();
                updateTmpStdDev(currentValue);
#if MICRO_LOGGING
                writer.WriteLine(currentF);      
#endif
#if DISTRIBUTION
                Count(currentValue, min, max);
#endif
            }
            times.Add(DateTime.Now.Subtract(start).TotalMilliseconds);
            titles.Add(testDescription);
            means.Add(tmp2 / SIZE);
            standardDeviations.Add(StdDev(SIZE));
#if MICRO_LOGGING
            writer.Close();     
#endif
#if DISTRIBUTION
            writer = new StreamWriter(testName + "_counts.txt");
            int current = min;
            for (int i = 0; i < counts.Length; i++)
            {
                writer.Write(String.Format("{0,5:0.000}{1}", current, DELIMITER));
                current++;
            }
            writer.Write("\n");
            for (int i = 0; i < counts.Length; i++)
            {
                writer.Write(String.Format("{0,5:}{1}", counts[i].ToString(), DELIMITER));
            }
            writer.Close();
#endif
            Console.Write("finished " + testName + "\n");
        }

        private static void TestSystemRandom()
        {
            System.Random rnd = new System.Random();
#if MICRO_LOGGING
            writer = new StreamWriter("classic_single.txt");
#endif
            tmp = 0f;
            tmp2 = 0f;
            float currentF;
            DateTime start = DateTime.Now;
            for (int i = 0; i < SIZE; i++)
            {
                currentF = (float)rnd.NextDouble();
                updateTmpStdDev(currentF);
#if MICRO_LOGGING
                writer.WriteLine(currentF);
#endif
            }
            times.Add(DateTime.Now.Subtract(start).TotalMilliseconds);
            titles.Add("classic double");
            means.Add(tmp2 / SIZE);
            standardDeviations.Add(StdDev(SIZE));
#if MICRO_LOGGING
            writer.Close();
#endif
            Console.Write("\nfinished classic single uniform\n");


            rnd = new System.Random();
#if MICRO_LOGGING
            writer = new StreamWriter("classic_5_500.txt");
#endif
            int currentI;
            tmp = 0f;
            tmp2 = 0f;
            start = DateTime.Now;
            for (int i = 0; i < SIZE; i++)
            {
                currentI = rnd.Next(5, 500);
                updateTmpStdDev(currentI);
#if MICRO_LOGGING
                writer.WriteLine(currentI);
#endif
            }
            times.Add(DateTime.Now.Subtract(start).TotalMilliseconds);
            titles.Add("classic 5-500");
            means.Add(tmp2 / SIZE);
            standardDeviations.Add(StdDev(SIZE));
#if MICRO_LOGGING
            writer.Close();
#endif
            Console.Write("\nfinished classic int uniform\n");
        }
    }
}
