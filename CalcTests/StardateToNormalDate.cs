using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DoenaSoft.StarTrekCalculator.Tests
{
    [TestClass]
    public sealed class StardateToNormalDate
    {
        private const string TimeFormat = "yyyy-MM-dd HH:mm:ss";

        [TestMethod]
        public void MinStardate()
        {
            var normalDate = Stardate.StardateToNormalDate(Stardate.MinStardate);

            Assert.AreEqual("0001-01-01 00:00:00", normalDate.ToString(TimeFormat));
        }

        [TestMethod]
        public void MaxStardate()
        {
            var normalDate = Stardate.StardateToNormalDate(Stardate.MaxStardate);

            Assert.AreEqual("9999-12-31 23:59:59", normalDate.ToString(TimeFormat));
        }

        [TestMethod]
        [ExpectedException(typeof(CalculationException))]
        public void MinStardateFail()
        {
            Stardate.StardateToNormalDate(Stardate.MinStardate - 1);
        }

        [TestMethod]
        [ExpectedException(typeof(CalculationException))]
        public void MaxStardateFail()
        {
            Stardate.StardateToNormalDate(Stardate.MaxStardate + 1);
        }

        [TestMethod]
        public void FirstStardate()
        {
            var normalDate = Stardate.StardateToNormalDate(0);

            Assert.AreEqual("2323-01-01 00:00:00", normalDate.ToString(TimeFormat));
        }

        [TestMethod]
        public void EncounterAtFarpoint()
        {
            var normalDate = Stardate.StardateToNormalDate(41153.7);

            Assert.AreEqual("2364-02-26 06:06:02", normalDate.ToString(TimeFormat));
        }

        [TestMethod]
        public void BattleOfWolf359()
        {
            var normalDate = Stardate.StardateToNormalDate(43997);

            Assert.AreEqual("2366-12-30 21:43:12", normalDate.ToString(TimeFormat));
        }
    }
}