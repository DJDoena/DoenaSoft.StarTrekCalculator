using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DoenaSoft.StarTrekCalculator.Tests
{
    [TestClass]
    public sealed class NormalDateToStardate
    {
        [TestMethod]
        public void MinStardate()
        {
            var stardate = Stardate.NormalDateToStardate(new DateTime(1, 1, 1));

            Assert.AreEqual(Stardate.MinStardate, stardate);
        }

        [TestMethod]
        public void MaxStardate()
        {
            var stardate = Stardate.NormalDateToStardate(new DateTime(9999, 12, 31, 23, 59, 59));

            Assert.AreEqual(7676999.999968, stardate);
        }

        [TestMethod]
        public void FirstStardate()
        {
            var stardate = Stardate.NormalDateToStardate(new DateTime(2323, 1, 1));

            Assert.AreEqual(0d, stardate);
        }

        [TestMethod]
        public void EncounterAtFarpoint()
        {
            var stardate = Stardate.NormalDateToStardate(new DateTime(2364, 2, 26, 6, 6, 2));

            Assert.AreEqual(41153.699972, stardate);
        }

        [TestMethod]
        public void BattleOfWolf359()
        {
            var stardate = Stardate.NormalDateToStardate(new DateTime(2366, 12, 30, 21, 43, 12));

            Assert.AreEqual(43997, stardate);
        }
    }
}