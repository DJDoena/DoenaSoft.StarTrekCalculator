using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DoenaSoft.StarTrekCalculator.Tests
{
    /// <summary>
    /// Warp scale see official chart https://i.stack.imgur.com/ZBsFO.gif
    /// </summary>
    [TestClass]
    public sealed class TravelTime
    {
        [TestMethod]
        public void VoyagerDeltaQuadrant()
        {
            var travelTime = Warp.WarpToTravelTime(8, 70000);

            Assert.AreEqual(68, travelTime.Years);
            Assert.AreEqual(123, travelTime.Days);
            Assert.AreEqual(15, travelTime.Hours);
            Assert.AreEqual(27, travelTime.Minutes);
            Assert.AreEqual(41, travelTime.Seconds);
            Assert.AreEqual("68a 123d 15h 27m 41s", travelTime.ToString());

            var timeSpan = travelTime.ToTimeSpan();

            Assert.AreEqual("24960.03:13:17", timeSpan.ToString());
        }
    }
}