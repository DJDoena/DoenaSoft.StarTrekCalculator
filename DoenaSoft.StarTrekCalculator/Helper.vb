Friend Module Helper
    Friend Function Truncate(number As Double) As Integer
        Return CType(Math.Truncate(number), Integer)
    End Function
End Module
