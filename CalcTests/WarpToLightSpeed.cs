using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DoenaSoft.StarTrekCalculator.Tests
{
    /// <summary>
    /// Warp scale see official chart https://i.stack.imgur.com/ZBsFO.gif
    /// </summary>
    [TestClass]
    public sealed class WarpToLightSpeed
    {
        [TestMethod]
        public void MinWarp()
        {
            var lightSpeed = Warp.WarpToLightSpeed(Warp.MinWarpFactor);

            Assert.AreEqual(Warp.MinLightSpeed, lightSpeed);
        }

        [TestMethod]
        public void MaxWarp()
        {
            var lightSpeed = Warp.WarpToLightSpeed(Warp.MaxWarpFactor);

            Assert.AreEqual(502439.251678, lightSpeed);
        }

        [TestMethod]
        [ExpectedException(typeof(CalculationException))]
        public void MinWarpFail()
        {
            Warp.WarpToLightSpeed(0.9);
        }

        [TestMethod]
        [ExpectedException(typeof(CalculationException))]
        public void MaxWarpFail()
        {
            Warp.WarpToLightSpeed(9.9999991);
        }

        [TestMethod]
        public void Warp1()
        {
            var lightSpeed = Warp.WarpToLightSpeed(1);

            Assert.AreEqual(1, lightSpeed);
        }

        [TestMethod]
        public void Warp2()
        {
            var lightSpeed = Warp.WarpToLightSpeed(2);

            Assert.AreEqual(10.079369, lightSpeed);
        }

        [TestMethod]
        public void Warp3()
        {
            var lightSpeed = Warp.WarpToLightSpeed(3);

            Assert.AreEqual(38.940744, lightSpeed);
        }

        [TestMethod]
        public void Warp4()
        {
            var lightSpeed = Warp.WarpToLightSpeed(4);

            Assert.AreEqual(101.593719, lightSpeed);
        }

        [TestMethod]
        public void Warp5()
        {
            var lightSpeed = Warp.WarpToLightSpeed(5);

            Assert.AreEqual(213.747391, lightSpeed);
        }

        [TestMethod]
        public void Warp6()
        {
            var lightSpeed = Warp.WarpToLightSpeed(6);

            Assert.AreEqual(392.501078, lightSpeed);
        }

        [TestMethod]
        public void Warp7()
        {
            var lightSpeed = Warp.WarpToLightSpeed(7);

            Assert.AreEqual(656.161051, lightSpeed);
        }

        [TestMethod]
        public void Warp8()
        {
            var lightSpeed = Warp.WarpToLightSpeed(8);

            Assert.AreEqual(1024.3124, lightSpeed);
        }

        [TestMethod]
        public void Warp9()
        {
            var lightSpeed = Warp.WarpToLightSpeed(9);

            Assert.AreEqual(1516.425835, lightSpeed);
        }

        [TestMethod]
        public void Warp9_2()
        {
            var lightSpeed = Warp.WarpToLightSpeed(9.2);

            Assert.AreEqual(1648.95694, lightSpeed);
        }

        [TestMethod]
        public void Warp9_6()
        {
            var lightSpeed = Warp.WarpToLightSpeed(9.6);

            Assert.AreEqual(1909.292835, lightSpeed);
        }

        [TestMethod]
        public void Warp9_9()
        {
            var lightSpeed = Warp.WarpToLightSpeed(9.9);

            Assert.AreEqual(3052.946441, lightSpeed);
        }

        [TestMethod]
        public void Warp9_99()
        {
            var lightSpeed = Warp.WarpToLightSpeed(9.99);

            Assert.AreEqual(7912.353483, lightSpeed);
        }
        [TestMethod]
        public void Warp9_9999()
        {
            var lightSpeed = Warp.WarpToLightSpeed(9.9999);

            Assert.AreEqual(199515.905343, lightSpeed);
        }
    }
}