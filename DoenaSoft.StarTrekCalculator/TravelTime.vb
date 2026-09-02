''' <summary>
''' Represents a travel time for a given speed and distance.
''' Warp scale see <a href="https://i.stack.imgur.com/ZBsFO.gif">The Official Star Trek Fact Files</a>.
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
        Dim timeSpan As TimeSpan = TimeSpan.FromDays((Me.Years * OneYearInDays) + Me.Days)

        timeSpan = timeSpan.Add(TimeSpan.FromHours(Me.Hours))

        timeSpan = timeSpan.Add(TimeSpan.FromMinutes(Me.Minutes))

        timeSpan = timeSpan.Add(TimeSpan.FromSeconds(Me.Seconds))

        Return timeSpan
    End Function

    ''' <summary>
    ''' Returns a string representation of this instance.
    ''' </summary>
    ''' <returns>a string representation</returns>
    Public Overrides Function ToString() As String
        If Me.Years > 0 Then
            Return $"{Me.Years}a {Me.Days}d {Me.Hours}h {Me.Minutes}m {Me.Seconds}s"
        ElseIf Me.Days > 0 Then
            Return $"{Me.Days}d {Me.Hours}h {Me.Minutes}m {Me.Seconds}s"
        Else
            Return $"{Me.Hours}h {Me.Minutes}m {Me.Seconds}s"
        End If
    End Function

End Structure