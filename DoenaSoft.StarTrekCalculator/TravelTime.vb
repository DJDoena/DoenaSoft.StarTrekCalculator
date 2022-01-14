''' <summary>
''' Return value of the static methods of the Stardate class.
''' </summary>
Public Structure TravelTime
    Public Property Years As Long

    Public Property Days As Short

    Public Property Hours As Short

    Public Property Minutes As Short

    Public Property Seconds As Short

    Public Function ToTimeSpan() As TimeSpan
        Dim timeSpan As TimeSpan = TimeSpan.FromDays(Years * OneYearInDays + Days)

        timeSpan.Add(TimeSpan.FromHours(Hours))

        timeSpan.Add(TimeSpan.FromMinutes(Minutes))

        timeSpan.Add(TimeSpan.FromSeconds(Seconds))

        Return timeSpan
    End Function

End Structure