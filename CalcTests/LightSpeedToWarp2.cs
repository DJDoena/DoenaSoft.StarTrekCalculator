using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DoenaSoft.StarTrekCalculator.Tests
{
    [TestClass]
    public sealed class LightSpeedToWarp2
    {
        [TestMethod]
        public void MinLightSpeed()
        {
            var warp = Warp.LightSpeedToWarp(Warp.MinLightSpeed);

            Assert.AreEqual(Warp.MinWarpFactor, warp);
        }

        [TestMethod]
        public void MaxLightSpeed()
        {
            var warp = Warp.LightSpeedToWarp(Warp.MaxLightSpeed);

            Assert.AreEqual(Warp.MaxWarpFactor, warp);
        }

        [TestMethod]
        [ExpectedException(typeof(CalculationException))]
        public void MinLightSpeedFail()
        {
            Warp.LightSpeedToWarp(0.9);
        }

        [TestMethod]
        [ExpectedException(typeof(CalculationException))]
        public void MaxLightSpeedFail()
        {
            Warp.LightSpeedToWarp(Warp.MaxLightSpeed + 1);
        }

        [TestMethod]
        public void Warp1()
        {
            var warp = Warp.LightSpeedToWarp(1);

            Assert.AreEqual(1, warp);
        }

        [TestMethod]
        public void Warp2()
        {
            var warp = Warp.LightSpeedToWarp(10.079369);

            Assert.AreEqual(2, warp);
        }

        [TestMethod]
        public void Warp3()
        {
            var warp = Warp.LightSpeedToWarp(38.940744);

            Assert.AreEqual(3, warp);
        }

        [TestMethod]
        public void Warp4()
        {
            var warp = Warp.LightSpeedToWarp(101.593719);

            Assert.AreEqual(4, warp);
        }

        [TestMethod]
        public void Warp5()
        {
            var warp = Warp.LightSpeedToWarp(213.747391);

            Assert.AreEqual(5, warp);
        }

        [TestMethod]
        public void Warp6()
        {
            var warp = Warp.LightSpeedToWarp(392.501078);

            Assert.AreEqual(6, warp);
        }

        [TestMethod]
        public void Warp7()
        {
            var warp = Warp.LightSpeedToWarp(656.161051);

            Assert.AreEqual(7, warp);
        }

        [TestMethod]
        public void Warp8()
        {
            var warp = Warp.LightSpeedToWarp(1024.3124);

            Assert.AreEqual(8, warp);
        }

        [TestMethod]
        public void Warp9()
        {
            var warp = Warp.LightSpeedToWarp(1516.425835);

            Assert.AreEqual(9, warp);
        }

        [TestMethod]
        public void Warp9_2()
        {
            var warp = Warp.LightSpeedToWarp(1648.95694);

            Assert.AreEqual(9.2, warp);
        }

        [TestMethod]
        public void Warp9_6()
        {
            var warp = Warp.LightSpeedToWarp(1909.292835);

            Assert.AreEqual(9.6, warp);
        }

        [TestMethod]
        public void Warp9_9()
        {
            var warp = Warp.LightSpeedToWarp(3052.946441);

            Assert.AreEqual(9.9, warp);
        }

        [TestMethod]
        public void Warp9_99()
        {
            var warp = Warp.LightSpeedToWarp(7912.353483);

            Assert.AreEqual(9.99, warp);
        }
        [TestMethod]
        public void Warp9_9999()
        {
            var warp = Warp.LightSpeedToWarp(199515.905343);

            Assert.AreEqual(9.9999, warp);
        }
    }
}