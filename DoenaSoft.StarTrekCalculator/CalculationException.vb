Public NotInheritable Class CalculationException
  Inherits Exception

  Friend Sub New(ByVal mesage As String)
    MyBase.New(mesage)
  End Sub
End Class
