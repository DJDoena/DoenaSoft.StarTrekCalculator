''' <summary>
''' Represents a travel time for a given speed and distance.
''' Warp scale see <a href="https://i.stack.imgur.com/ZBsFO.gif">The Official Star Trek Fact Files</a>.
''' </summary>
Public Structure TravelTime
    ''' <summary>
    ''' The whole years of the travel time.
    ''' </summary>
    Public ReadOnly Property Years As Long

    ''' <summary>
    ''' The remaining whole days of the travel time, after the years are subtracted.
    ''' </summary>
    Public ReadOnly Property Days As Short

    ''' <summary>
    ''' The remaining whole hours of the travel time, after the years and days are subtracted.
    ''' </summary>
    Public ReadOnly Property Hours As Short

    ''' <summary>
    ''' The remaining whole minutes of the travel time, after the years, days and hours are subtracted.
    ''' </summary>
    Public ReadOnly Property Minutes As Short

    ''' <summary>
    ''' The remaining whole seconds of the travel time, after the years, days, hours and minutes are subtracted.
    ''' </summary>
    Public ReadOnly Property Seconds As Short

    ''' <summary>
    ''' Initializes a new instance of the <see cref="TravelTime"/> structure.
    ''' </summary>
    ''' <param name="years">the whole years of the travel time</param>
    ''' <param name="days">the remaining whole days of the travel time</param>
    ''' <param name="hours">the remaining whole hours of the travel time</param>
    ''' <param name="minutes">the remaining whole minutes of the travel time</param>
    ''' <param name="seconds">the remaining whole seconds of the travel time</param>
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