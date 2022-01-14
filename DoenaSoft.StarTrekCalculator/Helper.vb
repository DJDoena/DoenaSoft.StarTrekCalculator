Friend Module Helper
    Friend Function Fix(number As Double) As Integer
        Return CType(Math.Truncate(number), Integer)
    End Function

    Friend Function Int(number As Double) As Integer
        Return CType(Math.Truncate(number), Integer)
    End Function
End Module
