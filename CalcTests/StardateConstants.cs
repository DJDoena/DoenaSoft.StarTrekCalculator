using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DoenaSoft.StarTrekCalculator.Tests;

[TestClass]
public sealed class StardateConstants
{
    private const string TimeFormat = "yyyy-MM-dd HH:mm:ss";

    [TestMethod]
    public void MinStardateEqualsCommentedDate()
    {
        // per XML comment: Equals 01/01/0001 00:00:00
        var normalDate = Stardate.StardateToNormalDate(Stardate.MinStardate);

        Assert.AreEqual("0001-01-01 00:00:00", normalDate.ToString(TimeFormat));
    }

    [TestMethod]
    public void MaxStardateEqualsCommentedDate()
    {
        // per XML comment: Equals 12/31/9999 23:59:59
        var normalDate = Stardate.StardateToNormalDate(Stardate.MaxStardate);

        Assert.AreEqual("9999-12-31 23:59:59", normalDate.ToString(TimeFormat));
    }
}
