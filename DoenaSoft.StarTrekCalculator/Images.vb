Imports System.IO

''' <summary>
''' Gives access to the images stored in the resources of this library
''' </summary>
Public Module Images
  ''' <summary>
  ''' Returns the warp chart from The Official Star Trek Fact Files.
  ''' </summary>
  Public Function GetWarpChartJpeg() As Stream
    Dim result As Stream = GetType(Images).Assembly.GetManifestResourceStream("DoenaSoft.StarTrekCalculator.warpchart.jpg")

    Return result
  End Function

End Module