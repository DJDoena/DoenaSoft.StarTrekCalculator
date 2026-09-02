''' <summary>
''' The exception that is thrown when a Star Trek calculation cannot be performed.
''' </summary>
Public NotInheritable Class CalculationException
    Inherits Exception

    ''' <summary>
    ''' Initializes a new instance of the <see cref="CalculationException"/> class.
    ''' </summary>
    ''' <param name="mesage">the error message</param>
    Friend Sub New(ByVal mesage As String)
        MyBase.New(mesage)
    End Sub
End Class
