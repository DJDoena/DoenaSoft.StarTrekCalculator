Imports System.IO

Public Module Images
  Public Function GetWarpChart() As Stream
    Dim result As Stream = GetType(Images).Assembly.GetManifestResourceStream("DoenaSoft.StarTrekCalculator.warpchart.jpg")

    Return result
  End Function

End Module