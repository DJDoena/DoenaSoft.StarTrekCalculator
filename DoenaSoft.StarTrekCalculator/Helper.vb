Imports System.Runtime.CompilerServices

Friend Module Helper
    ''' <summary>
    ''' Truncates the fractional part of a number, returning only its integral part.
    ''' </summary>
    ''' <param name="number">the number to truncate</param>
    ''' <returns>the integral part of <paramref name="number"/></returns>
    <Extension()>
    Friend Function Truncate(number As Double) As Integer
        Return CType(Math.Truncate(number), Integer)
    End Function
End Module
