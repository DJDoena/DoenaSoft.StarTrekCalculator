using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DoenaSoft.StarTrekCalculator.Tests
{
    [TestClass]
    public sealed class LightspeedToWarp
    {
        [TestMethod]
        public void MinLightspeed()
        {
            var lightspeed = Warp.LightspeedToWarp(Warp.MinLightspeed);

            Assert.AreEqual(1, lightspeed);
        }

        [TestMethod]
        public void MaxLightspeed()
        {
            var warp = Warp.LightspeedToWarp(Warp.MaxLightspeed);

            Assert.AreEqual(Warp.MaxWarp, warp);
        }

        [TestMethod]
        [ExpectedException(typeof(CalculationException))]
        public void MinLightspeedFail()
        {
            Warp.LightspeedToWarp(0.9);
        }

        [TestMethod]
        [ExpectedException(typeof(CalculationException))]
        public void MaxLightspeedFail()
        {
            Warp.LightspeedToWarp(Warp.MaxLightspeed + 1);
        }

        [TestMethod]
        public void Warp1()
        {
            var warp = Warp.LightspeedToWarp(1);

            Assert.AreEqual(1, warp);
        }

        [TestMethod]
        public void Warp2()
        {
            var warp = Warp.LightspeedToWarp(10.079369);

            Assert.AreEqual(2, warp);
        }

        [TestMethod]
        public void Warp3()
        {
            var warp = Warp.LightspeedToWarp(38.940744);

            Assert.AreEqual(3, warp);
        }

        [TestMethod]
        public void Warp4()
        {
            var warp = Warp.LightspeedToWarp(101.593719);

            Assert.AreEqual(4, warp);
        }

        [TestMethod]
        public void Warp5()
        {
            var warp = Warp.LightspeedToWarp(213.747391);

            Assert.AreEqual(5, warp);
        }

        [TestMethod]
        public void Warp6()
        {
            var warp = Warp.LightspeedToWarp(392.501078);

            Assert.AreEqual(6, warp);
        }

        [TestMethod]
        public void Warp7()
        {
            var warp = Warp.LightspeedToWarp(656.161051);

            Assert.AreEqual(7, warp);
        }

        [TestMethod]
        public void Warp8()
        {
            var warp = Warp.LightspeedToWarp(1024.3124);

            Assert.AreEqual(8, warp);
        }

        [TestMethod]
        public void Warp9()
        {
            var warp = Warp.LightspeedToWarp(1516.425835);

            Assert.AreEqual(9, warp);
        }

        [TestMethod]
        public void Warp9_2()
        {
            var warp = Warp.LightspeedToWarp(1648.95694);

            Assert.AreEqual(9.2, warp);
        }

        [TestMethod]
        public void Warp9_6()
        {
            var warp = Warp.LightspeedToWarp(1909.292835);

            Assert.AreEqual(9.6, warp);
        }

        [TestMethod]
        public void Warp9_9()
        {
            var warp = Warp.LightspeedToWarp(3052.946441);

            Assert.AreEqual(9.9, warp);
        }

        [TestMethod]
        public void Warp9_99()
        {
            var warp = Warp.LightspeedToWarp(7912.353483);

            Assert.AreEqual(9.99, warp);
        }
        [TestMethod]
        public void Warp9_9999()
        {
            var warp = Warp.LightspeedToWarp(199515.905343);

            Assert.AreEqual(9.9999, warp);
        }
    }
}