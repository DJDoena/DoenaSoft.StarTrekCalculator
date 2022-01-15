''' <summary>
''' Represents a travel time for a given speed and distance.
''' </summary>
Public Structure TravelTime
    ''' <summary />
    Public ReadOnly Property Years As Long

    ''' <summary />
    Public ReadOnly Property Days As Short

    ''' <summary />
    Public ReadOnly Property Hours As Short

    ''' <summary />
    Public ReadOnly Property Minutes As Short

    ''' <summary />
    Public ReadOnly Property Seconds As Short

    Friend Sub New(ByVal years As Long, ByVal days As Short, ByVal hours As Short, ByVal minutes As Short, ByVal seconds As Short)
        Me.Years = years
        Me.Days = days
        Me.Hours = hours
        Me.Minutes = minutes
        Me.Seconds = seconds
    End Sub

    ''' <summary>
    ''' Converts this instance into a standard <see cref="TimeSpan"/>.
    ''' </summary>
    ''' <returns>a time span</returns>
    ''' <remarks>
    ''' Since a year is calculated with 365.2425 days the <see cref="TimeSpan"/> properties will not necessarily contain the same values as the <see cref="TravelTime"/> properties.
    ''' </remarks>
    Public Function ToTimeSpan() As TimeSpan
        Dim timeSpan As TimeSpan = TimeSpan.FromDays((Years * OneYearInDays) + Days)

        timeSpan = timeSpan.Add(TimeSpan.FromHours(Hours))

        timeSpan = timeSpan.Add(TimeSpan.FromMinutes(Minutes))

        timeSpan = timeSpan.Add(TimeSpan.FromSeconds(Seconds))

        Return timeSpan
    End Function

    Public Overrides Function ToString() As String
        If Years > 0 Then
            Return $"{Years}a {Days}d {Hours}h {Minutes}m {Seconds}s"
        ElseIf Days > 0 Then
            Return $"{Days}d {Hours}h {Minutes}m {Seconds}s"
        Else
            Return $"{Hours}h {Minutes}m {Seconds}s"
        End If
    End Function

End Structure