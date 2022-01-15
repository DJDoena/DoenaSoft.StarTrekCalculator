''' <summary>
''' Represents a travel time for a give speed and distance
''' </summary>
Public Structure TravelTime
    ''' <summary />
    Public Property Years As Long

    ''' <summary />
    Public Property Days As Short

    ''' <summary />
    Public Property Hours As Short

    ''' <summary />
    Public Property Minutes As Short

    ''' <summary />
    Public Property Seconds As Short

    ''' <summary>
    ''' Converts this instance into a standard <see cref="TimeSpan"/>.
    ''' </summary>
    ''' <returns>a time span</returns>
    Public Function ToTimeSpan() As TimeSpan
        Dim timeSpan As TimeSpan = TimeSpan.FromDays((Years * OneYearInDays) + Days)

        timeSpan.Add(TimeSpan.FromHours(Hours))

        timeSpan.Add(TimeSpan.FromMinutes(Minutes))

        timeSpan.Add(TimeSpan.FromSeconds(Seconds))

        Return timeSpan
    End Function

End Structure