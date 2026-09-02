''' <summary>
''' The exception that is thrown when a Star Trek calculation cannot be performed.
''' </summary>
Public NotInheritable Class CalculationException
    Inherits Exception

    Friend Sub New(ByVal mesage As String)
        MyBase.New(mesage)
    End Sub
End Class
