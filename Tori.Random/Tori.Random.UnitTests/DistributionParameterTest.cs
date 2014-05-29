using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tori.Random.UnitTests
{
    [TestClass]
    public class DistributionParameterTest
    {
        [TestMethod]
        [TestCategory("Parameter")]
        [TestCategory("Positive")]
        [TestCategory("Integer")]
        [TestCategory("Uniform")]
        public void UniformIPositive()
        {
            //Arrange
            UniformDistribution dist = new UniformDistribution();

            //Act
            dist.Set(-100223, -100222);
            dist.Set(-100221, 102222);
            dist.Set(100221, 102222);
            dist.Set(0, 1);

            //Assert
            Assert.IsTrue(true);
        }

        [TestMethod]
        [TestCategory("Parameter")]
        [TestCategory("Positive")]
        [TestCategory("Double")]
        [TestCategory("Uniform")]
        public void UniformDPositive()
        {
            //Arrange
            UniformDistribution dist = new UniformDistribution();

            //Act
            dist.Set(-100222.001, -100222.0);
            dist.Set(-100221.01, 102222.1);
            dist.Set(100221.9999, 102222.0);
            dist.Set(0.0, 0.000000001);

            //Assert
            Assert.IsTrue(true);
        }

        [TestMethod]
        [TestCategory("Parameter")]
        [TestCategory("Positive")]
        [TestCategory("Single")]
        [TestCategory("Uniform")]
        public void UniformSPositive()
        {
            //Arrange
            UniformDistribution dist = new UniformDistribution();

            //Act
            dist.Set(-100222.01f, -100222.0f);
            dist.Set(-100221.01f, 102222.1f);
            dist.Set(100221.9999f, 102222.0f);
            dist.Set(0.0f, 0.000000001f);

            //Assert
            Assert.IsTrue(true);
        }

        [TestMethod]
        [TestCategory("Parameter")]
        [TestCategory("Negative")]
        [TestCategory("Integer")]
        [TestCategory("Uniform")]
        public void UniformINegative()
        {
            //Arrange
            UniformDistribution dist = new UniformDistribution();
            int errors = 0;
            //Act
            try
            {
                dist.Set(-100221, -100222);
            }
            catch (ArgumentException)
            {
                errors++;
            }
            try
            {
               dist.Set(100221, -102222);
            }
            catch (ArgumentException)
            {
                errors++;
            }
            try
            {
                dist.Set(100223, 100222);
            }
            catch (ArgumentException)
            {
                errors++;
            }
            try
            {
                dist.Set(1, 1);
            }
            catch (ArgumentException)
            {
                errors++;
            }

            //Assert
            Assert.IsTrue(errors == 4);
        }

        [TestMethod]
        [TestCategory("Parameter")]
        [TestCategory("Negative")]
        [TestCategory("Double")]
        [TestCategory("Uniform")]
        public void UniformDNegative()
        {
            //Arrange
            UniformDistribution dist = new UniformDistribution();
            int errors = 0;

            //Act
            try
            {
                dist.Set(-100222.0, -100222.001);
            }
            catch (ArgumentException)
            {
                errors++;
            }
            try
            {
                dist.Set(100221.01, -102222.1);
            }
            catch (ArgumentException)
            {
                errors++;
            }
            try
            {
                dist.Set(102222.0, 100221.9999);
            }
            catch (ArgumentException)
            {
                errors++;
            }
            try
            {
                dist.Set(0.000000001, 0.0);
            }
            catch (ArgumentException)
            {
                errors++;
            }

            //Assert
            Assert.IsTrue(errors == 4);
        }

        [TestMethod]
        [TestCategory("Parameter")]
        [TestCategory("Negative")]
        [TestCategory("Single")]
        [TestCategory("Uniform")]
        public void UniformSNegative()
        {
            //Arrange
            UniformDistribution dist = new UniformDistribution();
            int errors = 0;

            //Act
            try
            {
                dist.Set(-100222f, -100222.001f);
            }
            catch (ArgumentException)
            {
                errors++;
            }
            try
            {
                dist.Set(100221.01f, -102222.1f);
            }
            catch (ArgumentException)
            {
                errors++;
            }
            try
            {
                dist.Set(102222f, 100221.9999f);
            }
            catch (ArgumentException)
            {
                errors++;
            }
            try
            {
                dist.Set(0.000000001f, 0f);
            }
            catch (ArgumentException)
            {
                errors++;
            }

            //Assert
            Assert.IsTrue(errors == 4);
        }
    }
}
