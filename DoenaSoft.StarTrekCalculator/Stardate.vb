Imports System.Globalization

''' <summary>
''' This class calculates the Star Trek: The Next Generation / Deep Space Nine / Voyager stardates based on an actual given date.
''' </summary>
''' <remarks>
''' The TNG / DS9 / VOY system was invented to bring some kind of system to the random numbers of The Original Series.
''' The numbers started in the first season of Star Trek TNG with 41xyz which was meant as 4 = 24th century, 1 = first season.
''' Subsequently each season increases the second digit and the 3rd to 5th digit increase over the course of the season.
''' Since each season is one year in the real world, this calculator assumes that 1000 stardates equal one Earth year.
''' Based on the fact that the show starts in 2364, the baseline for the new stardate system is the year 2323.
''' </remarks>
Public Module Stardate
    ''' <summary>
    ''' Equals 01/01/0001 00:00:00
    ''' </summary>
    Public Const MinStardate As Double = -2_322_000

    ''' <summary>
    ''' Equals 12/31/9999 23:59:59
    ''' </summary>
    Public Const MaxStardate As Double = 7_676_999.999_999

    ''' <summary>
    ''' Calculates an actual date based on a given stardate
    ''' </summary>
    ''' <param name="stardate">the stardate</param>
    ''' <returns>the actual date</returns>
    ''' <exception cref="CalculationException">the stardate is smaller than <see cref="MinStardate"/> or bigger than <see cref="MaxStardate"/></exception>
    Public Function StardateToNormalDate(ByVal stardate As Double) As Date

        If stardate < MinStardate OrElse stardate > MaxStardate Then
            Throw New CalculationException($"Stardate is smaller than {MinStardate} or bigger than {MaxStardate}")
        End If

        stardate = Math.Round(stardate, 6)

        Dim temp As Double = stardate / 1000

        Dim myYear As Integer = temp.Truncate() + 2323

        temp -= temp.Truncate()

        Dim days As Integer = CultureInfo.InvariantCulture.Calendar.GetDaysInYear(myYear)

        temp *= days

        Dim normalDate As New Date(1, 1, 1, 0, 0, 0, 0)

        normalDate = normalDate.AddYears(myYear - 1)

        Dim ticks As Double = Math.Round(temp * TimeSpan.TicksPerDay)

        normalDate = normalDate.AddTicks(CLng(ticks))

        Return normalDate
    End Function

    ''' <summary>
    ''' Calculates a stardate based on an actual date
    ''' </summary>
    ''' <param name="normalDate">the actual date</param>
    ''' <returns>the stardate</returns>
    Public Function NormalDateToStardate(ByVal normalDate As Date) As Double
        Dim dayPart As Double = (normalDate.Second + normalDate.Minute * 60 + normalDate.Hour * 3600) / 86400

        Dim stardateLowerPart As Double = (normalDate.DayOfYear - 1 + dayPart) / CultureInfo.InvariantCulture.Calendar.GetDaysInYear(normalDate.Year) * 1000

        Dim stardateUpperPart As Double = (normalDate.Year - 2323) * 1000

        Return Math.Round(stardateUpperPart + stardateLowerPart, 6)
    End Function

End Module
