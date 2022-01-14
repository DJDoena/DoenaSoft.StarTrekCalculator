Public Module Stardate
    Public Const MinStardate As Double = -2322000

    Public Const MaxStardate As Double = 7676999.999999

    Public Function StardateToNormalDate(ByVal stardate As Double) As Date

        If stardate < MinStardate OrElse stardate > MaxStardate Then
            Throw New CalculationException("Stardate is smaller than " & MinStardate & " or bigger than " & MaxStardate)
        End If

        stardate = Math.Round(stardate, 6)

        Dim temp As Double = stardate / 1000

        Dim myYear As Short = CShort(Int(temp) + 2323)

        temp -= Int(temp)

        Dim days As Short = DaysOfYear(myYear)

        temp *= days

        Dim normalDate As New Date(1, 1, 1, 0, 0, 0, 0)

        normalDate.AddYears(myYear - 1)

        normalDate = normalDate.AddDays(temp)

        Return normalDate
    End Function

    Public Function NormalDateToStardate(ByVal normalDate As Date) As Double
        Dim dayPart As Double = (normalDate.Second + normalDate.Minute * 60 + normalDate.Hour * 3600) / 86400

        Dim stardateLowerPart As Double = (normalDate.DayOfYear - 1 + dayPart) / DaysOfYear(CShort(normalDate.Year)) * 1000

        Dim stardateUpperPart As Double = (normalDate.Year - 2323) * 1000

        Return Math.Round(stardateUpperPart + stardateLowerPart, 6)
    End Function


    Private Function DaysOfYear(ByVal year As Short) As Short
        If Date.IsLeapYear(year) Then
            Return 366
        Else
            Return 365
        End If
    End Function

End Module