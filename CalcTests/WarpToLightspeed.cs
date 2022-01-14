using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DoenaSoft.StarTrekCalculator.Tests
{
    /// <summary>
    /// Warp scale see official chart https://i.stack.imgur.com/ZBsFO.gif
    /// </summary>
    [TestClass]
    public sealed class WarpToLightspeed
    {
        [TestMethod]
        public void MinWarp()
        {
            var lightspeed = Warp.WarpToLightspeed(Warp.MinWarp);

            Assert.AreEqual(1, lightspeed);
        }

        [TestMethod]
        public void MaxWarp()
        {
            var lightspeed = Warp.WarpToLightspeed(Warp.MaxWarp);

            Assert.AreEqual(502439.251678, lightspeed);
        }

        [TestMethod]
        [ExpectedException(typeof(CalculationException))]
        public void MinWarpFail()
        {
            Warp.WarpToLightspeed(0.9);
        }

        [TestMethod]
        [ExpectedException(typeof(CalculationException))]
        public void MaxWarpFail()
        {
            Warp.WarpToLightspeed(9.9999991);
        }

        [TestMethod]
        public void Warp1()
        {
            var lightspeed = Warp.WarpToLightspeed(1);

            Assert.AreEqual(1, lightspeed);
        }

        [TestMethod]
        public void Warp2()
        {
            var lightspeed = Warp.WarpToLightspeed(2);

            Assert.AreEqual(10.079369, lightspeed);
        }

        [TestMethod]
        public void Warp3()
        {
            var lightspeed = Warp.WarpToLightspeed(3);

            Assert.AreEqual(38.940744, lightspeed);
        }

        [TestMethod]
        public void Warp4()
        {
            var lightspeed = Warp.WarpToLightspeed(4);

            Assert.AreEqual(101.593719, lightspeed);
        }

        [TestMethod]
        public void Warp5()
        {
            var lightspeed = Warp.WarpToLightspeed(5);

            Assert.AreEqual(213.747391, lightspeed);
        }

        [TestMethod]
        public void Warp6()
        {
            var lightspeed = Warp.WarpToLightspeed(6);

            Assert.AreEqual(392.501078, lightspeed);
        }

        [TestMethod]
        public void Warp7()
        {
            var lightspeed = Warp.WarpToLightspeed(7);

            Assert.AreEqual(656.161051, lightspeed);
        }

        [TestMethod]
        public void Warp8()
        {
            var lightspeed = Warp.WarpToLightspeed(8);

            Assert.AreEqual(1024.3124, lightspeed);
        }

        [TestMethod]
        public void Warp9()
        {
            var lightspeed = Warp.WarpToLightspeed(9);

            Assert.AreEqual(1516.425835, lightspeed);
        }

        [TestMethod]
        public void Warp9_2()
        {
            var lightspeed = Warp.WarpToLightspeed(9.2);

            Assert.AreEqual(1648.95694, lightspeed);
        }

        [TestMethod]
        public void Warp9_6()
        {
            var lightspeed = Warp.WarpToLightspeed(9.6);

            Assert.AreEqual(1909.292835, lightspeed);
        }

        [TestMethod]
        public void Warp9_9()
        {
            var lightspeed = Warp.WarpToLightspeed(9.9);

            Assert.AreEqual(3052.946441, lightspeed);
        }

        [TestMethod]
        public void Warp9_99()
        {
            var lightspeed = Warp.WarpToLightspeed(9.99);

            Assert.AreEqual(7912.353483, lightspeed);
        }
        [TestMethod]
        public void Warp9_9999()
        {
            var lightspeed = Warp.WarpToLightspeed(9.9999);

            Assert.AreEqual(199515.905343, lightspeed);
        }
    }
}