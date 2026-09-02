using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;

namespace DoenaSoft.StarTrekCalculator.Tests;

[TestClass]
public sealed class GetImage
{
    [TestMethod]
    public void Warpchart()
    {
        using var image = Images.GetWarpChartJpeg();

        var fileName = Path.Combine(Path.GetTempPath(), "warpchart.jpg");

        using var fileStream = File.Create(fileName);

        var buffer = new byte[8192];

        int bytesRead;
        while ((bytesRead = image.Read(buffer, 0, buffer.Length)) > 0)
        {
            fileStream.Write(buffer, 0, bytesRead);
        }
    }

    [TestMethod]
    public void WarpchartIsValidJpeg()
    {
        using var image = Images.GetWarpChartJpeg();

        Assert.IsNotNull(image);

        Assert.IsGreaterThan(0, image.Length);

        var header = new byte[2];

        var bytesRead = image.Read(header, 0, header.Length);

        Assert.AreEqual(2, bytesRead);

        // JPEG files start with the SOI (Start Of Image) marker 0xFF 0xD8
        Assert.AreEqual(0xFF, header[0]);

        Assert.AreEqual(0xD8, header[1]);
    }

    [TestMethod]
    public void WarpchartMatchesSourceFile()
    {
        using var image = Images.GetWarpChartJpeg();

        using var resourceMemoryStream = new MemoryStream();

        image.CopyTo(resourceMemoryStream);

        var resourceBytes = resourceMemoryStream.ToArray();

        var sourceFilePath = Path.Combine("..", "..", "..", "..", "DoenaSoft.StarTrekCalculator", "warpchart.jpg");

        var sourceBytes = File.ReadAllBytes(sourceFilePath);

        var areEqual = resourceBytes.SequenceEqual(sourceBytes);

        Assert.IsTrue(areEqual);
    }
}