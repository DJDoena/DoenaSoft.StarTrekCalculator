using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DoenaSoft.StarTrekCalculator.Tests
{
    [TestClass]
    public sealed class GetImage
    {
        [TestMethod]
        public void Warpchart()
        {
            using (var image = Images.GetWarpChartJpeg())
            {
                var fileName = Path.Combine(Path.GetTempPath(), "warpchart.jpg");

                using (var fileStream = File.Create(fileName))
                {
                    var buffer = new byte[8192];

                    int bytesRead;
                    while ((bytesRead = image.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        fileStream.Write(buffer, 0, bytesRead);
                    }
                }
            }
        }
    }
}