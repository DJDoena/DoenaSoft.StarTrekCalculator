Public NotInheritable Class CalculationException
    Inherits ApplicationException

    Friend Sub New(ByVal mesage As String)
        MyBase.New(mesage)
    End Sub
End Class